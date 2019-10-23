using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WillPower.WebMVC.Startup))]
namespace WillPower.WebMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
