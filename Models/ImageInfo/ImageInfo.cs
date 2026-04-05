using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KraevedAPI.Models
{
    public class ImageInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Filename { get; set; } = string.Empty;
        public string? Caption { get; set; }

        public int? GeoObjectId { get; set; }
        [JsonIgnore]
        public GeoObject? GeoObject { get; set; }

        public int? PersonId { get; set; }
        [JsonIgnore]
        public Person? Person { get; set; }
    }
}
