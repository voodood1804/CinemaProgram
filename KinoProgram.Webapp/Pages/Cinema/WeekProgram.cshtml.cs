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

        public IActionResult OnGet(int WeekNumber)
        {
            
            WeekProgram = _db.GetWeekProgram(WeekNumber);
            return Page();
        }
    }
}
