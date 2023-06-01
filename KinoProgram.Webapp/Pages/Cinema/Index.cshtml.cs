using KinoProgram.Application.Infrasturcture.Repositories;
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
        private readonly WeeklyProgramRepository _db;
        public IndexModel(WeeklyProgramRepository db)
        {
            _db = db;
        }
        public IReadOnlyList<WeeklyProgramRepository.WeeklyProgramDto> WeeklyPrograms { get; private set; } = new List<WeeklyProgramRepository.WeeklyProgramDto>();
        public void OnGet()
        {
            WeeklyPrograms = _db.GetProgramCount();
        }
    }
}
