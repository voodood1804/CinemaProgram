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
        public Movie Movie { get; private set; }
        public IActionResult OnGet(Guid guid)
        {
            // SELECT * FROM Stores INNER JOIN Offers ON (...)
            // INNER JOIN Product ON (...)
            var movie = _db.Movies
                .FirstOrDefault(s => s.Guid == guid);
            if (movie == null)
            {
                return RedirectToPage("/Cinema/Index");
            }
            Movie = movie;
            return Page();
        }
    }
}
