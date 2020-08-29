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
    public struct Vector2
    {
        public double X { set; get; }
        public double Y { set; get; }

        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Vector2 Add(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.X + vector2.X, vector1.Y + vector2.Y);
        }

        public void Add(Vector2 vector)
        {
            X = X + vector.X;
            Y = Y + vector.Y;
        }

        public static Vector2 Subtract(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.X - vector2.X, vector1.Y - vector2.Y);
        }

        public void Subtract(Vector2 vector)
        {
            X = X - vector.X;
            Y = Y - vector.Y;
        }

        public static Vector2 Multiply(Vector2 vector, double factor)
        {
            return new Vector2(vector.X * factor, vector.Y * factor);
        }

        public void Multiply(double factor)
        {
            X = X * factor;
            Y = Y * factor;
        }

        public static Vector2 Scale(Vector2 vector, double factor)
        {
            return new Vector2(vector.X * factor, vector.Y * factor);
        }

        public void Scale(double factor)
        {
            X = X * factor;
            Y = Y * factor;
        }

        public static Vector2 Rotate(Vector2 vector, double theta)
        {
            double cost = System.Math.Cos(theta);
            double sint = System.Math.Sin(theta);
            Vector2 result = new Vector2(vector.X * cost - vector.Y * sint, vector.X * sint + vector.Y * cost);
            return result;
        }

        public void Rotate(double theta)
        {
            double sx = X;
            double sy = Y;
            double cost = System.Math.Cos(theta);
            double sint = System.Math.Sin(theta);
            X = sx * cost - sy * sint;
            Y = sx * sint + sy * cost;
        }



        public static Vector2 operator +(Vector2 vector)
        {
            return new Vector2(vector.X, vector.Y);
        }

        public static Vector2 operator +(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.X + vector2.X, vector1.Y + vector2.Y);
        }

        public static Vector2 operator -(Vector2 vector)
        {
            return new Vector2(vector.X * -1.0, vector.Y * -1.0);
        }

        public static Vector2 operator -(Vector2 vector1, Vector2 vector2)
        {
            return new Vector2(vector1.X - vector2.X, vector1.Y - vector2.Y);
        }

        public static Vector2 operator *(Vector2 vector, double factor)
        {
            return new Vector2(vector.X * factor, vector.Y * factor);
        }

        public static Vector2 operator *(double factor, Vector2 vector)
        {
            return new Vector2(vector.X * factor, vector.Y * factor);
        }

        //Operator [^] As Rotate
        public static Vector2 operator ^(Vector2 vector, double theta)
        {
            double cost = System.Math.Cos(theta);
            double sint = System.Math.Sin(theta);
            Vector2 result = new Vector2(vector.X * cost - vector.Y * sint, vector.X * sint + vector.Y * cost);
            return result;
        }

    }
}
