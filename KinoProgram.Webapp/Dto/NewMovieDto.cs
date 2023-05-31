using KinoProgram.models;
using System;

namespace KinoProgram.Webapp.Dto
{
    public record NewMovieDto(
        string Name,
        string Description,
        int Duration,
        DateTime ReleaseDate,
        MovieCategory MovieCategory
        );
}
