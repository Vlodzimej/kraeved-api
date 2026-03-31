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
    }
}
