﻿using System;
using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.General;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Domain.Repository.General;
using SocialRequirements.Domain.Repository.Requirement;
using SocialRequirements.Utilities;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementModificationData : IRequirementModificationData
    {
        private readonly ContextModel _context;
        private IRequirementModificationVersionData _requirementModifVersionData;
        private readonly IRequirementModificationCommentData _requirementModifCommentData;
        private readonly IGeneralCatalogData _generalCatalogData;
        private readonly IPersonData _personData;
        private readonly IProjectData _projectData;
        private const int MaxShortDescriptionLength = 590;

        public RequirementModificationData(ContextModel context)
        {
            _context = context;
        }

        public RequirementModificationData(ContextModel context,
            IRequirementModificationCommentData requirementModifCommentData, IGeneralCatalogData generalCatalogData,
            IPersonData personData, IProjectData projectData)
        {
            _context = context;
            _requirementModifCommentData = requirementModifCommentData;
            _generalCatalogData = generalCatalogData;
            _personData = personData;
            _projectData = projectData;
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

                    // add hashtags
                    var reqModifHashtagData = new RequirementModificationHashtagData(_context);
                    reqModifHashtagData.Add(requirement.Id, requirement.Hashtags, requirement.HashtagsToAdd,
                        requirement.HashtagsToRemove, requirement.CreatedbyId);

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
                        (r.status_id == (int) GeneralCatalog.Detail.RequirementStatus.Draft ||
                         r.status_id == (int) GeneralCatalog.Detail.RequirementStatus.PendingApproval));

            return requirementModif != null ? GetDtoFromEntity(requirementModif) : null;
        }

        public List<RequirementModificationDto> GetList(List<long> projectIds)
        {
            var requirements =
                _context.RequirementModification.Where(req => projectIds.Contains(req.project_id))
                    .OrderByDescending(reqDate => reqDate.modifiedon)
                    .ToList();
            return requirements.Select(GetDtoFromEntity).ToList();
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

        public void Approve(long companyId, long projectId, long requirementId, long requirementModificationId, long personId)
        {
            using (var scope = _context.Database.BeginTransaction())
            {
                try
                {
                    // update requirement modification status to approved
                    // get requirement and update it
                    var requirementModif = GetEntity(companyId, projectId, requirementId, requirementModificationId);
                    requirementModif.status_id = (int)GeneralCatalog.Detail.RequirementStatus.Approved;
                    requirementModif.modifiedby_id = personId;
                    requirementModif.modifiedon = DateTime.Now;
                    _context.SaveChanges();

                    // update requirement version
                    _requirementModifVersionData = new RequirementModificationVersionData(_context);
                    _requirementModifVersionData.UpdateStatus(companyId, projectId, requirementId, requirementModificationId,
                        requirementModif.requirement_modification_version_id, requirementModif.status_id, personId);

                    // get all requirement modification data
                    var requirementModifDto = Get(companyId, projectId, requirementId, requirementModificationId);

                    // add new requirement version
                    var reqVersionData = new RequirementVersionData(_context);
                    var requirementDto = reqVersionData.Add(new RequirementDto(requirementModifDto));

                    // set current requirement version
                    var reqData = new RequirementData(_context);
                    reqData.UpdateVersionNumber(requirementDto);

                    // update requirement
                    // get requirement and update it
                    var requirement = reqData.GetEntity(companyId, requirementId);
                    requirement.title = requirementDto.Title;
                    requirement.description = requirementDto.Description;
                    requirement.priority_id = requirementDto.PriorityId;
                    requirement.modifiedby_id = personId;
                    requirement.modifiedon = DateTime.Now;
                    _context.SaveChanges();

                    scope.Commit();
                }
                catch
                {
                    scope.Rollback();
                    throw;
                }
            }
        }

        public void Like(long companyId, long projectId, long requirementId, long requirementModificationId, long personId)
        {
            using (var scope = _context.Database.BeginTransaction())
            {
                try
                {
                    // update like on requirement
                    var requirementModif = GetEntity(companyId, projectId, requirementId, requirementModificationId);
                    if (requirementModif == null) return;
                    requirementModif.agreed++;
                    _context.SaveChanges();

                    // update like on current requirement version
                    _requirementModifVersionData = new RequirementModificationVersionData(_context);
                    _requirementModifVersionData.Like(requirementModif.company_id, requirementModif.project_id,
                        requirementModif.requirement_id,
                        requirementModif.id, requirementModif.requirement_modification_version_id, personId);

                    scope.Commit();
                }
                catch
                {
                    scope.Rollback();
                    throw;
                }
            }
        }

        public void Dislike(long companyId, long projectId, long requirementId, long requirementModificationId, long personId)
        {
            using (var scope = _context.Database.BeginTransaction())
            {
                try
                {
                    // update like on requirement
                    var requirementModif = GetEntity(companyId, projectId, requirementId, requirementModificationId);
                    if (requirementModif == null) return;
                    requirementModif.disagreed++;
                    _context.SaveChanges();

                    // update like on current requirement version
                    _requirementModifVersionData = new RequirementModificationVersionData(_context);
                    _requirementModifVersionData.Dislike(requirementModif.company_id, requirementModif.project_id,
                        requirementModif.requirement_id,
                        requirementModif.id, requirementModif.requirement_modification_version_id, personId);

                    scope.Commit();
                }
                catch
                {
                    scope.Rollback();
                    throw;
                }
            }
        }

        public void UpdateStatus(long companyId, long projectId, long requirementId, long requirementModificationId, int statusId, long personId)
        {
            using (var scope = _context.Database.BeginTransaction())
            {
                try
                {
                    // get requirement and update it
                    var requirementModif = GetEntity(companyId, projectId, requirementId, requirementModificationId);
                    requirementModif.status_id = statusId;
                    requirementModif.modifiedby_id = personId;
                    requirementModif.modifiedon = DateTime.Now;
                    _context.SaveChanges();

                    // update requirement version
                    _requirementModifVersionData = new RequirementModificationVersionData(_context);
                    _requirementModifVersionData.UpdateStatus(companyId, projectId, requirementId, requirementModificationId,
                        requirementModif.requirement_modification_version_id, requirementModif.status_id, personId);

                    scope.Commit();
                }
                catch
                {
                    scope.Rollback();
                    throw;
                }
            }
        }

        public void Update(string title, string description, long companyId, long projectId, long requirementId,
            long requirementModificationId, int priorityId, long personId)
        {
            using (var scope = _context.Database.BeginTransaction())
            {
                try
                {
                    // get requirement and update it
                    var requirement = GetEntity(companyId, projectId, requirementId, requirementModificationId);
                    requirement.title = title;
                    requirement.description = description;
                    requirement.priority_id = priorityId;
                    requirement.modifiedby_id = personId;
                    requirement.modifiedon = DateTime.Now;
                    _context.SaveChanges();

                    // update requirement version
                    _requirementModifVersionData = new RequirementModificationVersionData(_context);
                    _requirementModifVersionData.Update(title, description, companyId, projectId, requirementId, requirementModificationId,
                        requirement.requirement_modification_version_id, priorityId, personId);

                    scope.Commit();
                }
                catch
                {
                    scope.Rollback();
                    throw;
                }
            }
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
                approvedon = requirementDto.Approvedon,
                priority_id = requirementDto.PriorityId
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
                PriorityId = requirement.priority_id,
                Project = requirement.Project != null ? requirement.Project.name : _projectData.GetTitle(requirement.project_id),
                Status = requirement.GeneralCatalogDetail != null ? requirement.GeneralCatalogDetail.name : _generalCatalogData.GetTitle(requirement.status_id),
                Priority = requirement.GeneralCatalogDetail1 != null ? requirement.GeneralCatalogDetail1.name : _generalCatalogData.GetTitle(requirement.priority_id),
                CreatedByName = requirement.Person != null ? StringUtilities.GetPersonFullName(requirement.Person) : _personData.GetFullName(requirement.createdby_id),
                ModifiedByName = requirement.Person1 != null ? StringUtilities.GetPersonFullName(requirement.Person1) : _personData.GetFullName(requirement.modifiedby_id),
                ShortDescription = StringUtilities.GetShort(requirement.description, MaxShortDescriptionLength),
                VersionId = requirement.requirement_modification_version_id,
                VersionNumber = requirement.version_number,
                CommentsQuantity = _requirementModifCommentData.GetQuantity(requirement.company_id,
                                        requirement.project_id, requirement.requirement_id, requirement.id, requirement.requirement_modification_version_id)
            };

            return requirementDto;
        }
    }
}
