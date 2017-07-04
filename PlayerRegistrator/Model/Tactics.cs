using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerRegistrator.Model
{
    public class Tactics : List<Place>
    {
        public int Half { get; set; }
        public int TimeVideo { get; set; }
    }
}
