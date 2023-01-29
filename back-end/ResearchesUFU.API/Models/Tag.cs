namespace ResearchesUFU.API.Models
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public List<ResearchTag> ResearchTag { get; set; } = null!;
    }
}
