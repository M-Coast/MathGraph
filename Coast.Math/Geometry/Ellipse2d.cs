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
        //椭圆
        //Coefficients
        //
        //标准方程
        //  x^2/a^2 + x^2/b^2 = 1   长轴/2
        //旋转
        //  x = x*cos(th) - y*sin(th)
        //  y = x*sin(th) + y*cos(th)
        //平移
        //  x = x + cx
        //  y = y + cy


        public double cx
        {
            get { return _cx; }
            set { _cx = value; CalculateGeneralEquationCoefficients(); }
        }
        public double cy
        {
            get { return _cy; }
            set { _cy = value; CalculateGeneralEquationCoefficients(); }
        }
        public double ra
        {
            get { return _ra; }
            set { _ra = value; CalculateGeneralEquationCoefficients(); }
        }
        public double rb
        {
            get { return _rb; }
            set { _rb = value; CalculateGeneralEquationCoefficients(); }
        }
        public double th
        {
            get { return _th; }
            set { _th = value; CalculateGeneralEquationCoefficients(); }
        }

        public double CenterX { get { return _cx; } }
        public double CenterY { get { return _cy; } }
        public double RadiusA { get { return _ra; } }
        public double RadiusB { get { return _rb; } }
        public double Rotation { get { return _th; } }

        private double _cx = 0;
        private double _cy = 0;
        private double _ra = 0;
        private double _rb = 0;
        private double _th = 0;
        

        //Ax^2 + 2Bxy + Cy^2  + Dx + Ey + F =0   <-  F = 1 
        public double A { get { return _A; } }
        public double B { get { return _B; } }
        public double C { get { return _C; } }
        public double D { get { return _D; } }
        public double E { get { return _E; } }
        public double F { get { return _F; } }

        private double _A = 0.0;
        private double _B = 0.0;
        private double _C = 0.0;
        private double _D = 0.0;
        private double _E = 0.0;
        private double _F = 0.0;



        public Ellipse2d() { }

        public Ellipse2d(double A, double B, double C, double D, double E, double F)
        {

            double __T_CX_01_ = 4 * A * C - B * B;
            double __T_CY_01_ = 4 * A * C - B * B;
            double __T_RA_01_ = A + C + System.Math.Sqrt((A - C) * (A - C) + B * B);
            double __T_RB_01_ = A + C - System.Math.Sqrt((A - C) * (A - C) + B * B);

            if (__T_CX_01_.WeakEquals(0.0)) return;
            if (__T_CY_01_.WeakEquals(0.0)) return;
            if (__T_RA_01_.WeakEquals(0.0)) return;
            if (__T_RB_01_.WeakEquals(0.0)) return;

            double __T_CX_ = (B * E - 2 * C * D) / __T_CX_01_;
            double __T_CY_ = (B * D - 2 * A * E) / __T_CY_01_;

            double __T_RA_02_ = A * __T_CX_ * __T_CX_ + C * __T_CY_ * __T_CY_ + B * __T_CX_ * __T_CY_ - 1;
            double __T_RB_02_ = A * __T_CX_ * __T_CX_ + C * __T_CY_ * __T_CY_ + B * __T_CX_ * __T_CY_ - 1;

            if (__T_RA_02_.WeakEquals(0.0)) return;
            if (__T_RA_02_.WeakEquals(0.0)) return;


            _cx = __T_CX_;
            _cy = __T_CY_;
            _ra = System.Math.Sqrt(2.0 * __T_RA_02_ / __T_RA_01_);
            _rb = System.Math.Sqrt(2.0 * __T_RB_02_ / __T_RB_01_);

            //
            if(_ra<_rb)
            {
                double temp = _ra;
                _ra = _rb;
                _rb = temp;
            }

            if (A.WeakEquals(C))
            {
                if (A <= C) _th = 90.0;
                else _th = -90.0;
            }
            else
            {
                _th = -(System.Math.Atan(B / (A - C))) / System.Math.PI * 180;
                if (th < 0) th = th + 180;
            }

            _A = A;
            _B = B;
            _C = C;
            _D = D;
            _E = E;
            _F = F;
        }

        public static bool ValidateGeneralEquationCoefficients(double A, double B, double C, double D, double E, double F)
        {
            double __T_CX_01_ = 4 * A * C - B * B;
            double __T_CY_01_ = 4 * A * C - B * B;
            double __T_RA_01_ = A + C + System.Math.Sqrt((A - C) * (A - C) + B * B);
            double __T_RB_01_ = A + C - System.Math.Sqrt((A - C) * (A - C) + B * B);

            if (__T_CX_01_.WeakEquals(0.0)) return false;
            if (__T_CY_01_.WeakEquals(0.0)) return false;
            if (__T_RA_01_.WeakEquals(0.0)) return false;
            if (__T_RB_01_.WeakEquals(0.0)) return false;

            double __T_CX = (B * E - 2 * C * D) / __T_CX_01_;
            double __T_CY = (B * D - 2 * A * E) / __T_CY_01_;

            double __T_RA_02_ = A * __T_CX * __T_CX + C * __T_CY * __T_CY + B * __T_CX * __T_CY - 1;
            double __T_RB_02_ = A * __T_CX * __T_CX + C * __T_CY * __T_CY + B * __T_CX * __T_CY - 1;

            if (__T_RA_02_.WeakEquals(0.0)) return false;
            if (__T_RA_02_.WeakEquals(0.0)) return false;

            return true;
        }


        // Calculate cx,cy,ra,ab,th   ->   A,B,C,D,E,F
        private void CalculateGeneralEquationCoefficients()
        {
            // Update later

        }


    }
}




