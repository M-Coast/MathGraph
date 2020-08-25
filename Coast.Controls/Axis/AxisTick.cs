using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coast.Controls
{
    public class AxisTick
    {
        public double Value { set; get; }
        public string Text { set; get; }
        public bool DisplayText { set; get; }


        public AxisTick()
        {
            DisplayText = true;
        }
        public AxisTick(double value, bool displayText = true)
        {
            Value = value;
            Text = string.Empty;
            DisplayText = displayText;
        }
        public AxisTick(double value, string text, bool displayText = true)
        {
            Value = value;
            Text = text;
            DisplayText = displayText;
        }
    }
}
