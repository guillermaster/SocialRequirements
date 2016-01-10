using System;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
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

        public RequirementModificationDto Get(long companyId, long projectId, long requirementId, long requirementModificationId, long? requirementModifVersionId = null)
        {
            // if version isn't specified, get the latest one
            if (requirementModifVersionId == null)
                return GetLastest(companyId, projectId, requirementId, requirementModificationId);

            // otherwise get specific version
            var requirementVersion = Get(companyId, projectId, requirementId, requirementModificationId, requirementModifVersionId.Value);

            return requirementVersion != null ? GetDtoFromEntity(requirementVersion) : null;
        }

        public string GetAttachmentTitle(long companyId, long projectId, long requirementId, long requirementModificationId,
            long? requirementModifVersionId = null)
        {
            var requirementModif = Get(companyId, projectId, requirementId, requirementModificationId, requirementModifVersionId);
            return requirementModif != null ? requirementModif.AttachmentTitle : string.Empty;
        }

        public byte[] GetAttachment(long companyId, long projectId, long requirementId, long requirementModificationId,
            long? requirementModifVersionId = null)
        {
            if (requirementModifVersionId == null)
            {
                var requirementVersion =
                    _context.RequirementModificationVersion.Where(
                        rv => rv.company_id == companyId && rv.project_id == projectId && rv.requirement_id == requirementId &&
                              rv.requirement_modification_id == requirementModificationId)
                        .OrderByDescending(v => v.id)
                        .FirstOrDefault();

                return requirementVersion != null ? requirementVersion.attachment : null;
            }
            else
            {
                var requirementVersion =
                   _context.RequirementModificationVersion.FirstOrDefault(
                       rv =>
                           rv.company_id == companyId && rv.project_id == projectId && rv.requirement_id == requirementId &&
                           rv.requirement_modification_id == requirementModificationId && rv.id == requirementModifVersionId);

                return requirementVersion != null ? requirementVersion.attachment : null;
            }
        }

        public void Like(long companyId, long projectId, long requirementId, long requirementModificationId, long requirementModifVersionId, long personId)
        {
            var requirementModifVersion = Get(companyId, projectId, requirementId, requirementModificationId,
                requirementModifVersionId);

            if (requirementModifVersion == null) return;

            requirementModifVersion.agreed++;
            _context.SaveChanges();
        }

        public void Dislike(long companyId, long projectId, long requirementId, long requirementModificationId, long requirementModifVersionId, long personId)
        {
            var requirementModifVersion = Get(companyId, projectId, requirementId, requirementModificationId,
                requirementModifVersionId);

            if (requirementModifVersion == null) return;

            requirementModifVersion.disagreed++;
            _context.SaveChanges();
        }

        public void UpdateStatus(long companyId, long projectId, long requirementId, long requirementModificationId, long versionId, int statusId, long personId)
        {
            var requirementVersion = Get(companyId, projectId, requirementId, requirementModificationId, versionId);
            requirementVersion.status_id = statusId;
            requirementVersion.modifiedby_id = personId;
            requirementVersion.modifiedon = DateTime.Now;
            if (statusId == (int)GeneralCatalog.Detail.RequirementStatus.Approved ||
                statusId == (int)GeneralCatalog.Detail.RequirementStatus.Rejected)
            {
                requirementVersion.approvedby_id = personId;
                requirementVersion.approvedon = DateTime.Now;
            }
            _context.SaveChanges();
        }

        public void Update(string title, string description, long companyId, long projectId, long requirementId,
            long requirementModificationId, long versionId, long personId)
        {
            var requirementVersion = Get(companyId, projectId, requirementId, requirementModificationId, versionId);
            requirementVersion.title = title;
            requirementVersion.description = description;
            requirementVersion.modifiedby_id = personId;
            requirementVersion.modifiedon = DateTime.Now;
            _context.SaveChanges();
        }

        public void UploadAttachment(long companyId, long projectId, long requirementId, long requirementModificationId, long versionId,
            string fileName, byte[] fileContent, long personId)
        {
            var requirementVers = Get(companyId, projectId, requirementId, requirementModificationId, versionId);
            requirementVers.attachment_title = fileName;
            requirementVers.attachment = fileContent;
            _context.SaveChanges();
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

        private RequirementModificationVersion Get(long companyId, long projectId, long requirementId, long requirementModificationId, long requirementModifVersionId)
        {
            var requirementVersion =
               _context.RequirementModificationVersion.FirstOrDefault(
                   rv =>
                       rv.company_id == companyId && rv.project_id == projectId && rv.requirement_id == requirementId &&
                       rv.requirement_modification_id == requirementModificationId && rv.id == requirementModifVersionId);
            return requirementVersion;
        }

        private RequirementModificationDto GetLastest(long companyId, long projectId, long requirementId, long requirementModificationId)
        {
            var requirementVersion =
                _context.RequirementModificationVersion.Where(
                    rv => rv.company_id == companyId && rv.project_id == projectId && rv.requirement_id == requirementId && 
                          rv.requirement_modification_id == requirementModificationId)
                    .OrderByDescending(v => v.id)
                    .FirstOrDefault();

            return requirementVersion != null ? GetDtoFromEntity(requirementVersion) : null;
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

        private static RequirementModificationDto GetDtoFromEntity(RequirementModificationVersion requirementVersion)
        {
            var requirementDto = new RequirementModificationDto
            {
                Id = requirementVersion.requirement_modification_id,
                CompanyId = requirementVersion.company_id,
                ProjectId = requirementVersion.project_id,
                RequirementId = requirementVersion.requirement_id,
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
                AttachmentTitle = requirementVersion.attachment_title
            };
            return requirementDto;
        }
    }
}
