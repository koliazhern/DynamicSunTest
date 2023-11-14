using DynamicSunTest.Models;
using Microsoft.EntityFrameworkCore;

namespace DynamicSunTest
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
        {
        }
        public DbSet<WeatherData> WeatherData { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=WeatherDb;Trusted_Connection=True;");
        }
    }
}
