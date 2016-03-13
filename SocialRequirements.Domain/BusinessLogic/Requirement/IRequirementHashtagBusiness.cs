using System.Collections.Generic;

namespace SocialRequirements.Domain.BusinessLogic.Requirement
{
    public interface IRequirementHashtagBusiness
    {
        List<string> GetMostUsedHashtags(int listSize);
    }
}
