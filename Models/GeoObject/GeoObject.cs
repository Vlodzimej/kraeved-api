namespace KraevedAPI.Models
{
    /// <summary>
    /// Географический объект
    /// </summary>
    public class GeoObject
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Тип локации
        /// </summary>
        /// <value></value>
        public GeoObjectType? Type { get; set; }
        public int? TypeId { get; set; }

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
        public List<ImageInfo>? Images { get; set; }

        /// <summary>
        /// Миниатюрное изображение
        /// </summary>
        /// <value></value>
        public string? Thumbnail { get; set; }

        /// <summary>
        /// Пользовательские поля (JSON)
        /// </summary>
        public string? CustomFields { get; set; }

        /// <summary>
        /// Родительский гео-объект
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// Родительский гео-объект
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public GeoObject? Parent { get; set; }

        /// <summary>
        /// Дочерние гео-объекты
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public List<GeoObject>? Children { get; set; }

        /// <summary>
        /// Связанные персоны
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public List<PersonGeoObject>? PersonGeoObjects { get; set; }

        /// <summary>
        /// Комментарии
        /// </summary>
        [System.Text.Json.Serialization.JsonIgnore]
        public List<Comment>? Comments { get; set; }
    }
}
