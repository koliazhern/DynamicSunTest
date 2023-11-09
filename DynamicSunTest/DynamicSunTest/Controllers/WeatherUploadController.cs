using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSunTest.Controllers
{
    public class WeatherUploadController : Controller
    {
        public ActionResult WeatherUpload()
        {
            return View();
        }
    }
}
