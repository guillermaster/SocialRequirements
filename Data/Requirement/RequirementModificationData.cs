﻿using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.Domain.Repository.Requirement;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementModificationData : IRequirementModificationData
    {
        private readonly ContextModel _context;

        public RequirementModificationData(ContextModel context)
        {
            _context = context;
        }

        public long Add(RequirementModificationDto requirement)
        {
            using (var scope = _context.Database.BeginTransaction())
            {
                try
                {
                    // add requirement modification request
                    var newRequirementModif = _context.RequirementModification.Add(GetEntityFromDto(requirement));
                    _context.SaveChanges();
                    requirement.Id = newRequirementModif.id;

                    // add requirement version
                    var reqModifVersionData = new RequirementModificationVersionData(_context);
                    requirement = reqModifVersionData.Add(requirement);

                    // set version keys to currently added requirement
                    UpdateVersionNumber(requirement);

                    scope.Commit();

                    return requirement.Id;
                }
                catch
                {
                    scope.Rollback();
                    throw;
                }
            }
        }

        public RequirementModificationDto Get(long companyId, long projectId, long requirementId, long requirementModificationId)
        {
            var requirementModif = GetEntity(companyId, projectId, requirementId, requirementModificationId);
            return requirementModif != null ? GetDtoFromEntity(requirementModif) : null;
        }

        public RequirementModificationDto Get(long companyId, long projectId, long requirementId)
        {
            // only one modification request with status 'pending' must exists per requirement
            var requirementModif =
                _context.RequirementModification.FirstOrDefault(
                    r =>
                        r.company_id == companyId && r.project_id == projectId && r.requirement_id == requirementId &&
                        r.status_id == (int) GeneralCatalog.Detail.RequirementStatus.Draft);

            return requirementModif != null ? GetDtoFromEntity(requirementModif) : null;
        }

        /// <summary>
        /// Updates the version number and version ID on the requirement modification
        /// </summary>
        /// <param name="requirementModifDto">Requirement modification</param>
        private void UpdateVersionNumber(RequirementModificationDto requirementModifDto)
        {
            var requirement = GetEntity(requirementModifDto.CompanyId, requirementModifDto.ProjectId,
                requirementModifDto.RequirementId, requirementModifDto.Id);

            if (requirement == null) return;

            requirement.version_number = requirementModifDto.VersionNumber;
            requirement.requirement_modification_version_id = requirementModifDto.VersionId;

            _context.SaveChanges();
        }

        private RequirementModification GetEntity(long companyId, long projectId, long requirementId, long requirementModificationId)
        {
            return
                _context.RequirementModification.FirstOrDefault(
                    r =>
                        r.company_id == companyId && r.project_id == projectId && r.requirement_id == requirementId &&
                        r.id == requirementModificationId);
        }

        private static RequirementModification GetEntityFromDto(RequirementModificationDto requirementDto)
        {
            var requirementModif = new RequirementModification
            {
                company_id = requirementDto.CompanyId,
                project_id = requirementDto.ProjectId,
                requirement_id = requirementDto.RequirementId,
                title = requirementDto.Title,
                description = requirementDto.Description,
                agreed = requirementDto.Agreed,
                disagreed = requirementDto.Disagreed,
                status_id = requirementDto.StatusId,
                createdby_id = requirementDto.CreatedbyId,
                createdon = requirementDto.Createdon,
                modifiedby_id = requirementDto.ModifiedbyId,
                modifiedon = requirementDto.Modifiedon,
                approvedby_id = requirementDto.ApprovedbyId,
                approvedon = requirementDto.Approvedon
            };
            return requirementModif;
        }

        private RequirementModificationDto GetDtoFromEntity(RequirementModification requirement)
        {
            var requirementDto = new RequirementModificationDto
            {
                Id = requirement.id,
                CompanyId = requirement.company_id,
                ProjectId = requirement.project_id,
                RequirementId = requirement.requirement_id,
                Title = requirement.title,
                Description = requirement.description,
                Agreed = requirement.agreed,
                Disagreed = requirement.disagreed,
                StatusId = requirement.status_id,
                CreatedbyId = requirement.createdby_id,
                Createdon = requirement.createdon,
                ModifiedbyId = requirement.modifiedby_id,
                Modifiedon = requirement.modifiedon,
                ApprovedbyId = requirement.approvedby_id,
                Approvedon = requirement.approvedon,
                Project = requirement.Project.name,
                Status = requirement.GeneralCatalogDetail.name,
                CreatedByName = requirement.Person.first_name + " " + requirement.Person.last_name,
                ModifiedByName = requirement.Person1.first_name + " " + requirement.Person1.last_name,
                VersionId = requirement.requirement_modification_version_id,
                VersionNumber = requirement.version_number
                //CommentsQuantity = _requirementCommentData.GetQuantity(requirement.id, requirement.company_id,
                //                        requirement.project_id, requirement.requirement_version_id)
            };

            return requirementDto;
        }
    }
}