using KinoProgram.Infrasturcture;
using KinoProgram.models;
using static KinoProgram.Application.Infrasturcture.Repositories.WeeklyProgramRepository;
using System.Collections.Generic;
using System.Linq;
using System;

namespace KinoProgram.Application.Infrasturcture.Repositories
{
    public class MovieRepository : Repository<Movie, int>
    {
        public MovieRepository(CinemaContext db) : base(db) { }

        public IReadOnlyList<Movie> GetMovies()
        {
            var movies = _db.Movies.OrderBy(m => m.Name).ToList();
            return movies;
        }

        public override (bool success, string? message) Insert(Movie entity)
        {
            return base.Insert(entity);
        }
    }

}
