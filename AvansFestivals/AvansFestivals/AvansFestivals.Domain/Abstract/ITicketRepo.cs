using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFestivals.Domain.Database;

namespace AvansFestivals.Domain.Abstract
{
    public interface ITicketRepo
    {
        //string CreateTicketPdf(Ticket ticket);
        TicketAmount GetTicketAmount(int id);
        void RemoveTicketAmount(TicketAmount ticketAmount);
        void EditTicketAmount(TicketAmount ticketAmount);
        void AddTicketAmount(TicketAmount ticketAmount);
        bool TicketTypeExists(int festivalId, TicketType ticketType);
    }
}
