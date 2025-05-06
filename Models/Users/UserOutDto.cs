namespace KraevedAPI.Models
{

    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserOutDto
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        /// <value></value>
        public int Id { get; set; }

        /// <summary>
        /// Телефонный номер пользователя
        /// </summary>
        public required string Phone { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        /// <value></value>
        public required string Email { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary> 
        public required string Surname { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public required string Role { get; set; }
    }
}