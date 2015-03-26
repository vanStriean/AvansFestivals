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
    public class AccountController : Controller
    {

        IUserRepo userProvider;

        readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public AccountController(IUserRepo userProvider)
        {
            this.userProvider = userProvider;
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (userProvider.CheckLogin(model.Username, model.Password))
            {
                Session["User"] = userProvider.GetUser(model.Username);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel model)
        {
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
                    Role = "User"
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
            Session.Remove("User");
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult CheckAvailabilty(string Username)
        {
            return Json(userProvider.GetUser(Username) == null);
        }
    }
}
