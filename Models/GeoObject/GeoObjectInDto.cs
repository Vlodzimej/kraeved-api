namespace KraevedAPI.Models
{
    public class GeoObjectInDto
    {
        /// <summary>
        /// Название
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Идентификатор типа локации
        /// </summary>
        /// <value></value>
        public required int TypeId { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Короткое описание
        /// </summary>
        public string ShortDescription { get; set; } = string.Empty;

        /// <summary>
        /// Широта
        /// </summary> 
        public double? Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Идентификатор региона
        /// </summary>
        public int? RegionId { get; set; }

        /// <summary>
        /// Список изображений
        /// </summary>
        /// <value></value>
        public List<ImageInfoDto>? Images { get; set; } 

        /// <summary>
        /// Миниатюрное изображение
        /// </summary>
        /// <value></value>
        public string? Thumbnail { get; set; }
    }

    public class ImageInfoDto
    {
        public int? Id { get; set; }
        public string Filename { get; set; } = string.Empty;
        public string? Caption { get; set; }
    }
}