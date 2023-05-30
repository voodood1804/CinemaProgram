using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace KinoProgram.models
{
    [Table("WeeklyProgram")]
    public class WeeklyProgram
    {
        public WeeklyProgram(int calendarWeek, Movie movie, CinemaHall cinemaHall, DateTime playTime) 
        {
            CalendarWeek = calendarWeek;
            Movie = movie;
            MovieId = movie.Id;
            CinemaHall = cinemaHall;
            CinemaHallId = cinemaHall.Id;
            PlayTime = playTime;
        }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public WeeklyProgram() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public int Id { get; private set; }
        public int CalendarWeek { get; set; }
        public virtual Movie Movie { get; set; }
        public int MovieId { get; set; }
        public virtual CinemaHall CinemaHall { get; set; }
        public  int CinemaHallId { get; set; }
        public DateTime PlayTime { get; set; }
        public int Movies { get; set; } = 0;
    }
}
