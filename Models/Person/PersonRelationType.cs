namespace KraevedAPI.Models
{
    /// <summary>
    /// Тип родственной связи (справочник)
    /// </summary>
    public class PersonRelationType
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название связи (например: "Отец", "Мать", "Сын", "Дочь", "Брат", "Сестра")
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Системное имя (например: "father", "mother", "son", "daughter", "brother", "sister")
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Парный тип связи (например: для "Отец" парный — "Сын"/"Дочь")
        /// </summary>
        public int? PairedTypeId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public PersonRelationType? PairedType { get; set; }
    }
}
