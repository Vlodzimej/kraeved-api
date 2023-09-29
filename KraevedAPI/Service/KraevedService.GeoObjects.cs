using KraevedAPI.Models;

namespace KraevedAPI.Service
{
    public partial class KraevedService : IKraevedService
    {
        /// <summary>
        /// Получение гео-объекта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор гео-объекта</param>
        /// <returns></returns>
        public async Task<GeoObject?> getGeoObjectById(int id)
        {
            var geoObject = _unitOfWork.GeoObjectsRepository.GetByID(id);

            return geoObject;
        }

        /// <summary>
        /// Получение списка гео-объектов по идентификатору региона
        /// </summary>
        /// <param name="regionId">Идентификатор региона</param>
        /// <returns></returns>
        public async Task<IEnumerable<GeoObjectBrief>> getGeoObjectsByRegionId(int regionId)
        {
            var geoObjects = _unitOfWork.GeoObjectsRepository
                .Get(x => (regionId == x.RegionId), x => x.OrderBy(item => item.Name))
                .Select(geoObject => _mapper.Map<GeoObjectBrief>(geoObject));

            return geoObjects;
        }

        /// <summary>
        /// Добавление гео-объекта в БД
        /// </summary>
        /// <param name="geoObject"></param>
        /// <returns></returns>
        public async Task insertGeoObject(GeoObject geoObject)
        {
            _unitOfWork.GeoObjectsRepository.Insert(geoObject);

            await _unitOfWork.SaveAsync();
        }
    }
}
