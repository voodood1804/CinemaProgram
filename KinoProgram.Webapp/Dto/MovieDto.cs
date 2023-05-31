using KinoProgram.models;
using System;

namespace KinoProgram.Webapp.Dto
{
    public record MovieDto(
        Guid Guid,
        string Name,
        string Description,
        int Duration,
        DateTime ReleaseDate,
        MovieCategory MovieCategory
        );
}
