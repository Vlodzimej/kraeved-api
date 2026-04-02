using KraevedAPI.DAL;
using KraevedAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KraevedAPI.Service
{
    public partial class KraevedService : IKraevedService
    {
        public async Task<IEnumerable<Person>> GetAllPersons()
        {
            return _unitOfWork.PersonsRepository.Get(
                includeProperties: "PersonGeoObjects.GeoObject"
            );
        }

        public async Task<Person?> GetPersonById(int id)
        {
            return _unitOfWork.PersonsRepository.Get(
                filter: p => p.Id == id,
                includeProperties: "PersonGeoObjects.GeoObject.Type"
            ).FirstOrDefault();
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

            existing.Surname = person.Surname;
            existing.FirstName = person.FirstName;
            existing.Patronymic = person.Patronymic;
            existing.Biography = person.Biography;
            existing.BirthDate = person.BirthDate;
            existing.DeathDate = person.DeathDate;
            existing.Photos = person.Photos;

            _unitOfWork.PersonsRepository.Update(existing);
            await _unitOfWork.SaveAsync();
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

            _unitOfWork.PersonsRepository.Delete(person);
            await _unitOfWork.SaveAsync();
            return person;
        }

        public async Task<IEnumerable<Person>> GetPersonsByGeoObjectId(int geoObjectId)
        {
            return _unitOfWork.PersonGeoObjectsRepository.Get(x => x.GeoObjectId == geoObjectId)
                .Select(pg => pg.Person)
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
    }
}
