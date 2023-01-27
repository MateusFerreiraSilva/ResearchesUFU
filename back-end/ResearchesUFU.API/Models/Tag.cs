using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResearchesUFU.API.Models
{
    public class Tag
    {
        [Key]
        public string Name { get; set; } = null!;

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [JsonIgnore]
        public DateTime LastUpdated { get; set; }
    }
}
