namespace KraevedAPI.Models
{
    public class GeoObjectCategory
    {
        /// <summary>
        /// Идентификатор типа гео-объекта
        /// </summary>
        public int? Id { get; set; }
        /// <summary>
        /// Системное имя (латиница) 
        /// </summary>
        public required string Name { get; set; }
        /// <summary>
        /// Название
        /// </summary>
        public required string Title { get; set; }
    }
}