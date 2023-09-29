using AutoMapper;
using KraevedAPI.Models;

namespace KraevedAPI.Helpers 
{ 
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GeoObject, GeoObjectBrief>();
        }
    }
}
