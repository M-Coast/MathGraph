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
    public class CS2dAxisX : CS2dAxis
    {
        public CS2dAxisX()
        {

        }


        protected override Size MeasureOverride(Size constraint)
        {
            Size desiredSize = new Size(0, 0);

            desiredSize.Width = this.ActualWidth;

            double max = TickSize + TextMargin.Top + TextMargin.Bottom + FormatText("100.00", TextAlignment.Center).Height;

            for (int i = 0; i < Ticks.Count; i++)
            {
                FormattedText formarttedText = FormatText(Ticks[i].Text, TextAlignment.Center);
                double height = TickSize + TextMargin.Top + TextMargin.Bottom + formarttedText.Height;
                if (max < height) max = height;
            }

            desiredSize.Height = max;

            return desiredSize;
        }

        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            return base.ArrangeOverride(arrangeBounds);
        }

        protected override void DrawAxisLine(DrawingContext drawingContext)
        {
            drawingContext.DrawLine(
                new Pen(Foreground, Thickness),
                new Point(TransformX(LowerRange), 0),
                new Point(TransformX(UpperRange), 0)
                );
        }

        protected override void DrawAxisTicks(DrawingContext drawingContext)
        {
            if (Ticks == null) return;
            if (Ticks.Count < 1) return;

            for (int i = 0; i < Ticks.Count; i++)
            {
                double t = TransformX(Ticks[i].Value);

                drawingContext.DrawLine(
                    new Pen(Foreground, Thickness),
                    new Point(t, 0),
                    new Point(t, TickSize)
                    );

                FormattedText formarttedText = FormatText(Ticks[i].Text, TextAlignment.Center);

                drawingContext.DrawText(
                    formarttedText,
                    new Point(t, TickSize + TextMargin.Top)
                    );
            }
        }


    }
}
