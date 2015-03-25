using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AvansFestivals.Domain.Database;
using AvansFestivals.Domain.Abstract;
using AvansFestivals.Domain.TestData;
using AvansFestivals.Domain.Patterns.TemplatePatternEmail;
using AvansFestivals.Models;

namespace AvansFestivals.Controllers
{
    public class HomeController : Controller
    {
        IFestivalRepo festivalProvider;

        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public HomeController(IFestivalRepo festivalRepo)
        {
            festivalProvider = festivalRepo;

            //If the database is empty, uncommand this line for test data in the database.
            //TestData.Fill(30);

            //Email email = new NewUserEmail();
            //email.sendEmail();

            //LOGGER options:
            //logger.Info("Info");
            //logger.Debug("Debug");
            //logger.Error("Error", ex);
            //Dir: projectfolder\AvansFestivals\logs\log.txt

        }

        public ViewResult Index()
        {
            FestivalListModel model = new FestivalListModel
            {
                Festivals = festivalProvider.getUpcomingFirstTen(),
                RandomFestivals = festivalProvider.GetRandomFestivals(5)
            };

            return View(model);
        }

        public ActionResult Business()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ViewResult Events(int page = 1)
        {
            int PageSize = 8;
            FestivalListModel model = new FestivalListModel
            {
                Festivals = festivalProvider.getUpcoming()
                .Skip((page - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = festivalProvider.getUpcoming().Count()
                }
            };
            return View(model);
        }

    }
}