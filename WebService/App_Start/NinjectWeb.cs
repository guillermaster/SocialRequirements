using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject.Web;
using WebActivatorEx;
using WebService.App_Start;

[assembly: PreApplicationStartMethod(typeof(NinjectWeb), "Start")]

namespace WebService.App_Start
{
    public static class NinjectWeb 
    {
        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
        }
    }
}
