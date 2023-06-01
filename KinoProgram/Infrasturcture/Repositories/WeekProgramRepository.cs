using KinoProgram.Infrasturcture;
using KinoProgram.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KinoProgram.Application.Infrasturcture.Repositories
{
    public class WeekProgramRepository : Repository<Movie, int>
    {
        public record MoviesDto(
            int WeekNumber,
            string MovieName,
            string Genre,
            int Hall,
            int Duration,
            Guid Guid,
            DateTime PlayTime
            );

        public WeekProgramRepository(CinemaContext db) : base(db)
        {
        }

        public IReadOnlyList<MoviesDto> GetWeekProgram(int WeekNumber)
        {
            var weekProgram = _db.WeeklyPrograms
                .Where(w => w.CalendarWeek == WeekNumber)
                .Select(g => new MoviesDto(
                    g.CalendarWeek,
                    g.Movie.Name,
                    g.Movie.MovieCategory.ToString(),
                    g.CinemaHallId,
                    g.Movie.Duration,
                    g.Movie.Guid,
                    g.PlayTime))
                .ToList();
            return weekProgram;
        }
    }
}
