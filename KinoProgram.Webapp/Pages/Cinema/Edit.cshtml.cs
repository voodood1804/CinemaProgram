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
        public MovieDto Movie { get; set; } = null!;
        public IActionResult OnPostEdit(Guid guid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var movie = _db.FindByGuid(guid);
            if (movie is null)
            {
                return RedirectToPage("/Cinema/Movies");
            }
            _mapper.Map(Movie, movie);
            var (success, message) = _db.Update(movie);
            if (!success)
            {
                ModelState.AddModelError("", message ?? string.Empty);
                return Page();
            }
            return RedirectToPage("/Cinema/Movies");
        }
        public IActionResult OnGet(Guid guid)
        {

            var movie = _db.Set.FirstOrDefault(m => m.Guid == guid);
            if (movie == null)
            {
                return RedirectToPage("/Cinema/Movies");
            }
            Movie = _mapper.Map<MovieDto>(movie);
            return Page();
        }
    }
}
