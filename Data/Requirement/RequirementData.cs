using System;
using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Domain.DTO.Requirement;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.General;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Domain.Repository.General;
using SocialRequirements.Domain.Repository.Requirement;
using SocialRequirements.Utilities;

namespace SocialRequirements.Data.Requirement
{
    public class RequirementData : IRequirementData
    {
        private readonly ContextModel _context;
        private IRequirementVersionData _requirementVersionData;
        private readonly IRequirementCommentData _requirementCommentData;
        private readonly IGeneralCatalogData _generalCatalogData;
        private readonly IPersonData _personData;
        private readonly IProjectData _projectData;
        private readonly IRequirementHashtagData _requirementHashtagData;
        private const int MaxShortDescriptionLength = 590;
        private const int MaxSearchResultDescription = 100;
        
        public RequirementData(ContextModel context)
        {
            _context = context;
        }

        public RequirementData(ContextModel context, IRequirementVersionData requirementVersionData,
            IRequirementCommentData requirementCommentData, IGeneralCatalogData generalCatalogData,
            IPersonData personData, IProjectData projectData, IRequirementHashtagData requirementHashtagData)
        {
            _context = context;
            _requirementVersionData = requirementVersionData;
            _requirementCommentData = requirementCommentData;
            _generalCatalogData = generalCatalogData;
            _personData = personData;
            _projectData = projectData;
            _requirementHashtagData = requirementHashtagData;
        }

        public int GetNumberOfRequirements(long companyId)
        {
            var numReq = _context.Requirement.Count(r => r.company_id == companyId);
            return numReq;
        }

        public int GetNumberOfRequirements(List<ProjectDto> projects)
        {
            var projectsIds = projects.Select(p => p.Id).ToList();
            var numReq = _context.Requirement.Count(r => projectsIds.Contains(r.project_id));
            return numReq;
        }

        public long Add(RequirementDto requirement)
        {
            using (var scope = _context.Database.BeginTransaction())
            {
                try
                {
                    // add requirement
                    var newRequirement = _context.Requirement.Add(GetEntityFromDto(requirement));
                    _context.SaveChanges();
                    requirement.Id = newRequirement.id;

                    // add requirement version
                    var reqVersionData = new RequirementVersionData(_context);
                    requirement = reqVersionData.Add(requirement);

                    // add hashtags
                    var reqHashtagData = new RequirementHashtagData(_context);
                    reqHashtagData.Add(requirement.Id, requirement.Hashtags, requirement.CreatedbyId);

                    // set version keys to currently added requirement
                    UpdateVersionNumber(requirement);

                    scope.Commit();

                    return requirement.Id;
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                {
                    // Retrieve the error messages as a list of strings.
                    var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                    // Join the list to a single string.
                    var fullErrorMessage = string.Join("; ", errorMessages);

                    // Combine the original exception message with the new one.
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                    // Throw a new DbEntityValidationException with the improved exception message.
                    throw new System.Data.Entity.Validation.DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
                catch
                {
                    scope.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Updates the version number and version ID on the requirement
        /// </summary>
        /// <param name="requirementDto">Requirement</param>
        public void UpdateVersionNumber(RequirementDto requirementDto)
        {
            var requirement = GetEntity(requirementDto.CompanyId, requirementDto.Id);

            if (requirement == null) return;

            requirement.version_number = requirementDto.VersionNumber;
            requirement.requirement_version_id = requirementDto.VersionId;

            _context.SaveChanges();
        }

        /// <summary>
        /// Search for requirements matching the text
        /// </summary>
        /// <param name="text">Search criteria</param>
        /// <returns>List of requirements</returns>
        public List<Domain.DTO.General.SearchResultDto> SearchRequirement(string text)
        {
            var requirements =
                _context.Requirement.Where(req => req.title.Contains(text) || req.description.Contains(text)).ToList();

            return requirements.Select(GetSearchResultDtoFromRequirementEntity).ToList();
        }

        public RequirementDto Get(long companyId, long projectId, long requirementId)
        {
            var requirement = GetEntity(companyId, requirementId);
            return requirement != null ? GetDtoFromEntity(requirement) : null;
        }

        public string GetTitle(long companyId, long projectId, long requirementId)
        {
            var requirement = GetEntity(companyId, requirementId);
            return requirement != null ? requirement.title : string.Empty;
        }

        public void Like(long companyId, long projectId, long requirementId, long personId)
        {
            using (var scope = _context.Database.BeginTransaction())
            {
                try
                {
                    // update like on requirement
                    var requirement = GetEntity(companyId, requirementId);
                    if (requirement == null) return;
                    requirement.agreed++;
                    _context.SaveChanges();

                    // update like on current requirement version
                    _requirementVersionData.Like(requirement.company_id, requirement.project_id, requirement.id,
                        requirement.requirement_version_id, personId);

                    scope.Commit();
                }
                catch
                {
                    scope.Rollback();
                    throw;
                }
            }
        }

        public void Dislike(long companyId, long projectId, long requirementId, long personId)
        {
            using (var scope = _context.Database.BeginTransaction())
            {
                try
                {
                    // update like on requirement
                    var requirement = GetEntity(companyId, requirementId);
                    if (requirement == null) return;
                    requirement.disagreed++;
                    _context.SaveChanges();

                    // update like on current requirement version
                    _requirementVersionData.Dislike(requirement.company_id, requirement.project_id, requirement.id,
                        requirement.requirement_version_id, personId);

                    scope.Commit();
                }
                catch
                {
                    scope.Rollback();
                    throw;
                }
            }
        }
        
        public List<RequirementDto> GetList(List<long> projectIds)
        {
            var requirements =
                _context.Requirement.Where(req => projectIds.Contains(req.project_id))
                    .OrderByDescending(reqDate => reqDate.modifiedon)
                    .ToList();
            return requirements.Select(GetDtoFromEntity).ToList();
        }

        public List<RequirementDto> GetList(List<long> projectIds, string hashtag)
        {
            var requirementsId = _requirementHashtagData.GetRequirementsId(hashtag);
            var requirements =
                _context.Requirement.Where(req => requirementsId.Contains(req.id) && projectIds.Contains(req.project_id))
                    .OrderByDescending(reqDate => reqDate.modifiedon)
                    .ToList();
            return requirements.Select(GetDtoFromEntity).ToList();
        }

        public void UpdateStatus(long companyId, long projectId, long requirementId, int statusId, long personId)
        {
            using (var scope = _context.Database.BeginTransaction())
            {
                try
                {
                    // get requirement and update it
                    var requirement = GetEntity(companyId, requirementId);
                    requirement.status_id = statusId;
                    requirement.modifiedby_id = personId;
                    requirement.modifiedon = DateTime.Now;
                    _context.SaveChanges();

                    // update requirement version
                    _requirementVersionData = new RequirementVersionData(_context);
                    _requirementVersionData.UpdateStatus(companyId, projectId, requirementId,
                        requirement.requirement_version_id, requirement.status_id, personId);

                    scope.Commit();
                }
                catch
                {
                    scope.Rollback();
                    throw;
                }
            }
        }

        public void Update(string title, string description, long newProjectId, long companyId, long projectId,
            long requirementId, int priorityId, long personId)
        {
            using (var scope = _context.Database.BeginTransaction())
            {
                try
                {
                    // get requirement and update it
                    var requirement = GetEntity(companyId, requirementId);
                    requirement.title = title;
                    requirement.description = description;
                    requirement.project_id = newProjectId;
                    requirement.priority_id = priorityId;
                    requirement.modifiedby_id = personId;
                    requirement.modifiedon = DateTime.Now;

                    // if requirement being updated was previously rejected, then set as draft
                    if (requirement.status_id == (int) GeneralCatalog.Detail.RequirementStatus.Rejected)
                    {
                        requirement.status_id = (int) GeneralCatalog.Detail.RequirementStatus.Draft;
                    }

                    _context.SaveChanges();

                    // update requirement version
                    _requirementVersionData = new RequirementVersionData(_context);
                    _requirementVersionData.Update(title, description, newProjectId, companyId, projectId, 
                        requirementId, requirement.requirement_version_id, priorityId, personId);

                    scope.Commit();
                }
                catch
                {
                    scope.Rollback();
                    throw;
                }
            }
        }

        public Context.Entities.Requirement GetEntity(long companyId, long requirementId)
        {
            return
                _context.Requirement.FirstOrDefault(
                    r => r.company_id == companyId && r.id == requirementId);
        }

        private static Context.Entities.Requirement GetEntityFromDto(RequirementDto requirementDto)
        {
            var requirement = new Context.Entities.Requirement
            {
                company_id =  requirementDto.CompanyId,
                project_id = requirementDto.ProjectId,
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
                requirement_version_id = requirementDto.VersionId,
                version_number = requirementDto.VersionNumber,
                priority_id = requirementDto.PriorityId
            };
            return requirement;
        }

        private RequirementDto GetDtoFromEntity(Context.Entities.Requirement requirement)
        {
            var requirementDto = new RequirementDto
            {
                Id = requirement.id,
                CompanyId = requirement.company_id,
                ProjectId = requirement.project_id,
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
                ShortDescription = StringUtilities.GetShort(requirement.description, MaxShortDescriptionLength),
                Project = requirement.Project != null ? requirement.Project.name : _projectData.GetTitle(requirement.project_id),
                Status = requirement.GeneralCatalogDetail != null ? requirement.GeneralCatalogDetail.name : _generalCatalogData.GetTitle(requirement.status_id),
                Priority = requirement.GeneralCatalogDetail1 != null ? requirement.GeneralCatalogDetail1.name : _generalCatalogData.GetTitle(requirement.priority_id),
                CreatedByName = requirement.Person != null ? StringUtilities.GetPersonFullName(requirement.Person) : _personData.GetFullName(requirement.createdby_id),
                ModifiedByName = requirement.Person1 != null ? StringUtilities.GetPersonFullName(requirement.Person1) : _personData.GetFullName(requirement.modifiedby_id),
                CommentsQuantity = _requirementCommentData.GetQuantity(requirement.id, requirement.company_id, 
                                        requirement.project_id, requirement.requirement_version_id)
            };
            
            return requirementDto;
        }

        private static Domain.DTO.General.SearchResultDto GetSearchResultDtoFromRequirementEntity(
            Context.Entities.Requirement requirement)
        {
            var searchResult = new Domain.DTO.General.SearchResultDto
            {
                Id = requirement.id,
                Title = requirement.title,
                Description = requirement.description.Length > MaxSearchResultDescription ? 
                              requirement.description.Substring(0, MaxSearchResultDescription) : requirement.description,
                Url = CommonConstants.FormsUrl.Requirement + "?" + 
                      CommonConstants.QueryStringParams.Id + "=" + requirement.id + "&" + 
                      CommonConstants.QueryStringParams.CompanyId + "=" + requirement.company_id + "&" + 
                      CommonConstants.QueryStringParams.ProjectId + "=" + requirement.project_id
            };

            return searchResult;
        }
    }
}
