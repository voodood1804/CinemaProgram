using KinoProgram.models;
using System;

namespace KinoProgram.Webapp.Dto
{
    public record BulkMovieDto(
        int Id,
        Guid Guid,
        string Name,
        string Description,
        int Duration,
        DateTime ReleaseDate,
        MovieCategory MovieCategory
        );
}
