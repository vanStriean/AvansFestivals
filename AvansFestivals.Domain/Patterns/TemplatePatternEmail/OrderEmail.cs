using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFestivals.Domain.Database;

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

            sb.Append("<p> Welkom bij avans tickets, bedankt voor het regeistreren.</p>");
            sb.Append("<p> U kunt nu aan de slag om festival tickets aan te kopen.</p>");

            return sb.ToString();
        }

        public override string getEmailSubject()
        {
            string subject = "Ticket Order Avans Festivals.";

            return subject;
        }

        public override string getFilepath() // als de filepath "" bevat wordt er niets meegestuurd.
        {
            string filePath = "";

            return filePath;
        }
    }
}
