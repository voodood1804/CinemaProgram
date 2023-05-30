using KinoProgram.Infrasturcture;
using KinoProgram.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace KinoProgram.Webapp.Pages.Cinema
{
    public class IndexModel : PageModel
    {
        public record WeeklyProgramDto(
            int WeekNumber,
            string GenreList,
            int? MoviesCount);
        private readonly CinemaContext _db;
        public List<WeeklyProgramDto> WeeklyPrograms { get; private set; } = new();
        public IndexModel(CinemaContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            var weeklyPrograms = _db.WeeklyPrograms
                .OrderBy(w=>w.CalendarWeek)
                .GroupBy(g => g.CalendarWeek)
                .Select(g => new WeeklyProgramDto(
                    g.Key,
                    string.Join(", ", g.Select(g => g.Movie.MovieCategory.ToString())),
                    g.Count()))
                .ToList(); 
            WeeklyPrograms = Enumerable.Range(1, 52)
                .GroupJoin(weeklyPrograms, w => w, w => w.WeekNumber, (week, movies) =>
                {
                    if (movies.Any()) { return movies.First(); }
                    return new WeeklyProgramDto(week, "There is no Program this Week.", null);
                })
                .OrderBy(m => m.WeekNumber)
                .ToList();
            
        }
    }
}
