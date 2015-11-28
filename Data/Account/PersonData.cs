using System;
using System.Linq;
using DataContext;
using DataContext.Entities;
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

        public long GetPersonId(string username)
        {
            var user = _context.Person.FirstOrDefault(p => p.user_name == username);
            if (user == null) throw new AccountException.UserNotFound();

            return user.id;
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