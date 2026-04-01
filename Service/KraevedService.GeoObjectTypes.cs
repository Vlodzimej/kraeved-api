using KraevedAPI.Constants;
using KraevedAPI.Models;

namespace KraevedAPI.Service
{
    public partial class KraevedService : IKraevedService
    {
        /// <summary>
        /// Получение исторического события 
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        public Task<GeoObjectType> GetGeoObjectTypeById(int id) {

            var currentUser = GetCurrentUser();

            var geoObjectType = _unitOfWork.GeoObjectTypesRepository
                .Get(x => x.Id == id, includeProperties: "Category")
                .FirstOrDefault() ?? 
                throw new Exception(ServiceConstants.Exception.NotFound);
            
            return Task.FromResult(geoObjectType);
        }

        public Task<IEnumerable<GeoObjectType>> GetAllGeoObjectTypes() {
            var geoObjectTypes = _unitOfWork.GeoObjectTypesRepository
                .Get(includeProperties: "Category") ??
                throw new Exception(ServiceConstants.Exception.UnknownError);

            return Task.FromResult(geoObjectTypes);
        }
        
        /// <summary>
        /// Добавление нового исторического события
        /// </summary>
        /// <param name="GeoObjectType">Историческое событие</param>
        /// <returns></returns>
        public async Task<GeoObjectType> InsertGeoObjectType(GeoObjectType geoObjectType) {
            Validate(geoObjectType);

            _unitOfWork.GeoObjectTypesRepository.Insert(geoObjectType);
            await _unitOfWork.SaveAsync();
 
            var newGeoObjectType = _unitOfWork.GeoObjectTypesRepository
                .Get(x => 
                    (x.Name == geoObjectType.Name) && 
                    (x.Title == geoObjectType.Title))
                .FirstOrDefault() ?? 
                    throw new Exception(ServiceConstants.Exception.CreatedObjectNotFound);

            return newGeoObjectType;
        }

        /// <summary>
        /// Удаление исторического события
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        public async Task<GeoObjectType> DeleteGeoObjectType(int id) {
            var geoObjectType = _unitOfWork.GeoObjectTypesRepository.Get(x => id == x.Id).FirstOrDefault() ?? 
            throw new Exception(ServiceConstants.Exception.NotFound); 

            _unitOfWork.GeoObjectTypesRepository.Delete(id);
            await _unitOfWork.SaveAsync();

            return geoObjectType;
        }

        /// <summary>
        /// Изменение исторического события
        /// </summary>
        /// <param name="historicalEvent">Историческое событие</param>
        /// <returns></returns>
        public async Task<GeoObjectType?> UpdateGeoObjectType(GeoObjectType geoObjectType) {
            GeoObjectType? existingGeoObjectType = null;

            if (geoObjectType.Id != null) {
                existingGeoObjectType = _unitOfWork.GeoObjectTypesRepository.GetByID(geoObjectType.Id) ?? 
                    throw new Exception(ServiceConstants.Exception.NotFound); 
                Validate(geoObjectType);

                existingGeoObjectType.Name = geoObjectType.Name;
                existingGeoObjectType.Title = geoObjectType.Title;
                existingGeoObjectType.CategoryId = geoObjectType.CategoryId;
                _unitOfWork.GeoObjectTypesRepository.Update(existingGeoObjectType);
                await _unitOfWork.SaveAsync();
            }
            return existingGeoObjectType;
        }

        /// <summary>
        /// Валидация объекта исторического события
        /// </summary>
        /// <param name="GeoObjectType"></param>
        private void Validate(GeoObjectType? geoObjectType) {
            if (geoObjectType == null) {
                throw new Exception(ServiceConstants.Exception.ObjectEqualsNull);
            }

            List<string> errorMessages = [];

            var nameLenght = geoObjectType.Name.Trim().Length;

            if (nameLenght == 0) {
                errorMessages.Add("Не заполнено название");
            }

            if (nameLenght > 100) {
                errorMessages.Add("Название не должно превышать 100 символов");
            }

            if (errorMessages.Count() > 0) {
                throw new Exception(string.Join("\n", errorMessages));
            }
        }
    }
}