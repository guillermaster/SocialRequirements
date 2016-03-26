using System.Collections.Generic;
using SocialRequirements.Domain.DTO.Account;

namespace SocialRequirements.Domain.BusinessLogic.Account
{
    public interface IPersonBusiness
    {
        void Add(string firstName, string lastName, string birthdate, string primaryEmail, string secondaryEmail, string phone,
            string mobilePhone, string password);

        bool UserExists(string email);

        bool ValidatePassword(string username, string password);

        void SetPassword(string username, string password);

        PersonDto Get(string username);

        List<ProjectPermissionsDto> GetPermissionsInProjects(string username);
    }
}
