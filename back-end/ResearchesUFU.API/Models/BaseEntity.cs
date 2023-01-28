using System.ComponentModel.DataAnnotations.Schema;

namespace ResearchesUFU.API.Models
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string LastUpdated { get; set; } = DateTime.UtcNow.ToString();
    }
}
