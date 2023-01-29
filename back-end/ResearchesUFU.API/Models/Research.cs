namespace ResearchesUFU.API.Models
{
    public class Research : BaseEntity
    {
        public string Title { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string Status { get; set; } = Constants.RESEARCH_STATUS_ONGOING;

        public string PublicationDate { get; set; } = string.Empty;

        public string Thumbnail { get; set; } = string.Empty;

        public string ResearchStructure { get; set; } = string.Empty;

        public List<ResearchField> ResearchField { get; set; } = null!;

        public List<ResearchTag> ResearchTag { get; set; } = null!;

        public List<ResearchAuthor> ResearchAuthor { get; set; } = null!;
    }
}
