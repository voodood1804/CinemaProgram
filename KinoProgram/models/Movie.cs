using KinoProgram.Application.models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoProgram.models
{
    public enum MovieCategory {Horror = 1, ScienceFiction, Fantasy, Action, Thriller, Drama, Mystery, Comedy}
    [Table("Movie")]
    public class Movie : IEntity<int>
    {
        //duration is in Minutes
        public Movie(string name,string description, int duration, DateTime releaseDate, MovieCategory movieCategory) 
        {
            Name = name;
            Duration = duration;
            Description = description;
            ReleaseDate = releaseDate;
            MovieCategory = movieCategory;
            Guid = Guid.NewGuid();
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        protected Movie() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public int Id { get; private set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Duration { get; set; }
        public DateTime ReleaseDate { get; set; }
        public MovieCategory MovieCategory { get; set; }
        public Guid Guid { get; private set; }
    }
}
