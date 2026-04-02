namespace KraevedAPI.Models
{
    /// <summary>
    /// Персоналия (историческая личность)
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Отчество
        /// </summary>
        public string? Patronymic { get; set; }

        /// <summary>
        /// Биография
        /// </summary>
        public string? Biography { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Дата смерти
        /// </summary>
        public DateTime? DeathDate { get; set; }

        /// <summary>
        /// Список фотографий (имена файлов)
        /// </summary>
        public List<string>? Photos { get; set; }

        /// <summary>
        /// Связанные гео-объекты
        /// </summary>
        public List<PersonGeoObject>? PersonGeoObjects { get; set; }
    }
}
