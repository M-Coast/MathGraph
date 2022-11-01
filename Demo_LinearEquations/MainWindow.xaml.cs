using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using System.Data;

using Coast.Math;

namespace Demo_LinearEquations
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += new RoutedEventHandler(this_Loaded);
            
        }

        private void InitializeControls()
        {
            comboTestData.SelectedIndex = 0;

        }

        private void this_Loaded(object sender, RoutedEventArgs e)
        {
            comboTestData.SelectedIndex = 0;
        }
        
        private void test()
        {
            double[,] t = new double[,]
            {
                {1,2,3,4 },
                {5,6,7,8 },
                {9,10,11,12 },
            };

            MatrixNxM mat = new MatrixNxM(t);

            //mat.DebugTest();


            double[,] t2 = new double[,]
            {
                { 9,   -1,  -1,  7 },
                {-1,  10,  -1,  8 },
                {-1,  -1,  15,  13 },
            };

            MatrixNxM m2 = new MatrixNxM(t2);

            LinearEquations le = new LinearEquations();
            le.MatrixDataSource = t;

            //le.Matrix = m2;
            le.Solve();



            
        }

        private void comboTestData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            if (c.SelectedIndex == 0)
            {
                dataGridInputData.ItemsSource = ConvertArray2DataTable(testData1).DefaultView;
                //currentData = testData1;
            }
            if (c.SelectedIndex == 1)
            {
                dataGridInputData.ItemsSource = ConvertArray2DataTable(testData2).DefaultView;
                //currentData = testData2;
            }
            if (c.SelectedIndex == 2)
            {
                dataGridInputData.ItemsSource = ConvertArray2DataTable(testData3).DefaultView;
                //currentData = testData3;
            }
            if (c.SelectedIndex == 3)
            {
                dataGridInputData.ItemsSource = ConvertArray2DataTable(testData4).DefaultView;
                //currentData = testData4;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LinearEquations LE = new LinearEquations();

            DataView dv = dataGridInputData.ItemsSource as DataView;

            double[,] data = ConvertDataTable2Array(dv.Table);

            LE.MatrixDataSource = data;

            LE.Solve();

            if(LE.Errored)
            {
                textBlockStatus.Foreground = new SolidColorBrush(Colors.Red);
            }
            else
            {
                textBlockStatus.Foreground = new SolidColorBrush(Colors.Black);
            }
            textBlockStatus.Text = LE.ErrorCode.ToString();

            dataGridProcessData.ItemsSource =
                LE.Matrix.Data == null ? null : ConvertArray2DataTable(LE.Matrix.Data).DefaultView;

            dataGridOutputData.ItemsSource =
                LE.Result == null ? null : ConvertArray2DataTable(LE.Result).DefaultView;
        }

        private DataTable ConvertArray2DataTable(double[,] data)
        {
            if (data == null) return null;

            DataTable dt = new DataTable();

            int r = data.GetLength(0);
            int c = data.GetLength(1);


            for (int i = 0; i < c; i++)
            {
                if (i <= c - 1)
                {
                    dt.Columns.Add(new DataColumn("A" + i.ToString()));
                }
                else
                {
                    dt.Columns.Add(new DataColumn("B" + i.ToString()));
                }
            }

            for (int j = 0; j < r; j++)
            {
                DataRow dr = dt.NewRow();
                
                for (int i = 0; i < c; i++)
                {
                    //dr[i] = data[j, i];
                    dr[i] = data[j, i].ToString("F3");
                }
                
                dt.Rows.Add(dr);
            }
            
            return dt;
        }

        private double[,] ConvertDataTable2Array(DataTable table)
        {
            if (table == null) return null;

            int r = table.Rows.Count;
            int c = table.Columns.Count;

            double[,] array = new double[r, c];


            for (int j = 0; j < r; j++)
            {
                for (int i = 0; i < c; i++)
                {
                    array[j, i] = double.Parse(table.Rows[j][i] as string);
                }
            }

            return array;
        }
        
        private DataTable ConvertArray2DataTable(double[] array)
        {
            if (array == null) return null;

            DataTable dt = new DataTable();

            int r = array.Length;
            int c = 1;


            for (int i = 0; i < c; i++)
            {
                dt.Columns.Add(new DataColumn("R" + i.ToString()));
            }

            for (int j = 0; j < r; j++)
            {
                DataRow dr = dt.NewRow();

                //dr[0] = data[j];
                dr[0] = array[j].ToString("F3");


                dt.Rows.Add(dr);
            }

            return dt;
        }
        
        private List<List<double>> Convert2dArray2List(double[,] array)
        {
            List<List<double>> l = new List<List<double>>();

            int r = array.GetLength(0);
            int c = array.GetLength(1);

            for (int j = 0; j < r; j++)
            {
                List<double> d = new List<double>(c);
                for (int i = 0; i < c; i++)
                {
                    d.Add(array[j, i]);
                }
                l.Add(d);
            }
            return l;
        }

        private void PasteToDataGrid()
        {
            string pasteText = Clipboard.GetText();
            if (string.IsNullOrEmpty(pasteText)) return;

            char[] rowSeperators = { '\n' };
            char[] colSeperators = { '\t' };

            string[] ss = pasteText.Replace("\r\n", "\n").Split(rowSeperators, StringSplitOptions.RemoveEmptyEntries);
            if (ss.Length < 1) return;

            List<List<string>> texts = new List<List<string>>();

            foreach (string s in ss)
            {
                List<string> sl = new List<string>();
                texts.Add(sl);
                string[] tt = s.Split(colSeperators);
                foreach (string t in tt) { sl.Add(t); }
            }

            int rows = texts.Count;
            int cols = int.MinValue;

            foreach (List<string> l in texts)
            {
                if (cols < l.Count) cols = l.Count;
            }

            double[,] data = new double[rows,cols];

            for (int j = 0; j < texts.Count; j++)
            {
                List<string> l = texts[j];
                for (int i = 0; i < l.Count; i++)
                {
                    if (i < l.Count)
                    {
                        double v;
                        if (!double.TryParse(l[i], out v)) { throw new Exception("Parse value errored."); }
                        data[j, i] = v;
                    }
                    else
                    {
                        data[j, i] = 0;
                    }
                }
            }

            dataGridInputData.ItemsSource = ConvertArray2DataTable(data).DefaultView;

        }

        
        //Result = [1,1,1]T
        private double[,] testData1 = new double[,]
        {
            {+9,   -1,   -1,        +7 },
            {-1,  +10,   -1,        +8 },
            {-1,   -1,  +15,       +13 },
        };

        //NoSolution
        private double[,] testData2 = new double[,]
        {
            {1,   2,  3,        4 },
            {5,   6,  7,        8 },
            {9,  10,  11,       12 },
        };
        
        //Result = [1/3,1/3,1/3]T
        private double[,] testData3 = new double[,]
        {
            { 1,-1,2,-2 },
            { -2,1,-1,2 },
            { 4,-1,2,1 },
        };

        //Result = [1,2,3,-1]T
        private double[,] testData4 = new double[,]
        {
            { 1,1,1,1,     5},
            { 1,2,-1,4,    -2},
            {2,-3,-1,-5,   -2},
            {3,1,2,11,     0},
        };

        private void dataGridInputData_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyboardDevice.Modifiers== ModifierKeys.Control && e.Key== Key.V)
            {
                PasteToDataGrid();
            }
        }
    }
}
