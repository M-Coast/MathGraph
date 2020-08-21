using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coast.Math
{
    //Plane Fitter
    //Mathematics Method: Least Square Method
    //Calling:
    //  1.new this object
    //  2.assign Points
    //  3.call Solve function
    //  4.check if errored
    //  5.get results
    //
    public class PlaneFitter
    {
        //Plane Points List
        public List<Vector3> Points
        {
            get { return _points; }
            set { _points = value; }
        }

        //Plane Points Array
        public Vector3[] PointSource
        {
            set { _points = new List<Vector3>(value); }
        }

        //Plane Coefficients
        //z=ax+by+c
        public double a { get { return _a; } }
        public double b { get { return _b; } }
        public double c { get { return _c; } }

        //Plane Coefficients
        //Ax+By+Cz+D=0, Normal=(A,B,C) PositiveDirection: z-upward
        public double A { get { return _A; } }
        public double B { get { return _B; } }
        public double C { get { return _C; } }
        public double D { get { return _D; } }

        //Plane 
        public Plane Plane { get { return new Plane(A, B, C, D); } }
        
        public bool Errored { get; private set; } = false;
        public PlaneFitterErrorCode ErrorCode { get; private set; } = PlaneFitterErrorCode.NoError;

        private double _a = 0;
        private double _b = 0;
        private double _c = 0;
        private double _A = 0;
        private double _B = 0;
        private double _C = 0;
        private double _D = 0;
        private List<Vector3> _points = null;


        public PlaneFitter()
        {

        }

        public PlaneFitter(List<Vector3> points)
        {
            Points = points;
        }

        public PlaneFitter(Vector3[] pointsSource)
        {
            PointSource = pointsSource;
        }


        //public PlaneFitter(List<Vector3> points, bool solve)
        //{
        //    Points = points;
        //    if (solve) Solve();
        //}

        //public PlaneFitter(Vector3[] pointsSource, bool solve)
        //{
        //    PointSource = pointsSource;
        //    if (solve) Solve();
        //}


        public bool Solve()
        {
            Reset();

            if (Points == null)
            {
                SetError(PlaneFitterErrorCode.PointsCollectionIsNull);
                return false;
            }
            if (Points.Count < 3)
            {
                SetError(PlaneFitterErrorCode.PointsCountLessThan3);
                return false;
            }

            MatrixNxM matrix = SetupMatrix();
            
            LinearEquations LE = new LinearEquations(matrix);

            LE.Solve();

            if(LE.Errored)
            {
                SetError(PlaneFitterErrorCode.SolveEquationsError);
                return false;
            }

            _a = LE.Result[0];
            _b = LE.Result[1];
            _c = LE.Result[2];

            _A = _a * -1;
            _B = _b * -1;
            _C = 1;
            _D = _c * -1;

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
                matrix[0, 3] = matrix[0, 3] + Points[i].X * Points[i].Z;

                matrix[1, 0] = matrix[1, 0] + Points[i].X * Points[i].Y;
                matrix[1, 1] = matrix[1, 1] + Points[i].Y * Points[i].Y;
                matrix[1, 2] = matrix[1, 2] + Points[i].Y;
                matrix[1, 3] = matrix[1, 3] + Points[i].Y * Points[i].Z;

                matrix[2, 0] = matrix[2, 0] + Points[i].X;
                matrix[2, 1] = matrix[2, 1] + Points[i].Y;
                matrix[2, 2] = Points.Count;
                matrix[2, 3] = matrix[2, 3] + Points[i].Z;
            }
            return matrix;
        }

        private void Reset()
        {
            Errored = false;
            ErrorCode = PlaneFitterErrorCode.NoError;

            _a = 0;
            _b = 0;
            _c = 0;
            _A = 0;
            _B = 0;
            _C = 0;
            _D = 0;
        }

        private void SetError(PlaneFitterErrorCode errorCode)
        {
            Errored = true;
            ErrorCode = errorCode;
        }

    }


}
