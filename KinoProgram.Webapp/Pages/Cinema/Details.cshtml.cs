using KinoProgram.Infrasturcture;
using KinoProgram.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using static System.Formats.Asn1.AsnWriter;
using System;
using System.Linq;

namespace KinoProgram.Webapp.Pages.Cinema
{
    public class DetailsModel : PageModel
    {
        private readonly CinemaContext _db;
        public DetailsModel(CinemaContext db)
        {
            _db = db;
        }
        public Movie Movie { get; private set; } = default!;
        public IActionResult OnGet(Guid guid)
        {
            var movie = _db.Movies
                .FirstOrDefault(m => m.Guid == guid);
            if (movie == null)
            {
                return RedirectToPage("/Cinema/Movies");
            }
            Movie = movie;
            return Page();
        }
    }
}
