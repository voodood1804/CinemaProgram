using AutoMapper;
using KinoProgram.models;

namespace KinoProgram.Webapp.Dto
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<MovieDto, Movie>();
            CreateMap<Movie, MovieDto>();
            CreateMap<WeeklyProgram, WeeklyProgramDto>();
            CreateMap<WeeklyProgramDto, WeeklyProgram>();
        }
    }
}
