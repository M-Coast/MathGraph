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
    public class CS2dAxis : Control
    {
        public CS2dAxis()
        {

        }


        internal double LowerRange { get; set; }
        internal double UpperRange { get; set; }
        internal double AxisShift { get; set; }

        internal Transform CSTransform { get; set; }

        internal ObservableCollection<CS2dAxisTick> Ticks { get; private set; } = new ObservableCollection<CS2dAxisTick>();


        //AxisBrush, TextBrush, <- Foreground
        //TextFont  <- 

        internal double Thickness
        {
            get { return (double)GetValue(ThicknessProperty); }
            set { SetValue(ThicknessProperty, value); }
        }

        internal static readonly DependencyProperty ThicknessProperty =
            DependencyProperty.Register("Thickness", typeof(double), typeof(CS2dAxis), new PropertyMetadata(1.0));



        internal double TickSize
        {
            get { return (double)GetValue(TickSizeProperty); }
            set { SetValue(TickSizeProperty, value); }
        }

        internal static readonly DependencyProperty TickSizeProperty =
            DependencyProperty.Register("TickSize", typeof(double), typeof(CS2dAxis), new PropertyMetadata(0.0));   //5.0



        internal Thickness TextMargin
        {
            get { return (Thickness)GetValue(TextMarginProperty); }
            set { SetValue(TextMarginProperty, value); }
        }

        internal static readonly DependencyProperty TextMarginProperty =
            DependencyProperty.Register("TextMargin", typeof(Thickness), typeof(CS2dAxis), new PropertyMetadata(new Thickness(10, 5, 10, 5)));

        

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            DrawAxisLine(drawingContext);
            DrawAxisTicks(drawingContext);
        }

        protected virtual void DrawAxisLine(DrawingContext drawingContext)
        {

        }

        protected virtual void DrawAxisTicks(DrawingContext drawingContext)
        {

        }


        protected double TransformY(double source)
        {
            if (CSTransform == null) return 0;
            return CSTransform.Transform(new Point(0, source)).Y + AxisShift;
        }

        protected double TransformX(double source)
        {
            if (CSTransform == null) return 0;
            return CSTransform.Transform(new Point(source, 0)).X + AxisShift;
        }
        
        protected FormattedText FormatText(string text, TextAlignment textAlignment = TextAlignment.Left)
        {
            Typeface typeface = new Typeface(
                this.FontFamily,
                this.FontStyle,
                this.FontWeight,
                this.FontStretch);

            FormattedText formattedText = new FormattedText(
                text,
                System.Globalization.CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                typeface,
                this.FontSize,
                this.Foreground);

            formattedText.TextAlignment = textAlignment;

            return formattedText;
        }

    }
}
