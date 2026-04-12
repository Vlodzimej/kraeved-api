using KraevedAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KraevedAPI.Service
{
    public partial interface IKraevedService
    {
        Task<GeoObject?> GetGeoObjectById(int id);
        Task<IEnumerable<GeoObjectBrief>> GetGeoObjectsByFilter(GeoObjectFilter filter);
        Task<GeoObject> InsertGeoObject(GeoObject geoObject, bool skipExistenceCheck = false);
        Task<GeoObject> DeleteGeoObject(int id);
        Task<GeoObject> UpdateGeoObject(GeoObject geoObject);
        Task<ImageInfo> AddImageToGeoObject(int geoObjectId, string filename, string? caption = null);
    }
}
