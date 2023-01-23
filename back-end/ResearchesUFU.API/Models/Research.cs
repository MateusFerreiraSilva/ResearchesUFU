using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResearchesUFU.API.Models
{
    public class Research
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        //public DateTime LastUpdated { get; set; }
    }
}
