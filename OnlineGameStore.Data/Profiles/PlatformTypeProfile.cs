using AutoMapper;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Profiles
{
    public class PlatformTypeProfile : Profile
    {
        public PlatformTypeProfile()
        {
            CreateMap<PlatformTypeModel, PlatformType>().ForMember(dest => dest.Id,
                opt => opt.MapFrom(src => src.Id));

            CreateMap<PlatformTypeModel, GamePlatformType>().ForMember(dest => dest.PlatformTypeId,
                opt => opt.MapFrom(x => x.Id));

           
        }
    }
}