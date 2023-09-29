using KraevedAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KraevedAPI.DAL
{
    public class KraevedContext : DbContext
    {
        public KraevedContext()
        {
        }

        public KraevedContext(DbContextOptions<KraevedContext> options) : base(options)
        {
        }

        public DbSet<GeoObject> GeoObjects => Set<GeoObject>();
    }
}
