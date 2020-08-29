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
    public partial class CircleFitterControl : UserControl
    {
        public CircleFitterControl()
        {
            InitializeComponent();

            TestPoints = new ObservableCollection<Vector2>();
            this.Loaded += new RoutedEventHandler(this_Loaded);
        }


        private void this_Loaded(object sender, RoutedEventArgs e)
        {
            comboTestData.SelectedIndex = 0;
        }


        public Circle2d Circle
        {
            get { return (Circle2d)GetValue(CircleProperty); }
            set { SetValue(CircleProperty, value); }
        }

        public static readonly DependencyProperty CircleProperty =
            DependencyProperty.Register("Circle", typeof(Circle2d), typeof(CircleFitterControl), new PropertyMetadata());


        public CircleFitterErrorCode ErrorCode
        {
            get { return (CircleFitterErrorCode)GetValue(ErrorCodeProperty); }
            set { SetValue(ErrorCodeProperty, value); }
        }
        
        public static readonly DependencyProperty ErrorCodeProperty =
            DependencyProperty.Register("ErrorCode", typeof(CircleFitterErrorCode), typeof(CircleFitterControl), new PropertyMetadata());


        public ObservableCollection<Vector2> TestPoints
        {
            get { return (ObservableCollection<Vector2>)GetValue(TestPointsProperty); }
            set { SetValue(TestPointsProperty, value); }
        }
        
        public static readonly DependencyProperty TestPointsProperty =
            DependencyProperty.Register("TestPoints", typeof(ObservableCollection<Vector2>), typeof(CircleFitterControl), new PropertyMetadata());



        private ObservableCollection<CS2dShape> _cs2dElements = new ObservableCollection<CS2dShape>();
        
        private CircleFitter _fitter = new CircleFitter();

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
            Circle = _fitter.Circle;
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


            CS2dCircle circle = new CS2dCircle()
            {
                CenterX = Circle.CenterX,
                CenterY = Circle.CenterY,
                Radius = Circle.r,
                Stroke = new SolidColorBrush(Colors.Black),
                Thickness = 1
            };

            circle.Value = _fitter.Circle;

            _cs2dElements.Add(circle);
            _cs2d.Elements = _cs2dElements;
        }

        private void SetPoints()
        {
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
            double a = 40;
            double b = 20;
            double R = 20;
            
            double space = 1.0;

            int count = 20;

            space = System.Math.PI * 2.0 / count;

            TestPoints.Clear();

            for (int i = 0; i < count; i++)
            {
                double x = a + Math.Cos(space * i) * R;
                double y = b + Math.Sin(space * i) * R;
                TestPoints.Add(new Vector2(x, y));
            }
        }

        private void CreatePoints2()
        {
            double a = 15;
            double b = 5;
            double R = 20;

            double space = 1.0;

            int count = 40;

            space = System.Math.PI * 2.0 / count;

            TestPoints.Clear();

            for (int i = 0; i < count; i++)
            {
                double x = a + Math.Cos(space * i) * R + NextRangedRandomNumber(1);
                double y = b + Math.Sin(space * i) * R + NextRangedRandomNumber(1);
                TestPoints.Add(new Vector2(x, y));
            }
        }

        private void CreatePoints3()
        {
            double a = 100;
            double b = 200;
            double R = 500;

            double space = 1.0;

            int count = 40;

            space = System.Math.PI * 1.0 / count;
            
            double startAngle = NextRangedRandomNumber(System.Math.PI * 2.0);

            TestPoints.Clear();

            for (int i = 0; i < count; i++)
            {
                double x = a + Math.Cos(startAngle + space * i) * R + NextRangedRandomNumber(R / 50);
                double y = b + Math.Sin(startAngle + space * i) * R + NextRangedRandomNumber(R / 50);
                TestPoints.Add(new Vector2(x, y));
            }
        }

        private void CreatePoints4()
        {
            double a = 10;
            double b = 20;
            double R = 20;

            double space = 1.0;

            int count = 3;

            space = System.Math.PI * 2.0 / count;

            TestPoints.Clear();

            for (int i = 0; i < count; i++)
            {
                double x = a + Math.Cos(space * i) * R + NextRangedRandomNumber(1);
                double y = b + Math.Sin(space * i) * R + NextRangedRandomNumber(1);
                TestPoints.Add(new Vector2(x, y));
            }

        }


        private double NextRangedRandomNumber(double range)
        {
            return range * _random.NextDouble();
        }

    }
}
