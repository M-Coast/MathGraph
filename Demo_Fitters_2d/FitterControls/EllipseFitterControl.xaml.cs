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
    /// EllipseFitter.xaml 的交互逻辑
    /// </summary>
    public partial class EllipseFitterControl : UserControl
    {
        public EllipseFitterControl()
        {
            InitializeComponent();

            TestPoints = new ObservableCollection<Vector2>();
            this.Loaded += new RoutedEventHandler(this_Loaded);
        }


        private void this_Loaded(object sender, RoutedEventArgs e)
        {
            comboTestData.SelectedIndex = 0;
        }


        public Ellipse2d Ellipse
        {
            get { return (Ellipse2d)GetValue(EllipseProperty); }
            set { SetValue(EllipseProperty, value); }
        }

        public static readonly DependencyProperty EllipseProperty =
            DependencyProperty.Register("Ellipse", typeof(Ellipse2d), typeof(EllipseFitterControl), new PropertyMetadata());


        public EllipseFitterErrorCode ErrorCode
        {
            get { return (EllipseFitterErrorCode)GetValue(ErrorCodeProperty); }
            set { SetValue(ErrorCodeProperty, value); }
        }
        
        public static readonly DependencyProperty ErrorCodeProperty =
            DependencyProperty.Register("ErrorCode", typeof(EllipseFitterErrorCode), typeof(EllipseFitterControl), new PropertyMetadata());


        public ObservableCollection<Vector2> TestPoints
        {
            get { return (ObservableCollection<Vector2>)GetValue(TestPointsProperty); }
            set { SetValue(TestPointsProperty, value); }
        }
        
        public static readonly DependencyProperty TestPointsProperty =
            DependencyProperty.Register("TestPoints", typeof(ObservableCollection<Vector2>), typeof(EllipseFitterControl), new PropertyMetadata());



        private ObservableCollection<CS2dShape> _cs2dElements = new ObservableCollection<CS2dShape>();
        
        private EllipseFitter _fitter = new EllipseFitter();

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
            Ellipse = _fitter.Ellipse;
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


            CS2dEllipse ellipse = new CS2dEllipse()
            {
                CenterX = Ellipse.CenterX,
                CenterY = Ellipse.CenterY,
                RadiusA = Ellipse.RadiusA,
                RadiusB = Ellipse.RadiusB,
                Rotation = Ellipse.Rotation,
                Stroke = new SolidColorBrush(Colors.Black),
                Thickness = 1
            };

            ellipse.Value = _fitter.Ellipse;

            _cs2dElements.Add(ellipse);
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



        private void CreatePoints(
            double centerX, double centerY, double radiusA, double radiusB, double rotation,
            double startAngle, double swapAngle, int swapDirection,
            int pointsCount, double randomRangeX, double randomRangeY)
        {
            double theta = (rotation / 180.0) * System.Math.PI;
            double space = System.Math.PI * 2.0 / pointsCount;
            Vector2 shiftVector = new Vector2(centerX, centerY);
            Vector2 point = new Vector2();

            TestPoints.Clear();

            for (int i = 0; i < pointsCount; i++)
            {
                //参数方程
                //  x = a*cos(t)
                //  y = b*cos(t)
                point.X = Math.Cos(space * i) * radiusA;
                point.Y = Math.Sin(space * i) * radiusB;

                //加入随机数
                if (randomRangeX > 0) point.X = point.X + NextRangedRandomNumber(randomRangeX);
                if (randomRangeY > 0) point.Y = point.Y + NextRangedRandomNumber(randomRangeY);

                //旋转 th
                point.Rotate(theta);

                //平移 cx,cy
                point.Add(shiftVector);

                TestPoints.Add(point);

            }
        }


        //CreatePoints 写成同一个函数

        private void CreatePoints1()
        {
            double cx = 50;
            double cy = 50;
            double ra = 80;
            double rb = 40;
            double th = 60;


            int count = 200;
            double theta = (th / 180.0) * System.Math.PI;
            double space = System.Math.PI * 2.0 / count;
            Vector2 shiftVector = new Vector2(cx, cy);

            TestPoints.Clear();

            Vector2[] points = new Vector2[count];


            for (int i = 0; i < count; i++)
            {
                //参数方程
                //  x = a*cos(t)
                //  y = b*cos(t)
                points[i].X = Math.Cos(space * i) * ra;
                points[i].Y = Math.Sin(space * i) * rb;

                //旋转th
                points[i].Rotate(theta);

                //平移
                points[i].Add(shiftVector);

            }


            for (int i = 0; i < count; i++)
            {
                TestPoints.Add(points[i]);
            }

        }

        private void CreatePoints2()
        {
            double cx = 200;
            double cy = 50;
            double ra = 200;
            double rb = 50;
            double th = 120;


            int count = 200;
            double theta = (th / 180.0) * System.Math.PI;
            double space = System.Math.PI * 2.0 / count;
            Vector2 shiftVector = new Vector2(cx, cy);

            TestPoints.Clear();

            Vector2[] points = new Vector2[count];


            for (int i = 0; i < count; i++)
            {
                //参数方程
                //  x = a*cos(t)
                //  y = b*cos(t)
                points[i].X = Math.Cos(space * i) * ra + NextRangedRandomNumber(ra / 50);
                points[i].Y = Math.Sin(space * i) * rb + +NextRangedRandomNumber(ra / 50);

                //旋转th
                points[i].Rotate(theta);

                //平移
                points[i].Add(shiftVector);

            }


            for (int i = 0; i < count; i++)
            {
                TestPoints.Add(points[i]);
            }

        }

        private void CreatePoints3()
        {
            double cx = 0;
            double cy = 0;
            double ra = 20;
            double rb = 10;

            double space = 1.0;

            int count = 40;

            space = System.Math.PI * 2.0 / count;

            TestPoints.Clear();

            for (int i = 0; i < count; i++)
            {
                double x = cx + Math.Cos(space * i) * ra;// + NextRangedRandomNumber(1);
                double y = cy + Math.Sin(space * i) * rb;// + NextRangedRandomNumber(1);
                TestPoints.Add(new Vector2(x, y));
            }
        }
        
        private void CreatePoints4()
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


        private double NextRangedRandomNumber(double range)
        {
            return range * _random.NextDouble();
        }

    }
}
