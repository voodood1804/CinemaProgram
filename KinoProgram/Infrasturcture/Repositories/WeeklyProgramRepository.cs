using KinoProgram.Infrasturcture;
using KinoProgram.models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KinoProgram.Application.Infrasturcture.Repositories
{

    public class WeeklyProgramRepository : Repository<WeeklyProgram, int>
    {
        public record WeeklyProgramDto(
            int WeekNumber,
            string GenreList,
            int? MoviesCount);

        public record NewWeekprogramDto(
            Guid Guid,
            int CalendarWeek,
            Movie Movie,
            CinemaHall CinemaHall,
            DateTime PlayTime
            );

        public WeeklyProgramRepository(CinemaContext db) : base(db) { }

        public IReadOnlyList<WeeklyProgramDto> GetProgramCount()
        {
            var weeklyPrograms = _db.WeeklyPrograms
                .OrderBy(w => w.CalendarWeek)
                .GroupBy(g => g.CalendarWeek)
                .Select(g => new WeeklyProgramDto(
                    g.Key,
                    string.Join(", ", g.Select(g => g.Movie.MovieCategory.ToString())),
                    g.Count()))
                .ToList();
            weeklyPrograms = Enumerable.Range(1, 52)
                .GroupJoin(weeklyPrograms, w => w, w => w.WeekNumber, (week, movies) =>
                {
                    if (movies.Any()) { return movies.First(); }
                    return new WeeklyProgramDto(week, "There is no Program this Week.", null);
                })
                .OrderBy(m => m.WeekNumber)
                .ToList();

            return weeklyPrograms;
        }

        public override (bool success, string? message) Delete(WeeklyProgram wp)
        {
            return base.Delete(wp);
        }

        public (bool success, string? message) Insert(int weeknumber, Guid movieGuid, Guid hallGuid, DateTime playtime)
        {
            var movie = _db.Movies.FirstOrDefault(m => m.Guid == movieGuid);
            if (movie == null) { return (false, "Invalid movie."); }
            var hall = _db.CinemaHalls.FirstOrDefault(h => h.Guid == hallGuid);
            if (hall == null) { return (false, "Invalid cinemahall."); }
            return base.Insert(new WeeklyProgram(
                calendarWeek: weeknumber,
                movie: movie,
                cinemaHall: hall,
                playTime: playtime
                ));
        }
    }
}
