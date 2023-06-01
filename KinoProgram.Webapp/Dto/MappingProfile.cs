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
            CreateMap<Movie, NewMovieDto>();
            CreateMap<NewMovieDto, Movie>();
            CreateMap<BulkMovieDto, Movie>();
            CreateMap<Movie, BulkMovieDto>();
            CreateMap<WeeklyProgram, WeeklyProgramDto>();
            CreateMap<WeeklyProgramDto, WeeklyProgram>();
        }
    }
}
