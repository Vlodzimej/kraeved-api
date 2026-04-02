namespace KraevedAPI.Models
{
    /// <summary>
    /// Географический объект сокращенный
    /// </summary>
    public class GeoObjectBrief
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
        /// Короткое описание
        /// </summary>
        public string ShortDescription { get; set; } = string.Empty;

        /// <summary>
        /// Системное имя типа
        /// </summary>
        public string? TypeName { get; set; }

        /// <summary>
        /// Идентификатор типа
        /// </summary>
        public int? TypeId { get; set; }

        /// <summary>
        /// Отображаемое название типа
        /// </summary>
        public string? TypeTitle { get; set; }

        /// <summary>
        /// Имя категории типа
        /// </summary>
        public string? TypeCategoryName { get; set; }

        /// <summary>
        /// Широта
        /// </summary> 
        public double? Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public double? Longitude { get; set; }

        /// <summary>
        /// Миниатюрное изображение
        /// </summary>
        /// <value></value>
        public string? Thumbnail { get; set; } 
    }
}
