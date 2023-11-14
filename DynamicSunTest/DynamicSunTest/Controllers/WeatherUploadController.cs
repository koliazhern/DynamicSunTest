using System.Net;
using System.Web;
using DynamicSunTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace DynamicSunTest.Controllers
{
    public class WeatherUploadController : Controller
    {
        private readonly WeatherDbContext _context;
        private readonly WeatherUploadService _uploadService;
        public WeatherUploadController(WeatherDbContext context)
        {
            _context = context;
            _uploadService = new WeatherUploadService(context);
        }

        [Route("/WeatherUpload")]
        public ActionResult WeatherUpload()
        {
            return View();
        }

        [HttpPost]
        [Route("/WeatherUpload/Upload")]
        public ActionResult WeatherUploadFiles()
        {
            var files = Request.Form.Files;
            foreach (var file in files)
            {
                // Проверяем, что файл существует и не пуст
                if (file.Length > 0)
                {
                    try
                    {
                        // Получаем имя файла
                        string fileName = Path.GetFileName(file.FileName);

                        // Сохраняем файл на сервере
                        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads", fileName);

                        if (!System.IO.File.Exists(path))
                        {
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                file.CopyTo(stream);
                            }

                            var weatherData = _uploadService.ParseWeatherXls(path);
                            _uploadService.UploadWeatherToDb(weatherData);
                        }
                    }
                    catch (Exception ex)
                    {
                        return Json("Ошибка при загрузке файла: " + ex.Message);
                    }
                }
            }

            return Json("Все файлы успешно загружены");
        }
    }
}
