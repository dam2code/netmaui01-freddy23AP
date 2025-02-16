namespace WeatherClient.Models
{
    public class WeatherData
    {
        public WeatherType Condition { get; set; } = WeatherType.Sunny;
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public int Precipitation { get; set; }
        public int Wind { get; set; }
    }
}
