﻿using Ninject.Modules;
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
            Bind<IRequirementModificationData>().To<RequirementModificationData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IRequirementVersionData>().To<RequirementVersionData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IRequirementModificationVersionData>().To<RequirementModificationVersionData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IProjectData>().To<ProjectData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IActivityFeedData>().To<ActivityFeedData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IRequirementCommentData>().To<RequirementCommentData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IRequirementModificationCommentData>().To<RequirementModificationCommentData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IRequirementQuestionData>().To<RequirementQuestionData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IRequirementQuestionAnswerData>().To<RequirementQuestionAnswerData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IRequirementHashtagData>().To<RequirementHashtagData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IRequirementModificationHashtagData>().To<RequirementModificationHashtagData>().WithConstructorArgument(contextCnxStr, dbContext);
            Bind<IRoleData>().To<RoleData>().WithConstructorArgument(contextCnxStr, dbContext);

            Bind<IPersonBusiness>().To<PersonBusiness>();
            Bind<ICompanyBusiness>().To<CompanyBusiness>();
            Bind<IRequirementBusiness>().To<RequirementBusiness>();
            Bind<IRequirementModificationBusiness>().To<RequirementModificationBusiness>();
            Bind<IProjectBusiness>().To<ProjectBusiness>();
            Bind<IActivityFeedBusiness>().To<ActivityFeedBusiness>();
            Bind<IRequirementCommentBusiness>().To<RequirementCommentBusiness>();
            Bind<IRequirementModificationCommentBusiness>().To<RequirementModificationCommentBusiness>();
            Bind<IRequirementQuestionBusiness>().To<RequirementQuestionBusiness>();
            Bind<IRequirementQuestionAnswerBusiness>().To<RequirementQuestionAnswerBusiness>();
            Bind<IRequirementHashtagBusiness>().To<RequirementHashtagBusiness>();
            Bind<IRequirementVersionBusiness>().To<RequirementVersionBusiness>();
            Bind<IGeneralCatalogBusiness>().To<GeneralCatalogBusiness>();
        }

        private static ContextModel GetContext()
        {
            return new ContextModel();
        }
    }
}
