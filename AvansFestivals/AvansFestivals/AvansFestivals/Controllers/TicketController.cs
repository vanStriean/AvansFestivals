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
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: /Ticket/
        public ActionResult Order()
        {
            return View();
        }
        public ActionResult Overview()
        {
            TicketOrderModel model = (TicketOrderModel)Session["TIcketModel"];
            return View(model);
        }
	}
}