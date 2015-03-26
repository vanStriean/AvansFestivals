using AvansFestivals.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFestivals.Domain.Patterns.FactoryPatternTickets
{
    public abstract class TicketFactory
    {
        public abstract Ticket CreateTicket(int price);
    }
}
