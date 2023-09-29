using KraevedAPI.Models;

namespace KraevedAPI.Service
{
    public partial class KraevedService : IKraevedService
    {
        public async Task<GeoObject?> getGeoObjectById(int id)
        {
            var geoObject = _unitOfWork.GeoObjectsRepository.GetByID(id);
            return geoObject;
        }

        public async Task insertGeoObject(GeoObject geoObject)
        {
            _unitOfWork.GeoObjectsRepository.Insert(geoObject);
            await _unitOfWork.SaveAsync();
        }
    }
}
