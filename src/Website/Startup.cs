using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(mbsoft.BrewClub.Website.Startup))]
namespace mbsoft.BrewClub.Website
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
