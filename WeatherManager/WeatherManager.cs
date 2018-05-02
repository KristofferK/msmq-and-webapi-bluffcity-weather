using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace WeatherManager
{
    internal class WeatherManager
    {
        private const string API_KEY = WeatherManagerCredentials.API_KEY;

        public Forecast GetForecast(string location)
        {
            var xml = GetForecastXml(location);

            CultureInfo ci = new CultureInfo("en-GB");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;

            var forecast = new Forecast();
            forecast.City = xml.SelectSingleNode("/current/city").Attributes["name"].Value;
            forecast.Cloud = int.Parse(xml.SelectSingleNode("/current/clouds").Attributes["value"].Value);
            forecast.Coordinates = new Coordainte()
            {
                Latitude = double.Parse(xml.SelectSingleNode("/current/city/coord").Attributes["lat"].Value),
                Longitude = double.Parse(xml.SelectSingleNode("/current/city/coord").Attributes["lon"].Value)
            };
            forecast.Country = xml.SelectSingleNode("/current/city/country").InnerText;
            forecast.Humidity = int.Parse(xml.SelectSingleNode("/current/humidity").Attributes["value"].Value);
            forecast.Pressure = int.Parse(xml.SelectSingleNode("/current/pressure").Attributes["value"].Value);
            forecast.Sunrise = DateTimeFromString(xml.SelectSingleNode("/current/city/sun").Attributes["rise"].InnerText);
            forecast.Sunset = DateTimeFromString(xml.SelectSingleNode("/current/city/sun").Attributes["set"].InnerText);
            forecast.Temperature = (double.Parse(xml.SelectSingleNode("/current/temperature").Attributes["value"].Value) - 32) / 1.8;
            // forecast.Visibility = 
            forecast.Wind = new Wind()
            {
                Degree = int.Parse(xml.SelectSingleNode("/current/wind/direction").Attributes["value"].Value),
                Speed = double.Parse(xml.SelectSingleNode("/current/wind/speed").Attributes["value"].Value),
            };
            return forecast;
        }


        private XmlDocument GetForecastXml(string location)
        {
            location = WebUtility.UrlEncode(location);
            return GetSourceAsXml("http://api.openweathermap.org/data/2.5/weather?q=" + location + "&mode=xml&units=imperial&APPID=" + API_KEY);
        }

        private XmlDocument GetSourceAsXml(string url)
        {
            using (var wc = new WebClient())
            {
                string source = wc.DownloadString(url);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(source);
                return xml;
            }
        }

        public static DateTime DateTimeFromString(string s)
        {
            var match = Regex.Match(s, "([0-9]+)-([0-9]+)-([0-9]+)T([0-9]+):([0-9]+):([0-9]+)").Groups;
            var year = int.Parse(match[1].Value);
            var month = int.Parse(match[2].Value);
            var day = int.Parse(match[3].Value);
            var hour = int.Parse(match[4].Value);
            var minute = int.Parse(match[5].Value);
            var second = int.Parse(match[6].Value);
            return new DateTime(year, month, day, hour, minute, second);
        }
    }
}
