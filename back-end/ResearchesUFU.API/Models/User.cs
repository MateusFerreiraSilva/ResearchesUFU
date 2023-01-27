using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResearchesUFU.API.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string? UserType { get; set; } = Constants.USER_TYPE_EDITOR;

        public string Name { get; set; } = null!;

        public string Birthdate { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string? ProfilePictureUrl { get; set; }

        public string? CoverPhotoUrl { get; set; }

        public string? Bio { get; set; }

        public List<Research> AutoredResearches { get; set; } = new List<Research>();

        //public List<Research> Favs { get; set; } = new List<Research>();

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [JsonIgnore]

        public DateTime LastUpdated { get; set; }
    }
}
