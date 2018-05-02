using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{
    public class Wind
    {
        public double Speed { get; set; }
        public int Degree { get; set; }

        public override string ToString()
        {
            return "{Speed: "+Speed+", Degree: "+Degree+"}";
        }
    }
}
