namespace ResearchesUFU.API.Models
{
    public class Author : BaseEntity
    {
        public string UserType { get; set; } = Constants.USER_TYPE_EDITOR;

        public string Name { get; set; } = string.Empty;

        public string Birthdate { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string ProfilePictureUrl { get; set; } = string.Empty;

        public string CoverPhotoUrl { get; set; } = String.Empty;

        public string Bio { get; set; } = String.Empty;

        public int UserId { get; set; }

        public User User { get; set; } = null!;

        //public List<Research> AutoredResearches { get; set; } = new List<Research>();

        //public List<Research> Favs { get; set; } = new List<Research>();
    }
}
