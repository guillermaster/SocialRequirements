using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SocialRequirements.WebApp.Startup))]
namespace SocialRequirements.WebApp
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
