using KraevedAPI.Models;

namespace KraevedAPI.DAL.Repository
{
    public class GeoObjectsRepository : GenericRepository<GeoObject>, IGeoObjectsRepository
    {
        public GeoObjectsRepository(KraevedContext context) : base(context)
        {
        }
    }
}
