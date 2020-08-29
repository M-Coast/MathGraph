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
    public class Plane
    {
        // Ax+By+Cz+D=0
        // Normal=Vector3(A,B,C) 
        // Positive Direction: z-upward
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }

        // z=ax+by+c
        public double a { get { return A * -1; } }
        public double b { get { return B * -1; } }
        public double c { get { return D * -1; } }

        public Vector3 Normal
        {
            get { return new Vector3(A, B, C); }
        }

        public bool Errored { get; private set; } = false;

        public Plane(double A, double B, double C, double D)
        {
            this.A = A;
            this.B = B;
            this.C = C;
            this.D = D;
        }

        public Plane(double a, double b, double c)
        {
            A = a * -1;     // a=A*-1
            B = b * -1;     // b=B*-1
            C = 1;          // 0
            D = c * -1;     // c=D*-1
        }

        public Plane(Vector3 p1,Vector3 normal)
        {

        }

        public Plane(Vector3 p1, Vector3 p2, Vector3 p3)
        {

        }

        //Set a,b,c
        //Not allowed to set plane a,b,c directly after initialized
        //Use this way to set a,b,c
        public void Set(double a, double b, double c)
        {
            A = a * -1;     // a=A*-1
            B = b * -1;     // b=B*-1
            C = 1;          // 0
            D = c * -1;     // c=D*-1
        }

        public void Set(Vector3 p1, Vector3 normal)
        {

        }

        public void Set(Vector3 p1, Vector3 p2, Vector3 p3)
        {

        }

        public double GetDistance(Vector3 point)
        {
            double sqrt = System.Math.Sqrt(A * A + B * B + C * C);

            if (sqrt == 0) return double.NaN;

            return (A * point.X + B * point.Y + C * point.Z + D) / sqrt;
        }

        public double GetSignedDistance(Vector3 point)
        {
            double sqrt = System.Math.Sqrt(A * A + B * B + C * C);

            if (sqrt == 0) return double.NaN;

            return (A * point.X + B * point.Y + C * point.Z + D) / sqrt;
        }

        public static double GetDistance(Plane plane, Vector3 point)
        {
            double A = plane.A;
            double B = plane.B;
            double C = plane.C;
            double D = plane.D;

            double sqrt = System.Math.Sqrt(A * A + B * B + C * C);

            if (sqrt == 0) return double.NaN;

            return (A * point.X + B * point.Y + C * point.Z + D) / sqrt;
        }

        public static double GetSignedDistance(Plane plane, Vector3 point)
        {
            double A = plane.A;
            double B = plane.B;
            double C = plane.C;
            double D = plane.D;

            double sqrt = System.Math.Sqrt(A * A + B * B + C * C);

            if (sqrt == 0) return double.NaN;

            return (A * point.X + B * point.Y + C * point.Z + D) / sqrt;
        }

        public double GetZ(double x, double y)
        {
            return a * x + b * y + c;
        }

    }
}
