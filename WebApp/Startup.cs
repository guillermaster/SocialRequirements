﻿using Microsoft.Owin;
using Owin;
using SocialRequirements;

[assembly: OwinStartup(typeof(Startup))]
namespace SocialRequirements
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
