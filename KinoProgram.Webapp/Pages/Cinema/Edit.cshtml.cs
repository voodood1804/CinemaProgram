using AutoMapper;
using KinoProgram.Application.Infrasturcture.Repositories;
using KinoProgram.Infrasturcture;
using KinoProgram.models;
using KinoProgram.Webapp.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace KinoProgram.Webapp.Pages.Cinema
{
    public class EditModel : PageModel
    {
        private readonly MovieRepository _db;
        private readonly IMapper _mapper;
        public EditModel(MovieRepository db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [BindProperty]
        public Movie Movie { get; set; } = null!;
        public IActionResult OnPost(Guid guid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var movie = _db.Set.FirstOrDefault(m => m.Guid == guid);
            if (movie is null)
            {
                return RedirectToPage("/Cinema/Movies");
            }
            var (success, message) = _db.Update(movie);
            if (!success)
            {
                ModelState.AddModelError("", message);
                return Page();
            }
            return RedirectToPage("/Cinema/Movies");
        }
        public IActionResult OnGet(Guid guid)
        {

            Movie = _db.FindByGuid(guid)!;
            if (Movie == null)
            {
                return RedirectToPage("/Cinema/Movies");
            }
            return Page();
        }
    }
}
