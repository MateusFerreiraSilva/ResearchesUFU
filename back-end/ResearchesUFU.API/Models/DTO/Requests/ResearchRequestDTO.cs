namespace ResearchesUFU.API.Models.DTO.Requests
{
    public class ResearchRequestDTO
    {
        public string Title { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string Status { get; set; } = Constants.RESEARCH_STATUS_ONGOING;

        public string PublicationDate { get; set; } = string.Empty;

        public string Thumbnail { get; set; } = string.Empty;

        public List<ResearchFieldRequestDto> Fields { get; set; } = null!;

        public List<ResearchTagRequestDto> Tags { get; set; } = null!;

        public List<ResearchAuthorRequestDto> Authors { get; set; } = null!;
    }
}