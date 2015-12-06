using System;

namespace SocialRequirements.Domain.Repository.General
{
    public interface IActivityFeedData
    {
        void Add(long companyId, long? projectId, int entityId, long recordId, DateTime createdon, long personId);
    }
}
