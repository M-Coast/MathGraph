using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coast.Math
{
    [Serializable]
    public struct Vector1
    {
        public double V { set; get; }
        public Vector1(double v)
        {
            V = v;
        }
    }

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

    [Serializable]
    public struct Vector3
    {
        public double X { set; get; }
        public double Y { set; get; }
        public double Z { set; get; }

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
    }

    [Serializable]
    public struct Vector4
    {
        public double X { set; get; }
        public double Y { set; get; }
        public double Z { set; get; }
        public double W { set; get; }
        public Vector4(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            W = 1;
        }
        public Vector4(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}
