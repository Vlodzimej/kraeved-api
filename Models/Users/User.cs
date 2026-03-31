using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KraevedAPI.Models
{

    /// <summary>
    /// Пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        /// <value></value>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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
        /// Хэш пароля
        /// </summary>
        public required byte[] PasswordHash { get; set; }

        /// <summary>
        /// Соль пароля
        /// </summary>
        public required byte[] PasswordSalt { get; set; }

        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        /// <value></value>
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}