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
    //Solve Flatness

    public class Flatness
    {
        public static double Solve(List<Vector3> points)
        {
            PlaneFitter pf = new PlaneFitter();
            pf.Points = points;
            pf.Solve();

            if (pf.Errored) return double.NaN;

            double max = double.MinValue;
            double min = double.MaxValue;

            int i;
            for (i = 0; i < points.Count; i++)
            {
                double temp = Distance.Point2PlaneSigned(points[i], pf.A, pf.B, pf.C, pf.D);
                if (double.IsNaN(temp)) return double.NaN;
                if (max < temp) max = temp;
                if (min > temp) min = temp;
            }

            double result = max - min;

            return result;
        }

        public static double Solve(List<Vector3> points, List<Vector3> referecePlanePoints)
        {
            PlaneFitter pf = new PlaneFitter();
            pf.Points = referecePlanePoints;
            pf.Solve();

            if (pf.Errored) return double.NaN;

            double max = double.MinValue;
            double min = double.MaxValue;

            int i;
            for (i = 0; i < points.Count; i++)
            {
                double temp = Distance.Point2PlaneSigned(points[i], pf.A, pf.B, pf.C, pf.D);
                if (double.IsNaN(temp)) return double.NaN;
                if (max < temp) max = temp;
                if (min > temp) min = temp;
            }

            double result = max - min;

            return result;

        }

        public static double Solve(List<Vector3> points, double refPlaneA, double refPlaneB, double refPlaneC, double refPlaneD)
        {
            double max = double.MinValue;
            double min = double.MaxValue;
            int i;
            for (i = 0; i < points.Count; i++)
            {
                double temp = Distance.Point2PlaneSigned(points[i], refPlaneA, refPlaneB, refPlaneC, refPlaneD);
                if (double.IsNaN(temp)) return double.NaN;
                if (max < temp) max = temp;
                if (min > temp) min = temp;
            }

            double result = max - min;

            return result;

        }

        public static double Solve(List<Vector3> points, int dropCount)
        {
            PlaneFitter pf = new PlaneFitter();
            List<Vector3> tmpPoints = new List<Vector3>();
            double maxDist = double.MinValue;
            int maxDistIndex;
            int i, j, k;

            if (points.Count - dropCount < 3) return double.NaN;

            tmpPoints = new List<Vector3>();

            for (i = 0; i < points.Count; i++)
            {
                Vector3 p = new Vector3();
                p.X = points[i].X;
                p.Y = points[i].Y;
                p.Z = points[i].Z;
                tmpPoints.Add(p);
            }

            for (k = 0; k < dropCount; k++)
            {
                pf.Points = tmpPoints;
                pf.Solve();

                if (pf.Errored) return double.NaN;

                //find max distance index
                maxDist = double.MinValue;
                maxDistIndex = -1;
                for (i = 0; i < tmpPoints.Count; i++)
                {
                    double temp = Distance.Point2Plane(tmpPoints[i], pf.A, pf.B, pf.C, pf.D);
                    if (double.IsNaN(temp)) return double.NaN;
                    if (maxDist < temp)
                    {
                        maxDist = temp;
                        maxDistIndex = i;
                    }
                }
                //remove max distance point 
                tmpPoints.RemoveAt(maxDistIndex);
            }

            return Solve(tmpPoints);
        }

        public static double Solve(List<Vector3> points, int dropCount, List<Vector3> referecePlanePoints)
        {
            if (dropCount >= points.Count) return double.NaN;

            PlaneFitter pf = new PlaneFitter();
            pf.Points = referecePlanePoints;
            pf.Solve();

            if (pf.Errored) return double.NaN;

            List<double> dists = new List<double>();

            int i;
            for (i = 0; i < points.Count; i++)
            {
                double temp = Distance.Point2PlaneSigned(points[i], pf.A, pf.B, pf.C, pf.D);
                if (double.IsNaN(temp)) return double.NaN;
                dists.Add(temp);
            }
            dists.Sort();

            for (i = 0; i < dropCount; i++)
            {
                dists.RemoveAt(dists.Count - 1);
            }

            double result = System.Math.Abs(dists[dists.Count - 1] - dists[0]);

            return result;

        }

    }
}
