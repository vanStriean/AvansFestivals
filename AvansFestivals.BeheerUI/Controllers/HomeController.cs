using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AvansFestivals.Domain.Database;
using AvansFestivals.Domain.Abstract;
using AvansFestivals.BeheerUI.Models;

namespace AvansFestivals.BeheerUI.Controllers
{
    public class HomeController : Controller
    {
        IFestivalRepo festivalProvider;

        public HomeController(IFestivalRepo festivalRepo)
        {
            this.festivalProvider = festivalRepo;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}