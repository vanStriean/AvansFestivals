using AvansFestivals.Domain.Abstract;
using AvansFestivals.Domain.Database;
using AvansFestivals.Domain.Patterns.FactoryPatternTickets;
using AvansFestivals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvansFestivals.Controllers
{
    public class TicketController : Controller
    {

        IOrderRepo orderRepo;
        AvansFestivalsEntities db = new AvansFestivalsEntities();
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TicketController(IOrderRepo repo)
        {
            orderRepo = repo;
        }

        // GET: /Ticket/
        public ActionResult Order()
        {
            TicketOrderModel model = (TicketOrderModel)Session["tempModel"];
            if (model != null)
            {
                Order order = new Order();
                order.Ordered = DateTime.Now;

                Festival festival = db.Festivals.FirstOrDefault(m => m.Id == model.Festival.Id);
                User user = db.Users.FirstOrDefault(m => m.Id == model.User.Id);

                if (model.Tickets.Earlybird > 0)
                {
                    OrderItem item = new OrderItem();
                    TicketFactory factory = new EarlybirdFactory();
                    for (int i = 0; i < model.Tickets.Earlybird; i++)
                    {
                        Ticket earlybird = factory.CreateTicket(model.Festival.TicketAmounts.FirstOrDefault(w => w.TicketType == TicketType.Earlybird).Price);
                        festival.Tickets.Add(earlybird);
                        user.Tickets.Add(earlybird);
                        item.Tickets.Add(earlybird);
                    }
                    order.OrderItems.Add(item);
                }

                if (model.Tickets.Normal > 0)
                {
                    OrderItem item = new OrderItem();
                    TicketFactory factory = new NormalFactory();
                    for (int i = 0; i < model.Tickets.Normal; i++)
                    {
                        Ticket normal = factory.CreateTicket(model.Festival.TicketAmounts.FirstOrDefault(w => w.TicketType == TicketType.Normal).Price);
                        festival.Tickets.Add(normal);
                        user.Tickets.Add(normal);
                        item.Tickets.Add(normal);
                    }
                    order.OrderItems.Add(item);
                }

                if (model.Tickets.VIP > 0)
                {
                    OrderItem item = new OrderItem();
                    TicketFactory factory = new VipFactory();
                    for (int i = 0; i < model.Tickets.VIP; i++)
                    {
                        Ticket vip = factory.CreateTicket(model.Festival.TicketAmounts.FirstOrDefault(w => w.TicketType == TicketType.VIP).Price);
                        festival.Tickets.Add(vip);
                        user.Tickets.Add(vip);
                        item.Tickets.Add(vip);
                    }
                    order.OrderItems.Add(item);
                }

                db.Orders.Add(order);
                db.SaveChanges();
                ViewBag.Email = model.User.Email;
                Session["tempModel"] = null;
                return View();

            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Overview()
        {
            TicketOrderModel model = (TicketOrderModel)Session["tempModel"];
            return View(model);
        }
    }
}