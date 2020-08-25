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
    public class CS2dChart : CoordinateSystem2d
    {
        public CS2dChart()
        {
            Content = new CS2dGraph(); 
        }

        public double XRangeLow { get; private set; }     //对应X轴
        public double XRangeHigh { get; private set; }    //对应X轴
        public double YRangeLow { get; private set; }     //对应Y轴
        public double YRangeHigh { get; private set; }    //对应Y轴

        public double RangeExtendRate { get; set; } = 0.1;




        public string Title
        {
            get { return _title; }
            set { _title = value; ResetError(); SetAxis(); SetContent(); PlotContent(); }
        }

        private string _title = null;





        public ObservableCollection<CS2dShape> Elements
        {
            get { return _elements; }
            set { _elements = value; ResetError(); SetAxis(); SetContent(); PlotContent(); }
        }

        private ObservableCollection<CS2dShape> _elements = null;
        

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            SetAxis();
            SetContent();
            PlotContent();
        }

        private void ResetError()
        {
            Errored = false;
        }

        private void SetContent()
        {
            CS2dGraph content = (Content as CS2dGraph);
            content.Title = this.Title;
            content.Elements = this.Elements;
            content.Transform = this.Transform;
        }
        
        private void SetAxis()
        {
            if (Elements == null) return;

            double xMin = double.MaxValue;
            double xMax = double.MinValue;
            double yMin = double.MaxValue;
            double yMax = double.MinValue;
            foreach (CS2dShape element in Elements)
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

            double xR = xMax - xMin;
            double yR = yMax - yMin;
            double R;

            if (xR > yR)
            {
                R = xR;
            }
            else
            {
                R = yR;
            }

            XRangeLow = xMin - R * RangeExtendRate;
            XRangeHigh = xMax + R + R * RangeExtendRate;
            YRangeLow = yMin - R * RangeExtendRate;
            YRangeHigh = yMax + R+ R * RangeExtendRate;

            //XRangeLow = xMin - xR * RangeExtendRate;
            //XRangeHigh = xMax + xR * RangeExtendRate;
            //YRangeLow = yMin - yR * RangeExtendRate;
            //YRangeHigh = yMax + yR * RangeExtendRate;

            //XRangeLow = xMin;
            //XRangeHigh = xMax;
            //YRangeLow = yMin;
            //YRangeHigh = yMax;

            XAxis.RangeLowerLimit = XRangeLow;
            XAxis.RangeUpperLimit = XRangeHigh;
            YAxis.RangeLowerLimit = YRangeLow;
            YAxis.RangeUpperLimit = YRangeHigh;

            List<AxisTick> xAxisTicks = new List<AxisTick>();
            List<AxisTick> yAxisTicks = new List<AxisTick>();

            double xSpace = (R) / DisireXAxisTickCount;
            double ySpace = (R) / DisireYAxisTickCount;

            for (int i = 0; i < DisireXAxisTickCount; i++)
            {
                double v = xMin + xSpace * (i + 1);
                string s = v.ToString("F2");
                xAxisTicks.Add(new AxisTick(v, s));
            }
            for (int i = 0; i < DisireYAxisTickCount; i++)
            {
                double v = yMin + ySpace * (i + 1);
                string s = (v).ToString("F2");
                yAxisTicks.Add(new AxisTick(v, s));
            }

            //Call base.base.SetAxis
            SetAxis(XRangeLow, XRangeHigh, YRangeLow, YRangeHigh, xAxisTicks, yAxisTicks);
            
        }

        private void PlotContent()
        {
            //Plot Content
            if (!Errored) ((CS2dGraph)Content).Plot();

        }
    }
}