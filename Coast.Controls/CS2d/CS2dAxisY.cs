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
    public class CS2dAxisY : CS2dAxis
    {
        public CS2dAxisY()
        {

        }


        protected override Size MeasureOverride(Size constraint)
        {
            Size desiredSize = new Size(0, 0);

            desiredSize.Height = this.ActualHeight;

            double max = TickSize + TextMargin.Right + TextMargin.Left + FormatText("100.00", TextAlignment.Right).Width;

            for (int i = 0; i < Ticks.Count; i++)
            {
                FormattedText formarttedText = FormatText(Ticks[i].Text, TextAlignment.Right);
                double width = TickSize + TextMargin.Right + TextMargin.Left + formarttedText.Width;
                if (max < width) max = width;
            }

            desiredSize.Width = max;

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
                new Point(this.ActualWidth, TransformY(LowerRange)),
                new Point(this.ActualWidth, TransformY(UpperRange))
                );
        }

        protected override void DrawAxisTicks(DrawingContext drawingContext)
        {
            if (Ticks == null) return;
            if (Ticks.Count < 1) return;

            for (int i = 0; i < Ticks.Count; i++)
            {
                double t = TransformY(Ticks[i].Value);

                drawingContext.DrawLine(
                    new Pen(Foreground, Thickness),
                    new Point(this.ActualWidth, t),
                    new Point(this.ActualWidth - TickSize, t)
                    );

                FormattedText formarttedText = FormatText(Ticks[i].Text, TextAlignment.Right);

                drawingContext.DrawText(
                    formarttedText,
                    new Point(this.ActualWidth - TickSize - TextMargin.Right, t - formarttedText.Height / 2)
                    );
            }
        }



    }
}
