using KraevedAPI.DAL.Repository;
using KraevedAPI.Models;

namespace KraevedAPI.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly KraevedContext context;
        private GenericRepository<GeoObject>? geoObjectsRepository;

        public GenericRepository<GeoObject> GeoObjectsRepository
        {
            get
            {

                if (this.geoObjectsRepository == null)
                {
                    this.geoObjectsRepository = new GenericRepository<GeoObject>(context);
                }
                return geoObjectsRepository;
            }
        }

        public UnitOfWork(KraevedContext context)
        {
            this.context = context;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        async public Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}