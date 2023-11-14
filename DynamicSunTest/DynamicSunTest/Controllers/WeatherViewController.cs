using DynamicSunTest.Models;
using DynamicSunTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSunTest.Controllers
{
    public class WeatherViewController : Controller
    {
        private readonly WeatherDbContext _context;
        private readonly WeatherViewService _viewService;

        public WeatherViewController(WeatherDbContext context)
        {
            _context = context;
            _viewService = new WeatherViewService(context);
        }

        [Route("/WeatherView")]
        public ActionResult WeatherView()
        {
            return View();
        }

        [Route("/WeatherView/GetYears")]
        public List<int> GetYears()
        {
            var years = _viewService.GetYears().OrderBy(x => x).ToList();

            return years;
        }

        [HttpGet]
        [Route("/WeatherView/GetWeatherData")]
        public IActionResult GetWeatherData(int year)
        {
            var weatherData = _viewService.WeatherByYear(year);
            var returnData = from a in weatherData
                select new 
                {
                    Date = a.Date.ToString("d"),
                    Time = a.Time.ToString("T"),
                    Temperature = a.Temperature,
                    Humidity = a.Humidity,
                    DewPoint = a.DewPoint,
                    AtmospherePressure = a.AtmospherePressure,
                    WindDirection = a.WindDirection,
                    WindSpeed = a.WindSpeed,
                    Cloudy = a.Cloudy,
                    CloudBase = a.CloudBase,
                    WeatherConditions = a.WeatherConditions
                };

            return Json(returnData);
        }

        [HttpGet]
        [Route("/WeatherView/WeatherDataView")]
        public IActionResult WeatherDataView()
        {
            return View("~/Views/WeatherView/WeatherData.cshtml");
        }
    }
}
