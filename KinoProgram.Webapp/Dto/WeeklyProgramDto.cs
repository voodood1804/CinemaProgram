using KinoProgram.models;
using System;

namespace KinoProgram.Webapp.Dto
{
    public record WeeklyProgramDto
    (
        int Weeknumber,
        Movie Movie,
        CinemaHall CinemaHall,
        DateTime PlayTime
    );
    
}
