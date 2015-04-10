using AvansFestivals.Domain.Abstract;
using AvansFestivals.Models;
using AvansFestivals.Domain.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AvansFestivals.Domain.Patterns.TemplatePatternEmail;

namespace AvansFestivals.Controllers
{
    public class AccountController : CultureBaseController
    {

        IUserRepo userProvider;

        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AccountController(IUserRepo userProvider)
        {
            this.userProvider = userProvider;
        }

        public ActionResult Login()
        {
            logger.Info("Maak link voor Login aan in de view");
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            logger.Info("Controleer of de loginvelden goed zijn ingevuld, zoniet word de login view gereturned");
            if (ModelState.IsValid)
            {

           logger.Info("Controleer of Inloggegevens correct zijn ingevuld, indien ja dan geef word de view getoond");
            if (userProvider.CheckLogin(model.Username, model.Password))
            {
                Session["User"] = userProvider.GetUser(model.Username);
                return RedirectToAction("Index", "Home");
            }
            }
            return View(model);
        }

        public ActionResult Register()
        {
            logger.Info("Maak link voor Register aan in de view");
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
            logger.Info("Controleer of alle velden correct zijn ingevoerd om een account aan te maken.");
            if (ModelState.IsValid && Session["User"] == null)
            {
                User user = new User() 
                {
                    Firstname = model.Firstname,
                    Lastname = model.Lastname,
                    Age = model.Age,
                    Email = model.Email,
                    Username = model.Username,
                    Password = model.Password,
                    
                };

                Email email = new NewUserEmail(user);
                email.sendEmail();

                Session["User"] = userProvider.CreateUser(user) ? user : null;

                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            logger.Info("Maak link voor Logout aan in de view");

            Session.Remove("User");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult CheckAvailabilty(string Username)
        {
            logger.Info("Controleer of gebruikersnaam in gebruik is");
            return Json(userProvider.GetUser(Username) == null);
        }
    }
}
