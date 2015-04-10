using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AvansFestivals.Controllers
{
    public class ErrorController : CultureBaseController
    {
        // GET: Error
        public ActionResult UnAuthorized()
        {
            return View();
        }

        public ActionResult NoAccess()
        {
            return View();
        }
    }
}