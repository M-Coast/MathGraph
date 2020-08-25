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
using System.Globalization;

namespace Coast.Controls
{
    public class Axis : Control
    {
        public Axis()
        {
            //this.DefaultStyleKey = typeof(Axis);

            this.ClipToBounds = true;
            _transform = new TranslateTransform();
        }
        


        public AxisAlignment AxisAlignment
        {
            get { return (AxisAlignment)GetValue(AxisAligmentProperty); }
            set { SetValue(AxisAligmentProperty, value); }
        }

        public static readonly DependencyProperty AxisAligmentProperty =
            DependencyProperty.Register("AxisAlignment", typeof(AxisAlignment), typeof(Axis), new PropertyMetadata(
                AxisAlignment.VerticalLeft,
                new PropertyChangedCallback(OnAxisAlignmentChanged)));

        private static void OnAxisAlignmentChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Axis axis = (Axis)sender;
            axis.OnAxisAlignmentChanged(e);
        }

        protected virtual void OnAxisAlignmentChanged(DependencyPropertyChangedEventArgs e)
        {
            Update();
        }

        
        public double RangeLowerLimit
        {
            get { return (double)GetValue(RangeLowerLimitProperty); }
            set { SetValue(RangeLowerLimitProperty, value); }
        }

        public static readonly DependencyProperty RangeLowerLimitProperty =
            DependencyProperty.Register("RangeLowerLimit", typeof(double), typeof(Axis), new PropertyMetadata(
                (double)0,
                new PropertyChangedCallback(OnRangeLowerLimitChanged)));

        private static void OnRangeLowerLimitChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((Axis)sender).OnRangeLowerLimitChanged(e);
        }
        

        protected virtual void OnRangeLowerLimitChanged(DependencyPropertyChangedEventArgs e)
        {
            Update();
        }


        public double RangeUpperLimit
        {
            get { return (double)GetValue(RangeUpperLimitProperty); }
            set { SetValue(RangeUpperLimitProperty, value); }
        }

        public static readonly DependencyProperty RangeUpperLimitProperty =
            DependencyProperty.Register("RangeUpperLimit", typeof(double), typeof(Axis), new PropertyMetadata(
                (double)100,
                new PropertyChangedCallback(OnRangeUpperLimitChanged)));

        private static void OnRangeUpperLimitChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((Axis)sender).OnRangeUpperLimitChanged(e);
        }

        protected virtual void OnRangeUpperLimitChanged(DependencyPropertyChangedEventArgs e)
        {
            Update();
        }




        public double TickSize
        {
            get { return (double)GetValue(TickSizeProperty); }
            set { SetValue(TickSizeProperty, value); }
        }

        public static readonly DependencyProperty TickSizeProperty =
            DependencyProperty.Register("TickSize", typeof(double), typeof(Axis), new PropertyMetadata(
                (double)5,
                new PropertyChangedCallback(OnTickSizeChanged)));

        private static void OnTickSizeChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((Axis)sender).OnTickSizeChanged(e);
        }

        protected virtual void OnTickSizeChanged(DependencyPropertyChangedEventArgs e)
        {
            Update();
        }





        public double AxisThickness
        {
            get { return (double)GetValue(AxisThicknessProperty); }
            set { SetValue(AxisThicknessProperty, value); }
        }

        public static readonly DependencyProperty AxisThicknessProperty =
            DependencyProperty.Register("AxisThickness", typeof(double), typeof(Axis), new PropertyMetadata(
                (double)1,
                new PropertyChangedCallback(OnAxisThicknessChanged)));

        private static void OnAxisThicknessChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((Axis)sender).OnAxisThicknessChanged(e);
        }

        protected virtual void OnAxisThicknessChanged(DependencyPropertyChangedEventArgs e)
        {
            Update();
        }


        public Brush AxisBrush
        {
            get { return (Brush)GetValue(AxisBrushProperty); }
            set { SetValue(AxisBrushProperty, value); }
        }

        public static readonly DependencyProperty AxisBrushProperty =
            DependencyProperty.Register("AxisBrush", typeof(Brush), typeof(Axis), new PropertyMetadata(
                new SolidColorBrush(Colors.Black),
                new PropertyChangedCallback(OnAxisBrushChanged)));

        private static void OnAxisBrushChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((Axis)sender).OnAxisBrushChanged(e);
        }

        protected virtual void OnAxisBrushChanged(DependencyPropertyChangedEventArgs e)
        {
            Update();
        }


        public string TextFormat
        {
            get { return (string)GetValue(TextFormatProperty); }
            set { SetValue(TextFormatProperty, value); }
        }

        public static readonly DependencyProperty TextFormatProperty =
            DependencyProperty.Register("TextFormat", typeof(string), typeof(Axis), new PropertyMetadata(
                new PropertyChangedCallback(OnTextFormatChanged)));

        private static void OnTextFormatChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((Axis)sender).OnTextFormatChanged(e);
        }

        protected virtual void OnTextFormatChanged(DependencyPropertyChangedEventArgs e)
        {
            Update();
        }


        public int TickDivides
        {
            get { return (int)GetValue(TickDividesProperty); }
            set { SetValue(TickDividesProperty, value); }
        }

        public static readonly DependencyProperty TickDividesProperty =
            DependencyProperty.Register("TickDivides", typeof(int), typeof(Axis), new PropertyMetadata(
                1,
                new PropertyChangedCallback(OnTickDividesChanged)));

        private static void OnTickDividesChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((Axis)sender).OnTickDividesChanged(e);
        }

        protected virtual void OnTickDividesChanged(DependencyPropertyChangedEventArgs e)
        {
            Update();
        }


        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(Axis), new PropertyMetadata(
                new PropertyChangedCallback(OnTitleChanged)));

        private static void OnTitleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ((Axis)sender).OnTitleChanged(e);
        }

        protected virtual void OnTitleChanged(DependencyPropertyChangedEventArgs e)
        {
            Update();
        }




        private List<AxisTick> _ticks = null;
        public List<AxisTick> Ticks
        {
            get { return _ticks; }
            set { _ticks = new List<AxisTick>(value.ToArray()); Update(); }
        }



        private void SetupTransform()
        {
            TransformGroup tsg = new TransformGroup();
            TranslateTransform t1 = null;
            ScaleTransform t2 = null;
            TranslateTransform t3 = null;

            if (AxisAlignment == AxisAlignment.VerticalLeft || AxisAlignment == AxisAlignment.VerticalRight)
            {
                double scale = this.ActualHeight / (RangeUpperLimit - RangeLowerLimit) * -1.0;
                t1 = new TranslateTransform(RangeLowerLimit * -1.0, 0);
                t2 = new ScaleTransform(scale, 1);
                t3 = new TranslateTransform(this.ActualHeight, 0);
            }
            else if (AxisAlignment == AxisAlignment.HorizontalTop || AxisAlignment == AxisAlignment.HorizontalBottom)
            {
                double scale = this.ActualWidth / (RangeUpperLimit - RangeLowerLimit) * 1.0;
                t1 = new TranslateTransform(RangeLowerLimit * -1, 0);
                t2 = new ScaleTransform(scale, 1);
                t3 = new TranslateTransform(0, 0);
            }

            tsg.Children.Add(t1);
            tsg.Children.Add(t2);
            tsg.Children.Add(t3);

            _transform = tsg;
        }
        
        private double TransformValue(double value)
        {
            return _transform.Transform(new Point(value,0)).X;
        }


        public override void OnApplyTemplate()
        {

        }

        protected override Size MeasureOverride(Size constraint)
        {
            Size desiredSize = new Size(0, 0);

            if (!double.IsInfinity(constraint.Height))
                desiredSize.Height = constraint.Height;
            else
                desiredSize.Height = 100;


            if (!double.IsInfinity(constraint.Width))
                desiredSize.Width = constraint.Width;
            else
                desiredSize.Width = 100;
            
            if (AxisAlignment == AxisAlignment.VerticalLeft || AxisAlignment == AxisAlignment.VerticalRight)
            {
                double n = _textMaxWidth > 1 ? _textMaxWidth : 80;
                desiredSize.Width = TickSize + _textMargin + n + _textMargin+2;
            }
            else if ( AxisAlignment == AxisAlignment.HorizontalTop || AxisAlignment == AxisAlignment.HorizontalBottom )
            {
                double n = _textMaxHeight > 1 ? _textMaxHeight : 20;
                desiredSize.Height = TickSize + _textMargin + n + _textMargin+2;
            }

            return desiredSize;
        }
        
        protected override Size ArrangeOverride(Size arrangeBounds)
        {
            Update();
            return base.ArrangeOverride(arrangeBounds);
        }
        
        protected override void OnRender(DrawingContext drawingContext)
        {
            RenderAxis(drawingContext);
            base.OnRender(drawingContext);
        }
        
        public void Update()
        {
            this.InvalidateVisual();
        }


        private Transform _transform = null;    //Transform Only to use X as value

        private List<double> _transformedPositions = new List<double>(16);

        private List<FormattedText> _formattedTexts = new List<FormattedText>(16);

        private double _textMaxWidth = 0;

        private double _textMaxHeight = 0;

        private double _textMargin = 3;


        private void RenderAxis(DrawingContext drawingContext)
        {
            if (_ticks == null) return;

            PreRenderAxis(drawingContext);

            if (AxisAlignment == AxisAlignment.VerticalLeft)
                RenderAxis_VerticalLeft(drawingContext);

            if (AxisAlignment == AxisAlignment.VerticalRight)
                RenderAxis_VerticalRight(drawingContext);

            if (AxisAlignment == AxisAlignment.HorizontalBottom)
                RenderAxis_HorizontalBottom(drawingContext);

            if (AxisAlignment == AxisAlignment.HorizontalTop)
                RenderAxis_HorizontalTop(drawingContext);

        }
        
        private void PreRenderAxis(DrawingContext drawingContext)
        {
            _transformedPositions.Clear();
            _formattedTexts.Clear();
            _textMaxWidth = 0;
            _textMaxHeight = 0;

            _ticks.OrderBy(p => p.Value);

            SetupTransform();

            foreach (AxisTick a in Ticks)
            {
                _transformedPositions.Add(TransformValue(a.Value));

                if (string.IsNullOrEmpty(a.Text)) { a.Text = string.IsNullOrEmpty(TextFormat) ? a.Value.ToString() : a.Value.ToString(TextFormat); }

                FormattedText ft = GetFormattedText(a.Text);
                _formattedTexts.Add(ft);
                if (_textMaxWidth < ft.Width) _textMaxWidth = ft.Width;
                if (_textMaxHeight < ft.Height) _textMaxHeight = ft.Height;
            }
        }

        private void RenderAxis_VerticalLeft(DrawingContext drawingContext)
        {
            Pen axisPen = new Pen(AxisBrush, AxisThickness);

            double W = this.ActualWidth;
            double H = this.ActualHeight;

            //Draw Border
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Left), new Point(0, 0), new Point(0, H));
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Top), new Point(0, 0), new Point(W, 0));
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Bottom), new Point(0, H), new Point(W, H));

            //Draw Axis Line
            drawingContext.DrawLine(axisPen, new Point(W, 0), new Point(W, H)); 

            //Draw Ticks & Texts
            for (int i = 0; i < Ticks.Count; i++)
            {
                double y = _transformedPositions[i];

                drawingContext.DrawLine(axisPen, new Point(W - TickSize, y), new Point(W, y));

                //Draw Sub ticks
                if (i < Ticks.Count - 1 && TickDivides > 1)
                {
                    double space = (_transformedPositions[i + 1] - _transformedPositions[i]) / TickDivides;
                    for (int t = 0; t < TickDivides - 1; t++)
                    {
                        drawingContext.DrawLine(axisPen, new Point(W - TickSize / 2.0, y + space * (t + 1)), new Point(W, y + space * (t + 1)));
                    }
                }

                if (Ticks[i].DisplayText)
                {
                    drawingContext.DrawText(_formattedTexts[i],
                        new Point(
                            W - TickSize - _textMargin - _textMaxWidth - (_textMaxWidth - _formattedTexts[i].Width),
                            y - _formattedTexts[i].Height / 2.0));
                }
                //drawingContext.DrawText(_formattedTexts[i],
                //    new Point(_textMargin + _textMaxWidth - _formattedTexts[i].Width, y - _formattedTexts[i].Height / 2.0));
            }
        }

        private void RenderAxis_VerticalRight(DrawingContext drawingContext)
        {
            Pen axisPen = new Pen(AxisBrush, AxisThickness);

            double W = this.ActualWidth;
            double H = this.ActualHeight;

            //Draw Border
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Right), new Point(W, 0), new Point(W, H));
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Top), new Point(0, 0), new Point(W, 0));
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Bottom), new Point(0, H), new Point(W, H));

            //Draw Axis Line
            drawingContext.DrawLine(axisPen, new Point(0, 0), new Point(0, H));

            //Draw Ticks & Texts
            for (int i = 0; i < Ticks.Count; i++)
            {
                double y = _transformedPositions[i];

                drawingContext.DrawLine(axisPen, new Point(0, y), new Point(TickSize, y));

                //Draw Sub ticks
                if (i < Ticks.Count - 1 && TickDivides > 1)
                {
                    double space = (_transformedPositions[i + 1] - _transformedPositions[i]) / TickDivides;
                    for (int t = 0; t < TickDivides - 1; t++)
                    {
                        drawingContext.DrawLine(axisPen, new Point(0, y + space * (t + 1)), new Point(TickSize / 2.0, y + space * (t + 1)));
                    }
                }

                if (Ticks[i].DisplayText)
                {
                    drawingContext.DrawText(_formattedTexts[i],
                    new Point(
                        TickSize + _textMargin + _textMaxWidth - _formattedTexts[i].Width,
                        y - _formattedTexts[i].Height / 2.0));
                }
            }
        }

        private void RenderAxis_HorizontalBottom(DrawingContext drawingContext)
        {
            Pen axisPen = new Pen(AxisBrush, AxisThickness);

            double W = this.ActualWidth;
            double H = this.ActualHeight;

            //Draw Border
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Left), new Point(W, 0), new Point(W, H));
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Right), new Point(W, 0), new Point(W, H));
            //drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Top), new Point(0, 0), new Point(W, 0));
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Bottom), new Point(0, H), new Point(W, H));

            //Draw Axis Line
            drawingContext.DrawLine(axisPen, new Point(0, 0), new Point(W, 0));

            //Draw Ticks & Texts
            for (int i = 0; i < Ticks.Count; i++)
            {
                double x = _transformedPositions[i];

                drawingContext.DrawLine(axisPen, new Point(x, 0), new Point(x, TickSize));

                //Draw Sub ticks
                if (i < Ticks.Count - 1 && TickDivides > 1)
                {
                    double space = (_transformedPositions[i + 1] - _transformedPositions[i]) / TickDivides;
                    for (int t = 0; t < TickDivides - 1; t++)
                    {
                        drawingContext.DrawLine(axisPen, new Point(x + space * (t + 1), 0), new Point( x + space * (t + 1), TickSize / 2.0));
                    }
                }

                if (Ticks[i].DisplayText)
                {
                    drawingContext.DrawText(_formattedTexts[i],
                    new Point(
                        x- _formattedTexts[i].Width / 2.0,
                        TickSize + _textMargin));
                }
            }
        }

        private void RenderAxis_HorizontalTop(DrawingContext drawingContext)
        {
            Pen axisPen = new Pen(AxisBrush, AxisThickness);

            double W = this.ActualWidth;
            double H = this.ActualHeight;

            //Draw Border
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Left), new Point(W, 0), new Point(W, H));
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Right), new Point(W, 0), new Point(W, H));
            drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Top), new Point(0, 0), new Point(W, 0));
            //drawingContext.DrawLine(new Pen(BorderBrush, BorderThickness.Bottom), new Point(0, H), new Point(W, H));

            //Draw Axis Line
            drawingContext.DrawLine(axisPen, new Point(0, H), new Point(W, H));

            //Draw Ticks & Texts
            for (int i = 0; i < Ticks.Count; i++)
            {
                double x = _transformedPositions[i];

                drawingContext.DrawLine(axisPen, new Point(x, H-TickSize), new Point(x, H));

                //Draw Sub ticks
                if (i < Ticks.Count - 1 && TickDivides > 1)
                {
                    double space = (_transformedPositions[i + 1] - _transformedPositions[i]) / TickDivides;
                    for (int t = 0; t < TickDivides - 1; t++)
                    {
                        drawingContext.DrawLine(axisPen, new Point(x + space * (t + 1), H - TickSize / 2.0), new Point(x + space * (t + 1), H));
                    }
                }

                if (Ticks[i].DisplayText)
                {
                    drawingContext.DrawText(_formattedTexts[i],
                    new Point(
                        x - _formattedTexts[i].Width / 2.0,
                        H- TickSize - _textMargin- _formattedTexts[i].Height));
                }
            }
        }

        private FormattedText GetFormattedText(string text)
        {
            Typeface typeface = new Typeface(
                this.FontFamily,
                this.FontStyle,
                this.FontWeight,
                this.FontStretch);

            FormattedText formattedText = new FormattedText(
                text,
                CultureInfo.CurrentCulture,
                FlowDirection.LeftToRight,
                typeface,
                this.FontSize,
                this.Foreground);
            
            formattedText.TextAlignment = TextAlignment.Left;
            
            return formattedText;
        }
       




    }
}







//public class Axis_BK : Control
//{

//    public Axis()
//    {
//        this.DefaultStyleKey = typeof(Axis);
//        Transform = new TranslateTransform();





//    }

//    private Grid _mainGrid;
//    private Canvas _tickPanel=new Canvas();
//    private Canvas _textPanel = new Canvas();

//    public override void OnApplyTemplate()
//    {
//        _mainGrid = (Grid)Template.FindName("PART_MainGrid", this);
//        _mainGrid.Children.Add(_tickPanel);
//        _mainGrid.Children.Add(_textPanel);

//        _tickPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
//        _tickPanel.VerticalAlignment = VerticalAlignment.Stretch;

//        _textPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
//        _textPanel.VerticalAlignment = VerticalAlignment.Stretch;

//        SetDefault();
//        //base.OnApplyTemplate();
//    }

//    public AxisAlignment AxisAlignment
//    {
//        get { return (AxisAlignment)GetValue(AxisAligmentProperty); }
//        set { SetValue(AxisAligmentProperty, value); }
//    }

//    public static readonly DependencyProperty AxisAligmentProperty =
//        DependencyProperty.Register("AxisAlignment", typeof(AxisAlignment), typeof(Axis), new PropertyMetadata(
//            AxisAlignment.VerticalLeft,
//            new PropertyChangedCallback(OnAxisAlignmentChanged)));

//    private static void OnAxisAlignmentChanged(object sender, DependencyPropertyChangedEventArgs e)
//    {
//        Axis axis = (Axis)sender;
//        axis.OnAxisAlignmentChanged(e);
//    }


//    public double TickSize
//    {
//        get { return (double)GetValue(TickSizeProperty); }
//        set { SetValue(TickSizeProperty, value); }
//    }

//    public static readonly DependencyProperty TickSizeProperty =
//        DependencyProperty.Register("TickSize", typeof(double), typeof(Axis), new PropertyMetadata((double)5));






//    protected virtual void OnAxisAlignmentChanged(DependencyPropertyChangedEventArgs e)
//    {
//        AxisAlignment oldValue = (AxisAlignment)e.OldValue;
//        AxisAlignment newValue = (AxisAlignment)e.NewValue;

//        _mainGrid.ColumnDefinitions.Clear();

//        if (newValue == AxisAlignment.VerticalLeft)
//        {
//            _mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
//            _mainGrid.ColumnDefinitions.Add(new ColumnDefinition() {Width = GridLength.Auto });// { Width = new GridLength(TickSize) });

//            _tickPanel.SetValue(Grid.RowProperty, 0);
//            _tickPanel.SetValue(Grid.ColumnProperty, 1);

//            _textPanel.SetValue(Grid.RowProperty, 0);
//            _textPanel.SetValue(Grid.ColumnProperty, 0);
//        }
//    }



//    public double Minimum
//    {
//        get { return (double)GetValue(MinimumProperty); }
//        set { SetValue(MinimumProperty, value); }
//    }

//    public static readonly DependencyProperty MinimumProperty =
//        DependencyProperty.Register("Minimum", typeof(double), typeof(Axis), new PropertyMetadata(
//            0.0,
//            new PropertyChangedCallback(OnMinimumChanged)));

//    private static void OnMinimumChanged(object sender, DependencyPropertyChangedEventArgs e)
//    {
//        ((Axis)sender).OnMinimumChanged(e);
//    }

//    protected virtual void OnMinimumChanged(DependencyPropertyChangedEventArgs e)
//    {
//        SetTransform();
//    }


//    public double Maximum
//    {
//        get { return (double)GetValue(MaximumProperty); }
//        set { SetValue(MaximumProperty, value); }
//    }

//    public static readonly DependencyProperty MaximumProperty =
//        DependencyProperty.Register("Maximum", typeof(double), typeof(Axis), new PropertyMetadata(
//            1.0,
//            new PropertyChangedCallback(OnMaximumChanged)));

//    private static void OnMaximumChanged(object sender, DependencyPropertyChangedEventArgs e)
//    {
//        ((Axis)sender).OnMaximumChanged(e);
//    }

//    protected virtual void OnMaximumChanged(DependencyPropertyChangedEventArgs e)
//    {
//        SetTransform();
//    }




//    public Transform Transform
//    {
//        get { return (Transform)GetValue(TransformProperty); }
//        set { SetValue(TransformProperty, value); }
//    }

//    public static readonly DependencyProperty TransformProperty =
//        DependencyProperty.Register("Transform", typeof(Transform), typeof(Axis), new PropertyMetadata(
//            null,
//            new PropertyChangedCallback(OnTransformChanged)));

//    private static void OnTransformChanged(object sender, DependencyPropertyChangedEventArgs e)
//    {
//        ((Axis)sender).OnTransformChanged(e);
//    }

//    protected virtual void OnTransformChanged(DependencyPropertyChangedEventArgs e)
//    {

//    }




//    void SetTransform()
//    {
//        //Transform.Value.
//        double scaleY = (Maximum - Minimum) / ActualHeight;
//        //double offsetY = 
//    }




//    protected virtual void SetDefault()
//    {
//        _mainGrid.ColumnDefinitions.Clear();

//        if (AxisAlignment == AxisAlignment.VerticalLeft)
//        {
//            _mainGrid.ColumnDefinitions.Add(new ColumnDefinition());
//            _mainGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(TickSize) });

//            _tickPanel.SetValue(Grid.RowProperty, 0);
//            _tickPanel.SetValue(Grid.ColumnProperty, 1);

//            _textPanel.SetValue(Grid.RowProperty, 0);
//            _textPanel.SetValue(Grid.ColumnProperty, 0);
//        }
//    }


//    List<AxisTick> _axisTicks = null;
//    public List<AxisTick> AxisTicks
//    {
//        get { return _axisTicks; }
//        set { _axisTicks = value; DoAxis(); }
//    }


//    private int _markCount = 0;
//    private List<Line> _tickLines = new List<Line>();
//    private List<TextBlock> _valueTexts = new List<TextBlock>();


//    public double TickThickness = 1;
//    public Color TickColor = Colors.Red;

//    private void SetupAxisTickCount()
//    {
//        if (_markCount< AxisTicks.Count)
//        {
//            foreach (AxisTick at in AxisTicks)
//            {

//                Line l = new Line() { StrokeThickness = TickThickness, Stroke = new SolidColorBrush(Colors.Red) };
//            }


//        }

//    }

//    private void DoAxis()
//    {
//        _textPanel.Children.Clear();
//        _tickPanel.Children.Clear();

//        foreach(AxisTick at in AxisTicks)
//        {
//            Point p = Transform.Transform(new Point(0, at.Value));
//            double y = p.Y;

//            Line l = new Line() { StrokeThickness = 1, Stroke = new SolidColorBrush(Colors.Red) };
//            l.X1 = 0;
//            l.Y1 = y;
//            l.X2 = _tickPanel.ActualWidth;
//            l.Y2 = y;

//            TextBlock textBlock = new TextBlock();



//            _tickPanel.Children.Add(l);
//            _textPanel.Children.Add(textBlock);



//            textBlock.Text = at.Value.ToString();
//            textBlock.TextAlignment = TextAlignment.Right;
//            textBlock.HorizontalAlignment = HorizontalAlignment.Right;
//            textBlock.UpdateLayout();
//            //textBlock.VerticalAlignment 
//            textBlock.SetValue(Canvas.TopProperty, y- textBlock.ActualWidth/2);


//        }


//    }


//    protected override void OnRender(DrawingContext drawingContext)
//    {

//        base.OnRender(drawingContext);
//    }

//    private void DrawTick(DrawingContext drawingContext)
//    {

//    }




//}

//