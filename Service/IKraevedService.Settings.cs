using KraevedAPI.Models;

namespace KraevedAPI.Service
{
    public partial interface IKraevedService
    {
        Task<IEnumerable<AppSetting>> GetAllSettings();
        Task<AppSetting?> GetSettingByKey(string key);
        Task<AppSetting> UpsertSetting(string key, string value, string? description = null);
    }
}
