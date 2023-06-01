using KinoProgram.Application.models;
using KinoProgram.Infrasturcture;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KinoProgram.Application.Infrasturcture.Repositories
{
    public abstract class Repository<Tentity, Tkey> where Tentity : class, IEntity<Tkey> where Tkey : struct
    {
        protected readonly CinemaContext _db;
        public IQueryable<Tentity> Set => _db.Set<Tentity>();
        protected Repository(CinemaContext db)
        {
            _db = db;
        }
        public Tentity? FindById(Guid Guid) => _db.Set<Tentity>().FirstOrDefault(w => w.Id.Equals(Guid));
        public Tentity? FindByGuid(Guid Guid) => _db.Set<Tentity>().FirstOrDefault(w => w.Guid.Equals(Guid));

        public virtual (bool success, string? message) Insert(Tentity entity)
        {
            _db.Entry(entity).State = EntityState.Added;
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex) 
            {
                return (false, ex.InnerException?.Message ?? ex.Message);
            }
            return (true, string.Empty);
        }

        public virtual (bool success, string? message) Update(Tentity entity)
        {
            if (entity.Id.Equals(default)) { return (false, "Missing primary key."); }
            _db.Entry(entity).State = EntityState.Modified;
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return (false, ex.InnerException?.Message ?? ex.Message);
            }
            return (true, string.Empty);
        }
        public virtual (bool success, string? message) Delete(Tentity entity)
        {
            if (entity.Id.Equals(default)) { return (false, "Missing primary key."); }
            _db.Entry(entity).State = EntityState.Deleted;
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                return (false, ex.InnerException?.Message ?? ex.Message);
            }
            return (true, string.Empty);
        }
    }
}
