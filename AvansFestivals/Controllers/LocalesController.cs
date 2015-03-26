using System;
using System.Web.Mvc;
using Insya.Localization;

namespace AvansFestivals.Controllers
{
    public class LocalesController : Controller
    {

        public ActionResult Index(string lang = "nl_NL")
        {
            Response.Cookies["CacheLang"].Value = lang;
			
            if (Request.UrlReferrer != null)
                Response.Redirect(Request.UrlReferrer.ToString());
             
			var message = Localization.Get("changedlng");
    
			return Content(message);
        }

    }
}