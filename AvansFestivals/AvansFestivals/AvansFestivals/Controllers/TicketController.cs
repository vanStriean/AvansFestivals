using AvansFestivals.Domain.Abstract;
using AvansFestivals.Domain.Database;
using AvansFestivals.Domain.Patterns.FactoryPatternTickets;
using AvansFestivals.Domain.Patterns.StrategyPatternPayment;
using AvansFestivals.Domain.Patterns.TemplatePatternEmail;
using AvansFestivals.Filters;
using AvansFestivals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvansFestivals.Controllers
{
    [AuthorizeFilter]
    public class TicketController : CultureBaseController
    {

        IOrderRepo orderRepo;
        AvansFestivalsEntities db = new AvansFestivalsEntities();
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TicketController(IOrderRepo repo)
        {
            orderRepo = repo;
        }

        public ActionResult Order(string strategy)
        {
            logger.Info("Maak Payment Strategy aan");
            PaymentStategy strat = null;
            if (strategy == "ideal")
                strat = new IdealStrategy("2344", "IBAN349238492304");
            else
                strat = new PaypalStrategy("test@test.nl", "test");

            strat.Pay(6);

            logger.Info("Sessie voor TicketOrderModel aanmaken");
            TicketOrderModel model = (TicketOrderModel)Session["tempModel"];
            if (model != null)
            {
                logger.Info("Maak nieuwe Order aan");
                Order order = new Order();
                order.Ordered = DateTime.Now;

                Festival festival = db.Festivals.FirstOrDefault(m => m.Id == model.Festival.Id);
                User user = db.Users.FirstOrDefault(m => m.Id == model.User.Id);

                logger.Info("Indien gekozen voor Earlybird kaarten toevoegen aan Order");
                if (model.Tickets.Earlybird > 0)
                {
                    OrderItem item = new OrderItem();

                    /* factory pattern */ 
                    TicketFactory factory = new EarlybirdFactory();
                    for (int i = 0; i < model.Tickets.Earlybird; i++)
                    {
                        Ticket earlybird = factory.CreateTicket(model.Festival.TicketAmounts.FirstOrDefault(w => w.TicketType == TicketType.Earlybird).Price);
                        festival.Tickets.Add(earlybird);
                        item.Tickets.Add(earlybird);
                    }
                    /* factory pattern */ 

                    order.OrderItems.Add(item);
                }

                logger.Info("Indien gekozen voor Normaal kaarten toevoegen aan Order");
                if (model.Tickets.Normal > 0)
                {
                    OrderItem item = new OrderItem();

                    /* factory pattern */ 
                    TicketFactory factory = new NormalFactory();
                    for (int i = 0; i < model.Tickets.Normal; i++)
                    {
                        Ticket normal = factory.CreateTicket(model.Festival.TicketAmounts.FirstOrDefault(w => w.TicketType == TicketType.Normal).Price);
                        festival.Tickets.Add(normal);
                        item.Tickets.Add(normal);
                    }
                    /* factory pattern */ 

                    order.OrderItems.Add(item);
                }

                logger.Info("Indien gekozen voor VIP kaarten toevoegen aan Order");
                if (model.Tickets.VIP > 0)
                {
                    OrderItem item = new OrderItem();

                    /* factory pattern */ 
                    TicketFactory factory = new VipFactory();
                    for (int i = 0; i < model.Tickets.VIP; i++)
                    {
                        Ticket vip = factory.CreateTicket(model.Festival.TicketAmounts.FirstOrDefault(w => w.TicketType == TicketType.VIP).Price);
                        festival.Tickets.Add(vip);
                        item.Tickets.Add(vip);
                    }
                    /* factory pattern */ 

                    order.OrderItems.Add(item);
                }

                logger.Info("Order toevoegen aan users");
                user.Orders.Add(order);

                logger.Info("Order opslaan");
                db.SaveChanges();

                logger.Info("E-mail sturen met de kaarten naar user");

                /* template pattern */
                Email email = new OrderEmail(user, order);
                email.sendEmail();
                /* template pattern */
                
                ViewBag.Email = model.User.Email;
                Session["tempModel"] = null;

                logger.Info("View teruggeven");
                return View();

            }

            logger.Info("VIew teruggeven");
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Pay(string strategy)
        {
            logger.Info("Maak paymentModel aan");
            PaymentModel model = new PaymentModel();
            if(!string.IsNullOrEmpty(strategy)) {
                model.Strategy = strategy;
                model.TicketModel = (TicketOrderModel)Session["tempModel"];

                logger.Info("Keuze tussen Ideal of Paypal");
                if(strategy == "ideal") 
                {
                    model.IdealModel = new IDealModel();
                    model.PaypalModel = null;
                }
                else
                {
                    model.IdealModel = null;
                    model.PaypalModel = new PayPalModel();
                }
            }

            logger.Info("Geeft Pay view met model terug");
            return View(model);
        }

        public ActionResult Overview()
        {
            logger.Info("Maak sessie aan voor TicketOrderModel");
            TicketOrderModel model = (TicketOrderModel)Session["tempModel"];

            logger.Info("Geef Overview view en TicketOrderModel terug");
            return View(model);
        }
    }
}