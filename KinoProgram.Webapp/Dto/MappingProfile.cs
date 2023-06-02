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

            CreateMap<Movie, EditMovieDto>();
            CreateMap<EditMovieDto, Movie>();

            CreateMap<NewMovieDto, Movie>();
            CreateMap<Movie, NewMovieDto>();

            CreateMap<WeeklyProgram, WeeklyProgramDto>();
            CreateMap<WeeklyProgramDto, WeeklyProgram>();
        }
    }
}
