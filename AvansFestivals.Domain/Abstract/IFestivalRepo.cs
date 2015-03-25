using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFestivals.Domain.Database;

namespace AvansFestivals.Domain.Abstract
{
    public interface IFestivalRepo
    {
        IQueryable<Festival> getQuerableAllFestivals();

        IEnumerable<Festival> getAllFestivals();
        IEnumerable<Festival> getUpcomingFirstTen();
        IEnumerable<Festival> getUpcoming();
        IEnumerable<Festival> GetRandomFestivals(int amount);
        Festival ChangeStateToDone(Festival festival);
        Festival ChangeStateToCreated(Festival festival);
        Festival ChangeStateToInProgress(Festival festival);
        Festival ChangeStateToStartSale(Festival festival);
        Festival ChangeStateToSoldOut(Festival festival);
        Festival GetFestival(int id);
        void RemoveFestival(Festival festival);
        void EditFestival(Festival festival);
        void AddFestival(Festival festival);
    }
}
