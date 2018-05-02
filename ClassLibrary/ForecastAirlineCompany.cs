using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class ForecastAirlineCompany
    {
        public string City { get; set; }
        public string Country { get; set; }
        public double Temperature { get; set; }
        public int Cloud { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"City: {City}");
            sb.AppendLine($"Country: {Country}");
            sb.AppendLine($"Temperature: {Temperature}");
            sb.AppendLine($"Cloud: {Cloud}");
            return sb.ToString();
        }
    }
}
