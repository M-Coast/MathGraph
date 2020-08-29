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
    public class Ellipse2d
    {
        //Coefficients
        //
        //标准方程
        //  x^2/a^2 + x^2/b^2 = 1
        //旋转
        //  x = x*cos(th) - y*sin(th)
        //  y = x*sin(th) + y*cos(th)
        //平移
        //  x = x + cx
        //  y = y + cy
        public double ra { set; get; }
        public double rb { set; get; }
        public double th { set; get; }
        public double cx { set; get; }
        public double cy { set; get; }


        //Coefficients
        //Ax^2 + 2Bxy + Cy^2  + Dx + Ey +F =0
        public double A { get; set; }
        public double B { get; set; }
        public double C { get; set; }
        public double D { get; set; }
        public double E { get; set; }

        //public Ellipse2d() { }

        //public Ellipse2d(double a, double b, double R)
        //{
        //    this.a = a;
        //    this.b = b;
        //    this.R = R;
        //}

        //public Ellipse2d(Vector2 center, double R)
        //{
        //    this.a = center.X;
        //    this.b = center.Y;
        //    this.R = R;
        //}
    }
}
