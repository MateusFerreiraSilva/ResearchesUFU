namespace ResearchesUFU.API.Models.DTO.Responses
{
    public class ResearchResponseDTO
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Summary { get; set; } = string.Empty;

        public string Status { get; set; } = Constants.RESEARCH_STATUS_ONGOING;

        public string PublicationDate { get; set; } = string.Empty;

        public string Thumbnail { get; set; } = string.Empty;

        public string ResearchStructure { get; set; } = string.Empty;

        public List<FieldResponseDTO> Fields { get; set; } = new List<FieldResponseDTO>();

        public List<TagResponseDTO> Tags { get; set; } = new List<TagResponseDTO>();

        public List<AuthorResponseDTO> Authors { get; set; } = new List<AuthorResponseDTO>();

        public string LastUpdated { get; set; } = string.Empty;
    }
}
