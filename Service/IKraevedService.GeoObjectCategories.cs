using KraevedAPI.Models;

namespace KraevedAPI.Service
{
    public partial interface IKraevedService
    {
        Task<GeoObjectCategory> GetGeoObjectCategoryById(int id);
        Task<IEnumerable<GeoObjectCategory>> GetAllGeoObjectCategories();
        Task<GeoObjectCategory> InsertGeoObjectCategory(GeoObjectCategory geoObjectCategory);
        Task<GeoObjectCategory> DeleteGeoObjectCategory(int id);
        Task<GeoObjectCategory> UpdateGeoObjectCategory(GeoObjectCategory geoObjectCategory);
    }
}
