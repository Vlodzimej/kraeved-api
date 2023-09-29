using System.Drawing;

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
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Широта
        /// </summary>
        /// 
        public double Latitude { get; set; }
        /// <summary>
        /// Долгота
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// Идентификатор региона
        /// </summary>
        public int RegionId  { get; set; }
    }
}
