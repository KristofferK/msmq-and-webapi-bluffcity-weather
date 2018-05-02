using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class WeatherCondition
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
            sb.AppendLine($"Sunrise: {Sunrise.ToLongDateString()} {Sunrise.ToLongTimeString()}");
            sb.AppendLine($"Sunset: {Sunset.ToLongDateString()} {Sunset.ToLongTimeString()}");
            return sb.ToString();
        }

        public static WeatherConditionAirTrafficControlCenter GenerateWeatherConditionAirTrafficControlCenter(WeatherCondition weather)
        {
            return new WeatherConditionAirTrafficControlCenter()
            {
                City = weather.City,
                Coordinates = weather.Coordinates,
                Country = weather.Country,
                Temperature = weather.Temperature,
                Humidity = weather.Humidity,
                Pressure = weather.Pressure,
                Wind = weather.Wind,
                Cloud = weather.Cloud,
                Visibility = weather.Visibility
            };
        }

        public static WeatherConditionAirportInformationCenter GenerateWeatherConditionAirportInformationCenter(WeatherCondition weather)
        {
            return new WeatherConditionAirportInformationCenter()
            {
                City = weather.City,
                Country = weather.Country,
                Temperature = weather.Temperature,
                Sunrise = weather.Sunrise,
                Sunset = weather.Sunset
            };
        }

        public static WeatherConditionAirlineCompany GenerateWeatherConditionAirlineCompany(WeatherCondition weather)
        {
            return new WeatherConditionAirlineCompany()
            {
                City = weather.City,
                Country = weather.Country,
                Temperature = weather.Temperature,
                Cloud = weather.Cloud
            };
        }
    }
}
