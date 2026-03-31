using KraevedAPI.Models;

namespace KraevedAPI.Service
{
    public partial interface IKraevedService : IDisposable
    {
        Task<IEnumerable<UserOutDto>> GetAllUsers();
        Task<UserOutDto> GetUserById(int id);
        Task DeleteUser(int id);
        Task<UserOutDto> UpdateUserRole(int id, string roleName);
    }
}
