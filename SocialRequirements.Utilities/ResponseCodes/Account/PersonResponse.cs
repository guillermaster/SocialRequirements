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
