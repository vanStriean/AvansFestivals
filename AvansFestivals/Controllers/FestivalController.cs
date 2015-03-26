using AvansFestivals.Domain.Abstract;
using AvansFestivals.Domain.Database;
using AvansFestivals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AvansFestivals.Controllers
{
    public class FestivalController : Controller
    {

        IFestivalRepo festivalProvider;

        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FestivalController(IFestivalRepo festivalRepo)
        {
            festivalProvider = festivalRepo;
        }

        public ActionResult Index(int id)
        {
            Festival festival = festivalProvider.GetFestival(id);

            if (festival == null)
            {
                return HttpNotFound();
            }

            return View(festival);
        }

        public ActionResult Tickets(int id)
        {
            Festival festival = festivalProvider.GetFestival(id);

            if (festival == null)
            {
                return HttpNotFound();
            }

            TicketOrderModel model = new TicketOrderModel() 
            {
                Festival = festival,
                User = (User) Session["User"],
                Earlybird = 0,
                Normal = 0,
                VIP = 0
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Tickets(TicketOrderModel model)
        {
            if (ModelState.IsValid)
            {
                Session["TicketModel"] = model;
                return RedirectToAction("Overview", "Ticket");
            }
            return View();
        }
    }
}