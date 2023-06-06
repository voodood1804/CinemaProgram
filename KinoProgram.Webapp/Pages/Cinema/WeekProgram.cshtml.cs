using AutoMapper;
using KinoProgram.Application.Infrasturcture.Repositories;
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
using static KinoProgram.Application.Infrasturcture.Repositories.WeeklyProgramRepository;
using static KinoProgram.Application.Infrasturcture.Repositories.WeekProgramRepository;

namespace KinoProgram.Webapp.Pages.Cinema
{
    public class WeekProgramModel : PageModel
    {
        private readonly WeekProgramRepository _weekprogram;
        private readonly WeeklyProgramRepository _weeklyProgram;
        private readonly MovieRepository _movies;
        private readonly HallRepository _halls;

        private readonly IMapper _mapper;
        public WeekProgramModel(
            WeekProgramRepository weekprogram,
            WeeklyProgramRepository weeklyProgram,
            WeeklyProgramRepository weeklylyProgram,
            MovieRepository movies,
            HallRepository halls,
            IMapper mapper)
        {
            _weekprogram = weekprogram;
            _weeklyProgram = weeklyProgram;
            _movies = movies;
            _halls = halls;
            _mapper = mapper;
        }
        public IReadOnlyList<WeekProgramRepository.MoviesDto> WeekProgram { get; private set; } = new List<WeekProgramRepository.MoviesDto>();
        public IReadOnlyList<Movie> Movies { get; private set; } = new List<Movie>();
        public IReadOnlyList<CinemaHall> CinemaHalls { get; private set; } = new List<CinemaHall>();
        [FromRoute]
        public int WeekNumber { get; set; }
        [BindProperty]
        public TestDto NewWeekProgram { get; set; } = null!;
        public IEnumerable<SelectListItem> MovieSelectList =>
            _movies.Set.OrderBy(m => m.Name).Select(m => new SelectListItem(m.Name, m.Guid.ToString()));
        public IEnumerable<SelectListItem> HallSelectList =>
            _halls.Set.OrderBy(h => h.Id).Select(h => new SelectListItem(h.Id.ToString(), h.Guid.ToString()));

        public IActionResult OnPostNewWeekProgram(int weekNumber, TestDto newWeekProgram)
        {
            if (!ModelState.IsValid) { return Page(); }

            var (success, message) = _weeklyProgram.Insert(
                weeknumber: weekNumber,
                movieGuid: newWeekProgram.MovieGuid,
                hallGuid: newWeekProgram.CinemaHallGuid,
                playtime: newWeekProgram.PlayTime);

            if (!success)
            {
                ModelState.AddModelError("", message!);
                return Page();
            }
            return RedirectToPage();
        }

        public IActionResult OnGet(int WeekNumber)
        {
            
            WeekProgram = _weekprogram.GetWeekProgram(WeekNumber);
            Movies = _movies.GetMovies();
            CinemaHalls = _halls.GetHalls();
            return Page();
        }
    }
}
