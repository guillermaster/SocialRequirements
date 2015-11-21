using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Ninject;
using System.Reflection;
using Ninject.Parameters;
using Ninject.Web.Common;


namespace SocialRequirements
{
    public class Global : NinjectHttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {

        }

        //protected override IKernel CreateKernel()
        //{
            //var kernel = new StandardKernel();
            //kernel.Load(Assembly.GetExecutingAssembly(),
            //    Assembly
            //        .Load("SocialRequirements.Domain"),
            //    Assembly
            //        .Load("SocialRequirements.Data"),
            //    Assembly
            //        .Load("SocialRequirements.CompositionRoot"));
            //return kernel;
        //}

        protected override IKernel CreateKernel()
        {
            throw new NotImplementedException();
        }
    }
}