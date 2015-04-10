using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AvansFestivals.Domain.Abstract;
using AvansFestivals.Domain.Database;
using AvansFestivals.Domain.Helpers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Data.Entity;

namespace AvansFestivals.Domain.Concrete
{
    public class TicketRepo : ITicketRepo
    {
        AvansFestivalsEntities db = new AvansFestivalsEntities();

        //public string CreateTicketPdf(Ticket ticket)
        //{
        //   Document myTicket = new Document(PageSize.A4);
        //   Md5Hash md5 = new Md5Hash();
        //   string fileName = ticket.Festival.Name + ticket.Id + "O" + ticket.OrderItem.OrderId + ".pdf";
        //   string path = Path.Combine(Environment.CurrentDirectory, @"Tickets\", fileName);

        //   try
        //   {
        //       // Now create a writer that listens to this doucment and writes the document to desired Stream.

        //       PdfWriter.GetInstance(myTicket, new FileStream(path, FileMode.Create));

        //       // step 3:  Open the document now using
        //       myTicket.Open();

        //       // step 4: Now add some contents to the document
        //       //myTicket.Add(new Paragraph("test2"));

        //       myTicket.Add(new Paragraph(ticket.Festival.Name));
        //       myTicket.Add(new Paragraph("Barcode: " + md5.CalculateMD5Hash(ticket.Id.ToString())));
        //       myTicket.Add(new Paragraph("Ordernummer: " + ticket.OrderItem.OrderId.ToString()));
        //       myTicket.Add(new Paragraph("Prijs: " + ticket.Price.ToString()));
        //       myTicket.Add(new Paragraph("Ticket Type: " + ticket.TicketType.ToString()));
        //   }
        //   catch (DocumentException de)
        //   {
        //        Console.Error.WriteLine(de.Message);
        //   }
        //   catch (IOException ioe)
        //   {
        //        Console.Error.WriteLine(ioe.Message);
        //   }

        //    // step 5: Remember to close the documnet

        //   myTicket.Close();

        //   return path; 
        //}

        public TicketAmount GetTicketAmount(int id)
        {
            return (from e in db.TicketAmounts where e.Id == id select e).FirstOrDefault();
        }

        public void RemoveTicketAmount(TicketAmount ticketAmount)
        {
            db.TicketAmounts.Remove(ticketAmount);
            db.SaveChanges();
        }

        public void EditTicketAmount(TicketAmount ticketAmount)
        {
            db.Entry(ticketAmount).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void AddTicketAmount(TicketAmount ticketAmount)
        {
            db.TicketAmounts.Add(ticketAmount);
            db.SaveChanges();
        }

        public bool TicketTypeExists(int festivalId, TicketType ticketType)
        {
            TicketAmount tcka = (from e in db.TicketAmounts where e.TicketType == ticketType && e.FestivalId == festivalId select e).FirstOrDefault(); 

            if (tcka != null)
            {
                return true;
            }
            return false;
        }
    }
}
