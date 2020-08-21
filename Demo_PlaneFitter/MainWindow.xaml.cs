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

using Coast.Math;

namespace Demo_PlaneFitter
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

        private void this_Loaded(object sender,RoutedEventArgs e)
        {
            comboTestData.SelectedIndex = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SolvePlane();
        }


        private void comboTestData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox c = (ComboBox)sender;
            if (c.SelectedIndex == 0)
            {
                CreatePoints1();
            }
            if (c.SelectedIndex == 1)
            {
                CreatePoints2();
            }
            if (c.SelectedIndex == 2)
            {
                CreatePoints3();
            }
            if (c.SelectedIndex == 3)
            {
                CreatePoints4();
            }
        }

        public List<Vector3> TestPoints
        {
            get { return (List<Vector3>)GetValue(TestPointsProperty); }
            set { SetValue(TestPointsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TestPoints.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TestPointsProperty =
            DependencyProperty.Register("TestPoints", typeof(List<Vector3>), typeof(MainWindow), new PropertyMetadata());



        public Plane Plane
        {
            get { return (Plane)GetValue(PlaneProperty); }
            set { SetValue(PlaneProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Plane.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlaneProperty =
            DependencyProperty.Register("Plane", typeof(Plane), typeof(MainWindow), new PropertyMetadata());




        public PlaneFitterErrorCode ErrorCode
        {
            get { return (PlaneFitterErrorCode)GetValue(ErrorCodeProperty); }
            set { SetValue(ErrorCodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ErrorCode.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ErrorCodeProperty =
            DependencyProperty.Register("ErrorCode", typeof(PlaneFitterErrorCode), typeof(MainWindow), new PropertyMetadata());



        private PlaneFitter _planeFitter = new PlaneFitter();

        private void SolvePlane()
        {
            _planeFitter.Points = TestPoints;
            _planeFitter.Solve();
            Plane = _planeFitter.Plane;
            ErrorCode = _planeFitter.ErrorCode;
        }

        private void CreatePoints1()
        {
            double a = 1;
            double b = 2;
            double c = 3;

            Plane plane = new Plane(a, b, c);

            double xSpace = 1.0;
            double ySpace = 1.0;

            int xPointCount = 2;
            int yPointCount = 2;

            List<Vector3> points = new List<Vector3>();

            for (int i = 0; i < xPointCount; i++)
            {
                for (int j = 0; j < yPointCount; j++)
                {
                    double x = xSpace * i;
                    double y = ySpace * j;
                    double z = plane.GetZ(x, y);
                    points.Add(new Vector3(x, y, z));
                }
            }

            TestPoints = points;
        }

        private void CreatePoints2()
        {
            double a = 1;
            double b = 2;
            double c = 3;

            Plane plane = new Plane(a, b, c);

            double xSpace = 1.0;
            double ySpace = 1.0;

            int xPointCount = 20;
            int yPointCount = 20;

            List<Vector3> points = new List<Vector3>();

            for (int i = 0; i < xPointCount; i++)
            {
                for (int j = 0; j < yPointCount; j++)
                {
                    double x = xSpace * i;
                    double y = ySpace * j;
                    double z = plane.GetZ(x, y);
                    points.Add(new Vector3(x, y, z));
                }
            }

            TestPoints = points;
        }

        private void CreatePoints3()
        {
            double a = 4;
            double b = 7;
            double c = 8;

            Plane plane = new Plane(a, b, c);

            double xSpace = 1.0;
            double ySpace = 1.0;

            int xPointCount = 10;
            int yPointCount = 20;

            List<Vector3> points = new List<Vector3>();

            for (int i = 0; i < xPointCount; i++)
            {
                for (int j = 0; j < yPointCount; j++)
                {
                    double x = xSpace * i;
                    double y = ySpace * j;
                    double z = plane.GetZ(x, y);
                    points.Add(new Vector3(x, y, z));
                }
            }

            TestPoints = points;
        }

        private void CreatePoints4()
        {
            double a = 4;
            double b = 7;
            double c = 8;

            Plane plane = new Plane(a, b, c);

            double xSpace = 1.0;
            double ySpace = 1.0;

            int xPointCount = 1;
            int yPointCount = 1;

            List<Vector3> points = new List<Vector3>();

            for (int i = 0; i < xPointCount; i++)
            {
                for (int j = 0; j < yPointCount; j++)
                {
                    double x = xSpace * i;
                    double y = ySpace * j;
                    double z = plane.GetZ(x, y);
                    points.Add(new Vector3(x, y, z));
                }
            }

            TestPoints = points;
        }


    }



}
