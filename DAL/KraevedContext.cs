using KraevedAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KraevedAPI.DAL
{
    public class KraevedContext : DbContext
    {
        public KraevedContext() { }

        public KraevedContext(DbContextOptions<KraevedContext> options) : base(options) { }

        public DbSet<GeoObject> GeoObjects => Set<GeoObject>();
        public DbSet<HistoricalEvent> HistoricalEvents => Set<HistoricalEvent>();
        public DbSet<ImageObject> ImageObjects => Set<ImageObject>();
        public DbSet<User> Users => Set<User>();
        public DbSet<SmsCode> SmsCodes => Set<SmsCode>();
        public DbSet<Role> Roles => Set<Role>();
        public DbSet<GeoObjectCategory> GeoObjectCategories => Set<GeoObjectCategory>();
        public DbSet<GeoObjectType> GeoObjectTypes => Set<GeoObjectType>();
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<PersonGeoObject> PersonGeoObjects => Set<PersonGeoObject>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonGeoObject>()
                .HasKey(pg => new { pg.PersonId, pg.GeoObjectId });

            modelBuilder.Entity<PersonGeoObject>()
                .HasOne(pg => pg.Person)
                .WithMany(p => p.PersonGeoObjects)
                .HasForeignKey(pg => pg.PersonId);

            modelBuilder.Entity<PersonGeoObject>()
                .HasOne(pg => pg.GeoObject)
                .WithMany(g => g.PersonGeoObjects)
                .HasForeignKey(pg => pg.GeoObjectId);
        }
    }
}
