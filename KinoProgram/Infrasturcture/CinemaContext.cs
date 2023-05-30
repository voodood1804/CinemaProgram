using Bogus;
using KinoProgram.models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoProgram.Infrasturcture
{
    public class CinemaContext : DbContext
    {
        public CinemaContext(DbContextOptions opt) : base(opt){ }
        public DbSet<CinemaHall> CinemaHalls => Set<CinemaHall>();
        public DbSet<Movie> Movies => Set<Movie>();
        public DbSet<WeeklyProgram> WeeklyPrograms => Set<WeeklyProgram>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeeklyProgram>().HasOne(w => w.Movie);
            modelBuilder.Entity<WeeklyProgram>().HasOne(w => w.CinemaHall);
        }

        public void Seed()
        {
            Randomizer.Seed = new Random(1111);
           
            var movies = new Faker<Movie>("de").CustomInstantiator(m => new Movie(
                name: m.Person.FirstName,
                description: m.Music.Genre(),
                duration: m.Random.Int(60, 400),
                releaseDate: m.Person.DateOfBirth,
                movieCategory: m.PickRandom<MovieCategory>()
                ))
                .Generate(10)
                .ToList();
            Movies.AddRange(movies);
            SaveChanges();

            var cinemaHalls = new Faker<CinemaHall>("de").CustomInstantiator(c => new CinemaHall(
                rows: c.Random.Int(5, 12),
                columns: c.Random.Int(1, 15)
                ))
                .Generate(10)
                .ToList();
            CinemaHalls.AddRange(cinemaHalls);
            SaveChanges();

            var weeklyProgram = new Faker<WeeklyProgram>("de").CustomInstantiator(w => new WeeklyProgram(
                calendarWeek: w.Random.Int(1, 52),
                movie: w.Random.ListItem(movies),
                cinemaHall: w.Random.ListItem(cinemaHalls),
                playTime: w.Person.DateOfBirth
                ))
                .Generate(104)
                .ToList();
            WeeklyPrograms.AddRange(weeklyProgram);
            SaveChanges();      
        }
    }
}
