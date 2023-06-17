namespace ResearchesUFU.API.Models.DTO.Responses
{
    public class ResearchResponseDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string Status { get; set; } = Constants.RESEARCH_STATUS_ONGOING;

        public string PublicationDate { get; set; } = string.Empty;

        public string Thumbnail { get; set; } = string.Empty;

        public string ResearchStructure { get; set; } = string.Empty;
        public List<ResearchFieldResponseDto>? Fields { get; set; }

        public List<ResearchTagResponseDto>? Tags { get; set; }

        public List<ResearchAuthorResponseDto>? Authors { get; set; }

        public string LastUpdated { get; set; } = string.Empty;
    }
}
