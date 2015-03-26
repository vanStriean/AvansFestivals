using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AvansFestivals.Domain.Database;

namespace AvansFestivals.Models
{
    public class FestivalListModel
    {
        public IEnumerable<Festival> Festivals { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public IEnumerable<Festival> RandomFestivals { get; set; }
        public IEnumerable<TicketAmount> TicketAmounts { get; set; }
    }
}