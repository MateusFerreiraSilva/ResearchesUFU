using System.Text.RegularExpressions;

namespace ResearchesUFU.API.Utils
{
    public static class ValidationsUtils
    {
        private const string EmailPatternRegex = @"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$";
        private const int Sha256StringLength = 64;

        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            var match = Regex.Match(email, EmailPatternRegex);

            return match.Success;
        }

        public static bool IsValidPasswordHash(string passwordHash)
        {
            return !string.IsNullOrWhiteSpace(passwordHash) && passwordHash.Length.Equals(Sha256StringLength);
        }
    }
}
