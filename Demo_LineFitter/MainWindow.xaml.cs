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

namespace Demo_LineFitter
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
            DependencyProperty.Register("Line", typeof(Line2d), typeof(MainWindow), new PropertyMetadata());


        public LineFitterErrorCode ErrorCode
        {
            get { return (LineFitterErrorCode)GetValue(ErrorCodeProperty); }
            set { SetValue(ErrorCodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ErrorCode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorCodeProperty =
            DependencyProperty.Register("ErrorCode", typeof(LineFitterErrorCode), typeof(MainWindow), new PropertyMetadata());


        public List<Vector2> TestPoints
        {
            get { return (List<Vector2>)GetValue(TestPointsProperty); }
            set { SetValue(TestPointsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestPoints.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TestPointsProperty =
            DependencyProperty.Register("TestPoints", typeof(List<Vector2>), typeof(MainWindow), new PropertyMetadata());



        ObservableCollection<CS2dShape> CS2dElements = new ObservableCollection<CS2dShape>();



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Solve();

            if (_lineFitter.Errored) return;

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

            CS2dElements.Add(line);
            _canvas.Elements = CS2dElements;
        }

        private void comboTestData_SelectionChanged(object sender, SelectionChangedEventArgs e)
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

            CS2dElements.Clear();

            foreach (Vector2 point in TestPoints)
            {
                CS2dElements.Add(new CS2dPoint()
                {
                    X = point.X,
                    Y = point.Y,
                    Stroke = new SolidColorBrush(Colors.Brown),
                    FillBrush = new SolidColorBrush(Colors.Brown),
                    Size = 2,
                    Thickness = 0,
                });
            }
            _canvas.Elements = CS2dElements;
        }


        private LineFitter _lineFitter = new LineFitter();

        private void Solve()
        {
            _lineFitter.Points = TestPoints;
            _lineFitter.Solve();
            Line = _lineFitter.Line;
            ErrorCode = _lineFitter.ErrorCode;
        }

        private void CreatePoints1()
        {
            double a = 1;
            double b = 2;

            Line2d plane = new Line2d(a, b);

            double xSpace = 1.0;

            int xPointCount = 2;

            List<Vector2> points = new List<Vector2>();

            for (int i = 0; i < xPointCount; i++)
            {
                double x = xSpace * i;
                double y = plane.GetY(x);
                points.Add(new Vector2(x, y));
            }

            TestPoints = points;
        }

        private void CreatePoints2()
        {
            double a = 0.5;
            double b = 2;

            Line2d plane = new Line2d(a, b);

            double xSpace = 1.0;

            int xPointCount = 10;

            List<Vector2> points = new List<Vector2>();

            for (int i = 0; i < xPointCount; i++)
            {
                double x = xSpace * i;
                double y = plane.GetY(x);
                points.Add(new Vector2(x, y));
            }


            TestPoints = points;
        }

        private void CreatePoints3()
        {
            double a = 4;
            double b = 7;

            Line2d plane = new Line2d(a, b);

            double xSpace = 1.0;

            int xPointCount = 20;

            List<Vector2> points = new List<Vector2>();

            for (int i = 0; i < xPointCount; i++)
            {
                double x = xSpace * i;
                double y = plane.GetY(x);
                points.Add(new Vector2(x, y));
            }

            TestPoints = points;
        }

        private void CreatePoints4()
        {
            double a = 4;
            double b = 7;

            Line2d plane = new Line2d(a, b);

            double xSpace = 1.0;

            int xPointCount = 1;

            List<Vector2> points = new List<Vector2>();

            for (int i = 0; i < xPointCount; i++)
            {
                double x = xSpace * i;
                double y = plane.GetY(x);
                points.Add(new Vector2(x, y));
            }


            TestPoints = points;
        }

        private void SetPointsToCS2d()
        {
            
        }


    }
}
