using DataContext;
using Ninject.Modules;
using SocialRequirements.Business.Account;
using SocialRequirements.Data.Account;
using SocialRequirements.Data.General;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Domain.Repository.General;

namespace SocialRequirements.CompositionRoot
{
    public class DataDependencyMapper : NinjectModule
    {
        public override void Load()
        {
            var dbContext = GetContext();

            Bind<IPersonData>().To<PersonData>().WithConstructorArgument("context", dbContext);
            Bind<ICompanyData>().To<CompanyData>().WithConstructorArgument("context", dbContext);
            Bind<IGeneralCatalogData>().To<GeneralCatalogData>().WithConstructorArgument("context", dbContext);

            Bind<IPersonBusiness>().To<PersonBusiness>();
            Bind<ICompanyBusiness>().To<CompanyBusiness>();
        }

        private static ContextModel GetContext()
        {
            return new ContextModel();
            // new ContextModel(ConfigurationManager.ConnectionStrings["cnxSocialRequirementsDB"].ConnectionString);
        }
    }
}
