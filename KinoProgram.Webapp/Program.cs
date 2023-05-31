using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using KinoProgram.Infrasturcture;
using KinoProgram.Webapp.Dto;

// Create and seed databank
var opt = new DbContextOptionsBuilder()
    .UseSqlite("Data Source = KinoProgram.db")
    .Options;
using (var db = new CinemaContext(opt))
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Seed();
}

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<CinemaContext>(opt =>
{
    opt.UseSqlite("Data Source = KinoProgram.db");
});
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddRazorPages();

// MIDDLEWARE
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapRazorPages();
app.Run();
