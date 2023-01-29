namespace ResearchesUFU.API.Models.DTO.Responses
{
    public class AuthorResponseDTO
    {
        public int Id { get; set; }
        public string UserType { get; set; } = Constants.USER_TYPE_EDITOR;

        public string Name { get; set; } = string.Empty;

        public string Birthdate { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string ProfilePictureUrl { get; set; } = string.Empty;

        public string CoverPhotoUrl { get; set; } = String.Empty;

        public string Bio { get; set; } = String.Empty;
    }
}
