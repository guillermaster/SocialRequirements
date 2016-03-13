using System;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementHashtagData : IRequirementHashtagData
    {
        private readonly ContextModel _context;

        public RequirementHashtagData(ContextModel context)
        {
            _context = context;
        }

        public void Add(long requirementId, string[] hashtags, long personId)
        {
            foreach (var hashtag in hashtags)
            {
                _context.RequirementHashtag.Add(CreateRequirementHashtag(requirementId, hashtag, personId));
                _context.SaveChanges();
            }
        }

        public string[] Get(long requirementId)
        {
            var reqHashtags =
                _context.RequirementHashtag.Where(hashtag => hashtag.requirement_id == requirementId)
                    .Select(resHashtag => resHashtag.hashtag)
                    .ToList();
            return reqHashtags.ToArray();
        }

        public long[] GetRequirementsId(string hashtag)
        {
            var requirementsId =
                _context.RequirementHashtag.Where(reqHash => reqHash.hashtag == hashtag)
                    .ToList();
            return requirementsId.Select(reqHash => reqHash.requirement_id).ToArray();
        }

        private static RequirementHashtag CreateRequirementHashtag(long requirementId, string hashtag, long personId)
        {
            var requirementHashtag = new RequirementHashtag
            {
                requirement_id = requirementId,
                hashtag = hashtag,
                createdby_id = personId,
                createdon = DateTime.Now
            };
            return requirementHashtag;
        }
    }
}
