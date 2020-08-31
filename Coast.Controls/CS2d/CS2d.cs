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
            set { _elements = value; UpdateElements(); }
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
                else if(element is CS2dCircle)
                {
                    DrawCircle(drawingContext, element as CS2dCircle);
                }
                else if(element is CS2dEllipse)
                {
                    DrawEllipse(drawingContext, element as CS2dEllipse);
                }
            }
        }



        private void UpdateElements()
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
                else if (element is CS2dCircle)
                {
                    //CS2dCircle t = element as CS2dCircle;

                    //if (xMin > t.CenterX - t.Radius) xMin = t.CenterX - t.Radius;
                    //if (xMax < t.CenterX + t.Radius) xMax = t.CenterX + t.Radius;
                    //if (yMin > t.CenterX - t.Radius) yMin = t.CenterX - t.Radius;
                    //if (yMax < t.CenterX + t.Radius) yMax = t.CenterX + t.Radius;
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


        private void DrawPoint(DrawingContext drawingContext, CS2dPoint element)
        {
            Point p = TransformXY(new Point(element.X, element.Y));

            double r = element.Size;

            Pen pen = element.Stroke == null ? null : new Pen(element.Stroke, 1);

            Brush brush = element.FillBrush;

            drawingContext.DrawEllipse(brush, pen, p, r, r);
        }

        //private void DrawLine(DrawingContext drawingContext, CS2dLine line)
        //{
        //    Point p0 = TransformXY(line.StartPoint);
        //    Point p1 = TransformXY(line.EndPoint);

        //    Pen stroke = line.Stroke == null ? null : new Pen(line.Stroke, line.Thickness);

        //    drawingContext.DrawLine(stroke, p0, p1);

        //}

        private void DrawLine(DrawingContext drawingContext, CS2dLine element)
        {
            Point p0 = TransformXY(new Point(XLowerRange, element.Value.GetY(XLowerRange)));
            Point p1 = TransformXY(new Point(XUpperRange, element.Value.GetY(XUpperRange)));

            Pen stroke = element.Stroke == null ? null : new Pen(element.Stroke, element.Thickness);

            drawingContext.DrawLine(stroke, p0, p1);

        }

        private void DrawCircle(DrawingContext drawingContext, CS2dCircle element)
        {
            Pen stroke = element.Stroke == null ? null : new Pen(element.Stroke, element.Thickness);

            drawingContext.DrawEllipse(
                null,
                stroke,
                TransformXY(element.Center),
                System.Math.Abs(TransformX(element.Radius) - TransformX(0)),
                System.Math.Abs(TransformY(element.Radius) - TransformY(0)));
        }

        private void DrawEllipse(DrawingContext drawingContext, CS2dEllipse element)
        {
            Pen stroke = element.Stroke == null ? null : new Pen(element.Stroke, element.Thickness);

            int count = 200;
            double space = System.Math.PI * 2.0 / count;
            Coast.Math.Vector2 shiftVector = new Coast.Math.Vector2(element.CenterX, element.CenterY);
            Coast.Math.Vector2 v = new Math.Vector2();

            PathGeometry pathGeometry = new PathGeometry();
            PathFigure pathFeature = new PathFigure();
            pathFeature.IsClosed = true;
            pathGeometry.Figures.Add(pathFeature);

            for (int i = 0; i < count; i++)
            {
                //参数方程
                //  x = a*cos(t)
                //  y = b*cos(t)
                v.X = System.Math.Cos(space * i) * element.RadiusA;
                v.Y = System.Math.Sin(space * i) * element.RadiusB;

                //旋转th
                v.Rotate(element.Rotation / 180 * System.Math.PI);

                //平移
                v.Add(shiftVector);

                Point p = new Point(v.X, v.Y);

                if (i == 0)
                {
                    pathFeature.StartPoint = TransformXY(p);
                }
                else
                {
                    LineSegment segment = new LineSegment() { Point = TransformXY(p) };
                    pathFeature.Segments.Add(segment);
                }

            }

            drawingContext.DrawGeometry(null, stroke, pathGeometry);

        }



    }
}
