using KraevedAPI.Constants;
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
        public async Task<GeoObject?> GetGeoObjectById(int id)
        {
            var filter = new GeoObjectFilter() { Id = id };
            var result = _unitOfWork.GeoObjectsRepository
                .Get(x => filter.Id == null || x.Id == filter.Id, includeProperties: "Type,Type.Category,Parent,Parent.Type,Children.Type,PersonGeoObjects.Person,Images")
                ?? throw new Exception(ServiceConstants.Exception.UnknownError);

            var geoObject = result.FirstOrDefault();

            if (geoObject != null && geoObject.Images == null)
            {
                geoObject.Images = new List<ImageInfo>();
            }

            return geoObject;
        }

        /// <summary>
        /// Получение списка гео-объектов по фильтру
        /// </summary>
        /// <param name="filter">Фильтр гео-объекта</param>
        /// <returns></returns>
        public Task<IEnumerable<GeoObjectBrief>> GetGeoObjectsByFilter(GeoObjectFilter filter)
        {
            //TODO: Поправить поиск по имени с приведением в нижний регистр
            var result = _unitOfWork.GeoObjectsRepository
                .Get(x =>
                    (filter.Name == null || x.Name.ToLower().Contains(filter.Name.ToLower())) &&
                    (filter.RegionId == null || (filter.RegionId == x.RegionId)),
                    x => x.OrderBy(item => item.Name), includeProperties: "Type,Type.Category")
                .Select(_mapper.Map<GeoObjectBrief>) ??
                    throw new Exception(ServiceConstants.Exception.UnknownError);

            return Task.FromResult(result);
        }

        /// <summary>
        /// Добавление гео-объекта в БД
        /// </summary>
        /// <param name="geoObject"></param>
        /// <returns></returns>
        public async Task<GeoObject> InsertGeoObject(GeoObject geoObject)
        {
            if (geoObject.TypeId == null)
            {
                throw new Exception(ServiceConstants.Exception.GeoObjectTypeIsNull);
            }

            Validate(geoObject);

            var filter = new GeoObjectFilter() { Name = geoObject.Name, RegionId = geoObject.RegionId };
            var existedGeoObjectList = await GetGeoObjectsByFilter(filter);
            if (existedGeoObjectList.FirstOrDefault() != null)
            {
                throw new Exception(ServiceConstants.Exception.ObjectExists);
            }

            var type = _unitOfWork.GeoObjectTypesRepository.GetByID(geoObject.TypeId);

            if (type == null)
            {
                throw new Exception(ServiceConstants.Exception.GeoObjectTypeNotFound);
            }

            var newGeoObject = new GeoObject()
            {
                Name = geoObject.Name,
                Type = type,
                Description = geoObject.Description,
                ShortDescription = geoObject.ShortDescription,
                CustomFields = geoObject.CustomFields,
                ParentId = geoObject.ParentId,
            };

            _unitOfWork.GeoObjectsRepository.Insert(geoObject);
            await _unitOfWork.SaveAsync();

            var insertedGeoObject = _unitOfWork.GeoObjectsRepository
                .Get(x =>
                    (x.Name == geoObject.Name) &&
                    (x.Latitude == geoObject.Latitude) &&
                    (x.Longitude == geoObject.Longitude) &&
                    (x.Description == geoObject.Description))
                .FirstOrDefault() ?? throw new Exception(ServiceConstants.Exception.CreatedObjectNotFound);

            return insertedGeoObject;
        }

        /// <summary>
        /// Удаление гео-объекта по идентификатору
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<GeoObject> DeleteGeoObject(int id)
        {
            var geoObject = _unitOfWork.GeoObjectsRepository.Get(x => id == x.Id).FirstOrDefault() ??
                throw new Exception(ServiceConstants.Exception.NotFound);

            _unitOfWork.GeoObjectsRepository.Delete(id);
            await _unitOfWork.SaveAsync();

            return geoObject;
        }

        /// <summary>
        /// Изменение гео-объекта
        /// </summary>
        /// <param name="geoObject"></param>
        /// <returns></returns>
        public async Task<GeoObject> UpdateGeoObject(GeoObject geoObject)
        {
            var existingGeoObject = _unitOfWork.GeoObjectsRepository.Get(x => geoObject.Id == x.Id).FirstOrDefault() ??
                throw new Exception(ServiceConstants.Exception.NotFound);
            Validate(existingGeoObject);

            var type = _unitOfWork.GeoObjectTypesRepository.Get(x => geoObject.TypeId == x.Id).FirstOrDefault();

            existingGeoObject.Name = geoObject.Name;
            existingGeoObject.ShortDescription = geoObject.ShortDescription;
            existingGeoObject.Description = geoObject.Description;
            existingGeoObject.Longitude = geoObject.Longitude;
            existingGeoObject.Latitude = geoObject.Latitude;
            existingGeoObject.RegionId = geoObject.RegionId;
            existingGeoObject.Type = type;
            existingGeoObject.Thumbnail = geoObject.Thumbnail;
            existingGeoObject.Images = geoObject.Images;
            existingGeoObject.CustomFields = geoObject.CustomFields;
            existingGeoObject.ParentId = geoObject.ParentId;

            _unitOfWork.GeoObjectsRepository.Update(existingGeoObject);
            await _unitOfWork.SaveAsync();

            return existingGeoObject;
        }

        /// <summary>
        /// Валидация объекта исторического события
        /// </summary>
        /// <param name="historicalEvent"></param>
        private void Validate(GeoObject? geoObject)
        {
            if (geoObject == null)
            {
                throw new Exception(ServiceConstants.Exception.ObjectEqualsNull);
            }

            string[] errorMessages = [];

            var nameLenght = geoObject.Name.Trim().Length;

            if (nameLenght == 0)
            {
                errorMessages.Append("Не заполнено название");
            }

            if (nameLenght > 100)
            {
                errorMessages.Append("Название не должно превышать 100 символов");
            }

            //TODO: Сделать полную валидацию

            if (errorMessages.Length > 0)
            {
                throw new Exception(string.Join("\n", errorMessages));
            }
        }
    }
}
