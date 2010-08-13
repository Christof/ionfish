namespace Math
{
    public static class MathExtensions
    {
        public static bool EqualsWithDelta(this float value, float otherValue, float delta)
        {
            return System.Math.Abs(value - otherValue) <= delta;
        }

        public static bool EqualsWithDelta(this float value, float otherValue)
        {
            const float DEFAULT_DELTA = 0.001f;

            return value.EqualsWithDelta(otherValue, DEFAULT_DELTA);
        }
    }
}