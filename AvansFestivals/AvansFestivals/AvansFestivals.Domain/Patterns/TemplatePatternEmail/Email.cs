using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using AvansFestivals.Domain.Database;

namespace AvansFestivals.Domain.Patterns.TemplatePatternEmail
{
    public abstract class Email
    {
        protected User user;

        public abstract string getEmailBody();
        public abstract string getEmailSubject();
        public abstract List<string> getFilepath(); // als de filepath "" bevat wordt er niets meegestuurd.

        public SmtpClient getSmtpSettings()
        {
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("avansfestivals@gmail.com", "FXp-vUE-PbV-2wZ"),
                EnableSsl = true
            };

            return smtp;
        }

        public string getReciever()
        {
            string reciever = user.Email;

            return reciever;
        }

        public MailAddress getSender()
        {
            MailAddress senderAddress = new MailAddress("avansfestivals@gmail.com");

            return senderAddress;
        }

        public string getEmailStart()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<p> Beste " + user.Firstname + " " + user.Lastname + ",");
            sb.Append("<p> Dit is een automatisch verstuurd bericht van de AvansFestival Ticket Systeem. </p>");

            return sb.ToString();
        }

        public string getEmailSignature()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("<p> Met vriendelijke groet, </p>");
            sb.Append("<p> Avans Festivals </p>");

            return sb.ToString();
        }

        public void sendEmail()
        {
            MailMessage msg = new MailMessage();

            msg.To.Add(getReciever());
            msg.From = getSender();
            msg.Subject = getEmailSubject();
            msg.IsBodyHtml = true;

            List<string> filePath = getFilepath();

            if (filePath != null || filePath.All(x => string.IsNullOrWhiteSpace(x)))
            {
                foreach (string filepathitem in filePath)
                {
                    msg.Attachments.Add(new Attachment(filepathitem));
                }
            }

            StringBuilder sb = new StringBuilder();
            
            sb.Append(getEmailStart());
            sb.Append(getEmailBody());
            sb.Append(getEmailSignature());

            msg.Body = sb.ToString();
            getSmtpSettings().Send(msg);
            msg.Dispose();
        }
    }
}
