using System;
using SocialRequirements.Domain.BusinessLogic.Account;
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
            if (string.IsNullOrWhiteSpace(firstName)) throw new SocialRequirementsExcepction.MissingRequiredField();

            // check last name
            if (string.IsNullOrWhiteSpace(lastName)) throw new SocialRequirementsExcepction.MissingRequiredField();

            // check birthdate
            if (string.IsNullOrWhiteSpace(birthdate)) throw new SocialRequirementsExcepction.MissingRequiredField();

            // check birthdate
            if (string.IsNullOrWhiteSpace(primaryEmail)) throw new SocialRequirementsExcepction.MissingRequiredField();

            // check password
            if (string.IsNullOrWhiteSpace(password)) throw new SocialRequirementsExcepction.MissingRequiredField();

            // check primary email
            if (!EmailUtilities.IsValidEmail(primaryEmail)) throw new SocialRequirementsExcepction.WrongEmailFormat();

            // check secondary email
            if (!string.IsNullOrWhiteSpace(secondaryEmail) && !EmailUtilities.IsValidEmail(secondaryEmail))
                throw new SocialRequirementsExcepction.WrongEmailFormat();

            // check if user already exists
            if (UserExists(primaryEmail)) throw new SocialRequirementsExcepction.UserAlreadyExists();

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
            var hashedPasswordInDb = _personData.GetPassword(username);
            return PasswordHash.ValidatePassword(password, hashedPasswordInDb);
        }

        public class SocialRequirementsExcepction
        {
            public class WrongEmailFormat : Exception { }

            public class MissingRequiredField : Exception { }

            public class UserAlreadyExists : Exception { }
        }
    }
}
