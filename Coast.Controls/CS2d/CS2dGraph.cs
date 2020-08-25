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
    public class CS2dGraph : Control
    {

        public CS2dGraph()
        {

        }

        public string Title { get; set; }
        public Transform Transform { get; set; }
        public ObservableCollection<CS2dShape> Elements { get; set; }


        public virtual void Plot()
        {
            this.InvalidateVisual();
        }


        protected Point TransformXY(Point source)
        {
            return Transform.Transform(source);
        }


        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if (Elements == null) return;
            if (Transform == null) return;

            foreach (CS2dShape element in Elements)
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