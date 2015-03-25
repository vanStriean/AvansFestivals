using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AvansFestivals
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Paging",
                "{action}/Page/{page}",
                new { Controller = "Home", action = "Events" },
                new { page = @"\d+" }
            );

            routes.MapRoute(
                "Festivals",
                "Events",
                new { controller = "Home", action = "Events" }
            );

            routes.MapRoute(
                "Festival_tickets",
                "{controller}/{id}/Tickets",
                new { controller = "Festival", action = "Tickets" },
                new { id = @"\d+" }
            );

            routes.MapRoute(
                "Festival_details",
                "{controller}/{id}",
                new { controller = "Festival", action = "Index" },
                new { id = @"\d+" }
            );

            routes.MapRoute(
               "FestivalBeheer",
               "FestivalBeheer",
               new { controller = "FestivalBeheer", action = "Index" }
           );


            


            routes.MapRoute(
                "Account",
                "{action}",
                new { controller = "Account" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

    
            
        }
    }
}
