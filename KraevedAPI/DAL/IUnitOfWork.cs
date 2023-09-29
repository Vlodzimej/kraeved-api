using KraevedAPI.DAL.Repository;
using KraevedAPI.Models;

namespace KraevedAPI.DAL
{
    public interface IUnitOfWork: IDisposable
    {
        GenericRepository<GeoObject> GeoObjectsRepository { get; }

        void Save();
        Task SaveAsync();
    }
}
