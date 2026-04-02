using AutoMapper;
using KraevedAPI.Models;
using Microsoft.IdentityModel.Tokens;

namespace KraevedAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<GeoObject, GeoObjectBrief>()
            .ForMember(
                dest => dest.TypeId,
                src => src.MapFrom(
                    item => item.Type != null ? item.Type.Id : null
                )
            )
            .ForMember(
                dest => dest.TypeName,
                src => src.MapFrom(
                    item => item.Type != null ? item.Type.Name : "UNKNOWN"
                )
            )
            .ForMember(
                dest => dest.TypeTitle,
                src => src.MapFrom(
                    item => item.Type != null ? item.Type.Title : ""
                )
            )
            .ForMember(
                dest => dest.TypeCategoryName,
                src => src.MapFrom(
                    item => item.Type != null && item.Type.Category != null ? item.Type.Category.Name : null
                )
            )
            .ForMember( 
                dest => dest.ShortDescription,
                src => src.MapFrom(
                    item => !item.ShortDescription.IsNullOrEmpty() ? 
                        item.ShortDescription : 
                        item.Description.Substring(0, Math.Min(item.Description.Count(), 128))
                )
            );
            CreateMap<HistoricalEvent, HistoricalEventBrief>();
            CreateMap<User, UserOutDto>()
                .ForMember(
                dest => dest.Role,
                src => src.MapFrom(
                    item => item.Role.Name != null ? item.Role.Name : "UNKNOWN"
                )
            );
        }


    }
}
