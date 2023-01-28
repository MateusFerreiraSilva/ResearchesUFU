namespace ResearchesUFU.API.Models.DTO.Responses
{
    public class UserAuthenticationResponseDTO
    {
        public int UserId { get; set; }

        public string Token { get; set; } = string.Empty;
    }
}
