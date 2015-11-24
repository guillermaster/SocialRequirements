using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialRequirements.Utilities.ResponseCodes.Account
{
    public class PersonResponse
    {
        public enum PersonRegistration
        {
            Success,
            WrongEmailFormat,
            MissingRequiredFields,
            UserAlreadyExists,
            UnknownError
        }
    }
}
