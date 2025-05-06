using KraevedAPI.Models;

namespace KraevedAPI.Service
{
    public partial interface IKraevedService
    {
        Task<UserOutDto> Register(string? email, string? password);
        Task<Boolean> SendSms(string phone);
        Task<LoginOutDto> Login(LoginInDto loginDto);
    }
}
