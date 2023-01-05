using AutoMapper;
using FIlmsApp.Models.Dtos;
using FIlmsApp.Models.Entities;

namespace FIlmsApp.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            /* CreateMap<Film, FilmDto>()
                         .ForMember(dest => dest.Id, act => act.MapFrom(
                             src => src.Id
                             ))
                         .ForMember(dest => dest.Name, act => act.MapFrom(
                             src => src.Name
                             ))
                         .ForMember(dest => dest.ReleasedDate, act => act.MapFrom(
                             src => src.ReleasedDate
                             ))
                         .ForMember(dest => dest.Actors, act => act.MapFrom(
                             src => src.Actors
                             ))
                         .ForMember(dest => dest.Genre, act => act.MapFrom(
                             src => src.Genre
                             ));*/
            CreateMap<Actor, ActorDto>();
            CreateMap<Genre, GenreDto>();
            CreateMap<Film, FilmDto>();
        }
    }
}
