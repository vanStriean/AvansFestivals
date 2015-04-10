using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AvansFestivals.Domain.Database;

namespace AvansFestivals.Models
{
    public class FestivalModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime Start { get; set; }
        public System.DateTime End { get; set; }
        public string Logo { get; set; }
        public string Banner { get; set; }
        public FestivalState FestivalState { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<TicketAmount> TicketAmounts { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}