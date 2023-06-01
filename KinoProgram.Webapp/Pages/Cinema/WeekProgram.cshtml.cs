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

namespace KinoProgram.Webapp.Pages.Cinema
{
    public class WeekProgramModel : PageModel
    {
        private readonly WeekProgramRepository _db;
        private readonly IMapper _mapper;
        public WeekProgramModel(WeekProgramRepository db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public IReadOnlyList<WeekProgramRepository.MoviesDto> WeekProgram { get; private set; } = new List<WeekProgramRepository.MoviesDto>();

        [BindProperty]
        public WeeklyProgramDto NewWeekProgram { get; set; } = null!;
        public IEnumerable<SelectListItem> MovieSelectList =>
            _db.Set.OrderBy(m => m.Name).Select(m => new SelectListItem(m.Name, m.Id.ToString()));
        public IEnumerable<SelectListItem> HallSelectList =>
            _db.Set.Select(m => new SelectListItem(m.Id.ToString(), m.Id.ToString()));
        //public IActionResult OnPostNewWeekProgram(int WeekNumber)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }
        //    var weeklyprogram = _mapper.Map<WeeklyProgram>(NewWeekProgram);
        //    weeklyprogram.Movie = _db.Movies.FirstOrDefault(m => m.Id == NewWeekProgram.Movie.Id)
        //                ?? throw new ApplicationException("unvalid movie");
        //    weeklyprogram.CinemaHall = _db.CinemaHalls.FirstOrDefault(m => m.Id == NewWeekProgram.CinemaHall.Id)
        //                ?? throw new ApplicationException("unvalid hall");
        //    if (weeklyprogram is null)
        //    {
        //        return RedirectToPage("/Cinema/WeekProgram/" + WeekNumber);
        //    }
        //    _mapper.Map(NewWeekProgram, weeklyprogram);
        //    try
        //    {
        //        _db.Add(weeklyprogram);
        //        _db.SaveChanges();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        ModelState.AddModelError("", "Fehler beim Schreiben in die Datenbank");
        //        return Page();
        //    }
        //    return RedirectToPage("/Cinema/WeekProgram/" + WeekNumber);
        //}

        public IActionResult OnGet(int WeekNumber)
        {
            
            WeekProgram = _db.GetWeekProgram(WeekNumber);
            return Page();
        }
    }
}
