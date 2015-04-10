using AvansFestivals.Domain.Abstract;
using AvansFestivals.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFestivals.Domain.Concrete
{
    public class UserRepo : IUserRepo
    {
        AvansFestivalsEntities db = new AvansFestivalsEntities();
        public User GetUser(string username)
        {
            return db.Users.Where(w => w.Username.ToLower() == username.ToLower()).FirstOrDefault();
        }

        public bool CheckLogin(string username, string password)
        {
            User user = db.Users.Where(w => w.Username.ToLower() == username.ToLower()).FirstOrDefault();
            return user.Password == password;
        }

        public bool IsInRole(User user, string role)
        {
           //[ return db.Users.Find(user).Role == role;
            return false;
        }

        public bool CreateUser(User user)
        {
            UserAndRole uar = new UserAndRole();
            Role role1 = db.Role.Find(2);
            uar.User = user;
            uar.Role = role1;
            db.UserAndRoles.Add(uar);
            db.SaveChanges();
            return true;
        }

        public User EditUser()
        {
            throw new NotImplementedException();
        }
    }
}
