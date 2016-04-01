using System;
using System.Collections.Generic;
using System.Linq;
using SocialRequirements.Context;
using SocialRequirements.Context.Entities;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.Exception.Account;
using SocialRequirements.Domain.Repository.Account;

namespace SocialRequirements.Data.Account
{
    public class PersonData : IPersonData
    {
        private readonly ContextModel _context;

        public PersonData(ContextModel context)
        {
            _context = context;
        }

        public void Add(string firstName, string lastName, DateTime birthdate, string primaryEmail, string secondaryEmail, string phone,
            string mobilePhone, string username, string password)
        {
            var person = CreatePersonEntityInstance(firstName, lastName, birthdate, primaryEmail, secondaryEmail, phone,
                mobilePhone, username, password);

            _context.Person.Add(person);
            _context.SaveChanges();
        }

        public bool UserExists(string email)
        {
            var numberOfUsers = _context.Person.Count(p => p.user_name == email || p.primary_email == email);
            return numberOfUsers > 0;
        }

        public string GetPassword(string username)
        {
            var user = _context.Person.FirstOrDefault(p => p.user_name == username);
            if (user == null) throw new AccountException.UserNotFound();

            return user.password;
        }

        public void SetPassword(string username, string password)
        {
            var user = _context.Person.FirstOrDefault(p => p.user_name == username);
            if (user == null) throw new AccountException.UserNotFound();

            user.password = password;
            _context.SaveChanges();
        }

        public long GetPersonId(string username)
        {
            var user = _context.Person.FirstOrDefault(p => p.user_name == username);
            if (user == null) throw new AccountException.UserNotFound();

            return user.id;
        }

        public PersonDto Get(long personId)
        {
            var user = _context.Person.FirstOrDefault(person => person.id == personId);
            if (user == null) throw new AccountException.UserNotFound();

            return new PersonDto(user);
        }

        public string GetFullName(long personId)
        {
            var user = _context.Person.FirstOrDefault(person => person.id == personId);
            return user != null ? user.first_name + " " + user.last_name : string.Empty;
        }

        public string GetName(long personId)
        {
            var user = _context.Person.FirstOrDefault(person => person.id == personId);
            return user != null ? user.first_name : string.Empty;
        }

        public string GetLastname(long personId)
        {
            var user = _context.Person.FirstOrDefault(person => person.id == personId);
            return user != null ? user.last_name : string.Empty;
        }

        public List<ProjectPermissionsDto> GetPermissionsInProjects(long personId)
        {
            var permissions = from projectRole in _context.CompanyProjectPersonRole
                              join role in _context.Role on projectRole.role_id equals role.id
                              where projectRole.person_id == personId
                              select new ProjectPermissionsDto
                              {
                                  ProjectId = projectRole.project_id,
                                  PermissionsIds = role.Permission.Select(perm => perm.id).ToList()
                              };

            return permissions.ToList();
        }

        public List<PersonDto> GetUsersByCompany(long companyId)
        {
            var companiesPersons = _context.CompanyPerson.Where(cp => cp.company_id == companyId).ToList();
            return companiesPersons.Select(cp => cp.Person).ToList().Select(GetDtoFromEntity).ToList();
        }

        public List<PersonDto> GetUsersByProjectRole(long projectId, long roleId)
        {
            var projectsRoles =
                _context.CompanyProjectPersonRole.Where(cppr => cppr.project_id == projectId && cppr.role_id == roleId)
                    .ToList();
            return projectsRoles.Select(pr => pr.Person).ToList().Select(GetDtoFromEntity).ToList();
        }

        public static PersonDto GetDtoFromEntity(Person person)
        {
            var personDto = new PersonDto
            {
                Id = person.id,
                FirstName = person.first_name,
                LastName = person.last_name,
                BirthDate = person.birth_date,
                MobilePhone = person.mobile_phone,
                Phone = person.phone,
                PrimaryEmail = person.primary_email,
                SecondaryEmail = person.secondary_email,
                UserName = person.user_name
            };
            return personDto;
        }


        private static Person CreatePersonEntityInstance(string firstName, string lastName, DateTime birthdate, string primaryEmail, string secondaryEmail,
            string phone, string mobilePhone, string username, string password)
        {
            var person = new Person
            {
                first_name = firstName,
                last_name = lastName,
                birth_date = birthdate,
                primary_email = primaryEmail,
                secondary_email = secondaryEmail,
                phone = phone,
                mobile_phone = mobilePhone,
                user_name = username,
                password = password
            };
            return person;
        }
        
    }
}