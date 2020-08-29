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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Collections.ObjectModel;
using Coast.Math;
using Coast.Controls;

namespace Demo_Fitters_2d
{
    /// <summary>
    /// CircleFitter.xaml 的交互逻辑
    /// </summary>
    public partial class LineFitterControl : UserControl
    {
        public LineFitterControl()
        {
            InitializeComponent();

            TestPoints = new ObservableCollection<Vector2>();
            this.Loaded += new RoutedEventHandler(this_Loaded);
        }


        private void this_Loaded(object sender, RoutedEventArgs e)
        {
            comboTestData.SelectedIndex = 0;
        }


        public Line2d Line
        {
            get { return (Line2d)GetValue(LineProperty); }
            set { SetValue(LineProperty, value); }
        }

        public static readonly DependencyProperty LineProperty =
            DependencyProperty.Register("Line", typeof(Line2d), typeof(LineFitterControl), new PropertyMetadata());


        public LineFitterErrorCode ErrorCode
        {
            get { return (LineFitterErrorCode)GetValue(ErrorCodeProperty); }
            set { SetValue(ErrorCodeProperty, value); }
        }
        
        public static readonly DependencyProperty ErrorCodeProperty =
            DependencyProperty.Register("ErrorCode", typeof(LineFitterErrorCode), typeof(LineFitterControl), new PropertyMetadata());


        public ObservableCollection<Vector2> TestPoints
        {
            get { return (ObservableCollection<Vector2>)GetValue(TestPointsProperty); }
            set { SetValue(TestPointsProperty, value); }
        }
        
        public static readonly DependencyProperty TestPointsProperty =
            DependencyProperty.Register("TestPoints", typeof(ObservableCollection<Vector2>), typeof(LineFitterControl), new PropertyMetadata());



        private ObservableCollection<CS2dShape> _cs2dElements = new ObservableCollection<CS2dShape>();
        
        private LineFitter _fitter = new LineFitter();
        
        private Random _random = new Random();



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Solve();
            SetResult();
        }

        private void TestData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            if (c.SelectedIndex == 0)
            {
                CreatePoints1();
            }
            if (c.SelectedIndex == 1)
            {
                CreatePoints2();
            }
            if (c.SelectedIndex == 2)
            {
                CreatePoints3();
            }
            if (c.SelectedIndex == 3)
            {
                CreatePoints4();
            }

            SetPoints();
        }


        private void Solve()
        {
            _fitter.Points = TestPoints.ToList();
            _fitter.Solve();
            Line = _fitter.Line;
            ErrorCode = _fitter.ErrorCode;
        }

        private void SetResult()
        {
            if (_fitter.Errored) return;

            double xMin = double.MaxValue;
            double xMax = double.MinValue;

            foreach (Vector2 p in TestPoints)
            {
                if (xMin > p.X) xMin = p.X;
                if (xMax < p.X) xMax = p.X;
            }


            CS2dLine line = new CS2dLine()
            {
                StartPoint = new Point(xMin, Line.GetY(xMin)),
                EndPoint = new Point(xMax, Line.GetY(xMax)),
                Stroke = new SolidColorBrush(Colors.Black),
                Thickness = 1
            };

            line.Value = _fitter.Line;

            _cs2dElements.Add(line);
            _cs2d.Elements = _cs2dElements;
        }

        private void SetPoints()
        {
            if (TestPoints == null) return;

            _cs2dElements.Clear();

            foreach (Vector2 point in TestPoints)
            {
                _cs2dElements.Add(new CS2dPoint()
                {
                    X = point.X,
                    Y = point.Y,
                    Stroke = new SolidColorBrush(Colors.Brown),
                    FillBrush = new SolidColorBrush(Colors.Brown),
                    Size = 1,
                    Thickness = 0,
                });
            }
            _cs2d.Elements = _cs2dElements;
        }

        private void CreatePoints1()
        {
            double a = 1;
            double b = 2;

            Line2d plane = new Line2d(a, b);

            double xSpace = 1.0;

            int xPointCount = 10;

            TestPoints.Clear();

            for (int i = 0; i < xPointCount; i++)
            {
                double x = xSpace * i;
                double y = plane.GetY(x);
                TestPoints.Add(new Vector2(x, y));
            }

            //TestPoints = points;
        }

        private void CreatePoints2()
        {
            double a = 1;
            double b = 2;

            Line2d plane = new Line2d(a, b);

            double xSpace = 1.0;

            int xPointCount = 100;

            TestPoints.Clear();

            for (int i = 0; i < xPointCount; i++)
            {
                double x = xSpace * i + NextRangedRandomNumber(xSpace);
                double y = plane.GetY(x) + NextRangedRandomNumber(5);
                TestPoints.Add(new Vector2(x, y));
            }
            
        }

        private void CreatePoints3()
        {
            double a = 0.5;
            double b = 7;

            Line2d plane = new Line2d(a, b);

            double xSpace = 1.0;

            int xPointCount = 200;

            TestPoints.Clear();

            for (int i = 0; i < xPointCount; i++)
            {
                double x = xSpace * i + NextRangedRandomNumber(xSpace);
                double y = plane.GetY(x) + NextRangedRandomNumber(20);
                TestPoints.Add(new Vector2(x, y));
            }
            
        }

        private void CreatePoints4()
        {
            double a = 4;
            double b = 7;

            Line2d plane = new Line2d(a, b);

            double xSpace = 1.0;

            int xPointCount = 1;

            TestPoints.Clear();

            for (int i = 0; i < xPointCount; i++)
            {
                double x = xSpace * i;
                double y = plane.GetY(x);
                TestPoints.Add(new Vector2(x, y));
            }
        }


        private double NextRangedRandomNumber(double range)
        {
            return range * _random.NextDouble();
        }

    }
}
