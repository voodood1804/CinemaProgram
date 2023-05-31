using AutoMapper;
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
        private readonly CinemaContext _db;
        private readonly IMapper _mapper;
        public EditModel(CinemaContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        [BindProperty]
        public MovieDto Movie { get; set; } = null!;
        public IActionResult OnPost(Guid guid)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var movie = _db.Movies.FirstOrDefault(m => m.Guid == guid);
            if (movie is null)
            {
                return RedirectToPage("/Cinema/Movies");
            }
            _mapper.Map(Movie, movie);
            _db.Entry(movie).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Fehler beim Schreiben in die Datenbank");
                return Page();
            }
            return RedirectToPage("/Cinema/Movies");
        }
        public IActionResult OnGet(Guid guid)
        {
            var movie = _db.Movies
                .FirstOrDefault(m => m.Guid == guid);
            if (movie is null)
            {
                return RedirectToPage("/Cinema/Movies");
            }
            Movie = _mapper.Map<MovieDto>(movie);
            return Page();
        }
    }
}
