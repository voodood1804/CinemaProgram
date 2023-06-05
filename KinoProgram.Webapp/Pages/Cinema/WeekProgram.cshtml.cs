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
        private readonly WeekProgramRepository _db;
        private readonly WeeklyProgramRepository _weeklyProgram;
        private readonly IMapper _mapper;
        public WeekProgramModel(WeekProgramRepository db, WeeklyProgramRepository weeklyProgram, IMapper mapper)
        {
            _db = db;
            _weeklyProgram = weeklyProgram;
            _mapper = mapper;
        }
        public IReadOnlyList<WeekProgramRepository.MoviesDto> WeekProgram { get; private set; } = new List<WeekProgramRepository.MoviesDto>();

        [BindProperty]
        public NewWeekprogramDto NewWeekProgram { get; set; } = default!;
        public IEnumerable<SelectListItem> MovieSelectList =>
            _db.Set.OrderBy(m => m.Name).Select(m => new SelectListItem(m.Name, m.Guid.ToString()));
        public IEnumerable<SelectListItem> HallSelectList =>
            _db.Set.Select(h => new SelectListItem(h.Id.ToString(), h.Guid.ToString()));

        public IActionResult OnPostNewOffer(int weekNumber, NewWeekprogramDto newWeekProgram)
        {
            if (!ModelState.IsValid) { return Page(); }

            var (success, message) = _weeklyProgram.Insert(
                weeknumber: weekNumber,
                movieGuid: newWeekProgram.MovieGuid,
                hallGuid: newWeekProgram.HallGuid,
                playtime: newWeekProgram.PlayTime
                );

            if (!success)
            {
                ModelState.AddModelError("", message!);
                return Page();
            }
            return RedirectToPage();
        }

        public IActionResult OnGet(int WeekNumber)
        {
            
            WeekProgram = _db.GetWeekProgram(WeekNumber);
            return Page();
        }
    }
}
