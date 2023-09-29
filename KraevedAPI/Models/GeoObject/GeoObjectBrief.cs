using System.Drawing;

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
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Широта
        /// </summary>
        /// 
        public double Latitude { get; set; }

        /// <summary>
        /// Долгота
        /// </summary>
        public double Longitude { get; set; }
    }
}
