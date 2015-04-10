using AvansFestivals.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFestivals.Domain.TestData
{
    public class TestData
    {
        private static AvansFestivalsEntities db = new AvansFestivalsEntities();

        public static void Fill(int amount)
        {
            User user = new User()
            {
                Firstname = "Test",
                Lastname = "de Tester",
                Email = "avansfestivals@gmail.com",
                Age = 21,
                Username = "Test",
                Password = "qwerty"
            };

            User user2 = new User()
            {
                Firstname = "Gebruiker",
                Lastname = "de Gebruiker",
                Email = "avansfestivals@gmail.com",
                Age = 21,
                Username = "Gebruiker",
                Password = "qwerty"
            };

            Role role = new Role()
            {
                Name = "Administrator"
            };


            Role role1 = new Role()
            {
                Name = "User"
            };

            Role role2 = new Role()
            {
                Name = "Manager"
            };


            UserAndRole userandrole1 = new UserAndRole();
            userandrole1.Role = role;
            userandrole1.User = user;
            db.UserAndRoles.Add(userandrole1);

            UserAndRole userandrole2 = new UserAndRole();
            userandrole2.Role = role1;
            userandrole2.User = user;
            db.UserAndRoles.Add(userandrole2);

            UserAndRole userandrole3 = new UserAndRole();
            userandrole3.Role = role2;
            userandrole3.User = user;
            db.UserAndRoles.Add(userandrole3);

            UserAndRole userandrole4 = new UserAndRole();
            userandrole4.Role = role1;
            userandrole4.User = user2;
            db.UserAndRoles.Add(userandrole4);

            double startDate = 0;
            double endDate = 3;
            for (int i = 0; i < amount; i++)
            {
                Festival fest = new Festival()
                {
                    Name = "Festival " + (i + 1),
                    Start = DateTime.Now.AddDays(startDate+=3),
                    End = DateTime.Now.AddDays(endDate+=2),
                    Banner = "https://fbcdn-sphotos-g-a.akamaihd.net/hphotos-ak-xfp1/t31.0-8/10997607_904859586211467_8091433167082448319_o.jpg",
                    Logo = "http://www.knitsforyourinspiration.com/knits/userfiles/imagesCMS/LOWLANDS%20LOGO.jpg",
                    FestivalState = FestivalState.StartSale,
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis facilisis dui risus, at condimentum orci tristique at. Curabitur id massa ex. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Suspendisse accumsan leo a ligula ornare, sit amet sagittis nibh blandit. Nullam blandit ligula felis, sit amet convallis leo ultrices vel. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Curabitur pulvinar eget dolor vitae placerat. Nam eget vehicula purus. Vestibulum pellentesque neque purus, eu bibendum metus interdum id. Donec quis elit ipsum. Ut vitae massa elementum, fermentum augue vel, pellentesque massa. Donec pharetra lacus nisi, sed tincidunt quam eleifend sit "
                };

                TicketAmount normAmount = new TicketAmount()
                {
                    TicketType = TicketType.Normal,
                    Amount = 400,
                    Price = 15.5
                };

                TicketAmount earlyAmount = new TicketAmount()
                {
                    TicketType = TicketType.Earlybird,
                    Amount = 50,
                    Price = 9.5
                };

                TicketAmount vipAmount = new TicketAmount()
                {
                    TicketType = TicketType.VIP,
                    Amount = 100,
                    Price = 22.5
                };

                Comment comment = new Comment()
                {
                    Message = "Dit is een automatische test comment",
                    Created = DateTime.Now,
                    UserId = user.Id
                };

                fest.Comments.Add(comment);
                fest.TicketAmounts.Add(normAmount);
                fest.TicketAmounts.Add(earlyAmount);
                fest.TicketAmounts.Add(vipAmount);

                db.Festivals.Add(fest);
            }
            
            db.SaveChanges();
        }
    }
}
