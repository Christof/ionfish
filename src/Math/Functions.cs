
using System;

namespace Math
{
    public class Functions
    {
        private static readonly Random sRandomGenerator = new Random(0);

        public static float Sin(float angle)
        {
            return (float) System.Math.Sin(angle);
        }

        public static float Cos(float angle)
        {
            return (float) System.Math.Cos(angle);
        }

        public static float CoTan(float angle)
        {
            return Cos(angle) / Sin(angle);
        }

        public static float GetRandom(float min, float max)
        {
            return ((float)sRandomGenerator.NextDouble()).From01ToCustomRange(min, max);
        }
    }
}