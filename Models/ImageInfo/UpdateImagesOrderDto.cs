using System.Text.Json.Serialization;

namespace KraevedAPI.Models
{
    public class UpdateImagesOrderDto
    {
        [JsonPropertyName("imageIds")]
        public List<int> ImageIds { get; set; } = new();
    }
}
