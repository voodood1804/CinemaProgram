using AutoMapper;
using KinoProgram.models;
using System;

namespace KinoProgram.Webapp.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<MovieDto, Movie>();
            CreateMap<Movie, MovieDto>();

            CreateMap<Movie, EditMovieDto>();
            CreateMap<EditMovieDto, Movie>();

            CreateMap<NewMovieDto, Movie>();
            CreateMap<Movie, NewMovieDto>();

            CreateMap<WeeklyProgram, WeeklyProgramDto>();
            CreateMap<WeeklyProgramDto, WeeklyProgram>();

            CreateMap<TestDto, WeeklyProgram>()
                .ForMember(
                    o => o.Guid,
                    opt => opt.MapFrom(o => o.Guid == default ? Guid.NewGuid() : o.Guid));
            CreateMap<WeeklyProgram, TestDto>();
        }
    }
}
