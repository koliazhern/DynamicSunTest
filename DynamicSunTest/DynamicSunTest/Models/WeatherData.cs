namespace DynamicSunTest.Models
{
    public sealed class WeatherData
    {
        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public double Temperature { get; set; }

        public int Humidity { get; set; }

        public double DewPoint { get; set; }

        public int AtmospherePressure { get; set; }

        public string WindDirection { get; set; }

        public int? WindSpeed { get; set; }

        public int? Cloudy { get; set; }

        public int? CloudBase { get; set; }

        public string WeatherConditions { get; set; }
    }
}
