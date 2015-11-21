using Ninject.Modules;
using System.Configuration;
using Data.Account;
using DataContext;
using SocialRequirements.Domain.Repository.Account;

namespace SocialRequirements.CompositionRoot
{
    public class DataDependencyMapper : NinjectModule
    {
        public override void Load()
        {
            var dbContext = GetContext();
            Bind<IPersonData>().To<PersonData>().
                WithConstructorArgument("context", dbContext);
        }

        private static SocialRequirementsModel GetContext()
        {
            return
                new SocialRequirementsModel(ConfigurationManager.ConnectionStrings["cnxSocialRequirementsDB"].ConnectionString);
        }
    }
}
