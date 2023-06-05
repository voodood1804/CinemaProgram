using KinoProgram.models;
using System;

namespace KinoProgram.Webapp.Dto
{
    public record TestDto(
            Guid Guid,
            Guid MovieGuid,
            Guid CinemaHallGuid,
            int CalendarWeek,
            DateTime PlayTime
            );
}
