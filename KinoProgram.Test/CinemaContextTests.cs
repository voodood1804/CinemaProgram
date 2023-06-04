using KinoProgram.Application.Infrasturcture;
using KinoProgram.Infrasturcture;
using Microsoft.EntityFrameworkCore;

namespace SpaceProgram.Test
{
    [Collection("Sequential")]
    public class CinemaContextTests
    {
        private CinemaContext GetDatabase(bool deleteDb = false)
        {
            var db = new CinemaContext(new DbContextOptionsBuilder()
                .UseSqlite("Data Source = KinoProgram.db")
                .UseLazyLoadingProxies()
                .Options);
            if (deleteDb)
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();
            }
            return db;
        }

        [Fact]
        public void CreateDatabaseSuccessTest()
        {
            using var db = GetDatabase(deleteDb: true);
        }

        [Fact]
        public void SeedDatabaseTest()
        {
            using var db = GetDatabase(deleteDb: true);
            db.Seed(new CryptService());
        }
    }
}
