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

namespace Coast.Controls
{
    public class CS2dShape : DependencyObject
    {
        public double Thickness
        {
            get { return (double)GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }
        
        public static readonly DependencyProperty StrokeThicknessProperty =
            DependencyProperty.Register("StrokeThickness", typeof(double), typeof(CS2dShape), new PropertyMetadata(1.0));



        public Brush Stroke
        {
            get { return (Brush)GetValue(StrokeBrushProperty); }
            set { SetValue(StrokeBrushProperty, value); }
        }

        public static readonly DependencyProperty StrokeBrushProperty =
            DependencyProperty.Register("StrokeBrush", typeof(Brush), typeof(CS2dShape), new PropertyMetadata(new SolidColorBrush(Colors.Black)));


        public Brush FillBrush
        {
            get { return (Brush)GetValue(FillBrushProperty); }
            set { SetValue(FillBrushProperty, value); }
        }

        public static readonly DependencyProperty FillBrushProperty =
            DependencyProperty.Register("FillBrush", typeof(Brush), typeof(CS2dShape), new PropertyMetadata(new SolidColorBrush(Colors.Black)));


    }


    public class CS2dPoint:CS2dShape
    {
        //public Point Point { set; get; }
        public double X { set; get; }
        public double Y { set; get; }
        public double Size { set; get; }
        public int  Apperance { set; get; }
    }

    public class CS2dLine : CS2dShape
    {
        public Point StartPoint { set; get; }
        public Point EndPoint { set; get; }
    }

    public class CS2dCircle : CS2dShape
    {
        public Point Center { set; get; }
        public double Radius { set; get; }
    }

    public class CS2dArc : CS2dCircle
    {
        public double StartAngle { set; get; }
        public double EndAngle { set; get; }
    }

    public class CS2dEllipse : CS2dShape
    {
        public Point Center { set; get; }
        public double Radius1 { set; get; }
        public double Radius2 { set; get; }
    }

    public class CS2dEllipseArc : CS2dEllipse
    {
        public double StartAngle { set; get; }
        public double EndAngle { set; get; }
    }

    public class CS2dShapeCollection
    {
        public List<CS2dShape> Shapes;
    }

}
