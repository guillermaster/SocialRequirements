using Ninject.Modules;
using DataContext;
using SocialRequirements.Business.Account;
using SocialRequirements.Data.Account;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Utilities;

namespace SocialRequirements.CompositionRoot
{
    public class DataDependencyMapper : NinjectModule
    {
        public override void Load()
        {
            var dbContext = GetContext();

            Bind<IPersonData>().To<PersonData>().WithConstructorArgument("context", dbContext);

            Bind<IPersonBusiness>().To<PersonBusiness>();
        }

        private static ContextModel GetContext()
        {
            return new ContextModel();
            // new ContextModel(ConfigurationManager.ConnectionStrings["cnxSocialRequirementsDB"].ConnectionString);
        }
    }
}
