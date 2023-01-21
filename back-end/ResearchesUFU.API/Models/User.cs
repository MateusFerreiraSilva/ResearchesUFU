namespace ResearchesUFU.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
    
        public string Birthdate { get; set; }

        public string PhoneNumber { get; set; }

        public string ProfilePictureUrl { get; set; }

        public string CoverPhotoUrl { get; set; }

        public string Bio { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        //public List<Research> Favs { get; set; }
    }
}
