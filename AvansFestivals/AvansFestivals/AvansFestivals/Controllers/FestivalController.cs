using AvansFestivals.Domain.Abstract;
using AvansFestivals.Domain.Database;
using AvansFestivals.Models;
using AvansFestivals.Domain.Patterns.IteratorPatternComments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AvansFestivals.Domain.Patterns.TemplatePatternEmail;
using AvansFestivals.Filters;

namespace AvansFestivals.Controllers
{
    public class FestivalController : CultureBaseController
    {

        IFestivalRepo festivalProvider;
        ICommentRepo commentProvider;

        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public FestivalController(IFestivalRepo festivalRepo, ICommentRepo commentRepo)
        {
            festivalProvider = festivalRepo;
            commentProvider = commentRepo;
        }

        public ActionResult Index(int id)
        {
            logger.Info("Haal festival op a.d.v. id");
            Festival festival = festivalProvider.GetFestival(id);

            logger.Info("Indien festival is NULL geef een HttpNotFound terug");
            if (festival == null)
            {
                return HttpNotFound();
            }

            logger.Info("Maak lijst met comments aan");
           List<Comment> ModelComments = new List<Comment>();
            
            if (festival.Comments != null)
            {
                /* iterator pattern */
                Collection collection = new Collection();

                int i = 0;
                foreach (Comment comment in festival.Comments)
                {
                    collection[i] = new Item(comment);
                    i++;
                }

                Iterator iterator = new Iterator(collection);

                for (Item item = iterator.First();
                    !iterator.IsDone; item = iterator.Next())
                {
                    ModelComments.Add(item.Comment);
                }
                /* iterator pattern */
            }

            logger.Info("Maak nieuw model voor FestivalModel aan");
           FestivalModel model = new FestivalModel
           {
               Id = festival.Id,
               Name = festival.Name,
               Description = festival.Description,
               Start = festival.Start,
               End = festival.End,
               Logo = festival.Logo,
               Banner = festival.Banner,
               FestivalState = festival.FestivalState,
               Comments = ModelComments, // Iteratoter pattern
               TicketAmounts = festival.TicketAmounts,
               Tickets = festival.Tickets
           };

           logger.Info("Geef model in view terug");
            return View(model);
        }

        [AuthorizeFilter]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "Message, FestivalId")] Comment comment)
        {
            logger.Info("Maak sessie aan voor User");
            User sessionUser = (User)Session["User"];
            comment.Created = DateTime.Now;
            comment.UserId = sessionUser.Id;

            logger.Info("Controleer of comment correct is");
            if (ModelState.IsValid)
            {
                logger.Info("Voeg comment toe");
                commentProvider.AddComment(comment);

                logger.Info("Geef comment van festival");
                comment.Festival = festivalProvider.GetFestival(comment.FestivalId);

                logger.Info("Verstuur e-mail user bij een nieuwe reactie op comment");

                /* template pattern */
                Email email = new CommentEmail(sessionUser, comment);
                email.sendEmail();
                /* template pattern */

                logger.Info("Geef een RedirectToAction terug");
                return RedirectToAction("Index", new { id = comment.FestivalId });
            }

            logger.Info("Geef melding dat comment is geplaatst");
            ViewBag.Message = Resources.Resource.MessageValid;

            logger.Info("Geef een RedirectToAction terug");
            return RedirectToAction("Index", new { id = comment.FestivalId });
        }

        [AuthorizeFilter]
        public ActionResult Tickets(int id)
        {
            logger.Info("Geef festival a.d.v. id");
            Festival festival = festivalProvider.GetFestival(id);

            logger.Info("Indien festival is NULL geef een HttpNotFound terug");
            if (festival == null)
            {
                return HttpNotFound();
            }

            logger.Info("Maak nieuw model voor TicketOrderModel aan");
            TicketOrderModel model = new TicketOrderModel()
            {
                Festival = festival,
                User = (User)Session["User"],
                Tickets = new TicketAmounts()
            };

            logger.Info("Geef model van TicketOrderModel terug");
            return View(model);
        }

        [AuthorizeFilter]
        [HttpPost]
        public JsonResult Tickets(int festId, TicketAmounts tickets)
        {
            logger.Info("Maak nieuw model voor TiceketOrderModel aan");
            TicketOrderModel model = new TicketOrderModel()
            {
                Festival = festivalProvider.GetFestival(festId),
                User = (User)Session["User"],
                Tickets = tickets
            };

            Session["tempModel"] = model;

            logger.Info("Geef een Json terug");
            return Json(new
            {
                redirectUrl = Url.Action("Overview", "Ticket"),
                isRedirect = true
            });
        }
    }
}