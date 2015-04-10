using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AvansFestivals.Domain.Database;
using AvansFestivals.Domain.Abstract;
using AvansFestivals.Models;
using AvansFestivals.Filters;

namespace AvansFestivals.Controllers
{
    [AuthorizeFilter(Role = "Administrator")]
    public class FestivalBeheerController : CultureBaseController
    {
        IFestivalRepo festivalProvider;
        ITicketRepo ticketProvider;

        
        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FestivalBeheerController(IFestivalRepo festivalRepo, ITicketRepo ticketRepo)
        {
            festivalProvider = festivalRepo;
            ticketProvider = ticketRepo;
        }


        public ActionResult Index()
        {
            logger.Info("Festivals ophalen.");

            
            IEnumerable<Festival> festivals = festivalProvider.getAllFestivals();

            FestivalListModel model = new FestivalListModel
            {
                Festivals = festivals
            };

            logger.Info("Festivals opgehaald en in een model gestopt.");

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(string searchString)
        {
            logger.Info("Festivals ophalen.");


            IEnumerable<Festival> festivals = festivalProvider.getAllFestivals();

            if (!String.IsNullOrEmpty(searchString))
            {
                festivals = festivals.Where(f => f.Name.Contains(searchString));

                logger.Info("Zoek actie met '" + searchString + "'");
            }

            FestivalListModel model = new FestivalListModel
            {
                Festivals = festivals
            };

            logger.Info("Festivals opgehaald en in een model gestopt.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            logger.Info("Festivals ophalen");
            Festival festival = festivalProvider.GetFestival(id);

            logger.Info("Festivals ophalen, als id leeg is een HttpNotFound teruggeven");
            if (festival == null)
            {
                return HttpNotFound();
            }

            logger.Info("Details van festival teruggeven");
            return View(festival);
        }

        public ActionResult Create()
        {
            logger.Info("Maak link voor Create aan in de view");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Start,End,Logo,Banner,FestivalState")] Festival festival)
        {
            logger.Info("Maak festival aan");
            if (ModelState.IsValid)
            {
                festivalProvider.AddFestival(festival);
                return RedirectToAction("Index");
            }

            logger.Info("Geeft de view van aangemaakt festival terug");
            return View(festival);
        }

        public ActionResult Edit(int id)
        {
            logger.Info("Haal festival op a.d.v. id");
            Festival festival = festivalProvider.GetFestival(id);

            logger.Info("Wanneer festival id NULL is een HttpNotFound terug geven");
            if (festival == null)
            {
                return HttpNotFound();
            }

            logger.Info("Geeft view van festival terug");
            return View(festival);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Start,End,Logo,Banner,FestivalState")] Festival festival)
        {
            logger.Info("Controleer of gegevens in de ingevoerde velden correct zijn ingevoerd");
            if (ModelState.IsValid)
            {
                festivalProvider.EditFestival(festival);
                
                logger.Info("Geef index terug");
                return RedirectToAction("Index");
            }

            logger.Info("Geef view van festival terug");
            return View(festival);
        }

        public ActionResult Delete(int id)
        {
            logger.Info("Verwijder festival a.d.v. id");
            Festival festival = festivalProvider.GetFestival(id);

            logger.Info("Als id NULL is geeft een HttpNotFound terug");
            if (festival == null)
            {
                return HttpNotFound();
            }

            logger.Info("Geef festival terug");
            return View(festival);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            logger.Info("Geeft festival wat verwijder moet worden");
            Festival festival = festivalProvider.GetFestival(id);

            logger.Info("Vewijder festival");
            festivalProvider.RemoveFestival(festival);

            logger.Info("Geeft index terug");
            return RedirectToAction("Index");
        }

        public ActionResult EditTicketAmount(int ticketAmountid)
        {
            logger.Info("Wijzig het aantal kaarten");
            TicketAmount ticketAmount = (ticketProvider.GetTicketAmount(ticketAmountid));

            logger.Info("Indien er geen kaarten zijn geef HttpNotFound terug");
            if (ticketAmount == null)
            {
                return HttpNotFound();
            }

            logger.Info("Geef view met aantal kaarten terug");
            return View(ticketAmount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTicketAmount([Bind(Include = "Id,Price,TicketType,FestivalId,Amount")] TicketAmount ticketAmount)
        {
            logger.Info("Controleer of alle gegevens goed zijn ingevuld");
            if (ModelState.IsValid)
            {
                ticketProvider.EditTicketAmount(ticketAmount);

                logger.Info("Wijzig de gegevens");
                return RedirectToAction("Edit", new { id = ticketAmount.FestivalId });
            }

            logger.Info("Geef view ticketAmount terug");
            return View(ticketAmount);
        }

        public ActionResult CreateTicketAmount(int festivalID)
        {
            logger.Info("Maak niew model TicketAmount aan");
            TicketAmount model = new TicketAmount
            {
                FestivalId = festivalID
            };

            logger.Info("Geef view EditTicketAmount terug met model");
            return View("EditTicketAmount", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTicketAmount([Bind(Include = "Id,Price,TicketType,FestivalId,Amount")] TicketAmount ticketAmount)
        {
            logger.Info("Controleer of ingevoerde gegevens correct zijn");
            if (ModelState.IsValid)
            {
                logger.Info("Controleert of kaarten al is aangemaakt. Zo ja wordt er een ViewBag Message teruggegeven.");
                if (!ticketProvider.TicketTypeExists(ticketAmount.FestivalId, ticketAmount.TicketType))
                {
                    ticketProvider.AddTicketAmount(ticketAmount);

                    logger.Info("Maak kaarten aan");
                    return RedirectToAction("Edit", new { id = ticketAmount.FestivalId });
                }
           
                else
                {
                    ViewBag.Message = "Kan geen twee de zelfde ticket types aanmaken!";
                }
            }
            logger.Info("Geef view EditTicketAmount met ticketAmount terug");
            return View("EditTicketAmount", ticketAmount);
        }

        public ActionResult DeleteTicketAmount(int ticketAmountid)
        {
            logger.Info("Geeft aantal kaarten");
            TicketAmount ticketAmount = (ticketProvider.GetTicketAmount(ticketAmountid));

            logger.Info("Indien id niet aanwezig geef HttpNotFound returg");
            if (ticketAmount == null)
            {
                return HttpNotFound();
            }

            logger.Info("Geef view ticketAmountTerug");
            return View(ticketAmount);
        }

        [HttpPost, ActionName("DeleteTicketAmount")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedTicketAmount(int ticketAmountid)
        {
            logger.Info("Verwijder TicketAmount");
            TicketAmount ticketAmount = ticketProvider.GetTicketAmount(ticketAmountid);
            ticketProvider.RemoveTicketAmount(ticketAmount);

            logger.Info("Geef een redirectToAction terug");
            return RedirectToAction("Edit", new { id = ticketAmount.FestivalId });
        }
    }
}
