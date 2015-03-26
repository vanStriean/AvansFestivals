using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AvansFestivals.Domain.Database;
using System.ComponentModel.DataAnnotations;

namespace AvansFestivals.Models
{
    public class TicketOrderModel
    {
        public Festival Festival { get; set; }
        public User User { get; set; }
        public TicketAmounts Tickets { get; set; }

        public double getTotalPrice()
        {
            double totalprice = 0.0;
            foreach (TicketAmount t in Festival.TicketAmounts)
            {
                switch (t.TicketType)
                {
                    case TicketType.Earlybird:
                        totalprice += t.Price * Tickets.Earlybird;
                        break;
                    case TicketType.Normal:
                        totalprice += t.Price * Tickets.Normal;
                        break;
                    case TicketType.VIP:
                        totalprice += t.Price * Tickets.VIP;
                        break;

                }
            }

            return totalprice;
        }
    }
}

public class TicketAmounts
{
    [Required]
    [Display(Name = "Earlybird ticket")]
    public int Earlybird { get; set; }

    [Required]
    [Display(Name = "Normale ticket")]
    public int Normal { get; set; }

    [Required]
    [Display(Name = "VIP ticket")]
    public int VIP { get; set; }
}