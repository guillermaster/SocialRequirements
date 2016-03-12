using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;

namespace DataContext.Entities
{
    public class RequirementModificationHashtagData
    {
         private readonly ContextModel _context;

         public RequirementModificationHashtagData(ContextModel context)
        {
            _context = context;
        }

        public void Add(long requirementModifId, string[] hashtagsToAdd, string[] hashtagsToRemove, long personId)
        {
            foreach (var hashtag in hashtagsToAdd)
            {
                _context.RequirementModificationHashtag.Add(CreateRequirementHashtag(requirementModifId, hashtag, personId));
                _context.SaveChanges();
            }
        }

        private static RequirementModificationHashtag CreateRequirementHashtag(long requirementModifId, string hashtag, long personId)
        {
            var requirementModifHashtag = new RequirementModificationHashtag
            {
                requirement_modification_id = requirementModifId,
                hashtag = hashtag,
                createdby_id = personId,
                createdon = DateTime.Now
            };
            return requirementHashtag;
        }
    }
}
