using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coast.Math
{
    [Serializable]
    public struct Vector2
    {
        public double X { set; get; }
        public double Y { set; get; }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }
    }
}
