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

        public List<FieldResponseDTO> Fields { get; set; } = null!;

        //public List<Tag> Tags { get; set; } = new List<Tag>();

        //public List<User> Authors { get; set; } = new List<User>();

        //public ResearchStructure Structure { get; set; } = null!;

        public string LastUpdated { get; set; } = string.Empty;
    }
}
