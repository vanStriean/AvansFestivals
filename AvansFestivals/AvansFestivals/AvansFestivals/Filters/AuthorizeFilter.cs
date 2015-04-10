using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using AvansFestivals.Domain;
using AvansFestivals.Domain.Database;

namespace AvansFestivals.Filters
{
    public class AuthorizeFilter : ActionFilterAttribute, IActionFilter
    {

        public string Role { get; set; }
        void IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            AvansFestivalsEntities db = new AvansFestivalsEntities();
            if (HttpContext.Current.Session["User"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(
                        new
                        {
                            controller = "Error",
                            action = "UnAuthorized"
                        })
                    );
            }
            else
            {
                User sessionUser = (User) HttpContext.Current.Session["User"];
                User user = db.Users.Find(sessionUser.Id);

                string roles = "";

                foreach (var r in user.UserAndRoles)
                {
                    roles += r.Role.Name;
                }

                if (!string.IsNullOrEmpty(Role))
                {
                    if (!roles.Contains(Role))
                    {
                        filterContext.Result = new RedirectToRouteResult(
                        new RouteValueDictionary(
                            new
                            {
                                controller = "Error",
                                action = "NoAccess"
                            })
                        );
                    }
                }
            }

            this.OnActionExecuting(filterContext);
        }
    }
}