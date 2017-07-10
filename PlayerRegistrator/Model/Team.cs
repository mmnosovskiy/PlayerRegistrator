using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PlayerRegistrator
{
    public class Team
    {
        public Color NumberColor { get; set; }
        public Color ShirtColor { get; set; }
        public List<Tactics> Tactics { get; set; }
    }
}
