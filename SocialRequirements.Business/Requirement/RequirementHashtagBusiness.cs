using System;
using System.Collections.Generic;
using SocialRequirements.Domain.BusinessLogic.Requirement;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Business.Requirement
{
    public class RequirementHashtagBusiness : IRequirementHashtagBusiness
    {
        private readonly IRequirementHashtagData _requirementHashtagData;

        public RequirementHashtagBusiness(IRequirementHashtagData requirementHashtagData)
        {
            _requirementHashtagData = requirementHashtagData;
        }

        public List<string> GetMostUsedHashtags(int listSize)
        {
            return _requirementHashtagData.GetMostUsedHashtags(listSize);
        }
    }
}
