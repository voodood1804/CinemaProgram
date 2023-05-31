using AutoMapper;
using KinoProgram.Infrasturcture;
using KinoProgram.models;
using KinoProgram.Webapp.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Schema;

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
        private readonly IMapper _mapper;
        public WeekProgramModel(CinemaContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public List<MoviesDto> WeekProgram { get; private set; } = new();

        [BindProperty]
        public WeeklyProgramDto NewWeekProgram { get; set; } = null!;
        public IEnumerable<SelectListItem> MovieSelectList =>
            _db.Movies.OrderBy(m=>m.Name).Select(m=> new SelectListItem(m.Name, m.Id.ToString()));
        public IEnumerable<SelectListItem> HallSelectList =>
            _db.CinemaHalls.Select(m => new SelectListItem(m.Id.ToString(), m.Id.ToString()));
        public IActionResult OnPostNewWeekProgram(int WeekNumber)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var weeklyprogram = _mapper.Map<WeeklyProgram>(NewWeekProgram);
            weeklyprogram.CalendarWeek = WeekNumber;
            weeklyprogram.Movie = _db.Movies.FirstOrDefault(m => m.Id == NewWeekProgram.Movie.Id)
                        ?? throw new ApplicationException("unvalid movie");
            weeklyprogram.CinemaHall = _db.CinemaHalls.FirstOrDefault(m => m.Id == NewWeekProgram.CinemaHall.Id)
                        ?? throw new ApplicationException("unvalid hall");
            weeklyprogram.PlayTime = NewWeekProgram.PlayTime;
            if (weeklyprogram == null)
            {
                return RedirectToPage("/Cinema/WeekProgram/" + WeekNumber);
            }
            _mapper.Map(NewWeekProgram, weeklyprogram);
            try
            {
                _db.WeeklyPrograms.Add(weeklyprogram);
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Fehler beim Schreiben in die Datenbank");
                return Page();
            }
            return RedirectToPage("/Cinema/WeekProgram/"+WeekNumber);
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
