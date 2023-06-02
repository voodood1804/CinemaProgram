using System;

namespace KinoProgram.Webapp.Dto
{
    public record EditMovieDto(
        Guid Guid,
        int Duration
        );
}
