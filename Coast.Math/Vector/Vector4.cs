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
    [Serializable]
    public struct Vector4
    {
        public double X { set; get; }
        public double Y { set; get; }
        public double Z { set; get; }
        public double W { set; get; }
        public Vector4(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
            W = 1;
        }
        public Vector4(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
    }
}
