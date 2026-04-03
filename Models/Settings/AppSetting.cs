namespace KraevedAPI.Models
{
    /// <summary>
    /// Настройка сервиса (ключ-значение)
    /// </summary>
    public class AppSetting
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Ключ настройки (уникальный)
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Значение настройки
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// Описание настройки
        /// </summary>
        public string? Description { get; set; }
    }
}
