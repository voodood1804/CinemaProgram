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
            Movies.Add(new Movie("movie1", 130, new DateTime(2003, 04, 04), MovieCategory.Fantasy));
            Movies.Add(new Movie("movie2", 140, new DateTime(2003, 05, 04), MovieCategory.Action));
            Movies.Add(new Movie("movie2", 150, new DateTime(2003, 06, 04), MovieCategory.Horror));
            Movies.Add(new Movie("movie3", 160, new DateTime(2003, 07, 04), MovieCategory.ScienceFiction));
        }
    }
}
