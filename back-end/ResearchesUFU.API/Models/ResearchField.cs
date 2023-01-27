namespace ResearchesUFU.API.Models
{
    public class ResearchField
    {
        public int ResearchId { get; set; }

        public Research Research { get; set; } = null!;

        public int FieldId { get; set; }

        public Field Field { get; set; } = null!;
    }
}
