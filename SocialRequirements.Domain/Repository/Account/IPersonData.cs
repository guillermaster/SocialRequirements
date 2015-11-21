using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialRequirements.Domain.Repository.Account
{
    public interface IPersonData
    {
        void Add(string firstName, string lastName, DateTime birthdate, string primaryEmail, string secondaryEmail, string phone, 
            string mobilePhone, string username, string password);

    }
}
