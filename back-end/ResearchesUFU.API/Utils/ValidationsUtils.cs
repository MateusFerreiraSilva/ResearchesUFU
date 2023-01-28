using System.Text.RegularExpressions;

namespace ResearchesUFU.API.Utils
{
    public static class ValidationsUtils
    {
        const string EMAIL_PATTERN_REGEX = @"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$";
        const int SHA256_STRING_LENGTH = 64;

        public static bool isValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            var match = Regex.Match(email, EMAIL_PATTERN_REGEX);

            return match.Success;
        }

        public static bool isValidPasswordHash(string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                return false;
            }

            return passwordHash.Length.Equals(SHA256_STRING_LENGTH);
        }
    }
}
