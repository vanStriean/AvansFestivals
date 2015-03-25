using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AvansFestival.BeheerUI.Startup))]
namespace AvansFestival.BeheerUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
