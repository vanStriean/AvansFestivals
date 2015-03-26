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
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TicketController(IOrderRepo repo)
        {
            orderRepo = repo;
        }

        // GET: /Ticket/
        public ActionResult Order()
        {
            TicketOrderModel model = (TicketOrderModel)Session["TIcketModel"];
            if (model != null)
            {
                Order order = new Order();
                order.Ordered = DateTime.Now;

                OrderItem item = new OrderItem();

                TicketFactory factory = new EarlybirdFactory();
                for (int i = 0; i < model.Tickets.Earlybird; i++)
                {
                    Ticket earlybird = factory.CreateTicket(model.Festival.TicketAmounts.FirstOrDefault(w => w.TicketType == TicketType.Earlybird).Price);
                    model.Festival.Tickets.Add(earlybird);
                    model.User.Tickets.Add(earlybird);
                    item.Tickets.Add(earlybird);
                }
                order.OrderItems.Add(item);

                item = new OrderItem();
                factory = new NormalFactory();
                for (int i = 0; i < model.Tickets.Normal; i++)
                {
                    Ticket normal = factory.CreateTicket(model.Festival.TicketAmounts.FirstOrDefault(w => w.TicketType == TicketType.Normal).Price);
                    model.Festival.Tickets.Add(normal);
                    model.User.Tickets.Add(normal);
                    item.Tickets.Add(normal);
                }
                order.OrderItems.Add(item);

                item = new OrderItem();
                factory = new VipFactory();
                for (int i = 0; i < model.Tickets.VIP; i++)
                {
                    Ticket vip = factory.CreateTicket(model.Festival.TicketAmounts.FirstOrDefault(w => w.TicketType == TicketType.VIP).Price);
                    model.Festival.Tickets.Add(vip);
                    model.User.Tickets.Add(vip);
                    item.Tickets.Add(vip);
                }
                order.OrderItems.Add(item);

                orderRepo.AddOrder(order);

                return View();

            }
            return View();
        }
        public ActionResult Overview()
        {
            TicketOrderModel model = (TicketOrderModel)Session["TIcketModel"];
            return View(model);
        }
    }
}