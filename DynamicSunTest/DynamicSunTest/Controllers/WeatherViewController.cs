using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSunTest.Controllers
{
    public class WeatherViewController : Controller
    {
        [Route("/WeatherView")]
        public ActionResult WeatherView()
        {
            return View();
        }

        [Route("/WeatherView/GetYears")]
        public List<int> GetYears()
        {
            var a = new List<int> { 1, 2, 3 };
            return a;
        }
    }
}
