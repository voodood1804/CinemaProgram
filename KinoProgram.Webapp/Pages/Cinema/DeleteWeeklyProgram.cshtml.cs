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
        [FromRoute]
        public Guid WeeklyProgramGuid { get; set; }

        [TempData]
        public string? Message { get; set; }
        public WeeklyProgram WeeklyProgram { get; set; }
        public IActionResult OnPostCancel() => RedirectToPage("/Cinema/WeekProgram");
        public IActionResult OnPostDelete(Guid weeklyProgramGuid)
        {
            var wp = _w.FindByGuid(weeklyProgramGuid);
            if (wp is null)
            {
                return RedirectToPage("/Cinema/WeekProgram");
            }
            int weeknr = wp.CalendarWeek;
            var (success, message) = _w.Delete(wp);
            if (!success) { Message = message; }
            return RedirectToPage("/Cinema/WeekProgram" + weeknr);
        }
        public IActionResult OnGet(Guid weeklyProgramGuid)
        {
            var weeklyProgram = _w.FindByGuid(weeklyProgramGuid);
            if (weeklyProgram == null)
            {
                return RedirectToPage("/Cinema/WeekProgram");
            }
            WeeklyProgram = weeklyProgram;
            return Page();
        }
    }
}
