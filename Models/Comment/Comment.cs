namespace KraevedAPI.Models
{
    /// <summary>
    /// Комментарий к гео-объекту
    /// </summary>
    public class Comment
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор гео-объекта
        /// </summary>
        public int GeoObjectId { get; set; }

        public GeoObject? GeoObject { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int UserId { get; set; }

        public User? User { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
