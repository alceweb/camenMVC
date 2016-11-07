using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CamenMVC.Startup))]
namespace CamenMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
