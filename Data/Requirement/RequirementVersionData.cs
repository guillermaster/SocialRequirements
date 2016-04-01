using System;
using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Data.Account;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Domain.Repository.General;
using SocialRequirements.Domain.Repository.Requirement;
using SocialRequirements.Utilities;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementVersionData : IRequirementVersionData
    {
        private readonly ContextModel _context;
        private readonly IGeneralCatalogData _generalCatalogData;
        private readonly IPersonData _personData;
        private readonly IProjectData _projectData;

        public RequirementVersionData(ContextModel context)
        {
            _context = context;
        }

        public RequirementVersionData(ContextModel context, IGeneralCatalogData generalCatalogData,
            IPersonData personData, IProjectData projectData)
        {
            _context = context;
            _generalCatalogData = generalCatalogData;
            _personData = personData;
            _projectData = projectData;
        }

        public RequirementDto Add(RequirementDto requirement)
        {
            var requirementVersion = GetEntityFromRequirementDto(requirement);

            requirementVersion.version_number = GetNextVersionNumber(requirementVersion.company_id,
                requirementVersion.project_id, requirementVersion.requirement_id);

            _context.RequirementVersion.Add(requirementVersion);
            _context.SaveChanges();

            // set key values to requirement dto
            requirement.VersionNumber = requirementVersion.version_number;
            requirement.VersionId = requirementVersion.id;

            return requirement;
        }

        public RequirementDto Get(long companyId, long projectId, long requirementId, long? requirementVersionId = null)
        {
            // if version isn't specified, get the latest one
            if (requirementVersionId == null)
                return GetLastest(companyId, projectId, requirementId);

            // otherwise get specific version
            var requirementVersion = Get(companyId, projectId, requirementId, requirementVersionId.Value);

            return requirementVersion != null ? GetDtoFromEntity(requirementVersion) : null;
        }

        public string GetAttachmentTitle(long companyId, long projectId, long requirementId, long? requirementVersionId = null)
        {
            var requirement = Get(companyId, projectId, requirementId, requirementVersionId);
            return requirement != null ? requirement.AttachmentTitle : string.Empty;
        }

        public byte[] GetAttachment(long companyId, long projectId, long requirementId, long? requirementVersionId = null)
        {
            if (requirementVersionId == null)
            {
                var requirementVersion =
                    _context.RequirementVersion.Where(
                        rv => rv.company_id == companyId && rv.project_id == projectId && rv.requirement_id == requirementId)
                        .OrderByDescending(v => v.id)
                        .FirstOrDefault();

                return requirementVersion != null ? requirementVersion.attachment : null;
            }
            else
            {
                var requirementVersion =
               _context.RequirementVersion.FirstOrDefault(
                   rv =>
                       rv.company_id == companyId && rv.project_id == projectId && rv.requirement_id == requirementId &&
                       rv.id == requirementVersionId);

                return requirementVersion != null ? requirementVersion.attachment : null;
            }
        }

        public void Like(long companyId, long projectId, long requirementId, long requirementVersionId, long personId)
        {
            var requirementVersion = Get(companyId, projectId, requirementId, requirementVersionId);
            if (requirementVersion == null) return;

            requirementVersion.agreed++;
            _context.SaveChanges();
        }

        public void Dislike(long companyId, long projectId, long requirementId, long requirementVersionId, long personId)
        {
            var requirementVersion = Get(companyId, projectId, requirementId, requirementVersionId);
            if (requirementVersion == null) return;

            requirementVersion.disagreed++;
            _context.SaveChanges();
        }

        public void UpdateStatus(long companyId, long projectId, long requirementId, long versionId, int statusId, long personId)
        {
            var requirementVersion = Get(companyId, projectId, requirementId, versionId);
            requirementVersion.status_id = statusId;
            requirementVersion.modifiedby_id = personId;
            requirementVersion.modifiedon = DateTime.Now;
            if (statusId == (int) GeneralCatalog.Detail.RequirementStatus.Approved ||
                statusId == (int) GeneralCatalog.Detail.RequirementStatus.Rejected)
            {
                requirementVersion.approvedby_id = personId;
                requirementVersion.approvedon = DateTime.Now;
            }
            _context.SaveChanges();
        }

        public void Update(string title, string description, long newProjectId, long companyId, long projectId,
            long requirementId, long versionId, int priorityId, long personId)
        {
            var requirementVersion = Get(companyId, projectId, requirementId, versionId);
            requirementVersion.title = title;
            requirementVersion.project_id = newProjectId;
            requirementVersion.description = description;
            requirementVersion.priority_id = priorityId;
            requirementVersion.modifiedby_id = personId;
            requirementVersion.modifiedon = DateTime.Now;

            // if requirement being updated was previously rejected, then set as draft
            if (requirementVersion.status_id == (int)GeneralCatalog.Detail.RequirementStatus.Rejected)
            {
                requirementVersion.status_id = (int)GeneralCatalog.Detail.RequirementStatus.Draft;
            }

            _context.SaveChanges();
        }

        public void UploadAttachment(long companyId, long projectId, long requirementId, long requirementVersionId, string fileName,
            byte[] fileContent, long personId)
        {
            var requirementVers = Get(companyId, projectId, requirementId, requirementVersionId);
            requirementVers.attachment_title = fileName;
            requirementVers.attachment = fileContent;
            _context.SaveChanges();
        }

        public List<RequirementDto> GetVersionHistory(long companyId, long projectId, long requirementId)
        {
            var requirementVersions =
                _context.RequirementVersion.Where(
                    rv => rv.company_id == companyId && rv.project_id == projectId && rv.requirement_id == requirementId)
                    .OrderByDescending(v => v.version_number)
                    .ToList();
            return requirementVersions.Select(GetDtoFromEntity).ToList();
        }

        public List<PersonDto> GetUsersInvolvedInRequirement(long companyId, long projectId, long requirementId)
        {
            var users = new List<PersonDto>();
            var requirementLastVers = GetLatestVersion(companyId, projectId, requirementId);

            if (requirementLastVers.Person != null)
                users.Add(PersonData.GetDtoFromEntity(requirementLastVers.Person));
            if (requirementLastVers.Person1 != null && requirementLastVers.createdby_id != requirementLastVers.modifiedby_id)
                users.Add(PersonData.GetDtoFromEntity(requirementLastVers.Person1));
            if (requirementLastVers.Person2 != null && requirementLastVers.createdby_id != requirementLastVers.approvedby_id && requirementLastVers.modifiedby_id != requirementLastVers.approvedby_id)
                users.Add(PersonData.GetDtoFromEntity(requirementLastVers.Person2));

            return users;
        }

        private RequirementVersion Get(long companyId, long projectId, long requirementId, long requirementVersionId)
        {
            var requirementVersion =
               _context.RequirementVersion.FirstOrDefault(
                   rv =>
                       rv.company_id == companyId && rv.project_id == projectId && rv.requirement_id == requirementId &&
                       rv.id == requirementVersionId);
            return requirementVersion;
        }

        private RequirementDto GetLastest(long companyId, long projectId, long requirementId)
        {
            var requirementVersion = GetLatestVersion(companyId, projectId, requirementId);

            return requirementVersion != null ? GetDtoFromEntity(requirementVersion) : null;
        }

        private RequirementVersion GetLatestVersion(long companyId, long projectId, long requirementId)
        {
            return
                _context.RequirementVersion.Where(
                    rv => rv.company_id == companyId && rv.project_id == projectId && rv.requirement_id == requirementId)
                    .OrderByDescending(v => v.id)
                    .FirstOrDefault();
        }

        private int GetNextVersionNumber(long companyId, long projectId, long requirementId)
        {
            var requirementVersion =
                _context.RequirementVersion.Where(
                    rv => rv.company_id == companyId && rv.project_id == projectId && rv.requirement_id == requirementId)
                    .OrderByDescending(vn => vn.version_number)
                    .FirstOrDefault();
            return requirementVersion != null ? requirementVersion.version_number + 1 : 1;
        }

        private static RequirementVersion GetEntityFromRequirementDto(RequirementDto requirement)
        {
            var requirementVersion = new RequirementVersion
            {
                company_id = requirement.CompanyId,
                project_id = requirement.ProjectId,
                requirement_id = requirement.Id,
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
                approvedon = requirement.Approvedon,
                priority_id = requirement.PriorityId
            };
            return requirementVersion;
        }

        private RequirementDto GetDtoFromEntity(RequirementVersion requirementVersion)
        {
            var requirementDto = new RequirementDto
            {
                Id = requirementVersion.requirement_id,
                CompanyId = requirementVersion.company_id,
                ProjectId = requirementVersion.project_id,
                Title = requirementVersion.title,
                Description = requirementVersion.description,
                Agreed = requirementVersion.agreed,
                Disagreed = requirementVersion.disagreed,
                StatusId = requirementVersion.status_id,
                CreatedbyId = requirementVersion.createdby_id,
                Createdon = requirementVersion.createdon,
                ModifiedbyId = requirementVersion.modifiedby_id,
                Modifiedon = requirementVersion.modifiedon,
                ApprovedbyId = requirementVersion.approvedby_id,
                Approvedon = requirementVersion.approvedon,
                VersionId = requirementVersion.id,
                VersionNumber = requirementVersion.version_number,
                AttachmentTitle = requirementVersion.attachment_title,
                PriorityId = requirementVersion.priority_id,
                ShortDescription = StringUtilities.GetShort(requirementVersion.description, RequirementData.MaxShortDescriptionLength),
                Project = requirementVersion.Project != null ? requirementVersion.Project.name : _projectData.GetTitle(requirementVersion.project_id),
                Status = requirementVersion.GeneralCatalogDetail != null ? requirementVersion.GeneralCatalogDetail.name : _generalCatalogData.GetTitle(requirementVersion.status_id),
                Priority = requirementVersion.GeneralCatalogDetail1 != null ? requirementVersion.GeneralCatalogDetail1.name : _generalCatalogData.GetTitle(requirementVersion.priority_id),
                CreatedByName = _personData.GetFullName(requirementVersion.createdby_id),
                ModifiedByName = _personData.GetFullName(requirementVersion.modifiedby_id),
            };
            return requirementDto;
        }
    }
}
