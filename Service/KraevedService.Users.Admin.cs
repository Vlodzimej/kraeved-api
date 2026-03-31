using KraevedAPI.ClassObjects;
using KraevedAPI.Constants;
using KraevedAPI.Models;

namespace KraevedAPI.Service
{
    public partial class KraevedService : IKraevedService
    {
        public async Task<IEnumerable<UserOutDto>> GetAllUsers()
        {
            var users = _unitOfWork.UsersRepository.Get(includeProperties: "Role").ToList();
            return users.Select(u => _mapper.Map<User, UserOutDto>(u));
        }

        public async Task<UserOutDto> GetUserById(int id)
        {
            var user = _unitOfWork.UsersRepository.Get(x => x.Id == id, includeProperties: "Role").FirstOrDefault()
                ?? throw new HttpResponseException(404, new { message = ServiceConstants.Exception.UserNotFound });
            
            return _mapper.Map<User, UserOutDto>(user);
        }

        public async Task DeleteUser(int id)
        {
            var user = _unitOfWork.UsersRepository.GetByID(id)
                ?? throw new HttpResponseException(404, new { message = ServiceConstants.Exception.UserNotFound });
            
            _unitOfWork.UsersRepository.Delete(id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<UserOutDto> UpdateUserRole(int id, string roleName)
        {
            var user = _unitOfWork.UsersRepository.Get(x => x.Id == id, includeProperties: "Role").FirstOrDefault()
                ?? throw new HttpResponseException(404, new { message = ServiceConstants.Exception.UserNotFound });
            
            var role = _unitOfWork.RolesRepository.GetRoleByName(roleName);
            if (role.Id == 0)
            {
                throw new HttpResponseException(404, new { message = "Role not found" });
            }
            
            user.RoleId = role.Id;
            _unitOfWork.UsersRepository.Update(user);
            await _unitOfWork.SaveAsync();
            
            return _mapper.Map<User, UserOutDto>(user);
        }
    }
}
