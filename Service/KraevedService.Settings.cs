using KraevedAPI.Models;

namespace KraevedAPI.Service
{
    public partial class KraevedService : IKraevedService
    {
        public async Task<IEnumerable<AppSetting>> GetAllSettings()
        {
            return _unitOfWork.AppSettingsRepository.Get().ToList();
        }

        public async Task<AppSetting?> GetSettingByKey(string key)
        {
            return _unitOfWork.AppSettingsRepository.Get(x => x.Key == key).FirstOrDefault();
        }

        public async Task<AppSetting> UpsertSetting(string key, string value, string? description = null)
        {
            var existing = _unitOfWork.AppSettingsRepository.Get(x => x.Key == key).FirstOrDefault();
            if (existing != null)
            {
                existing.Value = value;
                if (description != null) existing.Description = description;
                _unitOfWork.AppSettingsRepository.Update(existing);
            }
            else
            {
                var setting = new AppSetting { Key = key, Value = value, Description = description };
                _unitOfWork.AppSettingsRepository.Insert(setting);
                existing = setting;
            }
            await _unitOfWork.SaveAsync();
            return existing;
        }
    }
}
