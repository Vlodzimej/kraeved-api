using KraevedAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace KraevedAPI.Service
{
    public partial interface IKraevedService
    {
        Task<GeoObject?> getGeoObjectById(int id);
        Task insertGeoObject(GeoObject geoObject);
    }
}
