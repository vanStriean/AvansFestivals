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

namespace AvansFestivals.Controllers
{
    public class FestivalBeheerController : Controller
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
            Festival festival = festivalProvider.GetFestival(id);

            if (festival == null)
            {
                return HttpNotFound();
            }

            return View(festival);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,Start,End,Logo,Banner,FestivalState")] Festival festival)
        {
            if (ModelState.IsValid)
            {
                festivalProvider.AddFestival(festival);
                return RedirectToAction("Index");
            }

            return View(festival);
        }

        public ActionResult Edit(int id)
        {
            Festival festival = festivalProvider.GetFestival(id);

            if (festival == null)
            {
                return HttpNotFound();
            }
            return View(festival);
        }

        // POST: Festivals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,Start,End,Logo,Banner,FestivalState")] Festival festival)
        {
            if (ModelState.IsValid)
            {
                festivalProvider.EditFestival(festival);

                return RedirectToAction("Index");
            }
            return View(festival);
        }

        public ActionResult Delete(int id)
        {
            Festival festival = festivalProvider.GetFestival(id);
            
            if (festival == null)
            {
                return HttpNotFound();
            }
            return View(festival);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Festival festival = festivalProvider.GetFestival(id);
            festivalProvider.RemoveFestival(festival);
            return RedirectToAction("Index");
        }

        public ActionResult EditTicketAmount(int ticketAmountid)
        {
            TicketAmount ticketAmount = (ticketProvider.GetTicketAmount(ticketAmountid));

            if (ticketAmount == null)
            {
                return HttpNotFound();
            }
            return View(ticketAmount);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTicketAmount([Bind(Include = "Id,Price,TicketType,FestivalId,Amount")] TicketAmount ticketAmount)
        {
            if (ModelState.IsValid)
            {
                ticketProvider.EditTicketAmount(ticketAmount);

                return RedirectToAction("Edit", new { id = ticketAmount.FestivalId });
            }
            return View(ticketAmount);
        }

        public ActionResult CreateTicketAmount(int festivalID)
        {
            TicketAmount model = new TicketAmount
            {
                FestivalId = festivalID
            };

            return View("EditTicketAmount", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTicketAmount([Bind(Include = "Id,Price,TicketType,FestivalId,Amount")] TicketAmount ticketAmount)
        {
            if (ModelState.IsValid)
            {
                if (!ticketProvider.TicketTypeExists(ticketAmount.FestivalId, ticketAmount.TicketType))
                {
                    ticketProvider.AddTicketAmount(ticketAmount);
                    
                    return RedirectToAction("Edit", new { id = ticketAmount.FestivalId });
                }
                else
                {
                    ViewBag.Message = "Kan geen twee de zelfde ticket types aanmaken!";
                }
            }
            return View("EditTicketAmount", ticketAmount);
        }

        public ActionResult DeleteTicketAmount(int ticketAmountid)
        {
            TicketAmount ticketAmount = (ticketProvider.GetTicketAmount(ticketAmountid));

            if (ticketAmount == null)
            {
                return HttpNotFound();
            }
            return View(ticketAmount);
        }

        [HttpPost, ActionName("DeleteTicketAmount")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedTicketAmount(int ticketAmountid)
        {
            TicketAmount ticketAmount = ticketProvider.GetTicketAmount(ticketAmountid);
            ticketProvider.RemoveTicketAmount(ticketAmount);
            return RedirectToAction("Edit", new { id = ticketAmount.FestivalId });
        }
    }
}
