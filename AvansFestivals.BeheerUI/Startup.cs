using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AvansFestivals.BeheerUI.Startup))]
namespace AvansFestivals.BeheerUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
