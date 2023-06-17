namespace ResearchesUFU.API.Models.DTO.Responses
{
    public class UserAuthenticationResponseDto
    {
        public int UserId { get; set; }

        public string Token { get; set; } = string.Empty;
    }
}
