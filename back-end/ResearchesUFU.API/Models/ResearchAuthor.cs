namespace ResearchesUFU.API.Models
{
    public class ResearchAuthor
    {
        public int ResearchId { get; set; }

        public Research Research { get; set; } = null!;

        public int AuthorId { get; set; }

        public Author Author { get; set; } = null!;
    }
}