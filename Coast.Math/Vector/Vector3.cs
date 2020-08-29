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
    public struct Vector3
    {
        public double X { set; get; }
        public double Y { set; get; }
        public double Z { set; get; }

        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return "[" + X.ToString() + " " + Y.ToString() + " " + Z.ToString() + "]";
        }
    }
}
