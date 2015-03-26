using AvansFestivals.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFestivals.Domain.Abstract
{
    public interface IOrderRepo
    {
        void AddOrder(Order order);
        void AddOrderItem(OrderItem orderitem);
    }
}
