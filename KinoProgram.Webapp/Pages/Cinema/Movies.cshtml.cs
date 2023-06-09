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
using Microsoft.AspNetCore.Mvc.Filters;
using AutoMapper.QueryableExtensions;
using KinoProgram.Application.Infrasturcture.Repositories;
using Microsoft.AspNetCore.Authorization;
using KinoProgram.Webapp.Services;

namespace KinoProgram.Webapp.Pages.Cinema
{
    public class MoviesModel : PageModel
    {
        private readonly MovieRepository _db;
        private readonly IMapper _mapper;
        private readonly AuthService _authService;
        public MoviesModel(MovieRepository db, IMapper mapper, AuthService authService)
        {
            _db = db;
            _mapper = mapper;
            _authService = authService;
        }
        public IReadOnlyList<Movie> Movies { get; private set; } = new List<Movie>();
        public Dictionary<Guid, EditMovieDto> EditMovies { get; private set; } = new();
        [BindProperty]
        public NewMovieDto NewMovie { get; set; } = null!;

        public IActionResult OnPostEditMovie(Guid movieguid, Dictionary<Guid, EditMovieDto> editMovies)
        {
            var movie = _db.FindByGuid(movieguid);
            if (movie == null)
            {
                return RedirectToPage();
            }
            _mapper.Map(editMovies[movieguid], movie);
            var (success, message) = _db.Update(movie);
            if (!success)
            {
                ModelState.AddModelError("", message!);
                return Page();
            }
            return RedirectToPage();
        }

        public IActionResult OnPostNewMovie(NewMovieDto NewMovie)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var movie = new Movie(name: NewMovie.Name,
                    description: NewMovie.Description,
                    duration: NewMovie.Duration,
                    releaseDate: NewMovie.ReleaseDate,
                    movieCategory: NewMovie.MovieCategory);

            var (success, message) = _db.Insert(movie);
            if(!success) 
            {
                ModelState.AddModelError("", message ?? string.Empty);
                return Page();
            }
            return RedirectToPage("/Cinema/Movies");
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            Movies = _db.GetMovies();
            var movies = _mapper.ProjectTo<EditMovieDto>(_db.Set)
                .ToDictionary(m => m.Guid, m => m);
            EditMovies = movies;
        }
    }
}
