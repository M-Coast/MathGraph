using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coast.Math
{
    public enum CircleFitterErrorCode
    {
        NoError,
        PointsCollectionIsNull,
        PointsCountLessThan3,
        SolveEquationsError,
        RadiusError,
    }
}
