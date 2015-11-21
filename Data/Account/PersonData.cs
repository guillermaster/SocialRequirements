using System;
using SocialRequirements.Domain.Repository.Account;

namespace Data.Account
{
    public class PersonData : IPersonData
    {
        public void Add(string firstName, string lastName, DateTime birthdate, string primaryEmail, string secondaryEmail, string phone,
            string mobilePhone, string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}