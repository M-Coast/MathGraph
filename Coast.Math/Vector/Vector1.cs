﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coast.Math
{
    [Serializable]
    public struct Vector1
    {
        public double V { set; get; }
        public Vector1(double v)
        {
            V = v;
        }
    }
}
