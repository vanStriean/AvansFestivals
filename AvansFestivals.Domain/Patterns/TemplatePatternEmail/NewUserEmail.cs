using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFestivals.Domain.Database;

namespace AvansFestivals.Domain.Patterns.TemplatePatternEmail
{
    public class NewUserEmail : Email
    {
        public NewUserEmail(User user)
        {
            this.user = user;
        }

        public override string getEmailBody()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<p> Welkom bij avans tickets, bedankt voor het registreren.</p>");
            sb.Append("<p> U kunt nu aan de slag om festival tickets aan te kopen.</p>");

            return sb.ToString();
        }

        public override string getEmailSubject()
        {
            string subject = "Welkom bij Avans Festivals.";

            return subject;
        }

        public override string getFilepath() // als de filepath "" bevat wordt er niets meegestuurd.
        {
            string filePath = "";

            return filePath;
        }
    }
}
