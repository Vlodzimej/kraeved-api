using KraevedAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KraevedAPI.Service
{
    public partial interface IKraevedService
    {
        Task<GeoObject?> getGeoObjectById(int id);
        Task<IEnumerable<GeoObjectBrief>> getGeoObjectsByFilter(GeoObjectFilter filter);
        Task<GeoObject?> insertGeoObject(GeoObject geoObject);
        Task<GeoObject?> deleteGeoObject(int id);
        Task<GeoObject?> updateGeoObject(GeoObject geoObject);
    }
}
