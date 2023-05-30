using KinoProgram.Infrasturcture;
using KinoProgram.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KinoProgram.Webapp.Pages.Cinema
{
    public class WeekProgramModel : PageModel
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
        private readonly CinemaContext _db;
        public List<MoviesDto> WeekProgram { get; private set; } = new();
        public WeekProgramModel(CinemaContext db)
        {
            _db = db;
        }
        public IActionResult OnGet(int WeekNumber)
        {
            var weekProgram = _db.WeeklyPrograms
                .Where(w=>w.CalendarWeek == WeekNumber)
                .Select(g => new MoviesDto(
                    g.CalendarWeek,
                    g.Movie.Name,
                    g.Movie.MovieCategory.ToString(),
                    g.CinemaHallId,
                    g.Movie.Duration,
                    g.Movie.Guid,
                    g.PlayTime))
                .ToList();
            WeekProgram = weekProgram;
            if (weekProgram == null)
            {
                return RedirectToPage("/Cinema/Index");
            }
            return Page();
        }
    }
}
