using System;
using SocialRequirements.Context.Entities;

namespace SocialRequirements.Domain.DTO.Account
{
    public class PersonDto
    {
        public PersonDto()
        {
            
        }

        public PersonDto(Person personEntityItem)
        {
            Id = personEntityItem.id;
            FirstName = personEntityItem.first_name;
            LastName = personEntityItem.last_name;
            BirthDate = personEntityItem.birth_date;
            PrimaryEmail = personEntityItem.primary_email;
            SecondaryEmail = personEntityItem.secondary_email;
            Phone = personEntityItem.phone;
            MobilePhone = personEntityItem.mobile_phone;
            UserName = personEntityItem.user_name;
        }

        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public string PrimaryEmail { get; set; }

        public string SecondaryEmail { get; set; }

        public string Phone { get; set; }

        public string MobilePhone { get; set; }

        public string UserName { get; set; }
    }
}
