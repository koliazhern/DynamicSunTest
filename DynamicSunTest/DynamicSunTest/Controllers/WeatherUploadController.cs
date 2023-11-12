using System.Net;
using System.Web;
using DynamicSunTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSunTest.Controllers
{
    public class WeatherUploadController : Controller
    {
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

                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }

                        var service = new WeatherUploadService().ParseWeatherXls(path);
                        var j = 1;
                    }
                    catch (Exception ex)
                    {
                        // Обработка ошибок при сохранении файла
                        return Json("Ошибка при загрузке файла: " + ex.Message);
                    }
                }
            }

            return Json("Все файлы успешно загружены");
        }
    }
}
