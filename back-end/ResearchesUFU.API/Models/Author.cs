namespace ResearchesUFU.API.Models
{
    public class Author : User
    {
        public List<Research> Works { get; set; }

    }
}
