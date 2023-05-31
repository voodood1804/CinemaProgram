using KinoProgram.Infrasturcture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System;
using KinoProgram.models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using KinoProgram.Webapp.Dto;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KinoProgram.Webapp.Pages.Cinema
{
    public class MoviesModel : PageModel
    {
        private readonly CinemaContext _db;
        private readonly IMapper _mapper;
        public List<Movie> Movies { get; private set; } = new();
        [BindProperty]
        public NewMovieDto NewMovie { get; set; } = null!;
        public MoviesModel(CinemaContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public IActionResult OnPostNewMovie()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var movie = _mapper.Map<Movie>(NewMovie);
            if (movie is null)
            {
                return RedirectToPage("/Cinema/Movies");
            }
            _mapper.Map(NewMovie, movie);
            _db.Entry(movie).State = EntityState.Modified;
            try
            {
                _db.Add(movie);
                _db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Fehler beim Schreiben in die Datenbank");
                return Page();
            }
            return RedirectToPage("/Cinema/Movies");
        }
        public void OnGet()
        {
            Movies = _db.Movies.OrderBy(m => m.Name).ToList();
        }
    }
}
