using KinoProgram.Application.Infrasturcture.Repositories;
using KinoProgram.models;
using KinoProgram.Webapp.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using static System.Formats.Asn1.AsnWriter;

namespace KinoProgram.Webapp.Pages.Cinema
{
    [Authorize(Roles = "Admin")]
    public class DeleteWeeklyProgram : PageModel
    {
        private readonly WeeklyProgramRepository _w;
        public DeleteWeeklyProgram(WeeklyProgramRepository weeklyProgram)
        {
            _w = weeklyProgram;
        }

        [TempData]
        public string? Message { get; set; }
        public WeeklyProgram WeeklyProgram { get; set; }
        public IActionResult OnPostCancel() => RedirectToPage("/Cinema/Movies");
        public IActionResult OnPostDelete(Guid guid)
        {
            var wp = _w.FindByGuid(guid);
            if (wp is null)
            {
                return RedirectToPage("/Cinema/Movies");
            }
            int weeknr = wp.CalendarWeek;
            var (success, message) = _w.Delete(wp);
            if (!success) { Message = message; }
            return RedirectToPage("/Cinema/Movies" + weeknr);
        }
        public IActionResult OnGet(Guid guid)
        {
            var weeklyProgram = _w.FindByGuid(guid);
            if (weeklyProgram == null)
            {
                return RedirectToPage("/Cinema/Movies");
            }
            WeeklyProgram = weeklyProgram;
            return Page();
        }
    }
}
