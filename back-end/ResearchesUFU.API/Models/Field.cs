using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchesUFU.API.Models
{
    public class Field
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Acronym { get; set; } = string.Empty;

        public List<ResearchField> ResearchField { get; set; } = null!;
    }
}

