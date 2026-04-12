using System.Text.Json.Serialization;

namespace KraevedAPI.Models
{
    public class AddImageDto
    {
        [JsonPropertyName("filename")]
        public string Filename { get; set; } = string.Empty;
        
        [JsonPropertyName("caption")]
        public string? Caption { get; set; }
    }
}
