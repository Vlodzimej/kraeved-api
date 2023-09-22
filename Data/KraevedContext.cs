using KraevedAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace KraevedAPI.Data
{
    public class KraevedContext: DbContext
    {
        public KraevedContext(DbContextOptions<KraevedContext> options) : base(options)
        {
        }

        public DbSet<GeoObject> RpgCharacters => Set<GeoObject>();
    }
}
