using AvansFestivals.Domain.Database;
using AvansFestivals.Domain.Helpers;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvansFestivals.Domain.Patterns.ProxyPatternPDF
{
    public class ProxyPDF : IPDF
    {
        public string Create(Ticket ticket) 
        {
            Document myTicket = new Document(PageSize.A4);
            Md5Hash md5 = new Md5Hash();
            string fileName = ticket.Id + "O" + ticket.OrderItem.OrderId + ".pdf";
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Tickets\", fileName);

            bool exists = System.IO.Directory.Exists(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Tickets\"));

            if (!exists)
                System.IO.Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Tickets\"));

            try
            {
                PdfWriter.GetInstance(myTicket, new FileStream(path, FileMode.Create));

                myTicket.Open();

                myTicket.Add(new Paragraph(ticket.Festival.Name));
                myTicket.Add(new Paragraph("Barcode: " + md5.CalculateMD5Hash(ticket.Id.ToString())));
                myTicket.Add(new Paragraph("Ordernummer: " + ticket.OrderItem.OrderId.ToString()));
                myTicket.Add(new Paragraph("Prijs: " + ticket.Price.ToString()));
                myTicket.Add(new Paragraph("Ticket Type: " + ticket.TicketType.ToString()));
            }
            catch (DocumentException de)
            {
                Console.Error.WriteLine(de.Message);
            }
            catch (IOException ioe)
            {
                Console.Error.WriteLine(ioe.Message);
            }

            myTicket.Close();

            return path;
        }
    }
}
