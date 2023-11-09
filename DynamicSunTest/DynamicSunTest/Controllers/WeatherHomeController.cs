using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSunTest.Controllers
{
    public class WeatherHomeController : Controller
    {
        public ActionResult WeatherHome()
        {
            return View();
        }

    }
}
