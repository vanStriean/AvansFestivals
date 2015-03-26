﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFestivals.Domain.Abstract;
using AvansFestivals.Domain.Database;
using AvansFestivals.Domain.Patterns.StatePattern;
using AvansFestivals.Domain.Patterns.FactoryPatternTickets;
using System.Data.Entity;

namespace AvansFestivals.Domain.Concrete
{
    public class FestivalRepo : IFestivalRepo
    {
        AvansFestivalsEntities db = new AvansFestivalsEntities();

        public IQueryable<Festival> getQuerableAllFestivals()
        {
            return db.Festivals;
        }

        public IEnumerable<Festival> getAllFestivals()
        {
            return db.Festivals;
        }

        public IEnumerable<Festival> getUpcomingFirstTen()
        {
            return db.Festivals.Where(e => e.Start.CompareTo(DateTime.Now) > 0).OrderBy(e => e.Start).Take(10);
        }

        public IEnumerable<Festival> getUpcoming()
        {
            return db.Festivals.Where(e => e.Start.CompareTo(DateTime.Now) > 0).OrderBy(e => e.Start);
        }

        public IEnumerable<Festival> GetRandomFestivals(int amount)
        {
            Random rand = new Random();
            List<Festival> randFests = new List<Festival>();
            int count = 0;
            while(count < amount)
            {
                int random = rand.Next(0, db.Festivals.Count() - 1);
                Festival fest = db.Festivals.ToList().Skip(random).First();
                if (!randFests.Contains(fest))
                {
                    randFests.Add(fest);
                    count++;
                }
            }

            return randFests.AsEnumerable();
        }


        public Festival ChangeStateToDone(Festival festival)
        {
            festival.FestivalState = new DoneState().ChangeState();
            db.SaveChanges();
            return festival;
        }

        public Festival ChangeStateToCreated(Festival festival)
        {
            festival.FestivalState = new CreatedState().ChangeState();
            db.SaveChanges();
            return festival;
        }

        public Festival ChangeStateToInProgress(Festival festival)
        {
            festival.FestivalState = new InProgressState().ChangeState();
            db.SaveChanges();
            return festival;
        }

        public Festival ChangeStateToStartSale(Festival festival)
        {
            festival.FestivalState = new StartSaleState().ChangeState();
            db.SaveChanges();
            return festival;
        }

        public Festival ChangeStateToSoldOut(Festival festival)
        {
            festival.FestivalState = new SoldOutState().ChangeState();
            db.SaveChanges();
            return festival;
        }

        public Festival GetFestival(int id)
        {
            return (from e in db.Festivals where e.Id == id select e).FirstOrDefault();
        }
        
        public void RemoveFestival(Festival festival)
        {
            db.Festivals.Remove(festival);
            db.SaveChanges();
        }

        public void EditFestival(Festival festival)
        {
            db.Entry(festival).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void AddFestival(Festival festival)
        {
            db.Festivals.Add(festival);
            db.SaveChanges();
        }
    }
}
