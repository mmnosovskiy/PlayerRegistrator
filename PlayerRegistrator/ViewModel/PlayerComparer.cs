using PlayerRegistrator.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerRegistrator.ViewModel
{
    public class PlayerComparer : IEqualityComparer<Player>
    {
        public bool Equals(Player x, Player y)
        {
            return x.Name == y.Name && x.Number == y.Number;
        }

        public int GetHashCode(Player obj)
        {
            return obj.GetHashCode();
        }
    }
}
