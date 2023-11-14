using DynamicSunTest.Models;

namespace DynamicSunTest.Services
{
    public class WeatherViewService
    {
        private readonly WeatherDbContext _context;

        public WeatherViewService(WeatherDbContext context)
        {
            _context = context;
        }

        public List<WeatherData> WeatherByYear(int year)
        {
            List<WeatherData> weatherData = _context.WeatherData.Where(x => x.Date.Year == year).ToList();
            return weatherData;
        }

        public List<int> GetYears()
        {
            List<int> years = _context.WeatherData.Select(x => x.Date.Year).Distinct().ToList();
            return years;
        }
    }
}
