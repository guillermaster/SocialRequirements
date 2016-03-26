using System;
using System.Collections.Generic;
using SocialRequirements.Domain.BusinessLogic.Account;
using SocialRequirements.Domain.DTO.Account;
using SocialRequirements.Domain.Exception.Account;
using SocialRequirements.Domain.Repository.Account;
using SocialRequirements.Utilities;
using SocialRequirements.Utilities.Security;

namespace SocialRequirements.Business.Account
{
    public class PersonBusiness : IPersonBusiness
    {
        private readonly IPersonData _personData;

        public PersonBusiness(IPersonData personData)
        {
            _personData = personData;
        }

        public void Add(string firstName, string lastName, string birthdate, string primaryEmail, string secondaryEmail, string phone,
            string mobilePhone, string password)
        {
            var username = primaryEmail;

            // check first name
            if (string.IsNullOrWhiteSpace(firstName)) throw new AccountException.MissingRequiredField();

            // check last name
            if (string.IsNullOrWhiteSpace(lastName)) throw new AccountException.MissingRequiredField();

            // check birthdate
            if (string.IsNullOrWhiteSpace(birthdate)) throw new AccountException.MissingRequiredField();

            // check birthdate
            if (string.IsNullOrWhiteSpace(primaryEmail)) throw new AccountException.MissingRequiredField();

            // check password
            if (string.IsNullOrWhiteSpace(password)) throw new AccountException.MissingRequiredField();

            // check primary email
            if (!EmailUtilities.IsValidEmail(primaryEmail)) throw new AccountException.WrongEmailFormat();

            // check secondary email
            if (!string.IsNullOrWhiteSpace(secondaryEmail) && !EmailUtilities.IsValidEmail(secondaryEmail))
                throw new AccountException.WrongEmailFormat();

            // check if user already exists
            if (UserExists(primaryEmail)) throw new AccountException.UserAlreadyExists();

            // hash password
            var hashedPassword = PasswordHash.CreateHash(password);

            // add new person record
            _personData.Add(firstName, lastName, DateTime.Parse(birthdate), primaryEmail, secondaryEmail, phone,
                mobilePhone, username, hashedPassword);
        }

        public bool UserExists(string email)
        {
            return _personData.UserExists(email);
        }

        public bool ValidatePassword(string username, string password)
        {
            try
            {
                var hashedPasswordInDb = _personData.GetPassword(username);
                return PasswordHash.ValidatePassword(password, hashedPasswordInDb);
            }
            catch (AccountException.UserNotFound)
            {
                return false;
            }
        }

        public void SetPassword(string username, string password)
        {
            _personData.SetPassword(username, PasswordHash.CreateHash(password));
        }

        public PersonDto Get(string username)
        {
            var personId = _personData.GetPersonId(username);
            return _personData.Get(personId);
        }

        public List<ProjectPermissionsDto> GetPermissionsInProjects(string username)
        {
            var personId = _personData.GetPersonId(username);
            return _personData.GetPermissionsInProjects(personId);
        }
    }
}
