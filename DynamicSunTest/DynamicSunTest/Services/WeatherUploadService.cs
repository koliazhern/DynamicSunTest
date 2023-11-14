using DynamicSunTest.Models;
using NPOI;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace DynamicSunTest.Services
{
    public sealed class WeatherUploadService
    {
        private readonly WeatherDbContext _context;
        public WeatherUploadService(WeatherDbContext context)
        {
            _context = context;
        }
        public List<WeatherData> ParseWeatherXls(string pathToFile)
        {
            List<WeatherData> weatherList = new();
            // Открытие существующей рабочей книги
            IWorkbook workbook;
            try
            {
                using (FileStream fileStream = new FileStream(pathToFile, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(fileStream);
                }

                for (int sheetNumber = 0; sheetNumber < workbook.NumberOfSheets; sheetNumber++)
                {
                    // Получение листа
                    ISheet sheet = workbook.GetSheetAt(sheetNumber);
                    // Начинаем отсчет с 5й строки
                    for (int rowNumber = 4; rowNumber < sheet.PhysicalNumberOfRows; rowNumber++)
                    {
                        IRow row = sheet.GetRow(rowNumber);
                        WeatherData weatherData = new WeatherData
                        {
                            Date = Convert.ToDateTime(row.GetCell(0).StringCellValue),
                            Time = Convert.ToDateTime(row.GetCell(1).StringCellValue),
                            Temperature = row.GetCell(2).CellType == CellType.String ? Convert.ToDouble(row.GetCell(2).StringCellValue) : Convert.ToDouble(row.GetCell(2).NumericCellValue),
                            Humidity = row.GetCell(3).CellType == CellType.String ? Convert.ToInt32(row.GetCell(3).StringCellValue) : Convert.ToInt32(row.GetCell(3).NumericCellValue),
                            DewPoint = row.GetCell(4).CellType == CellType.String ? Convert.ToDouble(row.GetCell(4).StringCellValue) : Convert.ToDouble(row.GetCell(4).NumericCellValue),
                            AtmospherePressure = row.GetCell(5).CellType == CellType.String ? Convert.ToInt32(row.GetCell(5).StringCellValue) : Convert.ToInt32(row.GetCell(5).NumericCellValue),
                            WindDirection = row.GetCell(6).CellType == CellType.String ? row.GetCell(6).StringCellValue : row.GetCell(6).NumericCellValue.ToString(),
                            WindSpeed = row.GetCell(7).CellType == CellType.String ? null : Convert.ToInt32(row.GetCell(7).NumericCellValue),
                            Cloudy = row.GetCell(8).CellType == CellType.String ? null : Convert.ToInt32(row.GetCell(8).NumericCellValue),
                            CloudBase = row.GetCell(9).CellType == CellType.String ? null : Convert.ToInt32(row.GetCell(9).NumericCellValue),
                            WeatherConditions = row.GetCell(10).CellType == CellType.String ? row.GetCell(10).StringCellValue : row.GetCell(10).NumericCellValue.ToString(),
                        };
                        weatherList.Add(weatherData);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Файл не подлежит разбору");
            }

            return weatherList;
        }

        public void UploadWeatherToDb(List<WeatherData> weatherData)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.WeatherData.AddRange(weatherData);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Upload was failed", e);
                    transaction.Rollback();
                }
                
            }
        }
    }
}
