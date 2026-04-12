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
        Task<IEnumerable<PersonRelationType>> GetAllRelationTypes();
        Task<IEnumerable<PersonRelationDto>> GetRelationsByPersonId(int personId);
        Task<bool> AddRelation(int personId1, int personId2, int relationTypeId);
        Task<bool> RemoveRelation(int personId1, int personId2, int relationTypeId);
        Task<PersonTreeNode> GetFamilyTree(int personId);
        Task<ImageInfo> AddImageToPerson(int personId, string filename, string? caption = null);
        Task UpdatePersonImagesOrder(int personId, List<int> imageIdsInOrder);
    }
}
