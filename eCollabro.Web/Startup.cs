using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eCollabro.Web.Startup))]
namespace eCollabro.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
