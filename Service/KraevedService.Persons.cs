using KraevedAPI.DAL;
using KraevedAPI.Constants;
using KraevedAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KraevedAPI.Service
{
    public partial class KraevedService : IKraevedService
    {
        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            return _unitOfWork.PersonsRepository.Get(
                includeProperties: "PersonGeoObjects.GeoObject,Photos"
            );
        }

        public async Task<Person?> GetPersonById(int id)
        {
            var person = _unitOfWork.PersonsRepository.Get(
                filter: p => p.Id == id,
                includeProperties: "PersonGeoObjects.GeoObject.Type,Photos"
            ).FirstOrDefault();

            if (person?.Photos != null)
            {
                person.Photos = person.Photos.OrderBy(p => p.Order).ToList();
            }

            return person;
        }

        public async Task<Person?> InsertPerson(Person person)
        {
            _unitOfWork.PersonsRepository.Insert(person);
            await _unitOfWork.SaveAsync();
            return person;
        }

        public async Task<Person?> UpdatePerson(Person person)
        {
            var existing = _unitOfWork.PersonsRepository.GetByID(person.Id);
            if (existing == null) return null;

            var existingFilenames = existing.Photos?.Select(p => p.Filename).ToHashSet() ?? [];
            var newFilenames = person.Photos?.Select(p => p.Filename).ToHashSet() ?? [];
            var removedFilenames = existingFilenames.Except(newFilenames).ToList();

            existing.Surname = person.Surname;
            existing.FirstName = person.FirstName;
            existing.Patronymic = person.Patronymic;
            existing.Biography = person.Biography;
            existing.BirthDate = person.BirthDate;
            existing.DeathDate = person.DeathDate;
            existing.Photos = person.Photos;

            _unitOfWork.PersonsRepository.Update(existing);
            await _unitOfWork.SaveAsync();

            foreach (var filename in removedFilenames)
            {
                DeleteImageFiles(filename);
            }

            return existing;
        }

        public async Task<Person?> DeletePerson(int id)
        {
            var person = _unitOfWork.PersonsRepository.GetByID(id);
            if (person == null) return null;

            var links = _unitOfWork.PersonGeoObjectsRepository.Get(x => x.PersonId == id).ToList();
            foreach (var link in links)
            {
                _unitOfWork.PersonGeoObjectsRepository.Delete(link);
            }

            var photosToDelete = person.Photos?.Select(p => p.Filename).ToList() ?? [];

            _unitOfWork.PersonsRepository.Delete(person);
            await _unitOfWork.SaveAsync();

            foreach (var filename in photosToDelete)
            {
                DeleteImageFiles(filename);
            }

            return person;
        }

        public async Task<IEnumerable<Person>> GetPersonsByGeoObjectId(int geoObjectId)
        {
            return _unitOfWork.PersonGeoObjectsRepository.Get(
                x => x.GeoObjectId == geoObjectId,
                includeProperties: "Person.Photos"
            )
            .Select(pg => pg.Person)
            .Where(p => p != null)
            .ToList();
        }

        public async Task<IEnumerable<GeoObject>> GetGeoObjectsByPersonId(int personId)
        {
            return _unitOfWork.PersonGeoObjectsRepository.Get(x => x.PersonId == personId)
                .Select(pg => pg.GeoObject)
                .ToList();
        }

        public async Task<bool> LinkPersonToGeoObject(int personId, int geoObjectId)
        {
            var exists = _unitOfWork.PersonGeoObjectsRepository
                .Get(x => x.PersonId == personId && x.GeoObjectId == geoObjectId).Any();
            if (exists) return false;

            _unitOfWork.PersonGeoObjectsRepository.Insert(new PersonGeoObject { PersonId = personId, GeoObjectId = geoObjectId });
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> UnlinkPersonFromGeoObject(int personId, int geoObjectId)
        {
            var link = _unitOfWork.PersonGeoObjectsRepository
                .Get(x => x.PersonId == personId && x.GeoObjectId == geoObjectId).FirstOrDefault();
            if (link == null) return false;

            _unitOfWork.PersonGeoObjectsRepository.Delete(link);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<Person>> SearchPersons(string query)
        {
            var q = query.Trim().ToLower();
            return _unitOfWork.PersonsRepository.Get(
                x => x.Surname.ToLower().Contains(q) ||
                     x.FirstName.ToLower().Contains(q) ||
                     (x.Patronymic != null && x.Patronymic.ToLower().Contains(q))
            ).Take(20).ToList();
        }

        public async Task<IEnumerable<PersonRelationType>> GetAllRelationTypes()
        {
            return _unitOfWork.PersonRelationTypesRepository.Get().ToList();
        }

        public async Task<IEnumerable<PersonRelationDto>> GetRelationsByPersonId(int personId)
        {
            var relations = _unitOfWork.PersonRelationsRepository.Get(
                x => x.PersonId1 == personId,
                includeProperties: "RelationType"
            ).ToList();

            var result = new List<PersonRelationDto>();

            foreach (var rel in relations)
            {
                var otherPerson = rel.PersonId1 == personId
                    ? _unitOfWork.PersonsRepository.GetByID(rel.PersonId2)
                    : _unitOfWork.PersonsRepository.GetByID(rel.PersonId1);

                if (otherPerson != null)
                {
                    result.Add(new PersonRelationDto
                    {
                        PersonId = otherPerson.Id ?? 0,
                        Surname = otherPerson.Surname,
                        FirstName = otherPerson.FirstName,
                        Patronymic = otherPerson.Patronymic,
                        BirthDate = otherPerson.BirthDate,
                        DeathDate = otherPerson.DeathDate,
                        Photos = otherPerson.Photos?.Select(img => new ImageInfoDto { Id = img.Id, Filename = img.Filename, Caption = img.Caption, Order = img.Order }).ToList(),
                        RelationTitle = rel.RelationType?.Title,
                    });
                }
            }

            return result;
        }

        public async Task<bool> AddRelation(int personId1, int personId2, int relationTypeId)
        {
            var exists = _unitOfWork.PersonRelationsRepository.Get(
                x => x.PersonId1 == personId1 && x.PersonId2 == personId2 && x.RelationTypeId == relationTypeId
            ).Any();
            if (exists) return false;

            _unitOfWork.PersonRelationsRepository.Insert(new PersonRelation
            {
                PersonId1 = personId1,
                PersonId2 = personId2,
                RelationTypeId = relationTypeId,
            });

            // Add reverse relation if paired type exists
            var relationType = _unitOfWork.PersonRelationTypesRepository.GetByID(relationTypeId);
            if (relationType?.PairedTypeId != null)
            {
                var reverseExists = _unitOfWork.PersonRelationsRepository.Get(
                    x => x.PersonId1 == personId2 && x.PersonId2 == personId1 && x.RelationTypeId == relationType.PairedTypeId
                ).Any();
                if (!reverseExists)
                {
                    _unitOfWork.PersonRelationsRepository.Insert(new PersonRelation
                    {
                        PersonId1 = personId2,
                        PersonId2 = personId1,
                        RelationTypeId = relationType.PairedTypeId.Value,
                    });
                }
            }

            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<bool> RemoveRelation(int personId1, int personId2, int relationTypeId)
        {
            var rel = _unitOfWork.PersonRelationsRepository.Get(
                x => x.RelationTypeId == relationTypeId &&
                     ((x.PersonId1 == personId1 && x.PersonId2 == personId2) ||
                      (x.PersonId1 == personId2 && x.PersonId2 == personId1))
            ).FirstOrDefault();
            
            if (rel == null) return false;

            var relationType = _unitOfWork.PersonRelationTypesRepository.GetByID(rel.RelationTypeId);

            _unitOfWork.PersonRelationsRepository.Delete(rel);

            if (relationType?.PairedTypeId != null)
            {
                var reverseRel = _unitOfWork.PersonRelationsRepository.Get(
                    x => x.RelationTypeId == relationType.PairedTypeId &&
                         ((x.PersonId1 == personId1 && x.PersonId2 == personId2) ||
                          (x.PersonId1 == personId2 && x.PersonId2 == personId1))
                ).FirstOrDefault();
                
                if (reverseRel != null)
                {
                    _unitOfWork.PersonRelationsRepository.Delete(reverseRel);
                }
            }

            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<PersonTreeNode> GetFamilyTree(int personId)
        {
            var person = _unitOfWork.PersonsRepository.GetByID(personId);
            var allRelations = _unitOfWork.PersonRelationsRepository.Get(
                includeProperties: "RelationType"
            ).ToList();

            var node = new PersonTreeNode
            {
                Id = person?.Id ?? personId,
                Surname = person?.Surname,
                FirstName = person?.FirstName,
                Patronymic = person?.Patronymic,
                BirthDate = person?.BirthDate,
                DeathDate = person?.DeathDate,
                Photos = person?.Photos?.Select(img => new ImageInfoDto { Id = img.Id, Filename = img.Filename, Caption = img.Caption, Order = img.Order }).ToList(),
                Parents = new List<PersonTreeNode>(),
                Spouses = new List<PersonRelationDto>(),
                Children = new List<PersonRelationDto>(),
                Siblings = new List<PersonRelationDto>(),
            };

            var personRelations = allRelations.Where(r => r.PersonId1 == personId).ToList();

            foreach (var rel in personRelations)
            {
                var relatedPerson = _unitOfWork.PersonsRepository.GetByID(rel.PersonId2);
                if (relatedPerson == null) continue;

                var dto = new PersonRelationDto
                {
                    PersonId = relatedPerson.Id ?? 0,
                    Surname = relatedPerson.Surname,
                    FirstName = relatedPerson.FirstName,
                    Patronymic = relatedPerson.Patronymic,
                    BirthDate = relatedPerson.BirthDate,
                    DeathDate = relatedPerson.DeathDate,
                    Photos = relatedPerson.Photos?.Select(img => new ImageInfoDto { Id = img.Id, Filename = img.Filename, Caption = img.Caption, Order = img.Order }).ToList(),
                    RelationTitle = rel.RelationType?.Title,
                };

                var relName = rel.RelationType?.Name?.ToLower();
                if (relName == "parent")
                {
                    node.Parents.Add(new PersonTreeNode
                    {
                        Id = relatedPerson.Id ?? 0,
                        Surname = relatedPerson.Surname,
                        FirstName = relatedPerson.FirstName,
                        Patronymic = relatedPerson.Patronymic,
                        BirthDate = relatedPerson.BirthDate,
                        DeathDate = relatedPerson.DeathDate,
                        Photos = relatedPerson.Photos?.Select(img => new ImageInfoDto { Id = img.Id, Filename = img.Filename, Caption = img.Caption, Order = img.Order }).ToList(),
                    });
                }
                else if (relName == "child")
                {
                    node.Children.Add(dto);
                }
                else if (relName == "sibling")
                {
                    node.Siblings.Add(dto);
                }
                else if (relName == "spouse")
                {
                    node.Spouses.Add(dto);
                }
            }

            return node;
        }

        public async Task<ImageInfo> AddImageToPerson(int personId, string filename, string? caption = null)
        {
            var person = _unitOfWork.PersonsRepository.GetByID(personId)
                ?? throw new Exception(ServiceConstants.Exception.NotFound);

            var imageInfo = new ImageInfo
            {
                Filename = filename,
                Caption = caption,
                PersonId = personId
            };

            _unitOfWork.ImageInfosRepository.Insert(imageInfo);
            await _unitOfWork.SaveAsync();

            var saved = _unitOfWork.ImageInfosRepository.Get(x => x.Filename == filename && x.PersonId == personId).FirstOrDefault();
            return saved ?? imageInfo;
        }

        public async Task UpdatePersonImagesOrder(int personId, List<int> imageIdsInOrder)
        {
            var images = _unitOfWork.ImageInfosRepository.Get(x => x.PersonId == personId).ToList();
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

            await _unitOfWork.SaveAsync();
        }
    }
}
