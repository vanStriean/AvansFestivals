﻿using Microsoft.Owin;
using Owin;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Web.config", Watch = true)] 
[assembly: OwinStartupAttribute(typeof(AvansFestivals.Startup))]
namespace AvansFestivals
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
