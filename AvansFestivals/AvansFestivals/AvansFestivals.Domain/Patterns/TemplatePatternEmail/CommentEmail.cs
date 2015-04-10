using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFestivals.Domain.Database;

namespace AvansFestivals.Domain.Patterns.TemplatePatternEmail
{
    public class CommentEmail : Email
    {
        private Comment comment;
        
        public CommentEmail(User user, Comment comment)
        {
            this.user = user;
            this.comment = comment;
        }

        public override string getEmailBody()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<p> U heeft een reactie geplaatst bij het event: "+ comment.Festival.Name +".</p>");
            sb.Append("<p> De reactie is: <code>" + comment.Message + "</code></p>");
            sb.Append("<p> Reactie geplaatst op : " + comment.Created.ToShortDateString() + " " + comment.Created.ToShortTimeString() + " </p>");

            return sb.ToString();
        }

        public override string getEmailSubject()
        {
            string subject = "Reactie geplaats op " + comment.Festival.Name + ".";

            return subject;
        }

        public override List<string> getFilepath() // als de filepath "" bevat wordt er niets meegestuurd.
        {
            return new List<string>();
        }
    }
}
