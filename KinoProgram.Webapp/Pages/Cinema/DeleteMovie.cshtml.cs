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
    public class DeleteMovieModel : PageModel
    {
        private readonly MovieRepository _movies;
        public DeleteMovieModel(MovieRepository movies)
        {
            _movies = movies;
        }

        [TempData]
        public string? Message { get; set; }
        public Movie Movie { get; set; }
        public IActionResult OnPostCancel() => RedirectToPage("/Cinema/Movies");
        public IActionResult OnPostDelete(Guid guid)
        {
            var movie = _movies.FindByGuid(guid);
            if (movie is null)
            {
                return RedirectToPage("/Cinema/Movies");
            }
            var (success, message) = _movies.Delete(movie);
            if (!success) { Message = message; }
            return RedirectToPage("/Cinema/Movies");
        }
        public IActionResult OnGet(Guid guid)
        {
            var movie = _movies.FindByGuid(guid);
            if (movie == null)
            {
                return RedirectToPage("/Cinema/Movies");
            }
            Movie = movie;
            return Page();
        }
    }
}
