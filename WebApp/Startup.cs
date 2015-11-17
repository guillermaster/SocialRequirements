using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SocialRequirements.Startup))]
namespace SocialRequirements
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
