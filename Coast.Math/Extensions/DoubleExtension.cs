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

namespace Coast.Math
{
    public static class DoubleExtension
    {
        public static double WeakEqualityLimit { get; set; } = 1E-20;    //1E-9;

        public static bool WeakEquals(this double self, double value)
        {
            return System.Math.Abs(self - value) <= WeakEqualityLimit ? true : false;
        }

        public static bool WeakEquals(this double self, double value,double delta)
        {
            return System.Math.Abs(self - value) <= delta ? true : false;
        }
    }
}
