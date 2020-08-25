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

//using Coast.Math;

namespace Coast.Controls
{
    public class CoordinateSystem2d : Control
    {
        static CoordinateSystem2d()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CoordinateSystem2d), new FrameworkPropertyMetadata(typeof(CoordinateSystem2d)));
        }

        public CoordinateSystem2d()
        {
            XAxis = new Axis() { AxisAlignment = AxisAlignment.HorizontalBottom };
            YAxis = new Axis() { AxisAlignment = AxisAlignment.VerticalLeft };

            XAxis.Foreground = new SolidColorBrush(Colors.Black);
            XAxis.AxisBrush = new SolidColorBrush(Colors.Black);
            XAxis.AxisThickness = 1;
            XAxis.BorderBrush = new SolidColorBrush(Colors.Black);
            XAxis.BorderThickness = new Thickness(0);

            YAxis.Foreground = new SolidColorBrush(Colors.Black);
            YAxis.AxisBrush = new SolidColorBrush(Colors.Black);
            YAxis.AxisThickness = 1;
            YAxis.BorderBrush = new SolidColorBrush(Colors.Black);
            YAxis.BorderThickness = new Thickness(0);

            DebugTest();
        }

        public Control Content
        {
            get { return (Control)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }
        
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(Control), typeof(CoordinateSystem2d), new PropertyMetadata());


        public Axis XAxis
        {
            get { return (Axis)GetValue(XAxisProperty); }
            set { SetValue(XAxisProperty, value); }
        }
        
        public static readonly DependencyProperty XAxisProperty =
            DependencyProperty.Register("XAxis", typeof(Axis), typeof(CoordinateSystem2d), new PropertyMetadata());


        public Axis YAxis
        {
            get { return (Axis)GetValue(YAxisProperty); }
            set { SetValue(YAxisProperty, value); }
        }
        
        public static readonly DependencyProperty YAxisProperty =
            DependencyProperty.Register("YAxis", typeof(Axis), typeof(CoordinateSystem2d), new PropertyMetadata());


        public int DisireXAxisTickCount { get; set; } = 5; //runtime value will be close to disiere value
        public int DisireYAxisTickCount { get; set; } = 5; //runtime value will be close to disiere value

        public Transform Transform { get { return _transform; } }

        public bool Errored { get; protected set; } = false;

        public bool CoordinateSystemInitialized { get; protected set; } = false;

        protected void SetAxis(double xLow,double xHigh,double yLow,double yHigh, List<AxisTick> xAxisTicks, List<AxisTick> yAxisTicks)
        {
            SetRange(xLow, xHigh, yLow, yHigh);
            SetTransform(xLow, xHigh, yLow, yHigh);
            SetTicks(xAxisTicks, yAxisTicks);
            CoordinateSystemInitialized = true;
        }

        private void SetTicks(List<AxisTick> xAxisTicks, List<AxisTick> yAxisTicks)
        {
            XAxis.Ticks = xAxisTicks;
            YAxis.Ticks = yAxisTicks;
        }

        private void SetRange (double xLow, double xHigh, double yLow, double yHigh)
        {
            XAxis.RangeLowerLimit = xLow;       //LowerLimit
            XAxis.RangeUpperLimit = xHigh;      //UpperLimit
            YAxis.RangeLowerLimit = yLow;
            YAxis.RangeUpperLimit = yHigh;
        }

        
        private TransformGroup _transform = new TransformGroup();

        private void SetTransform(double xLow, double xHigh, double yLow, double yHigh)
        {
            //Update Layout First
            this.UpdateLayout();

            _transform.Children.Clear();

            double scaleX = Content.ActualWidth / (xHigh - xLow) * 1.0;
            double scaleY = Content.ActualHeight / (yHigh - yLow) * -1.0;

            TranslateTransform t1 = new TranslateTransform(xLow * -1, yLow * -1);
            ScaleTransform t2 = new ScaleTransform(scaleX, scaleY);
            TranslateTransform t3 = new TranslateTransform(0, Content.ActualHeight);

            _transform.Children.Add(t1);
            _transform.Children.Add(t2);
            _transform.Children.Add(t3);

        }

        public Point TransformXY(Point source)
        {
            return _transform.Transform(source);
        }

        public double TransformX(double source)
        {
            return _transform.Transform(new Point(source, 0)).X;
        }

        public double TransformY(double source)
        {
            return _transform.Transform(new Point(0, source)).Y;
        }


        
        private void DebugTest()
        {
            XAxis.RangeLowerLimit = -100;
            XAxis.RangeUpperLimit = 200;

            YAxis.RangeLowerLimit = 100;
            YAxis.RangeUpperLimit = 200;

            int count = 5;

            double xSpace = (XAxis.RangeUpperLimit - XAxis.RangeLowerLimit) / count;
            double ySpace = (YAxis.RangeUpperLimit - YAxis.RangeLowerLimit) / count;


            List<AxisTick> _xAxisTicks = new List<AxisTick>();
            List<AxisTick> _yAxisTicks = new List<AxisTick>();

            for (int i = 0; i < count-1; i++)
            {
                _xAxisTicks.Add(new AxisTick(XAxis.RangeLowerLimit + xSpace * (i + 1)));
                _yAxisTicks.Add(new AxisTick(YAxis.RangeLowerLimit + ySpace * (i + 1)));

            }
            XAxis.Ticks = _xAxisTicks;
            YAxis.Ticks = _yAxisTicks;
            
        }


    }
}
