using System.Linq;
using DataContext;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementData : IRequirementData
    {
        private readonly ContextModel _context;

        public RequirementData(ContextModel context)
        {
            _context = context;
        }

        public int GetNumberOfRequirements(long companyId)
        {
            var numReq = _context.Requirement.Count(r => r.company_id == companyId);
            return numReq;
        }

        
    }
}
