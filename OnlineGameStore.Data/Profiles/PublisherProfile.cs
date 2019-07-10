using AutoMapper;
using OnlineGameStore.Data.Dtos;
using OnlineGameStore.Domain.Entities;

namespace OnlineGameStore.Data.Profiles
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<PublisherForCreateModel, PublisherModel>();
            CreateMap<PublisherModel, Publisher>();
            CreateMap<Publisher, PublisherModel>();
        }
    }
}