using KraevedAPI.Constants;
using KraevedAPI.Models;

namespace KraevedAPI.Service
{
    public partial class KraevedService : IKraevedService
    {
        public Task<GeoObjectCategory> GetGeoObjectCategoryById(int id)
        {
            var currentUser = GetCurrentUser();

            var geoObjectCategory = _unitOfWork.GeoObjectCategoriesRepository.GetByID(id) ??
                throw new Exception(ServiceConstants.Exception.NotFound);

            return Task.FromResult(geoObjectCategory);
        }

        public Task<IEnumerable<GeoObjectCategory>> GetAllGeoObjectCategories()
        {
            var currentUser = GetCurrentUser();

            var geoObjectCategories = _unitOfWork.GeoObjectCategoriesRepository.Get() ??
                throw new Exception(ServiceConstants.Exception.UnknownError);

            return Task.FromResult(geoObjectCategories);
        }

        public async Task<GeoObjectCategory> InsertGeoObjectCategory(GeoObjectCategory geoObjectCategory)
        {
            ValidateGeoObjectCategory(geoObjectCategory);

            _unitOfWork.GeoObjectCategoriesRepository.Insert(geoObjectCategory);
            await _unitOfWork.SaveAsync();

            var newGeoObjectCategory = _unitOfWork.GeoObjectCategoriesRepository
                .Get(x =>
                    (x.Name == geoObjectCategory.Name) &&
                    (x.Title == geoObjectCategory.Title))
                .FirstOrDefault() ??
                    throw new Exception(ServiceConstants.Exception.CreatedObjectNotFound);

            return newGeoObjectCategory;
        }

        public async Task<GeoObjectCategory> DeleteGeoObjectCategory(int id)
        {
            var geoObjectCategory = _unitOfWork.GeoObjectCategoriesRepository.Get(x => id == x.Id).FirstOrDefault() ??
                throw new Exception(ServiceConstants.Exception.NotFound);

            _unitOfWork.GeoObjectCategoriesRepository.Delete(id);
            await _unitOfWork.SaveAsync();

            return geoObjectCategory;
        }

        public async Task<GeoObjectCategory?> UpdateGeoObjectCategory(GeoObjectCategory geoObjectCategory)
        {
            GeoObjectCategory? existingGeoObjectCategory = null;

            if (geoObjectCategory.Id != null)
            {
                existingGeoObjectCategory = _unitOfWork.GeoObjectCategoriesRepository.GetByID(geoObjectCategory.Id) ??
                    throw new Exception(ServiceConstants.Exception.NotFound);
                ValidateGeoObjectCategory(geoObjectCategory);

                existingGeoObjectCategory.Name = geoObjectCategory.Name;
                existingGeoObjectCategory.Title = geoObjectCategory.Title;
                _unitOfWork.GeoObjectCategoriesRepository.Update(existingGeoObjectCategory);
                await _unitOfWork.SaveAsync();
            }
            return existingGeoObjectCategory;
        }

        private void ValidateGeoObjectCategory(GeoObjectCategory? geoObjectCategory)
        {
            if (geoObjectCategory == null)
            {
                throw new Exception(ServiceConstants.Exception.ObjectEqualsNull);
            }

            List<string> errorMessages = [];

            var nameLength = geoObjectCategory.Name.Trim().Length;

            if (nameLength == 0)
            {
                errorMessages.Add("Не заполнено название");
            }

            if (nameLength > 100)
            {
                errorMessages.Add("Название не должно превышать 100 символов");
            }

            if (errorMessages.Count > 0)
            {
                throw new Exception(string.Join("\n", errorMessages));
            }
        }
    }
}
