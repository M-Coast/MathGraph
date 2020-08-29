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
    public class Distance
    {
        public static double Point2PlaneSigned(Vector3 point, double A, double B, double C, double D)
        {
            double sqrt = System.Math.Sqrt(A * A + B * B + C * C);

            if (sqrt == 0) return double.NaN;

            return (A * point.X + B * point.Y + C * point.Z + D) / sqrt;
        }

        public static double Point2Plane(Vector3 point, double A, double B, double C, double D)
        {
            double sqrt = System.Math.Sqrt(A * A + B * B + C * C);

            if (sqrt == 0) return double.NaN;

            return (A * point.X + B * point.Y + C * point.Z + D) / sqrt;
        }
    }
}
