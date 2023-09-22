using KraevedAPI.DAL.Repository;
using KraevedAPI.Data;
using KraevedAPI.Models;

namespace KraevedAPI.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private KraevedContext context = new KraevedContext();
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

        public void Save()
        {
            context.SaveChanges();
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