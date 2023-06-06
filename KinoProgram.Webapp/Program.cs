using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using KinoProgram.Infrasturcture;
using KinoProgram.Webapp.Dto;
using KinoProgram.Application.Infrasturcture.Repositories;
using KinoProgram.Application.Infrasturcture;
using KinoProgram.Application.models;
using KinoProgram.Webapp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;

// Create and seed databank
var opt = new DbContextOptionsBuilder()
    .UseSqlite("Data Source = KinoProgram.db")
    .Options;
using (var db = new CinemaContext(opt))
{
    db.Database.EnsureDeleted();
    db.Database.EnsureCreated();
    db.Seed(new CryptService());
}

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddDbContext<CinemaContext>(opt =>
{
    opt.UseSqlite("Data Source = KinoProgram.db");
});
builder.Services.AddTransient<WeeklyProgramRepository>();
builder.Services.AddTransient<MovieRepository>();
builder.Services.AddTransient<HallRepository>();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<WeekProgramRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddRazorPages();

// Services for auth
// To access httpcontext in services
builder.Services.AddHttpContextAccessor();
// Hashing
builder.Services.AddTransient<ICryptService, CryptService>();
builder.Services.AddTransient<AuthService>(provider => new AuthService(
    isDevelopment: builder.Environment.IsDevelopment(),
    db: provider.GetRequiredService<CinemaContext>(),
    crypt: provider.GetRequiredService<ICryptService>(),
    httpContextAccessor: provider.GetRequiredService<IHttpContextAccessor>()));
builder.Services.AddAuthentication(
    Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(o =>
    {
        o.LoginPath = "/User/Login";
        o.AccessDeniedPath = "/User/AccessDenied";
    });
builder.Services.AddAuthorization(o =>
{
    o.AddPolicy("OwnerOrAdminRole", p => p.RequireRole(Usertype.Owner.ToString(), Usertype.Admin.ToString()));
});


// MIDDLEWARE
var app = builder.Build();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.Run();
