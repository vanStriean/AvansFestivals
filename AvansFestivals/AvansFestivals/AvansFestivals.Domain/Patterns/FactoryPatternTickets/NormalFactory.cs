using AvansFestivals.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFestivals.Domain.Patterns.FactoryPatternTickets
{
    public class NormalFactory : TicketFactory
    {
        public override Ticket CreateTicket(double price)
        {
            return new Ticket() { Price = price, TicketType = TicketType.Normal };
        }
    }
}
