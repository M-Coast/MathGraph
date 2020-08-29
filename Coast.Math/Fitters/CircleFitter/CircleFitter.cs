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
    public class CircleFitter
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
        //(x-cx)^2 + (y-cy)^2 =R^2
        public double cx { get { return _cx; } }
        public double cy { get { return _cy; } }
        public double r { get { return _r; } }

        //Coefficients
        //x^2 + y^2 + Dx + Ey +F =0
        public double D { get { return _D; } }
        public double E { get { return _E; } }
        public double F { get { return _F; } }

        public bool Errored { get; private set; } = false;
        public CircleFitterErrorCode ErrorCode { get; private set; } = CircleFitterErrorCode.NoError;

        private double _cx = 0;
        private double _cy = 0;
        private double _r = 0;
        private double _D = 0;
        private double _E = 0;
        private double _F = 0;

        private List<Vector2> _points = null;

        public Circle2d Circle { get { return new Circle2d(cx, cy, r); } }

        public CircleFitter()
        {

        }

        public CircleFitter(List<Vector2> points)
        {
            Points = points;
        }

        public CircleFitter(Vector2[] pointsSource)
        {
            PointSource = pointsSource;
        }


        public bool Solve()
        {
            Reset();

            if (Points == null)
            {
                SetError(CircleFitterErrorCode.PointsCollectionIsNull);
                return false;
            }
            if (Points.Count < 3)
            {
                SetError(CircleFitterErrorCode.PointsCountLessThan3);
                return false;
            }

            MatrixNxM matrix = SetupMatrix();

            LinearEquations LE = new LinearEquations(matrix);

            LE.Solve();

            if (LE.Errored)
            {
                SetError(CircleFitterErrorCode.SolveEquationsError);
                return false;
            }

            _D = LE.Result[0];
            _E = LE.Result[1];
            _F = LE.Result[2];

            _cx = -_D / 2.0;
            _cy = -_E / 2.0;

            double n = _cx * _cx + _cy * _cy - _F;

            if (n < 0)
            {
                SetError(CircleFitterErrorCode.RadiusError);
                return false;
            }

            _r = System.Math.Sqrt(n);

            return true;

        }


        private MatrixNxM SetupMatrix()
        {
            MatrixNxM matrix = new MatrixNxM(3, 4);

            for (int i = 0; i < Points.Count; i++)
            {
                matrix[0, 0] = matrix[0, 0] + Points[i].X * Points[i].X;
                matrix[0, 1] = matrix[0, 1] + Points[i].X * Points[i].Y;
                matrix[0, 2] = matrix[0, 2] + Points[i].X;
                matrix[0, 3] = matrix[0, 3] + (Points[i].X * Points[i].X * Points[i].X + Points[i].X * Points[i].Y * Points[i].Y) * -1;

                matrix[1, 0] = matrix[1, 0] + Points[i].X * Points[i].Y;
                matrix[1, 1] = matrix[1, 1] + Points[i].Y * Points[i].Y;
                matrix[1, 2] = matrix[1, 2] + Points[i].Y;
                matrix[1, 3] = matrix[1, 3] + (Points[i].X * Points[i].X * Points[i].Y + Points[i].Y * Points[i].Y * Points[i].Y) * -1;

                matrix[2, 0] = matrix[2, 0] + Points[i].X;
                matrix[2, 1] = matrix[2, 1] + Points[i].Y;
                matrix[2, 2] = matrix[2, 2] + 1;
                matrix[2, 3] = matrix[2, 3] + (Points[i].X * Points[i].X + Points[i].Y * Points[i].Y) * -1;
            }
            return matrix;
        }

        private void Reset()
        {
            Errored = false;
            ErrorCode = CircleFitterErrorCode.NoError;

            _cx = 0;
            _cy = 0;
            _r = 0;
            _D = 0;
            _E = 0;
            _F = 0;
        }

        private void SetError(CircleFitterErrorCode errorCode)
        {
            Errored = true;
            ErrorCode = errorCode;
        }

    }


}
