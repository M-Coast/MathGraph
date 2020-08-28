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

namespace Coast.Controls
{
    public class CS2d : CS2dBase
    {
        public CS2d()
        {

        }


        public ObservableCollection<CS2dShape> Elements
        {
            get { return _elements; }
            set { _elements = value; SetRange(); }
        }



        public double RangeExtensionRate
        {
            get { return (double)GetValue(RangeExtensionRateProperty); }
            set { SetValue(RangeExtensionRateProperty, value); }
        }
        
        public static readonly DependencyProperty RangeExtensionRateProperty =
            DependencyProperty.Register("RangeExtensionRate", typeof(double), typeof(CS2d), new PropertyMetadata(0.1));



        protected override void OnGraphRender(DrawingContext drawingContext)
        {
            //TestDrawALine(drawingContext);

            if (_elements == null) return;
            if (CSTransform == null) return;

            foreach (CS2dShape element in _elements)
            {
                if (element is CS2dPoint)
                {
                    DrawPoint(drawingContext, element as CS2dPoint);
                }
                else if (element is CS2dLine)
                {
                    DrawLine(drawingContext, element as CS2dLine);
                }
            }
        }



        private void SetRange()
        {
            if (_elements == null) return;

            double xMin = double.MaxValue;
            double xMax = double.MinValue;
            double yMin = double.MaxValue;
            double yMax = double.MinValue;
            foreach (CS2dShape element in _elements)
            {
                if (element is CS2dPoint)
                {
                    CS2dPoint t = element as CS2dPoint;
                    if (xMin > t.X) xMin = t.X;
                    if (xMax < t.X) xMax = t.X;
                    if (yMin > t.Y) yMin = t.Y;
                    if (yMax < t.Y) yMax = t.Y;
                }
                else if (element is CS2dLine)
                {
                    CS2dLine t = element as CS2dLine;

                    if (xMin > t.StartPoint.X) xMin = t.StartPoint.X;
                    if (xMax < t.StartPoint.X) xMax = t.StartPoint.X;
                    if (yMin > t.StartPoint.Y) yMin = t.StartPoint.Y;
                    if (yMax < t.StartPoint.Y) yMax = t.StartPoint.Y;

                    if (xMin > t.EndPoint.X) xMin = t.EndPoint.X;
                    if (xMax < t.EndPoint.X) xMax = t.EndPoint.X;
                    if (yMin > t.EndPoint.Y) yMin = t.EndPoint.Y;
                    if (yMax < t.EndPoint.Y) yMax = t.EndPoint.Y;

                }
            }
            if (xMax - xMin <= 0) Errored = true;
            if (yMax - yMin <= 0) Errored = true;

            DisireXLowerRange = xMin - (xMax - xMin) * RangeExtensionRate;
            DisireXUpperRange = xMax + (xMax - xMin) * RangeExtensionRate;
            DisireYLowerRange = yMin - (yMax - yMin) * RangeExtensionRate;
            DisireYUpperRange = yMax + (yMax - yMin) * RangeExtensionRate;
        }


        private void TestDrawALine(DrawingContext drawingContext)
        {
            drawingContext.DrawLine(new Pen(new SolidColorBrush(Colors.Red), 1), CSTransform.Transform(new Point(0, 0)), CSTransform.Transform(new Point(80, 50)));
        }




        public ObservableCollection<CS2dShape> _elements = null;

        private void DrawPoint(DrawingContext drawingContext, CS2dPoint point)
        {
            Point p = TransformXY(new Point(point.X, point.Y));

            double r = point.Size;

            Pen pen = point.Stroke == null ? null : new Pen(point.Stroke, 1);

            Brush brush = point.FillBrush;

            drawingContext.DrawEllipse(brush, pen, p, r, r);
        }

        private void DrawLine(DrawingContext drawingContext, CS2dLine line)
        {
            Point p0 = TransformXY(line.StartPoint);
            Point p1 = TransformXY(line.EndPoint);

            Pen stroke = line.Stroke == null ? null : new Pen(line.Stroke, line.Thickness);

            drawingContext.DrawLine(stroke, p0, p1);

        }




    }
}
