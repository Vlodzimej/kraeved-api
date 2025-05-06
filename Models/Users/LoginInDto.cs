namespace KraevedAPI.Models
{

    /// <summary>
    /// Данные для входа в систему.
    /// </summary>
    public class LoginInDto 
    {
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Code { get; set; }
        public string? Password { get; set; }
    }
}
