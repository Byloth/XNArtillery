using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ByloEngine
{
    public class Maths
    {
        public const int FloatPrecision = 100000;
        public const float PI = (float)Math.PI;

        public const int MinDegrees = 0;
        public const int MaxDegrees = 360;
        public const int MinRadians = 0;
        public const float MaxRadians = 2 * PI;
        
        public static float Absolute(float value)
        {
            return (float)Math.Abs(value);
        }

        public static float ArcTan(float coordX, float coordY)
        {
            return (float)Math.Atan2(coordY, coordX);
        }

        public static float Cosine(float angle)
        {
            return (float)Math.Cos(angle);
        }

        public static float Hypotenuse(float cathetus1, float cathetus2)
        {
            return (float)Math.Sqrt(((cathetus1 * cathetus1) + (cathetus2 * cathetus2)));
        }

        public static float Proportion(float absoluteMaximum, float relativeMaximum, float absolutePartial)
        {
            return (absolutePartial * relativeMaximum) / absoluteMaximum;
        }

        public static float Sine(float angle)
        {
            return (float)Math.Sin(angle);
        }
    }
}
