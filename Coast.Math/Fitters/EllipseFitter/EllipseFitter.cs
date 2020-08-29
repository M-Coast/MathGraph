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
    //Fitter
    //Mathematics Method: Least Square Method
    //Calling:
    //  1.new this object
    //  2.assign Points
    //  3.call Solve function
    //  4.check if errored
    //  5.get results
    //
    public class EllipseFitter
    {
        //Points List
        public List<Vector2> Points
        {
            get { return _points; }
            set { _points = value; }
        }

        //Points Array
        public Vector2[] PointSource
        {
            set { _points = new List<Vector2>(value); }
        }

        //Coefficients
        //Ax^2 + 2Bxy + Cy^2  + Dx + Ey +F =0
        public double A { get { return _A; } }
        public double B { get { return _B; } }
        public double C { get { return _C; } }
        public double D { get { return _D; } }
        public double E { get { return _E; } }
        //public double F { get { return _F; } }

        public bool Errored { get; private set; } = false;
        public EllipseFitterErrorCode ErrorCode { get; private set; } = EllipseFitterErrorCode.NoError;


        private double _A = 0;
        private double _B = 0;
        private double _C = 0;
        private double _D = 0;
        private double _E = 0;
        //private double _F = 0;

        private List<Vector2> _points = null;

        public Ellipse2d Ellipse { get { return new Ellipse2d() { A = _A, B = _B, C = _C, D = _D, E = _E }; } }

        public EllipseFitter()
        {

        }

        public EllipseFitter(List<Vector2> points)
        {
            Points = points;
        }

        public EllipseFitter(Vector2[] pointsSource)
        {
            PointSource = pointsSource;
        }


        public bool Solve()
        {
            Reset();

            if (Points == null)
            {
                SetError(EllipseFitterErrorCode.PointsCollectionIsNull);
                return false;
            }
            if (Points.Count < 3)
            {
                SetError(EllipseFitterErrorCode.PointsCountLessThan3);
                return false;
            }

            MatrixNxM matrix = SetupMatrix();

            LinearEquations LE = new LinearEquations(matrix);

            LE.Solve();

            if (LE.Errored)
            {
                SetError(EllipseFitterErrorCode.SolveEquationsError);
                return false;
            }

            _A = LE.Result[0];
            _B = LE.Result[1];
            _C = LE.Result[2];
            _D = LE.Result[3];
            _E = LE.Result[4];

            //_a = -_D / 2.0;
            //_b = -_E / 2.0;

            //double n = _a * _a + _b * _b - _F;

            //if (n < 0)
            //{
            //    SetError(EllipseFitterErrorCode.RadiusError);
            //    return false;
            //}

            //_R = System.Math.Sqrt(n);

            return true;

        }


        private MatrixNxM SetupMatrix()
        {
            MatrixNxM matrix = new MatrixNxM(5, 6);

            for (int i = 0; i < Points.Count; i++)
            {
                double X = Points[i].X;
                double Y = Points[i].Y;

                matrix[0, 0] += X * X * X * X;
                matrix[0, 1] += X * X * X * Y;
                matrix[0, 2] += X * X * Y * Y;
                matrix[0, 3] += X * X * X;
                matrix[0, 4] += X * X * Y;
                matrix[0, 5] += X * X * -1;

                matrix[1, 0] += X * X * X * Y;
                matrix[1, 1] += X * X * Y * Y;
                matrix[1, 2] += X * Y * Y * Y;
                matrix[1, 3] += X * X * Y;
                matrix[1, 4] += X * Y * Y;
                matrix[1, 5] += X * Y * -1;

                matrix[2, 0] += X * X * Y * Y;
                matrix[2, 1] += X * Y * Y * Y;
                matrix[2, 2] += Y * Y * Y * Y;
                matrix[2, 3] += X * Y * Y;
                matrix[2, 4] += Y * Y * Y;
                matrix[2, 5] += Y * Y * -1;

                matrix[3, 0] += X * X * X;
                matrix[3, 1] += X * X * Y;
                matrix[3, 2] += X * Y * Y;
                matrix[3, 3] += X * X;
                matrix[3, 4] += X * Y;
                matrix[3, 5] += X * -1;

                matrix[4, 0] += X * X * Y;
                matrix[4, 1] += X * Y * Y;
                matrix[4, 2] += Y * Y * Y;
                matrix[4, 3] += X * Y;
                matrix[4, 4] += Y * Y;
                matrix[4, 5] += Y * -1;

            }
            return matrix;
        }

        private void Reset()
        {
            Errored = false;
            ErrorCode = EllipseFitterErrorCode.NoError;

            _A = 0;
            _B = 0;
            _C = 0;
            _D = 0;
            _E = 0;
            //_F = 0;
        }

        private void SetError(EllipseFitterErrorCode errorCode)
        {
            Errored = true;
            ErrorCode = errorCode;
        }

    }


}
