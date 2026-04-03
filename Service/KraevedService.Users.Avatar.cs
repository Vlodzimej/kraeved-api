using KraevedAPI.Models;
using KraevedAPI.Constants;

namespace KraevedAPI.Service
{
    public partial class KraevedService : IKraevedService
    {
        public async Task<UserOutDto> UploadUserAvatar(IFormFile avatar)
        {
            if (avatar == null || avatar.Length == 0)
            {
                throw new Exception(ServiceConstants.Exception.FileIsEmpty);
            }

            var ext = Path.GetExtension(avatar.FileName);
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtension.Contains(ext))
            {
                throw new Exception(ServiceConstants.Exception.WrongExtension);
            }

            var user = GetCurrentUser();
            var rootFolder = Directory.GetCurrentDirectory();
            var avatarsPath = Path.Combine(rootFolder, "avatars");
            if (!Directory.Exists(avatarsPath)) Directory.CreateDirectory(avatarsPath);

            var uniqueString = Guid.NewGuid().ToString();
            var newFileName = uniqueString + ext;
            var filePath = Path.Combine(avatarsPath, newFileName);

            await using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await avatar.CopyToAsync(fileStream);
            }

            user.Avatar = newFileName;
            _unitOfWork.UsersRepository.Update(user);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<User, UserOutDto>(user);
        }
    }
}
