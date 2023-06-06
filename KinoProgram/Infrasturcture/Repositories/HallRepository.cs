using KinoProgram.Infrasturcture;
using KinoProgram.models;
using System.Collections.Generic;
using System.Linq;

namespace KinoProgram.Application.Infrasturcture.Repositories
{
    public class HallRepository : Repository<CinemaHall, int>
    {
        public HallRepository(CinemaContext db) : base(db) { }

        public IReadOnlyList<CinemaHall> GetHalls()
        {
            var halls = _db.CinemaHalls.OrderBy(m => m.Id).ToList();
            return halls;
        }

        public override (bool success, string? message) Insert(CinemaHall entity)
        {
            return base.Insert(entity);
        }
    }

}
