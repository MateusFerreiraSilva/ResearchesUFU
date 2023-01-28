namespace ResearchesUFU.API.Models.DTO.Requests
{
    public class UserAuthenticationRequestDTO
    {
        public string Email { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;
    }
}
