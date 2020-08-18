using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;

namespace Coast.Math
{


    //MatrixNXM Type
    //  Definations:
    //      N Columns
    //      M Rows
    //      X By

    [Serializable]
    public class MatrixNxM
    {
        public const int MaxRows = 1024;
        public const int MaxColumns = 1024;


        private double[,] _data;
        
        public double[,] Data
        {
            get { return _data; }
        }

        public double[,] DataSource
        {
            set { SetDataSource(value); }
        }

        
        public int Rows { get {return _n; } }
        public int Columns { get { return _m; } }

        private int _n = 0;//rows
        private int _m = 0;//columns

        public MatrixErrorCode ErrorCode { get; private set; }

        public bool Errored { get; private set; }


        public MatrixNxM()
        {
            
        }

        public MatrixNxM(int rows, int columns)
        {
            if (rows < 1 || rows > MaxRows)
            {
                SetError(MatrixErrorCode.RowIndexError, rows);
            }
            if (columns < 1 || columns > MaxColumns)
            {
                SetError(MatrixErrorCode.RowIndexError, columns);
            }

            _n = rows;
            _m = columns;

            _data = new double[_n, _m];
        }

        public MatrixNxM(double[,] dataSource)
        {
            SetDataSource(dataSource);
        }


        public double this[int rowIndex, int columnIndex]
        {
            get
            {
                CheckData();
                CheckRowIndex(rowIndex);
                CheckColumnIndex(columnIndex);
                return _data[rowIndex, columnIndex];
            }
            set
            {
                CheckData();
                CheckRowIndex(rowIndex);
                CheckColumnIndex(columnIndex);
                _data[rowIndex, columnIndex] = value;
            }
        }
        

        private void CheckData()
        {
            if (_data == null) throw new Exception("Data is Null!");
        }
        private void CheckRowIndex(int rowIndex)
        {
            if (rowIndex < 0 || rowIndex > _n - 1)
            {
                SetError(MatrixErrorCode.RowIndexError, rowIndex);
            }

        }
        private void CheckColumnIndex(int columnIndex)
        {
            if (columnIndex < 0 || columnIndex > _m - 1)
            {
                SetError(MatrixErrorCode.RowIndexError, columnIndex);
            }
        }

        private void SetError(MatrixErrorCode errorCode, double value)
        {
            Errored = true;
            ErrorCode = errorCode;
            string desc = errorCode.ToString() + ": " + "Value = " + value.ToString();
            throw new Exception(desc);
        }

        private void SetDataSource(double[,] dataSource)
        {
            Clear();

            if (dataSource == null) return;

            int rows = dataSource.GetLength(0);
            int columns = dataSource.GetLength(1);

            if (rows < 1 || rows > MaxRows)
            {
                SetError(MatrixErrorCode.RowIndexError, rows);
            }
            if (columns < 1 || columns > MaxColumns)
            {
                SetError(MatrixErrorCode.RowIndexError, columns);
            }

            _n = rows;
            _m = columns;
            _data = new double[_n, _m];
            //Buffer.BlockCopy(dataSource, 0, _data, 0, _n * _m);
            Array.Copy(dataSource, _data, _n * _m);
        }

        public void SwapRows(int rowIndex1, int rowIndex2)
        {
            CheckData();
            CheckRowIndex(rowIndex1);
            CheckRowIndex(rowIndex2);

            if (rowIndex1 == rowIndex2) return;
            
            for (int i = 0; i < _m; i++)
            {
                double t = _data[rowIndex1, i];
                _data[rowIndex1, i] = _data[rowIndex2, i];
                _data[rowIndex2, i] = t;
            }
        }
        
        public void Clear()
        {
            _n = 0;
            _m = 0;
            _data = null;
        }



        public void Scale(double factor)
        {
            CheckData();

            for (int j = 0; j < _n; j++)
            {
                for (int i = 0; i < _m; i++)
                {
                    _data[j, i] = _data[j, i] * factor;
                }
            }
        }

        public void Scale(double factor, int rowIndex)
        {
            CheckData();
            CheckRowIndex(rowIndex);

            for (int i = 0; i < _m; i++)
            {
                _data[rowIndex, i] = _data[rowIndex, i] * factor;
            }
        }

        public void ScaleRow(double factor, int rowIndex)
        {
            CheckData();
            CheckRowIndex(rowIndex);

            for (int i = 0; i < _m; i++)
            {
                _data[rowIndex, i] = _data[rowIndex, i] * factor;
            }
        }

        public void ScaleColumn(double factor, int columnIndex)
        {
            CheckData();
            CheckColumnIndex(columnIndex);

            for (int j = 0; j < _n; j++)
            {
                _data[j, columnIndex] = _data[j, columnIndex] * factor;
            }
        }

        public override string ToString()
        {
            string s = string.Empty;

            s += "Matrix " + _n.ToString() + "X" + _m.ToString() + "\r\n";

            for (int j = 0; j < _n; j++)
            {
                for (int i = 0; i < _m; i++)
                { 
                    s += _data[j, i].ToString("F2") + "\t";
                }
                s += "\r\n";
            }

            return s;
        }

        public string ToString(string format)
        {
            string s = string.Empty;

            s += "Matrix " + _n.ToString() + "X" + _m.ToString() + "\r\n";

            for (int j = 0; j < _n; j++)
            {
                for (int i = 0; i < _m; i++)
                {
                    s += _data[j, i].ToString(format) + "\t";
                }
                s += "\r\n";
            }

            return s;
        }


        public string DataToText()
        {
            if (_data == null) return string.Empty;

            string text = string.Empty;

            for (int j = 0; j < _n; j++)
            {
                text += "[" + j.ToString("D3") + "]\t";
                for (int i = 0; i < _m; i++)
                {
                    //text += Data[j, i].ToString("F2") + "\t";
                    text += this[j, i].ToString("F2") + "\t";
                }
                text += "\r\n";
            }
            return text;
        }

        public void DebugTest()
        {
            CheckData();

            Debug.Print("0");
            Debug.Print(DataToText());

            CheckRowIndex(0);
            CheckRowIndex(_n - 1);
            CheckColumnIndex(0);
            SwapRows(0, _n - 1);

            Debug.Print("1");
            Debug.Print(DataToText());
        }


    }

    
    [Serializable]
    public struct Matrix3X3
    {

    }

    [Serializable]
    public struct Matrix3X4
    {

    }




    //public class MatrixNXM_BK
    //{
    //    public const int MaxRows = 1024;
    //    public const int MaxColumns = 1024;


    //    private List<double[]> _data;

    //    public List<double[]> Data
    //    {
    //        get { return _data; }
    //    }

    //    public double[,] DataSource
    //    {
    //        set { SetData_Array(value); }
    //    }


    //    //public double[,] DataArray
    //    //{
    //    //    get
    //    //    {
    //    //        if (_data == null) return null;
    //    //        //_data.Add()
    //    //    }
    //    //}


    //    public int Rows { get; }
    //    public int Columns { get; }

    //    private int _n = 0;//rows
    //    private int _m = 0;//columns

    //    public MatrixErrorCode ErrorCode { get; private set; }

    //    public bool Errored { get; private set; }


    //    public MatrixNXM_BK()
    //    {
    //        //_data.
    //    }

    //    public MatrixNXM_BK(int rows, int columns)
    //    {
    //        if (rows < 1 || rows > MaxRows)
    //        {
    //            SetError(MatrixErrorCode.RowIndexError, rows);
    //        }
    //        if (columns < 1 || columns > MaxColumns)
    //        {
    //            SetError(MatrixErrorCode.RowIndexError, columns);
    //        }

    //        _n = rows;
    //        _m = columns;

    //        _data = new List<double[]>(_n);
    //        for (int i = 0; i < _n; i++) _data.Add(new double[_m]);
    //    }

    //    public MatrixNXM_BK(double[,] data)
    //    {
    //        SetData_Array(data);
    //    }

    //    public void Clear()
    //    {
    //        _n = 0;
    //        _m = 0;
    //        _data.Clear();
    //    }

    //    private void SetData_Array(double[,] data)
    //    {
    //        _n = 0;
    //        _m = 0;

    //        if (data == null) return;

    //        if (_data == null)
    //        {
    //            _data = new List<double[]>();
    //        }
    //        else
    //        {
    //            _data.Clear();
    //        }

    //        int rows = data.GetLength(1);
    //        int columns = data.GetLength(0);

    //        if (rows < 1 || rows > MaxRows)
    //        {
    //            SetError(MatrixErrorCode.RowIndexError, rows);
    //        }
    //        if (columns < 1 || columns > MaxColumns)
    //        {
    //            SetError(MatrixErrorCode.RowIndexError, columns);
    //        }

    //        _n = rows;
    //        _m = columns;
    //        int offset = 0;

    //        for (int i = 0; i < _n; i++)
    //        {
    //            double[] v = new double[_m];
    //            Buffer.BlockCopy(data, offset, v, 0, _m);
    //            _data.Add(v);
    //            offset = offset + _m;
    //        }
    //    }

    //    public double this[int rowIndex, int columnIndex]
    //    {
    //        get
    //        {
    //            if (rowIndex < 0 || rowIndex > _n - 1)
    //            {
    //                SetError(MatrixErrorCode.RowIndexError, rowIndex);
    //            }
    //            if (columnIndex < 0 || columnIndex > _n - 1)
    //            {
    //                SetError(MatrixErrorCode.RowIndexError, columnIndex);
    //            }
    //            return _data[rowIndex][columnIndex];
    //        }
    //    }

    //    public void SwapRows(int rowIndex1, int rowIndex2)
    //    {
    //        if (rowIndex1 < 0 || rowIndex1 > _n - 1)
    //        {
    //            SetError(MatrixErrorCode.RowIndexError, rowIndex1);
    //        }

    //        if (rowIndex2 < 0 || rowIndex2 > _n - 1)
    //        {
    //            SetError(MatrixErrorCode.RowIndexError, rowIndex2);
    //        }

    //        double[] t = _data[rowIndex1];
    //        _data[rowIndex1] = _data[rowIndex2];
    //        _data[rowIndex2] = t;
    //    }

    //    private string SetError(MatrixErrorCode errorCode, double value)
    //    {
    //        Errored = true;
    //        ErrorCode = errorCode;
    //        string desc = errorCode.ToString() + ": " + "Value = " + value.ToString();
    //        throw new Exception(desc);
    //    }



    //}


}
