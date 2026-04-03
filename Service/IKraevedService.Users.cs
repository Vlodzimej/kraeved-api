using KraevedAPI.Models;

namespace KraevedAPI.Service
{
    public partial interface IKraevedService
    {
        Task<UserOutDto> GetCurrentUserInfo();
        Task<UserOutDto> PatchUser(UserInDto userInDto);
        Task<UserOutDto> UploadUserAvatar(IFormFile avatar);
    }
}
