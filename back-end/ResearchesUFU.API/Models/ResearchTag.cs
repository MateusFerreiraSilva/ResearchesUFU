namespace ResearchesUFU.API.Models
{
    public class ResearchTag
    {
        public int ResearchId { get; set; }

        public Research Research { get; set; } = null!;
        
        public int TagId { get; set; }
        
        public Tag Tag { get; set; } = null!;
    }
}