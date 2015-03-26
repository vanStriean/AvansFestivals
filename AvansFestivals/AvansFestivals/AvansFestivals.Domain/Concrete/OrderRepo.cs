using AvansFestivals.Domain.Abstract;
using AvansFestivals.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFestivals.Domain.Concrete
{
    public class OrderRepo : IOrderRepo
    {
        AvansFestivalsEntities db = new AvansFestivalsEntities();
        public void AddOrder(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
        }

        public void AddOrderItem(OrderItem orderitem)
        {
            throw new NotImplementedException();
        }
    }
}
