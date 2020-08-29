using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coast.Math
{
    public class Circle2d
    {
        //Coefficients
        //(x-cx)^2 + (y-cy)^2 =R^2
        public double cx { get; set; }
        public double cy { get; set; }
        public double r { get; set; }

        //Coefficients
        //x^2 + y^2 + Dx + Ey +F =0
        public double D { get { return -2 * cx; } }
        public double E { get { return -2 * cy; } }
        public double F { get { return cx * cx + cy * cy - r * r; } }

        public double CenterX { get { return cx; } }
        public double CenterY { get { return cy; } }
        public double Radius { get { return cy; } }

        public Circle2d() { }

        public Circle2d(double cx, double cy, double r)
        {
            this.cx = cx;
            this.cy = cy;
            this.r = r;
        }

        public Circle2d(Vector2 center, double R)
        {
            this.cx = center.X;
            this.cy = center.Y;
            this.r = R;
        }
    }
}
