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
        public DbSet<PersonRelationType> PersonRelationTypes => Set<PersonRelationType>();
        public DbSet<PersonRelation> PersonRelations => Set<PersonRelation>();
        public DbSet<AppSetting> AppSettings => Set<AppSetting>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<ImageInfo> ImageInfos => Set<ImageInfo>();

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

            modelBuilder.Entity<PersonRelation>()
                .HasKey(pr => new { pr.PersonId1, pr.PersonId2, pr.RelationTypeId });

            modelBuilder.Entity<PersonRelation>()
                .HasOne(pr => pr.Person1)
                .WithMany(p => p.RelationsFrom)
                .HasForeignKey(pr => pr.PersonId1)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonRelation>()
                .HasOne(pr => pr.Person2)
                .WithMany(p => p.RelationsTo)
                .HasForeignKey(pr => pr.PersonId2)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<PersonRelation>()
                .HasOne(pr => pr.RelationType)
                .WithMany()
                .HasForeignKey(pr => pr.RelationTypeId);

            modelBuilder.Entity<PersonRelationType>()
                .HasOne(prt => prt.PairedType)
                .WithMany()
                .HasForeignKey(prt => prt.PairedTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ImageInfo>()
                .HasOne(i => i.GeoObject)
                .WithMany(g => g.Images)
                .HasForeignKey(i => i.GeoObjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ImageInfo>()
                .HasOne(i => i.Person)
                .WithMany(p => p.Photos)
                .HasForeignKey(i => i.PersonId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<GeoObject>()
                .HasOne(g => g.Parent)
                .WithMany(g => g.Children)
                .HasForeignKey(g => g.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GeoObject>()
                .HasOne(g => g.Subtype)
                .WithMany()
                .HasForeignKey(g => g.SubtypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
