using DynamicSunTest;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<WeatherDbContext>(options =>
{
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WeatherDb;Trusted_Connection=True;");
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<WeatherDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=WeatherHome}/{action=WeatherHome}/{id?}");
app.MapControllerRoute(
    name: "weatherView",
    pattern: "{controller=WeatherView}/{id?}");
app.MapControllerRoute(
    name: "weatherUpload",
    pattern: "{controller=WeatherUpload}/{id?}");
app.MapControllerRoute(
    name: "weatherData",
    pattern: "{controller=WeatherData}/{id?}");

app.Run();
