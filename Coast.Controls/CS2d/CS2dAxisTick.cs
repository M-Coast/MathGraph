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

namespace Coast.Controls
{
    public struct CS2dAxisTick
    {
        public double Value { set; get; }
        public string Text { set; get; }
        
        public CS2dAxisTick(double value)
        {
            Value = value;
            Text = string.Empty;
        }
        public CS2dAxisTick(double value, string text)
        {
            Value = value;
            Text = text;
        }
    }
}
