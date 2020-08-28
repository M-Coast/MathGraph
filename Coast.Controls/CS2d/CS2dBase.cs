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
    public class CS2dBase : Control
    {
        static CS2dBase()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CS2dBase), new FrameworkPropertyMetadata(typeof(CS2dBase)));
        }

        public CS2dBase()
        {
            this.Loaded += new RoutedEventHandler(this_Loaded);
            InitializeCommands();
        }


        #region Set / Get Properties

        public double DisireXLowerRange
        {
            get { return _disireXLowerRange; }
            set { _disireXLowerRange = value; ApplyDisireRange();}
        }

        public double DisireXUpperRange
        {
            get { return _disireXUpperRange; }
            set { _disireXUpperRange = value; ApplyDisireRange();}
        }

        public double DisireYLowerRange
        {
            get { return _disireYLowerRange; }
            set { _disireYLowerRange = value; ApplyDisireRange(); }
        }

        public double DisireYUpperRange
        {
            get { return _disireYUpperRange; }
            set { _disireYUpperRange = value; ApplyDisireRange(); }
        }

        public int DisireXTicksCount
        {
            get { return _disireXTicksCount; }
            set { _disireXTicksCount = value; }
        }

        public int DisireYTicksCount
        {
            get { return _disireYTicksCount; }
            set { _disireYTicksCount = value; }
        }

        public CS2dScaleMode ScaleMode
        {
            get { return _scaleMode; }
            set { _scaleMode = value; }
        }



        public double AxisThickness
        {
            set { _axisX.Thickness = value; _axisY.Thickness = value; }
        }

        public double AxisTickSize
        {
            set { _axisX.TickSize = value; _axisY.TickSize = value; }
        }

        public Thickness AxisTextMargin
        {
            set { _axisX.TextMargin = value; _axisY.TextMargin = value; }
        }

        public Brush DividerBrush
        {
            get { return _dividerBrush; }
            set { _dividerBrush = value; }
        }

        public double DividerThickness
        {
            get { return _dividerThickness; }
            set { _dividerThickness = value; }
        }

        public Brush GraphBorderBrush
        {
            get { return _graphBorderBrush; }
            set { _graphBorderBrush = value; }
        }

        public Thickness GraphBorderThickness
        {
            get { return _graphBorderThickness; }
            set { _graphBorderThickness = value; }
        }

        public Brush ReferenceLineBrush
        {
            get { return _referenceLineBrush; }
            set { _referenceLineBrush = value; }
        }

        public double ReferenceLineThickness
        {
            get { return _referenceLineThickness; }
            set { _referenceLineThickness = value; }
        }

        public double ReferenceLineX
        {
            get { return _referenceLineX; }
            set { _referenceLineX = value; }
        }

        public double ReferenceLineY
        {
            get { return _referenceLineY; }
            set { _referenceLineY = value; }
        }



        public double XLowerRange
        {
            get { return _xLowerRange; }
        }

        public double XUpperRange
        {
            get { return _xUpperRange; }
        }

        public double YLowerRange
        {
            get { return _yLowerRange; }
        }

        public double YUpperRange
        {
            get { return _yUpperRange; }
        }

        public int XTicksCount
        {
            get { return (int)_xTicksCount; }
        }

        public int YTicksCount
        {
            get { return (int)_yTicksCount; }
        }

        public Transform CSTransform
        {
            get { return _csTransform; }
        }

        //public bool CSInitialized { get; protected set; } = false;

        public bool Errored { get; protected set; } = true; //Initializea as True

        protected bool CanPlot
        {
            get { return !Errored; }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(CS2dBase), new PropertyMetadata(string.Empty));




        public string MousePositionText
        {
            get { return (string)GetValue(MousePositionTextProperty); }
            set { SetValue(MousePositionTextProperty, value); }
        }

        public static readonly DependencyProperty MousePositionTextProperty =
            DependencyProperty.Register("MousePositionText", typeof(string), typeof(CS2dBase), new PropertyMetadata(string.Empty));



        #endregion


        private double _disireXLowerRange = 0;
        private double _disireXUpperRange = 0;
        private double _disireYLowerRange = 0;
        private double _disireYUpperRange = 0;

        //private double _disireXLowerRange = -100;
        //private double _disireXUpperRange = 100;
        //private double _disireYLowerRange = -100;
        //private double _disireYUpperRange = 100;

        private int _disireXTicksCount = 5;
        private int _disireYTicksCount = 5;


        private double _xLowerRange = 0;
        private double _xUpperRange = 0;
        private double _yLowerRange = 0;
        private double _yUpperRange = 0;

        private double _xTicksCount = 0;
        private double _yTicksCount = 0;

        private CS2dScaleMode _scaleMode = CS2dScaleMode.Default;

        private TransformGroup _csTransform = new TransformGroup();

        private Brush _dividerBrush = new SolidColorBrush(Colors.LightGray);
        private double _dividerThickness = 0.5;

        private Brush _graphBorderBrush = new SolidColorBrush(Colors.Gray);
        private Thickness _graphBorderThickness = new Thickness(0, 1, 1, 0);

        private Brush _referenceLineBrush = new SolidColorBrush(Colors.Blue);
        private double _referenceLineThickness = 0.5;
        private double _referenceLineX = 0;
        private double _referenceLineY = 0;


        private Grid _mainGrid = null;
        private CS2dAxisX _axisX = null;
        private CS2dAxisY _axisY = null;
        private CS2dGraph _graph = null;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _axisX = (CS2dAxisX)Template.FindName("PART_AxisX", this);
            _axisY = (CS2dAxisY)Template.FindName("PART_AxisY", this);
            _graph = (CS2dGraph)Template.FindName("PART_Graph", this);
            _mainGrid = (Grid)Template.FindName("PART_MainGrid", this);

            _graph.CS = this;

        }


        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            SetCS();
            Plot();
        }

        private void this_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyDisireRange();
        }
        
        private void ApplyDisireRange()
        {
            SetRange();
            CheckRange();
            SetCS();
            Plot();
        }

        private void SetRange()
        {
            if (_graph == null) return;

            if (_scaleMode == CS2dScaleMode.Default || _scaleMode == CS2dScaleMode.XYSameScale)
            {
                if (_graph.ActualWidth <= 0) return;
                if (_graph.ActualHeight <= 0) return;

                if ((_disireXUpperRange - _disireXLowerRange) / _graph.ActualWidth > (_disireYUpperRange - _disireYLowerRange) / _graph.ActualHeight)
                {
                    _xLowerRange = _disireXLowerRange;
                    _xUpperRange = _disireXUpperRange;
                    _xTicksCount = _disireXTicksCount;

                    double scale = (_xUpperRange - _xLowerRange) / _graph.ActualWidth;
                    double space = (_xUpperRange - _xLowerRange) / _disireXTicksCount;

                    //RangeLower Extend
                    _yLowerRange = _disireYLowerRange;
                    _yUpperRange = _disireYLowerRange + scale * _graph.ActualHeight;
                    _yTicksCount = ((_yUpperRange - _yLowerRange) / space);

                    //RangeCenter Extend
                    //_yLowerRange = (_disireYLowerRange + _disireYUpperRange) / 2 - scale * _graph.ActualHeight / 2;
                    //_yUpperRange = (_disireYLowerRange + _disireYUpperRange) / 2 + scale * _graph.ActualHeight / 2;
                    //_yTicksCount = ((_yUpperRange - _yLowerRange) / space);
                }
                else
                {
                    _yLowerRange = _disireYLowerRange;
                    _yUpperRange = _disireYUpperRange;
                    _yTicksCount = _disireYTicksCount;

                    double scale = (_yUpperRange - _yLowerRange) / _graph.ActualHeight;
                    double space = (_yUpperRange - _yLowerRange) / _disireYTicksCount;

                    //RangeLower Extend
                    _xLowerRange = _disireXLowerRange;
                    _xUpperRange = _disireXLowerRange + scale * _graph.ActualWidth;
                    _xTicksCount = ((_xUpperRange - _xLowerRange) / space);

                    //RangeCenter Extend
                    //_xLowerRange = (_disireXLowerRange + _disireXUpperRange) / 2 - scale * _graph.ActualWidth / 2;
                    //_xUpperRange = (_disireXLowerRange + _disireXUpperRange) / 2 + scale * _graph.ActualWidth / 2;
                    //_xTicksCount = ((_xUpperRange - _xLowerRange) / space);
                }
            }
            else if (_scaleMode == CS2dScaleMode.Self)
            {
                _xLowerRange = _disireXLowerRange;
                _xUpperRange = _disireXUpperRange;
                _xTicksCount = _disireXTicksCount;
                _yLowerRange = _disireYLowerRange;
                _yUpperRange = _disireYUpperRange;
                _yTicksCount = _disireYTicksCount;
            }
            else
            {

            }
        }
        
        private bool CheckRange()
        {
            bool t = true;

            t = t && _xLowerRange < _xUpperRange;
            t = t && !_xUpperRange.WeakEquals(_xLowerRange);
            t = t && _yLowerRange < _yUpperRange;
            t = t && !_yUpperRange.WeakEquals(_yLowerRange);

            if (t)
            {
                Errored = false;
                return false;
            }
            else
            {
                Errored = true;
                return true;
            }
        }

        private void SetCS()
        {
            SetTransform();
            SetAxis();
        }

        private void SetTransform()
        {
            if (Errored) return;

            _csTransform.Children.Clear();

            double scaleX = _graph.ActualWidth / (_xUpperRange - _xLowerRange) * 1.0;
            double scaleY = _graph.ActualHeight / (_yUpperRange - _yLowerRange) * -1.0;

            TranslateTransform t1 = new TranslateTransform(_xLowerRange * -1.0, _yLowerRange * -1.0);
            ScaleTransform t2 = new ScaleTransform(scaleX, scaleY);
            TranslateTransform t3 = new TranslateTransform(0, _graph.ActualHeight);

            _csTransform.Children.Add(t1);
            _csTransform.Children.Add(t2);
            _csTransform.Children.Add(t3);

        }

        private void SetAxis()
        {
            if (Errored) return;

            Point referencePoint = _graph.TranslatePoint(new Point(0, 0), _mainGrid);
            double space = 0;

            _axisX.CSTransform = _csTransform;
            _axisX.AxisShift = referencePoint.X;
            _axisX.LowerRange = _xLowerRange;
            _axisX.UpperRange = _xUpperRange;

            space = (_xUpperRange - _xLowerRange) / _xTicksCount;

            _axisX.Ticks.Clear();

            for (int i = 0; i <= _xTicksCount; i++)
            {
                double v = _xLowerRange + space * i;
                string t = v.ToString("F2");
                _axisX.Ticks.Add(new CS2dAxisTick(v, t));
            }


            _axisY.CSTransform = _csTransform;
            _axisY.AxisShift = referencePoint.Y;
            _axisY.LowerRange = _yLowerRange;
            _axisY.UpperRange = _yUpperRange;

            space = (_yUpperRange - _yLowerRange) / _yTicksCount;

            _axisY.Ticks.Clear();

            for (int i = 0; i <= _yTicksCount; i++)
            {
                double v = _yLowerRange + space * i;
                string t = v.ToString("F2");
                _axisY.Ticks.Add(new CS2dAxisTick(v, t));
            }


            _axisX.InvalidateVisual();
            _axisY.InvalidateVisual();

        }


        public Point TransformXY(Point source)
        {
            return _csTransform.Transform(source);
        }

        public double TransformX(double source)
        {
            return _csTransform.Transform(new Point(source, 0)).X;
        }

        public double TransformY(double source)
        {
            return _csTransform.Transform(new Point(0, source)).Y;
        }



        protected void Plot()
        {
            if (!CanPlot) return;
            _graph.InvalidateVisual();
        }

        private void DrawGraphAuxGrid(DrawingContext drawingContext)
        {
            if (_dividerThickness <= 0) return;

            Pen stroke = new Pen(_dividerBrush, _dividerThickness);

            for (int i = 1; i < _axisX.Ticks.Count; i++)
            {
                drawingContext.DrawLine(
                    stroke,
                    _csTransform.Transform(new Point(_axisX.Ticks[i].Value, _yLowerRange)),
                    _csTransform.Transform(new Point(_axisX.Ticks[i].Value, _yUpperRange))
                    );
            }

            for (int i = 1; i < _axisY.Ticks.Count; i++)
            {
                drawingContext.DrawLine(
                    stroke,
                    _csTransform.Transform(new Point(_xLowerRange, _axisY.Ticks[i].Value)),
                    _csTransform.Transform(new Point(_xUpperRange, _axisY.Ticks[i].Value))
                    );
            }

        }

        private void DrawReferenceLine(DrawingContext drawingContext)
        {
            if (_referenceLineThickness <= 0) return;

            Pen stroke = new Pen(_referenceLineBrush, _referenceLineThickness);

            drawingContext.DrawLine(
                stroke,
                _csTransform.Transform(new Point(_referenceLineX, _yLowerRange)),
                _csTransform.Transform(new Point(_referenceLineX, _yUpperRange))
                );

            drawingContext.DrawLine(
                stroke,
                _csTransform.Transform(new Point(_xLowerRange, _referenceLineY)),
                _csTransform.Transform(new Point(_xUpperRange, _referenceLineY))
                );
        }

        private void DrawGraphBoarder(DrawingContext drawingContext)
        {
            drawingContext.DrawLine(
                new Pen(_graphBorderBrush, _graphBorderThickness.Top),
                new Point(0, 0),
                new Point(_graph.ActualWidth, 0)
                );

            drawingContext.DrawLine(
                new Pen(_graphBorderBrush, _graphBorderThickness.Right),
                new Point(_graph.ActualWidth, 0),
                new Point(_graph.ActualWidth, _graph.ActualHeight)
                );

            drawingContext.DrawLine(
                new Pen(_graphBorderBrush, _graphBorderThickness.Bottom),
                new Point(_graph.ActualWidth, _graph.ActualHeight),
                new Point(0, _graph.ActualHeight)
                );

            drawingContext.DrawLine(
                new Pen(_graphBorderBrush, _graphBorderThickness.Left),
                new Point(0, _graph.ActualHeight),
                new Point(0, 0)
                );

        }

        internal void GraphRender(DrawingContext drawingContext)
        {
            if (!CanPlot) return;

            DrawGraphAuxGrid(drawingContext);
            DrawReferenceLine(drawingContext);
            DrawGraphBoarder(drawingContext);

            OnGraphRender(drawingContext);
        }

        protected virtual void OnGraphRender(DrawingContext drawingContext)
        {

        }


        #region Zooming

        private double ZoomingFactor = 0.05;

        private void Zooming(Point position, double delta)
        {
            Point p = CSTransform.Inverse.Transform(position);

            if (delta < 0)
            {
                _xLowerRange = (_xLowerRange - p.X) * (1 + ZoomingFactor) + p.X;
                _xUpperRange = (_xUpperRange - p.X) * (1 + ZoomingFactor) + p.X;
                _yLowerRange = (_yLowerRange - p.Y) * (1 + ZoomingFactor) + p.Y;
                _yUpperRange = (_yUpperRange - p.Y) * (1 + ZoomingFactor) + p.Y;
            }
            else if (delta > 0)
            {
                _xLowerRange = (_xLowerRange - p.X) * (1 - ZoomingFactor) + p.X;
                _xUpperRange = (_xUpperRange - p.X) * (1 - ZoomingFactor) + p.X;
                _yLowerRange = (_yLowerRange - p.Y) * (1 - ZoomingFactor) + p.Y;
                _yUpperRange = (_yUpperRange - p.Y) * (1 - ZoomingFactor) + p.Y;
            }

            CheckRange();
            SetCS();
            Plot();
        }

        internal void GraphMouseWheel(MouseWheelEventArgs e)
        {
            Zooming(e.GetPosition(_graph), e.Delta);
        }

        #endregion

        #region Dragging

        private Point _mousePosition;

        private bool _inDragging = false;

        internal void GraphMouseMove(MouseEventArgs e)
        {
            Point currentPosition = e.GetPosition(_graph);

            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                Point T0 = CSTransform.Inverse.Transform(_mousePosition);
                Point T1 = CSTransform.Inverse.Transform(currentPosition);

                _xLowerRange = _xLowerRange - (T1.X - T0.X);
                _xUpperRange = _xUpperRange - (T1.X - T0.X);
                _yLowerRange = _yLowerRange - (T1.Y - T0.Y);
                _yUpperRange = _yUpperRange - (T1.Y - T0.Y);

                _mousePosition = currentPosition;

                CheckRange();
                SetCS();
                Plot();
            }

            Point T = CSTransform.Inverse.Transform(currentPosition);

            string s = "X: " + T.X.ToString("F3") + ", " + "Y: " + T.Y.ToString("F3");

            MousePositionText = s;

        }

        internal void GraphMouseDown(MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Pressed)
            {
                if (!_inDragging)
                {
                    _mousePosition = e.GetPosition(_graph);
                    _inDragging = true;
                    this.Cursor = Cursors.Hand;
                }
            }
        }

        internal void GraphMouseUp(MouseButtonEventArgs e)
        {
            if (e.MiddleButton == MouseButtonState.Released && _inDragging)
            {
                _inDragging = false;
                this.Cursor = Cursors.Arrow;
            }
        }



        #endregion


        protected virtual void OnAutoFit()
        {

        }

        #region Commands

        public Commands.RelayCommand Command_AutoFit { get; private set; }

        public Commands.RelayCommand Command_Restore { get; private set; }

        public Commands.RelayCommand Command_ZoomIn { get; private set; }

        public Commands.RelayCommand Command_ZoomOut { get; private set; }

        public Commands.RelayCommand Command_DebugTest { get; private set; }

        private bool Command_AutoFit_CanExecute(object parameter)
        {
            return true;
        }

        private void Command_AutoFit_Executed(object parameter)
        {
            OnAutoFit();
        }

        private bool Command_Restore_CanExecute(object parameter)
        {
            return true;
        }

        private void Command_Restore_Executed(object parameter)
        {
            ApplyDisireRange();
            SetCS();
            Plot();
        }

        private bool Command_ZoomIn_CanExecute(object parameter)
        {
            return true;
        }

        private void Command_ZoomIn_Executed(object parameter)
        {
            Zooming(Mouse.GetPosition(_graph), 120);
        }

        private bool Command_ZoomOut_CanExecute(object parameter)
        {
            return true;
        }

        private void Command_ZoomOut_Executed(object parameter)
        {
            Zooming(Mouse.GetPosition(_graph), -120);
        }

        private bool Command_DebugTest_CanExecute(object parameter)
        {
            return true;
        }

        private void Command_DebugTest_Executed(object parameter)
        {

        }


        private void InitializeCommands()
        {
            Command_AutoFit = new Commands.RelayCommand((p) => Command_AutoFit_Executed(p), (p) => Command_AutoFit_CanExecute(p));
            Command_Restore = new Commands.RelayCommand((p) => Command_Restore_Executed(p), (p) => Command_Restore_CanExecute(p));
            Command_ZoomIn = new Commands.RelayCommand((p) => Command_ZoomIn_Executed(p), (p) => Command_ZoomIn_CanExecute(p));
            Command_ZoomOut = new Commands.RelayCommand((p) => Command_ZoomOut_Executed(p), (p) => Command_ZoomOut_CanExecute(p));
            Command_DebugTest = new Commands.RelayCommand((p) => Command_DebugTest_Executed(p), (p) => Command_DebugTest_CanExecute(p));
        }

        #endregion

        public void Test()
        {
            double a = _mainGrid.RowDefinitions[0].ActualHeight;

            Point p = _graph.TranslatePoint(new Point(0, 0), _mainGrid);

            SetCS();
            Plot();

            this.ContextMenu = new ContextMenu();

            this.ContextMenu.Items.Add("A");
            this.ContextMenu.Items.Add("B");
            this.ContextMenu.Items.Add("C");
        }
    }
}
