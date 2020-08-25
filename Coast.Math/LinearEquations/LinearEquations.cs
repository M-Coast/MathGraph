using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using Coast.Math.Utilities;

namespace Coast.Math
{
    //LinearEquationsSolution
    //
    //  Equations:  Ax=B
    //
    //  Matrix:     [A|B]
    //
    //  Result:        []
    //

    //Procedure:
    //  1.new this object
    //  2.assign Matrix (or already assigned when instance(Matrix))
    //  3.call Solve function
    //  4.check Error state. If errored see ErrorCode and processed Matrix for detail
    //  5.get results
    //

    #region Example Detail
    //
    //Example
    //
    //Input
    //Matrix 3X4
    //9.00	-1.00	-1.00	7.00	
    //-1.00	10.00	-1.00	8.00	
    //-1.00	-1.00	15.00	13.00	
    //
    //Result:
    //						1.00	
    //						1.00	
    //						1.00	

    //Processing:
    //Input Matrix
    //Matrix 3X4
    //9.00	-1.00	-1.00	7.00	
    //-1.00	10.00	-1.00	8.00	
    //-1.00	-1.00	15.00	13.00	

    //Forward Elimination Round: 1
    //Matrix 3X4
    //9.00	-1.00	-1.00	7.00	
    //0.00	9.89	-1.11	8.78	
    //0.00	-1.11	14.89	13.78	

    //Forward Elimination Round: 2
    //Matrix 3X4
    //9.00	-1.00	-1.00	7.00	
    //0.00	9.89	-1.11	8.78	
    //0.00	0.00	14.76	14.76	

    //Normalized
    //Matrix 3X4
    //1.00	-0.11	-0.11	0.78	
    //0.00	1.00	-0.11	0.89	
    //0.00	0.00	1.00	1.00	

    //Backward Elimination Round: 1
    //Matrix 3X4
    //1.00	-0.11	0.00	0.89	
    //0.00	1.00	0.00	1.00	
    //0.00	0.00	1.00	1.00	

    //Backward Elimination Round: 2
    //Matrix 3X4
    //1.00	0.00	0.00	1.00	
    //0.00	1.00	0.00	1.00	
    //0.00	0.00	1.00	1.00	

    //Processed Matrix
    //Matrix 3X4
    //1.00	0.00	0.00	1.00	
    //0.00	1.00	0.00	1.00	
    //0.00	0.00	1.00	1.00	

    //Result
    //1.00
    //1.00
    //1.00

    #endregion


    public class LinearEquations
    {

        public MatrixNxM Matrix
        {
            get
            {
                return _matrix;
            }
            set
            {
                if (value == null) return;
                _matrix = ClassUtils.DeepCopy(value);//Use copy of raw matrix for calculation
            }
        }

        //This Allows Matrix set by array
        public double[,] MatrixDataSource
        {
            set
            {
                _matrix = new MatrixNxM();
                _matrix.DataSource = value;
            }
        }

        public double[] Result { get; private set; } = null;

        public bool Errored { get; private set; } = false;
        public LinearEquationsErrorCode ErrorCode { get; private set; } = LinearEquationsErrorCode.NoError;

        private MatrixNxM _matrix = null;



        public LinearEquations()
        {

        }

        public LinearEquations(MatrixNxM matrix)
        {
            if (matrix == null) return;
            Matrix = matrix;
        }

        public LinearEquations(double[,] matrixDataSource)
        {
            if (matrixDataSource == null) return;
            MatrixDataSource = matrixDataSource;
        }


        public bool Solve()
        {
            Reset();

            CheckMatix();

            Debug.Print("Input Matrix");
            Debug.Print(_matrix.ToString());

            //Forward Elimination  
            if (!ForwardEliminate()) return false;

            //Normalize
            if (!Normalize()) return false;

            //Backward Elimination
            if (!BackwardEliminate()) return false;

            Debug.Print("Processed Matrix");
            Debug.Print(_matrix.ToString());

            //OutputResult
            OutputResult();

            return true;
        }



        private void Reset()
        {
            Result = null;
            Errored = false;
            ErrorCode = LinearEquationsErrorCode.NoError;
        }

        private void SetError(LinearEquationsErrorCode errorCode)
        {
            ErrorCode = errorCode;
            Errored = true;

            Debug.Print("Error: " + ErrorCode.ToString());
        }

        private bool CheckMatix()
        {
            if (_matrix == null)
            {
                SetError(LinearEquationsErrorCode.MatrixIsNull);
                return false;
            }
            if (_matrix.Rows < 1)
            {
                SetError(LinearEquationsErrorCode.MatrixError);
                return false;
            }
            if (_matrix.Rows + 1 != _matrix.Columns)
            {
                SetError(LinearEquationsErrorCode.MatrixError);
                return false;
            }

            return true;
        }

        private bool SelectPivotRow(int rowStart, int column)
        {
            if (rowStart >= _matrix.Rows - 1)
            {
                SetError(LinearEquationsErrorCode.MatrixError);
                return false;
            }

            double max = double.MinValue;
            int rowFound = -1;

            for (int j = rowStart; j < _matrix.Rows; j++)
            {
                if (max < System.Math.Abs(_matrix[rowStart, column]))
                {
                    max = _matrix[rowStart, column];
                    rowFound = j;
                }
            }

            if (rowFound != rowStart) _matrix.SwapRows(rowFound, rowStart);

            return true;
        }

        private bool Eliminate(int dstRow, int srcRow, int eliminateColumn)
        {
            if (_matrix[srcRow, eliminateColumn] == 0)
            {
                //All conditions have been Committed before this,
                //If still goes into this branch, there might be some unknown bug
                throw new DivideByZeroException();
            }

            double factor = _matrix[dstRow, eliminateColumn] / _matrix[srcRow, eliminateColumn] * -1;

            for (int i = 0; i < _matrix.Columns; i++)
            {
                _matrix[dstRow, i] = _matrix[dstRow, i] + _matrix[srcRow, i] * factor;
            }

            return true;
        }

        private bool ForwardEliminate()
        {
            for (int k = 0; k < _matrix.Rows - 1; k++)
            {
                if (!SelectPivotRow(k, k)) return false;

                //Pivot Element is 0, No exact result for some(one or more than one) Rows, Classified to NoSolution
                if (_matrix[k, k] == 0)
                {
                    SetError(LinearEquationsErrorCode.NoSolution); return false;
                }
                for (int j = k + 1; j < _matrix.Rows; j++)
                {
                    if (!Eliminate(j, k, k)) return false;
                }

                Debug.Print("Forward Elimination Round: " + (k + 1).ToString());
                Debug.Print(_matrix.ToString());
            }
            return true;
        }

        private bool BackwardEliminate()
        {
            for (int k = _matrix.Rows - 1; k > 0; k--)
            {
                //Pivot Element is 0, No exact result for some(one or more than one) Rows, Classified to NoSolution
                if (_matrix[k, k] == 0)
                {
                    SetError(LinearEquationsErrorCode.NoSolution); return false;
                }

                for (int j = k - 1; j >= 0; j--)
                {
                    if (!Eliminate(j, k, k)) return false;
                }

                Debug.Print("Backward Elimination Round: " + (_matrix.Rows - k).ToString());
                Debug.Print(_matrix.ToString());
            }
            return true;
        }

        private bool Normalize()
        {
            for (int j = 0; j < _matrix.Rows; j++)
            {
                double factor = 1.0 / _matrix[j, j];

                //Pivot Element is 0, No exact result for some(one or more than one) Rows, Classified to NoSolution
                if (_matrix[j, j] == 0.0)
                {
                    SetError(LinearEquationsErrorCode.NoSolution); return false;
                }
                _matrix.Scale(factor, j);
            }

            Debug.Print("Normalized");
            Debug.Print(_matrix.ToString());

            return true;
        }

        private void OutputResult()
        {
            Result = new double[_matrix.Rows];

            Debug.Print("Output");
            for (int k = 0; k < _matrix.Rows; k++)
            {
                Result[k] = _matrix[k, _matrix.Columns - 1];
                Debug.Print(Result[k].ToString("F2"));
            }


        }
    }



}






