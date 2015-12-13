using Ninject.Modules;
using SocialRequirements.Business.Account;
using SocialRequirements.Business.General;
using SocialRequirements.Business.Requirement;
using SocialRequirements.Context;
using SocialRequirements.Data.Account;
using SocialRequirements.Data.General;
using SocialRequirements.Data.Requirement;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Domain.BusinessLogic.General;
using SocialRequirements.Domain.BusinessLogic.Requirement;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Domain.Repository.General;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.CompositionRoot
{
    public class DataDependencyMapper : NinjectModule
    {
        public override void Load()
        {
            var dbContext = GetContext();
            const string contextCnxStr = "context";

            Bind<IPersonData>().To<PersonData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<ICompanyData>().To<CompanyData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IGeneralCatalogData>().To<GeneralCatalogData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IRequirementData>().To<RequirementData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IRequirementVersionData>().To<RequirementVersionData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IProjectData>().To<ProjectData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IActivityFeedData>().To<ActivityFeedData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IRequirementCommentData>().To<RequirementCommentData>().WithConstructorArgument(contextCnxStr, dbContext);

            Bind<IPersonBusiness>().To<PersonBusiness>();
            Bind<ICompanyBusiness>().To<CompanyBusiness>();
            Bind<IRequirementBusiness>().To<RequirementBusiness>();
            Bind<IProjectBusiness>().To<ProjectBusiness>();
            Bind<IActivityFeedBusiness>().To<ActivityFeedBusiness>();
        }

        private static ContextModel GetContext()
        {
            return new ContextModel();
            // new ContextModel(ConfigurationManager.ConnectionStrings["cnxSocialRequirementsDB"].ConnectionString);
        }
    }
}
