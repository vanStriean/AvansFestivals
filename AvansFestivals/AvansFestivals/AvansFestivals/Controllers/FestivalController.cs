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
                User = (User)Session["User"],
                Tickets = new TicketAmounts()
            };

            return View(model);
        }

        [HttpPost]
        public JsonResult Tickets(int festId, TicketAmounts tickets)
        {
            TicketOrderModel model = new TicketOrderModel()
            {
                Festival = festivalProvider.GetFestival(festId),
                User = (User)Session["User"],
                Tickets = tickets
            };

            Session["tempModel"] = model;

            return Json(new
            {
                redirectUrl = Url.Action("Overview", "Ticket"),
                isRedirect = true
            });
        }
    }
}