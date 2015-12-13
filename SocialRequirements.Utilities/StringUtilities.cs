namespace SocialRequirements.Utilities
{
    public class StringUtilities
    {
        public static string GetShort(string text, int maxlength)
        {
            return text.Length <= maxlength ? text : text.Substring(0, maxlength - 1) + "...";
        }
    }
}
