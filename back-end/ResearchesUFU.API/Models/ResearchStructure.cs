using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResearchesUFU.API.Models
{
    public class ResearchStructure
    {
        [Key]
        public int Id { get; set; }

        public string HtmlContent { get; set; } = string.Empty;


        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [JsonIgnore]
        public DateTime LastUpdated { get; set; }
    }
}
