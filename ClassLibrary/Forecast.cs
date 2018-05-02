using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Forecast
    {
        public string City { get; set; }
        public Coordainte Coordinates { get; set; }
        public string Country { get; set; }
        public double Temperature { get; set; }
        public int Humidity { get; set; }
        public int Pressure { get; set; }
        public Wind Wind { get; set; }
        public int Cloud { get; set; }
        public int Visibility { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"City: {City}");
            sb.AppendLine($"Coordinates: {Coordinates}");
            sb.AppendLine($"Country: {Country}");
            sb.AppendLine($"Temperature: {Temperature}");
            sb.AppendLine($"Humidity: {Humidity}");
            sb.AppendLine($"Pressure: {Pressure}");
            sb.AppendLine($"Wind: {Wind}");
            sb.AppendLine($"Cloud: {Cloud}");
            sb.AppendLine($"Visibility: {Visibility}");
            sb.AppendLine($"Sunrise: {Sunrise.ToLongDateString()}");
            sb.AppendLine($"Sunset: {Sunset.ToLongDateString()}");
            return sb.ToString();
        }


        public static ForecastAirTrafficControlCenter GenerateForecastAirTrafficControlCenter(Forecast forecast)
        {
            return new ForecastAirTrafficControlCenter()
            {
                City = forecast.City,
                Coordinates = forecast.Coordinates,
                Country = forecast.Country,
                Temperature = forecast.Temperature,
                Humidity = forecast.Humidity,
                Pressure = forecast.Pressure,
                Wind = forecast.Wind,
                Cloud = forecast.Cloud,
                Visibility = forecast.Visibility
            };
        }
    }
}
