using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Memoirs.Web.Startup))]
namespace Memoirs.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
