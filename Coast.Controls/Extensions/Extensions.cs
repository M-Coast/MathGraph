using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coast.Controls
{
    public static class DoubleExtensions
    {
        public const double WeakEqualityLimit = 1E-8;
        public static bool WeakEquals(this double self, double value)
        {
            return Math.Abs(self - value) <= WeakEqualityLimit ? true : false;
        }
    }
}
