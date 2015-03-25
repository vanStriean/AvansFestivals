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
        public Festival Festival{ get; set; }
        public User User { get; set; }

        [Required]
        [Display(Name = "Earlybird ticket")]
        public int Earlybird { get; set; }

        [Required]
        [Display(Name = "Normale ticket")]
        public int Normal { get; set; }

        [Required]
        [Display(Name = "VIP ticket")]
        public int VIP { get; set; }

        public double getTotalPrice()
        {
            double totalPrice = 0.0;
            foreach (var e in Festival.TicketAmounts)
            {
                if (e.TicketType == TicketType.Earlybird)
                    totalPrice += (e.Price * Earlybird);
                else if (e.TicketType == TicketType.Normal)
                    totalPrice += (e.Price * Normal);
                else
                    totalPrice += (e.Price * VIP);
            }

            return totalPrice;
        }
    }
}