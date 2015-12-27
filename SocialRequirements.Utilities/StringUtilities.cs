using SocialRequirements.Context.Entities;

namespace SocialRequirements.Utilities
{
    public class StringUtilities
    {
        public static string GetShort(string text, int maxlength)
        {
            return text.Length <= maxlength ? text : text.Substring(0, maxlength - 1) + "...";
        }

        public static string GetPersonFullName(Person person)
        {
            return person != null ? person.first_name + " " + person.last_name : string.Empty;
        }
    }
}
