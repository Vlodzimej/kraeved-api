using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public GeoObject? GeoObject { get; set; }

        public int? PersonId { get; set; }
        public Person? Person { get; set; }
    }
}
