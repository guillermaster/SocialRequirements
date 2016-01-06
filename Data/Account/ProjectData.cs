using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.Repository.Account;

namespace SocialRequirements.Data.Account
{
    public class ProjectData : IProjectData
    {
        private readonly ContextModel _context;

        public ProjectData(ContextModel context)
        {
            _context = context;
        }

        public List<ProjectDto> GetProjectsByCompany(long companyId)
        {
            var projects = _context.CompanyProject.Where(cp => cp.company_id == companyId).ToList();
            return projects.Select(GetProjectDto).ToList();
        }

        public List<ProjectDto> GetProjectsByUser(long personId)
        {
            var projectsQry = from cu in _context.CompanyPerson
                              join cp in _context.CompanyProject on cu.company_id equals cp.company_id
                              join p in _context.Project on cp.project_id equals p.id
                              where cu.person_id == personId
                              select new ProjectDto
                              {
                                  Id = p.id,
                                  Name = p.name,
                                  Description = p.description
                              };

            return projectsQry.ToList();
        }

        public List<ProjectDto> GetUnrelatedProjects(long companyId)
        {
            var compProjects = _context.CompanyProject.Where(compProj => compProj.company_id != companyId).ToList();
            var unrelatedProject = new List<ProjectDto>();

            foreach (var project in compProjects)
            {
                if (_context.CompanyProject.Count(compProj => compProj.company_id == companyId) == 0)
                {
                    unrelatedProject.Add(GetProjectDto(project));
                }
            }

            return unrelatedProject;
        }

        public int GetNumberOfProjects(long companyId)
        {
            var numProject = _context.CompanyProject.Count(c => c.company_id == companyId);
            return numProject;
        }

        public long Add(string name, string description, long companyId, long personId)
        {
            using (var scope = _context.Database.BeginTransaction())
            {
                try
                {
                    // add new project
                    var projectId = AddProject(name, description);
                    // relate company to user
                    AddCompanyRelationship(companyId, projectId);
                    scope.Commit();

                    return projectId;
                }
                catch
                {
                    scope.Rollback();
                    return -1;
                }
            }
        }

        /// <summary>
        /// Inserts a new project record in the DB
        /// </summary>
        /// <param name="name">Project name</param>
        /// <param name="description">Project description</param>
        /// <returns>ID of newly added project</returns>
        private long AddProject(string name, string description)
        {
            var project = new Project
            {
                name = name,
                description = description
            };
            project = _context.Project.Add(project);
            _context.SaveChanges();
            return project.id;
        }

        /// <summary>
        /// Creates a relationship between a company and a project
        /// </summary>
        /// <param name="companyId">Company identifier</param>
        /// <param name="projectId">Project identifier</param>
        public long AddCompanyRelationship(long companyId, long projectId)
        {
            var projectCompany = new CompanyProject
            {
                company_id = companyId,
                project_id = projectId
            };
            projectCompany = _context.CompanyProject.Add(projectCompany);
            _context.SaveChanges();
            return projectCompany.id;
        }

        private static ProjectDto GetProjectDto(CompanyProject companyProject)
        {
            var project = new ProjectDto
            {
                Id = companyProject.project_id,
                Name = companyProject.Project.name,
                Description = companyProject.Project.description
            };
            return project;
        }
    }
}
