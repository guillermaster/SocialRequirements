namespace SocialRequirements.Domain.Exception.Account
{
    public class AccountException
    {
        public class WrongEmailFormat : System.Exception { }

        public class MissingRequiredField : System.Exception { }

        public class UserAlreadyExists : System.Exception { }

        public class UserNotFound : System.Exception { }
    }
}
