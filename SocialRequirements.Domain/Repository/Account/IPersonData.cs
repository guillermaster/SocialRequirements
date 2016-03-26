using System;
using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Account;

namespace SocialRequirements.Domain.Repository.Account
{
    public interface IPersonData
    {
        void Add(string firstName, string lastName, DateTime birthdate, string primaryEmail, string secondaryEmail, string phone, 
            string mobilePhone, string username, string password);

        bool UserExists(string email);

        string GetPassword(string username);

        void SetPassword(string username, string password);

        long GetPersonId(string username);

        PersonDto Get(long personId);

        string GetFullName(long personId);

        string GetName(long personId);

        string GetLastname(long personId);

        List<ProjectPermissionsDto> GetPermissionsInProjects(long personId);
    }
}
