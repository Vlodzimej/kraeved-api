using KraevedAPI.DAL.Repository;
using KraevedAPI.Models;
using KraevedAPI.Repository;

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

        private GenericRepository<HistoricalEvent>? historicalEventsRepository;

        public GenericRepository<HistoricalEvent> HistoricalEventsRepository
        {
            get
            {
                if (this.historicalEventsRepository == null)
                {
                    this.historicalEventsRepository = new GenericRepository<HistoricalEvent>(context);
                }
                return historicalEventsRepository;
            }
        }
        private GenericRepository<GeoObjectCategory>? geoObjectCategoriesRepository;

        public GenericRepository<GeoObjectCategory> GeoObjectCategoriesRepository
        {
            get
            {
                if (this.geoObjectCategoriesRepository == null)
                {
                    this.geoObjectCategoriesRepository = new GenericRepository<GeoObjectCategory>(context);
                }
                return geoObjectCategoriesRepository;
            }
        }

        private GenericRepository<GeoObjectType>? geoObjectTypesRepository;

        public GenericRepository<GeoObjectType> GeoObjectTypesRepository
        {
            get
            {
                if (this.geoObjectTypesRepository == null)
                {
                    this.geoObjectTypesRepository = new GenericRepository<GeoObjectType>(context);
                }
                return geoObjectTypesRepository;
            }
        }

        private GenericRepository<ImageObject>? imageObjectsRepository;

        public GenericRepository<ImageObject> ImageObjectsRepository
        {
            get
            {
                if (this.imageObjectsRepository == null)
                {
                    this.imageObjectsRepository = new GenericRepository<ImageObject>(context);
                }
                return imageObjectsRepository;
            }
        }

        private GenericRepository<User>? usersRepository;

        public GenericRepository<User> UsersRepository
        {
            get
            {
                if (this.usersRepository == null)
                {
                    this.usersRepository = new GenericRepository<User>(context);
                }
                return usersRepository;
            }
        }


        private GenericRepository<SmsCode>? smsCodesRepository;

        public GenericRepository<SmsCode> SmsCodesRepository
        {
            get
            {
                if (this.smsCodesRepository == null)
                {
                    this.smsCodesRepository = new GenericRepository<SmsCode>(context);
                }
                return smsCodesRepository;
            }
        }

        private GenericRepository<Person>? personsRepository;

        public GenericRepository<Person> PersonsRepository
        {
            get
            {
                if (this.personsRepository == null)
                {
                    this.personsRepository = new GenericRepository<Person>(context);
                }
                return personsRepository;
            }
        }

        private GenericRepository<PersonGeoObject>? personGeoObjectsRepository;

        public GenericRepository<PersonGeoObject> PersonGeoObjectsRepository
        {
            get
            {
                if (this.personGeoObjectsRepository == null)
                {
                    this.personGeoObjectsRepository = new GenericRepository<PersonGeoObject>(context);
                }
                return personGeoObjectsRepository;
            }
        }

        private RolesRepository? rolesRepository;
        public RolesRepository RolesRepository
        {
            get
            {
                if (this.rolesRepository == null)
                {
                    this.rolesRepository = new RolesRepository(context);
                }
                return rolesRepository;
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