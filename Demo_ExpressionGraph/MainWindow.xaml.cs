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

using Coast.Math;
using Coast.Math.Expression;
using Coast.Controls;

namespace Demo_ExpressionGraph
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(this_Loaded);
        }

        private void this_Loaded(object sender, RoutedEventArgs e)
        {
            //comboTestData.SelectedIndex = 0;

            

            ExpressionText = "y = 2*x+5";

            //ExpressionText += "y = 2*x+5";

            ExpressionText = "y = 0.5 * x + 5 + 1 * cos(x)" +"\r\n";

            Points = new ObservableCollection<Vector2>();

            XRangeLow = -10;
            XRangeHigh = +10;
        }




        public string ExpressionText
        {
            get { return (string)GetValue(ExpressionTextProperty); }
            set { SetValue(ExpressionTextProperty, value); }
        }
        
        public static readonly DependencyProperty ExpressionTextProperty =
            DependencyProperty.Register("ExpressionText", typeof(string), typeof(MainWindow), new PropertyMetadata());




        public bool Errored
        {
            get { return (bool)GetValue(ErroredProperty); }
            set { SetValue(ErroredProperty, value); }
        }
        
        public static readonly DependencyProperty ErroredProperty =
            DependencyProperty.Register("Errored", typeof(bool), typeof(MainWindow), new PropertyMetadata(false));



        public string StatusText
        {
            get { return (string)GetValue(StatusTextProperty); }
            set { SetValue(StatusTextProperty, value); }
        }

        public static readonly DependencyProperty StatusTextProperty =
            DependencyProperty.Register("StatusText", typeof(string), typeof(MainWindow), new PropertyMetadata("No Error"));




        public double XRangeLow
        {
            get { return (double)GetValue(XRangeLowProperty); }
            set { SetValue(XRangeLowProperty, value); }
        }
        
        public static readonly DependencyProperty XRangeLowProperty =
            DependencyProperty.Register("XRangeLow", typeof(double), typeof(MainWindow), new PropertyMetadata());



        public double XRangeHigh
        {
            get { return (double)GetValue(XRangeHighProperty); }
            set { SetValue(XRangeHighProperty, value); }
        }
        
        public static readonly DependencyProperty XRangeHighProperty =
            DependencyProperty.Register("XRangeHigh", typeof(double), typeof(MainWindow), new PropertyMetadata());




        public int SampleCount
        {
            get { return (int)GetValue(SampleCountProperty); }
            set { SetValue(SampleCountProperty, value); }
        }

        public static readonly DependencyProperty SampleCountProperty =
            DependencyProperty.Register("SampleCount", typeof(int), typeof(MainWindow), new PropertyMetadata(1000));






        public ObservableCollection<Vector2> Points
        {
            get { return (ObservableCollection<Vector2>)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }

        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(ObservableCollection<Vector2>), typeof(MainWindow), new PropertyMetadata());


        private ObservableCollection<CS2dShape> _cs2dElements = new ObservableCollection<CS2dShape>();


        private void TestData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            if (c.SelectedIndex == 0)
            {
                //CreatePoints1();
            }
            if (c.SelectedIndex == 1)
            {
                //CreatePoints2();
            }
            if (c.SelectedIndex == 2)
            {
                //CreatePoints3();
            }
            if (c.SelectedIndex == 3)
            {
                //CreatePoints4();
            }

            //SetPoints();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Solve();
        }


        public Syntax Parser { get; private set; } = new Syntax();

        private void Solve()
        {
            //Syntax syntax = new Syntax();

            StatusText = "No Error";
            textBlockStatus.Background = new SolidColorBrush(Colors.Transparent);
            Errored = false;

            Parser.Parse(ExpressionText);

            if (Parser.Errored)
            {
                StatusText = "Parser Errored";
                textBlockStatus.Background = new SolidColorBrush(Colors.Red);
                Errored = true;
                return;
            }

            Dictionary<string, double> variableTable = new Dictionary<string, double>();

            variableTable.Add("x", 0);
            variableTable.Add("y", 0);

            Evaluator ev = new Evaluator();

            double space = (XRangeHigh - XRangeLow) / SampleCount;

            double x, y;

            Points.Clear();

            try
            {

                for (int i = 0; i < SampleCount; i++)
                {
                    x = XRangeLow + space * i;
                    variableTable["x"] = x;
                    ev.Solve(Parser.Expressions, variableTable);
                    y = variableTable["y"];
                    Points.Add(new Vector2(x, y));
                }
            }
            catch (Exception e)
            {
                StatusText = "Evaluator Errored";
                textBlockStatus.Background = new SolidColorBrush(Colors.Red);
                Errored = true;
                return;
            }

            //SetPoints();

            SetGraphPath();

        }

        private void SetPoints()
        {
            if (Points == null) return;

            _cs2dElements.Clear();

            foreach (Vector2 point in Points)
            {
                _cs2dElements.Add(new CS2dPoint()
                {
                    X = point.X,
                    Y = point.Y,
                    Stroke = new SolidColorBrush(Colors.Brown),
                    FillBrush = new SolidColorBrush(Colors.Brown),
                    Size = 1,
                    Thickness = 0,
                });
            }
            _cs2d.Elements = _cs2dElements;
        }

        private void SetGraphPath()
        {
            if (Points == null) return;

            //if (_cs2d.Elements == null) _cs2d.Elements = new ObservableCollection<CS2dShape>();

            _cs2dElements.Clear();

            CS2dPath path = new CS2dPath()
            {
                Stroke = new SolidColorBrush(Colors.Brown),
                FillBrush = new SolidColorBrush(Colors.Brown),
                Thickness = 1,
                Segments = Points.ToList()
            };

            //_cs2d.Elements.Add(path);

            _cs2dElements.Add(path);
            _cs2d.Elements = _cs2dElements;

        }

        private void Solve_n()
        {
            Syntax syntax = new Syntax();
            syntax.Parse(ExpressionText);

            Dictionary<string, double> variableTable = new Dictionary<string, double>();

            variableTable.Add("x", 0);
            variableTable.Add("y", 0);

            Evaluator ev = new Evaluator();

            double space = (XRangeHigh - XRangeLow) / SampleCount;

            double x, y;

            Points.Clear();

            //for (int i = 0; i < SampleCount; i++)
            //{
            //    x = XRangeLow + space * i;
            //    variableTable["x"] = x;
            //    ev.Solve(syntax.Expressions, variableTable);
            //    y = variableTable["y"];
            //    Points.Add(new Vector2(x, y));
            //}

            for (int i = 0; i < SampleCount; i++)
            {
                x = XRangeLow + space * i;
                variableTable["x"] = x;

                foreach (Coast.Math.Expression.Expression e in syntax.Expressions.Expressions)
                {
                    ev.Solve(syntax.Expressions, variableTable);
                    y = variableTable["y"];
                    Points.Add(new Vector2(x, y));
                }
            }

            //SetPoints();

            SetGraphPath();
        }


    }
}
