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
using AvansFestivals.HtmlHelpers;

namespace AvansFestivals.Controllers
{
    public class HomeController : CultureBaseController
    {
        IFestivalRepo festivalProvider;

        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public HomeController(IFestivalRepo festivalRepo)
        {
            festivalProvider = festivalRepo;

            //If the database is empty, uncommand this line for test data in the database.
            //TestData.Fill(30);

            //LOGGER options:
            //logger.Info("Info");
            //logger.Debug("Debug");
            //logger.Error("Error", ex);
            //Dir: projectfolder\AvansFestivals\logs\log.txt

        }  

        public ViewResult Index()
        {
            logger.Info("Opkomende festivals ophalen");
            logger.Info("Random festivals ophalen");

            FestivalListModel model = new FestivalListModel
            {
                Festivals = festivalProvider.getUpcomingFirstTen(),
                RandomFestivals = festivalProvider.GetRandomFestivals(5)
            };

            logger.Info("FestivalListModel maken en weergeven in view");
            
            return View(model);
        }

        public ActionResult Business()
        {
            logger.Info("Maak link voor Bussiness aan in de view");

            return View();
        }

        public ActionResult About()
        {
            logger.Info("Maak link voor About aan in de view");

            return View();
        }

        public ActionResult Contact()
        {
            logger.Info("Maak link voor Contact aan in de view");

            return View();
        }

        public ViewResult Events(int page = 1)
        {
            int PageSize = 8;
            logger.Info("Aantal events per pagina is 8");
            
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

            logger.Info("FestivalListModel maken en weergeven in view");

            return View(model);
        }

    }
}