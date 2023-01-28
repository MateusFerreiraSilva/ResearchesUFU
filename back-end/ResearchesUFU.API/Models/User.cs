namespace ResearchesUFU.API.Models
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;
    }
}
