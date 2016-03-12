using System;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.General;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementModificationHashtagData : IRequirementModificationHashtagData
    {
        private readonly ContextModel _context;
        
        public RequirementModificationHashtagData(ContextModel context)
        {
            _context = context;
        }

        public void Add(long requirementModifId, string[] hashtags, string[] hashtagsToAdd, string[] hashtagsToRemove, long personId)
        {
            foreach (var hashtag in hashtags)
            {
                _context.RequirementModificationHashtag.Add(CreateRequirementHashtag(requirementModifId, hashtag, personId,
                    GeneralCatalog.Detail.HashtagsStatus.Approved));
                _context.SaveChanges();
            }

            foreach (var hashtag in hashtagsToAdd)
            {
                _context.RequirementModificationHashtag.Add(CreateRequirementHashtag(requirementModifId, hashtag, personId,
                    GeneralCatalog.Detail.HashtagsStatus.New));
                _context.SaveChanges();
            }

            foreach (var hashtag in hashtagsToRemove)
            {
                _context.RequirementModificationHashtag.Add(CreateRequirementHashtag(requirementModifId, hashtag, personId,
                    GeneralCatalog.Detail.HashtagsStatus.Remove));
                _context.SaveChanges();
            }
        }

        private static RequirementModificationHashtag CreateRequirementHashtag(long requirementModifId, string hashtag, long personId,
            GeneralCatalog.Detail.HashtagsStatus status)
        {
            var requirementModifHashtag = new RequirementModificationHashtag
            {
                requirement_modification_id = requirementModifId,
                hashtag = hashtag,
                createdby_id = personId,
                createdon = DateTime.Now,
                status_id = (int)status
            };
            return requirementModifHashtag;
        }
    }
}
