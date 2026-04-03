using KraevedAPI.DAL.Repository;
using KraevedAPI.Models;
using KraevedAPI.Repository;

namespace KraevedAPI.DAL
{
    public interface IUnitOfWork: IDisposable
    {
        GenericRepository<GeoObject> GeoObjectsRepository { get; }
        GenericRepository<HistoricalEvent> HistoricalEventsRepository { get; }
        GenericRepository<GeoObjectCategory> GeoObjectCategoriesRepository { get; }
        GenericRepository<GeoObjectType> GeoObjectTypesRepository { get; }
        GenericRepository<ImageObject> ImageObjectsRepository { get; }
        GenericRepository<User> UsersRepository { get; }
        GenericRepository<SmsCode> SmsCodesRepository { get; }
        GenericRepository<Person> PersonsRepository { get; }
        GenericRepository<PersonGeoObject> PersonGeoObjectsRepository { get; }
        GenericRepository<PersonRelationType> PersonRelationTypesRepository { get; }
        GenericRepository<PersonRelation> PersonRelationsRepository { get; }
        GenericRepository<AppSetting> AppSettingsRepository { get; }
        GenericRepository<Comment> CommentsRepository { get; }
        RolesRepository RolesRepository { get; }

        void Save();
        Task SaveAsync();
    }
}
