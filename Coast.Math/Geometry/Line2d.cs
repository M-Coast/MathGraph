/*****************************************************************************************

    MathGraph
    
    Copyright (C)  Coast


    AUTHOR      :  Coast   
    DATE        :  2020/8/27
    DESCRIPTION :  

 *****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coast.Math
{
    public class Line2d
    {
        // Ax+By+C=0
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }

        // y=ax+b
        public double a { get { return A * -1; } }
        public double b { get { return C * -1; } }

        public Vector2 Direction
        {
            get { return new Vector2(A, B); }
        }

        public bool Errored { get; private set; } = false;

        public Line2d(double A, double B, double C)
        {
            this.A = A;
            this.B = B;
            this.C = C;
        }

        public Line2d(double a, double b )
        {
            A = a * -1;     // a=A*-1
            C = 1;          // 0
            C = b * -1;     // c=D*-1
        }

        public Line2d(Vector2 p1, double dx,double dy)
        {

        }

        public Line2d(Vector2 p1, Vector2 p2 )
        {

        }

        public void Set(double a, double b, double c)
        {
            A = a * -1;     // a=A*-1
            C = 1;          // 0
            C = b * -1;     // c=D*-1
        }

        public void Set(Vector2 p1, Vector2 normal)
        {

        }

        public void Set(Vector2 p1, Vector2 p2, Vector2 p3)
        {

        }

        public double GetDistance(Vector2 point)
        {
            double sqrt = System.Math.Sqrt(A * A + B * B);

            if (sqrt == 0) return double.NaN;

            return (A * point.X + B * point.Y + C) / sqrt;
        }

        public double GetSignedDistance(Vector2 point)
        {
            double sqrt = System.Math.Sqrt(A * A + B * B);

            if (sqrt == 0) return double.NaN;

            return (A * point.X + B * point.Y + C) / sqrt;
        }

        public static double GetDistance(Line2d line, Vector2 point)
        {
            double A = line.A;
            double B = line.B;
            double C = line.C;

            double sqrt = System.Math.Sqrt(A * A + B * B);

            if (sqrt == 0) return double.NaN;

            return (A * point.X + B * point.Y + C) / sqrt;
        }

        public static double GetSignedDistance(Line2d line, Vector2 point)
        {
            double A = line.A;
            double B = line.B;
            double C = line.C;

            double sqrt = System.Math.Sqrt(A * A + B * B);

            if (sqrt == 0) return double.NaN;

            return (A * point.X + B * point.Y + C) / sqrt;
        }

        public double GetY(double x)
        {
            return a * x + b;
        }
    }
}
