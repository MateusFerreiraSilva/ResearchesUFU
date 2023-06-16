namespace ResearchesUFU.API.Models.DTO.Requests
{
    public class UserAuthenticationRequestDto
    {
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
    }
}
