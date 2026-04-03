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

        private GenericRepository<PersonRelationType>? personRelationTypesRepository;

        public GenericRepository<PersonRelationType> PersonRelationTypesRepository
        {
            get
            {
                if (this.personRelationTypesRepository == null)
                {
                    this.personRelationTypesRepository = new GenericRepository<PersonRelationType>(context);
                }
                return personRelationTypesRepository;
            }
        }

        private GenericRepository<PersonRelation>? personRelationsRepository;

        public GenericRepository<PersonRelation> PersonRelationsRepository
        {
            get
            {
                if (this.personRelationsRepository == null)
                {
                    this.personRelationsRepository = new GenericRepository<PersonRelation>(context);
                }
                return personRelationsRepository;
            }
        }

        private GenericRepository<AppSetting>? appSettingsRepository;

        public GenericRepository<AppSetting> AppSettingsRepository
        {
            get
            {
                if (this.appSettingsRepository == null)
                {
                    this.appSettingsRepository = new GenericRepository<AppSetting>(context);
                }
                return appSettingsRepository;
            }
        }

        private GenericRepository<Comment>? commentsRepository;

        public GenericRepository<Comment> CommentsRepository
        {
            get
            {
                if (this.commentsRepository == null)
                {
                    this.commentsRepository = new GenericRepository<Comment>(context);
                }
                return commentsRepository;
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