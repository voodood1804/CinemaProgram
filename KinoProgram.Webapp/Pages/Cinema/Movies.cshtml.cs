using KinoProgram.Infrasturcture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System;
using KinoProgram.models;
using System.Linq;

namespace KinoProgram.Webapp.Pages.Cinema
{
    public class MoviesModel : PageModel
    {
        private readonly CinemaContext _db;
        public List<Movie> Movies { get; private set; } = new();
        public MoviesModel(CinemaContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Movies = _db.Movies.OrderBy(m => m.Name).ToList();
        }
    }
}
