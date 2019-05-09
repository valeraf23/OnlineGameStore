using AutoMapper;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Profiles
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<PublisherForCreateModel, Publisher>().ForMember(dest => dest.Name,
                opt => opt.MapFrom(src => src.Name));


        }
    }
}