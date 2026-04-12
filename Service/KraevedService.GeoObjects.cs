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
                .Get(x => filter.Id == null || x.Id == filter.Id, includeProperties: "Type,Type.Category,Subtype,Subtype.Category,Parent,Parent.Type,Children.Type,PersonGeoObjects.Person,Images")
                ?? throw new Exception(ServiceConstants.Exception.UnknownError);

            var geoObject = result.FirstOrDefault();

            if (geoObject != null)
            {
                if (geoObject.Images == null)
                {
                    geoObject.Images = new List<ImageInfo>();
                }
                else
                {
                    geoObject.Images = geoObject.Images.OrderBy(i => i.Order).ToList();
                }
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
                    x => x.OrderBy(item => item.Name), includeProperties: "Type,Type.Category,Subtype")
                .Select(_mapper.Map<GeoObjectBrief>) ??
                    throw new Exception(ServiceConstants.Exception.UnknownError);

            return Task.FromResult(result);
        }

        /// <summary>
        /// Добавление гео-объекта в БД
        /// </summary>
        /// <param name="geoObject"></param>
        /// <returns></returns>
        public async Task<GeoObject> InsertGeoObject(GeoObject geoObject, bool skipExistenceCheck = false)
        {
            if (geoObject.TypeId == null)
            {
                throw new Exception(ServiceConstants.Exception.GeoObjectTypeIsNull);
            }

            Validate(geoObject);

            if (!skipExistenceCheck)
            {
                var filter = new GeoObjectFilter() { Name = geoObject.Name, RegionId = geoObject.RegionId };
                var existedGeoObjectList = await GetGeoObjectsByFilter(filter);
                if (existedGeoObjectList.FirstOrDefault() != null)
                {
                    throw new Exception(ServiceConstants.Exception.ObjectExists);
                }
            }

            if (geoObject.ParentId != null)
            {
                ValidateParentId(geoObject.Id ?? 0, geoObject.ParentId);
            }

            var type = _unitOfWork.GeoObjectTypesRepository.GetByID(geoObject.TypeId);

            if (type == null)
            {
                throw new Exception(ServiceConstants.Exception.GeoObjectTypeNotFound);
            }

            GeoObjectType? subtype = null;
            if (geoObject.SubtypeId != null)
            {
                subtype = _unitOfWork.GeoObjectTypesRepository.GetByID(geoObject.SubtypeId);
            }

            var newGeoObject = new GeoObject()
            {
                Name = geoObject.Name,
                Type = type,
                Subtype = subtype,
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
            var geoObject = _unitOfWork.GeoObjectsRepository.Get(x => id == x.Id, includeProperties: "Images").FirstOrDefault() ??
                throw new Exception(ServiceConstants.Exception.NotFound);

            var imagesToDelete = geoObject.Images?.Select(i => i.Filename).ToList() ?? [];

            _unitOfWork.GeoObjectsRepository.Delete(id);
            await _unitOfWork.SaveAsync();

            foreach (var filename in imagesToDelete)
            {
                DeleteImageFiles(filename);
            }

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

            if (geoObject.ParentId != existingGeoObject.ParentId)
            {
                ValidateParentId(geoObject.Id ?? 0, geoObject.ParentId);
            }

            var type = _unitOfWork.GeoObjectTypesRepository.Get(x => geoObject.TypeId == x.Id).FirstOrDefault();

            GeoObjectType? subtype = null;
            if (geoObject.SubtypeId != null)
            {
                subtype = _unitOfWork.GeoObjectTypesRepository.Get(x => geoObject.SubtypeId == x.Id).FirstOrDefault();
            }

            var existingFilenames = existingGeoObject.Images?.Select(i => i.Filename).ToHashSet() ?? [];
            var newFilenames = geoObject.Images?.Select(i => i.Filename).ToHashSet() ?? [];
            var removedFilenames = existingFilenames.Except(newFilenames).ToList();

            existingGeoObject.Name = geoObject.Name;
            existingGeoObject.ShortDescription = geoObject.ShortDescription;
            existingGeoObject.Description = geoObject.Description;
            existingGeoObject.Longitude = geoObject.Longitude;
            existingGeoObject.Latitude = geoObject.Latitude;
            existingGeoObject.RegionId = geoObject.RegionId;
            existingGeoObject.Type = type;
            existingGeoObject.Subtype = subtype;
            existingGeoObject.Thumbnail = geoObject.Thumbnail;
            existingGeoObject.Images = geoObject.Images;
            existingGeoObject.CustomFields = geoObject.CustomFields;
            existingGeoObject.ParentId = geoObject.ParentId;

            _unitOfWork.GeoObjectsRepository.Update(existingGeoObject);
            await _unitOfWork.SaveAsync();

            foreach (var filename in removedFilenames)
            {
                DeleteImageFiles(filename);
            }

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

        /// <summary>
        /// Валидация родительского объекта (защита от циклических ссылок)
        /// </summary>
        private void ValidateParentId(int geoObjectId, int? parentId)
        {
            if (parentId == null) return;

            if (parentId == geoObjectId)
            {
                throw new Exception("Объект не может быть родителем самого себя");
            }

            var allObjects = _unitOfWork.GeoObjectsRepository
                .Get(x => true, includeProperties: "Parent,Children")
                .ToList();

            var isDescendant = IsDescendant(allObjects, parentId.Value, geoObjectId);
            if (isDescendant)
            {
                throw new Exception("Нельзя назначить родительским объект, который уже является дочерним");
            }

            var isAncestor = IsAncestor(allObjects, parentId.Value, geoObjectId);
            if (isAncestor)
            {
                throw new Exception("Нельзя назначить дочерним объект, который уже является родительским");
            }
        }

        private bool IsDescendant(List<GeoObject> allObjects, int candidateParentId, int geoObjectId)
        {
            var visited = new HashSet<int>();
            var current = candidateParentId;

            while (true)
            {
                if (visited.Contains(current)) break;
                visited.Add(current);

                if (current == geoObjectId) return true;

                var obj = allObjects.FirstOrDefault(x => x.Id == current);
                if (obj?.ParentId == null) break;
                current = obj.ParentId.Value;
            }

            return false;
        }

        private bool IsAncestor(List<GeoObject> allObjects, int candidateParentId, int geoObjectId)
        {
            var obj = allObjects.FirstOrDefault(x => x.Id == candidateParentId);
            return obj?.Children?.Any(c => c.Id == geoObjectId) == true;
        }

        public async Task<ImageInfo> AddImageToGeoObject(int geoObjectId, string filename, string? caption = null)
        {
            var geoObject = _unitOfWork.GeoObjectsRepository.GetByID(geoObjectId)
                ?? throw new Exception(ServiceConstants.Exception.NotFound);

            var imageInfo = new ImageInfo
            {
                Filename = filename,
                Caption = caption,
                GeoObjectId = geoObjectId
            };

            _unitOfWork.ImageInfosRepository.Insert(imageInfo);

            if (string.IsNullOrEmpty(geoObject.Thumbnail))
            {
                geoObject.Thumbnail = filename;
                _unitOfWork.GeoObjectsRepository.Update(geoObject);
            }

            await _unitOfWork.SaveAsync();

            var saved = _unitOfWork.ImageInfosRepository.Get(x => x.Filename == filename && x.GeoObjectId == geoObjectId).FirstOrDefault();
            return saved ?? imageInfo;
        }

        public async Task UpdateGeoObjectImagesOrder(int geoObjectId, List<int> imageIdsInOrder)
        {
            var images = _unitOfWork.ImageInfosRepository.Get(x => x.GeoObjectId == geoObjectId).ToList();
            var orderedImages = imageIdsInOrder
                .Select((id, index) => new { Id = id, Order = index })
                .ToDictionary(x => x.Id, x => x.Order);

            foreach (var image in images)
            {
                if (orderedImages.TryGetValue(image.Id, out var order))
                {
                    image.Order = order;
                    _unitOfWork.ImageInfosRepository.Update(image);
                }
            }

            var geoObject = _unitOfWork.GeoObjectsRepository.GetByID(geoObjectId);
            if (geoObject != null && imageIdsInOrder.Count > 0)
            {
                var firstImageId = imageIdsInOrder.FirstOrDefault();
                var firstImage = images.FirstOrDefault(i => i.Id == firstImageId);
                if (firstImage != null)
                {
                    geoObject.Thumbnail = firstImage.Filename;
                    _unitOfWork.GeoObjectsRepository.Update(geoObject);
                }
            }

            await _unitOfWork.SaveAsync();
        }
    }
}
