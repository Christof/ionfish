using System;

namespace Math
{
    public class Functions
    {
        public static float CoTan(float angle)
        {
            return (float)(System.Math.Cos(angle) / System.Math.Sin(angle));
        }
    }
}