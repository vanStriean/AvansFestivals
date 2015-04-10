using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFestivals.Domain.Database;
using AvansFestivals.Domain.Patterns.ProxyPatternPDF;

namespace AvansFestivals.Domain.Patterns.TemplatePatternEmail
{
    public class OrderEmail : Email
    {
        private Order order;
        
        public OrderEmail(User user, Order order)
        {
            this.user = user;
            this.order = order;
        }

        public override string getEmailBody()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<p> Order: "+ order.Id.ToString() + "</p>");
            sb.Append("<p> De ticket zitten in de email.</p>");

            return sb.ToString();
        }

        public override string getEmailSubject()
        {
            string subject = "Ticket Order: " + order.Id.ToString() + " Avans Festivals.";

            return subject;
        }

        public override List<string> getFilepath() 
        {
            List<string> filepaths = new List<string>();

            /* proxy pattern */
            ProxyPDF proxy = new ProxyPDF();

            foreach (OrderItem item in order.OrderItems)
            {
               foreach (Ticket ticket in item.Tickets)
               {
                   filepaths.Add(proxy.Create(ticket));
               }
            }
            /* proxy pattern */

            return filepaths;
        }
    }
}
