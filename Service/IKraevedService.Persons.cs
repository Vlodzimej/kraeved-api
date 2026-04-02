using KraevedAPI.Models;

namespace KraevedAPI.Service
{
    public partial interface IKraevedService
    {
        Task<IEnumerable<Person>> GetAllPersons();
        Task<Person?> GetPersonById(int id);
        Task<Person?> InsertPerson(Person person);
        Task<Person?> UpdatePerson(Person person);
        Task<Person?> DeletePerson(int id);
        Task<IEnumerable<Person>> GetPersonsByGeoObjectId(int geoObjectId);
        Task<IEnumerable<GeoObject>> GetGeoObjectsByPersonId(int personId);
        Task<bool> LinkPersonToGeoObject(int personId, int geoObjectId);
        Task<bool> UnlinkPersonFromGeoObject(int personId, int geoObjectId);
        Task<IEnumerable<Person>> SearchPersons(string query);
    }
}
