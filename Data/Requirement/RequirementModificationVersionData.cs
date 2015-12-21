using System;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementModificationVersionData : IRequirementModificationVersionData
    {
        private readonly ContextModel _context;

        public RequirementModificationVersionData(ContextModel context)
        {
            _context = context;
        }

        public RequirementModificationDto Add(RequirementModificationDto requirementModif)
        {
            var requirementModifVersion = GetEntityFromDto(requirementModif);

            requirementModifVersion.version_number = GetNextVersionNumber(requirementModifVersion.company_id,
                requirementModifVersion.project_id, requirementModifVersion.requirement_id, requirementModifVersion.requirement_modification_id);

            _context.RequirementModificationVersion.Add(requirementModifVersion);
            _context.SaveChanges();

            // set key values to requirement dto
            requirementModif.VersionNumber = requirementModifVersion.version_number;
            requirementModif.VersionId = requirementModifVersion.id;

            return requirementModif;
        }

        public RequirementDto Get(long companyId, long projectId, long requirementId, long? requirementVersionId = null)
        {
            throw new NotImplementedException();
        }

        public void Like(long companyId, long projectId, long requirementId, long requirementVersionId, long personId)
        {
            throw new NotImplementedException();
        }

        public void Dislike(long companyId, long projectId, long requirementId, long requirementVersionId, long personId)
        {
            throw new NotImplementedException();
        }

        public void UpdateStatus(long companyId, long projectId, long requirementId, long versionId, int statusId, long personId)
        {
            throw new NotImplementedException();
        }

        private int GetNextVersionNumber(long companyId, long projectId, long requirementId, long requirementModificationId)
        {
            var requirementModifVersion =
                _context.RequirementVersion.Where(
                    rv => rv.company_id == companyId && rv.project_id == projectId 
                        && rv.requirement_id == requirementId && rv.requirement_modification_id == requirementModificationId)
                    .OrderByDescending(vn => vn.version_number)
                    .FirstOrDefault();
            return requirementModifVersion != null ? requirementModifVersion.version_number + 1 : 1;
        }

        private static RequirementModificationVersion GetEntityFromDto(RequirementModificationDto requirement)
        {
            var requirementVersion = new RequirementModificationVersion
            {
                company_id = requirement.CompanyId,
                project_id = requirement.ProjectId,
                requirement_id = requirement.RequirementId,
                requirement_modification_id = requirement.Id,
                title = requirement.Title,
                description = requirement.Description,
                agreed = requirement.Agreed,
                disagreed = requirement.Disagreed,
                status_id = requirement.StatusId,
                createdby_id = requirement.CreatedbyId,
                createdon = requirement.Createdon,
                modifiedby_id = requirement.ModifiedbyId,
                modifiedon = requirement.Modifiedon,
                approvedby_id = requirement.ApprovedbyId,
                approvedon = requirement.Approvedon
            };
            return requirementVersion;
        }
    }
}
