using KinoProgram.Infrasturcture;
using KinoProgram.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace KinoProgram.Webapp.Pages.Cinema
{
    public class IndexModel : PageModel
    {
        private readonly CinemaContext _db;
        public List<Movie> Movies { get; private set; } = new();
        public IndexModel(CinemaContext db)
        {
            _db = db;
        }
        public void OnGet()
        {
            Movies = _db.Movies.OrderBy(m => m.Name).ToList();
        }
    }
}
