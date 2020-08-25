using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coast.Math
{
    //Line2d Fitter
    //Mathematics Method: Least Square Method
    //Calling:
    //  1.new this object
    //  2.assign Points
    //  3.call Solve function
    //  4.check if errored
    //  5.get results
    //
    public class LineFitter
    {
        //Line2d Points List
        public List<Vector2> Points
        {
            get { return _points; }
            set { _points = value; }
        }

        //Line2d Points Array
        public Vector2[] PointSource
        {
            set { _points = new List<Vector2>(value); }
        }

        //2DLine Coefficients
        //y=ax+b
        public double a { get { return _a; } }
        public double b { get { return _b; } }

        //2DLine Coefficients
        //Ax+By+C=0
        public double A { get { return _A; } }
        public double B { get { return _B; } }
        public double C { get { return _C; } }

         
        public Line2d Line { get { return new Line2d(A, B, C); } }
        
        public bool Errored { get; private set; } = false;
        public LineFitterErrorCode ErrorCode { get; private set; } = LineFitterErrorCode.NoError;

        private double _a = 0;
        private double _b = 0;
        private double _A = 0;
        private double _B = 0;
        private double _C = 0;
        private List<Vector2> _points = null;


        public LineFitter()
        {

        }

        public LineFitter(List<Vector2> points)
        {
            Points = points;
        }

        public LineFitter(Vector2[] pointsSource)
        {
            PointSource = pointsSource;
        }


        //public PlaneFitter(List<Vector2> points, bool solve)
        //{
        //    Points = points;
        //    if (solve) Solve();
        //}

        //public PlaneFitter(Vector2[] pointsSource, bool solve)
        //{
        //    PointSource = pointsSource;
        //    if (solve) Solve();
        //}


        public bool Solve()
        {
            Reset();

            if (Points == null)
            {
                SetError(LineFitterErrorCode.PointsCollectionIsNull);
                return false;
            }
            if (Points.Count < 2)
            {
                SetError(LineFitterErrorCode.PointsCountLessThan2);
                return false;
            }

            MatrixNxM matrix = SetupMatrix();

            LinearEquations LE = new LinearEquations(matrix);

            LE.Solve();

            if (LE.Errored)
            {
                SetError(LineFitterErrorCode.SolveEquationsError);
                return false;
            }

            _a = LE.Result[0];
            _b = LE.Result[1];

            _A = _a * -1;
            _B = 1;
            _C = _b * -1;

            return true;

        }

        private MatrixNxM SetupMatrix()
        {
            MatrixNxM matrix = new MatrixNxM(2, 3);

            for (int i = 0; i < Points.Count; i++)
            {
                matrix[0, 0] = matrix[0, 0] + Points[i].X * Points[i].X;
                matrix[0, 1] = matrix[0, 1] + Points[i].X;
                matrix[0, 2] = matrix[0, 2] + Points[i].X * Points[i].Y;

                matrix[1, 0] = matrix[1, 0] + Points[i].X;
                matrix[1, 1] = matrix[1, 1] + 1;
                matrix[1, 2] = matrix[1, 2] + Points[i].Y;

            }
            return matrix;
        }

        private void Reset()
        {
            Errored = false;
            ErrorCode = LineFitterErrorCode.NoError;

            _a = 0;
            _b = 0;
            _A = 0;
            _B = 0;
            _C = 0;
        }

        private void SetError(LineFitterErrorCode errorCode)
        {
            Errored = true;
            ErrorCode = errorCode;
        }

    }


}
