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

        /// <summary>
        /// Clamps the value to the range given by min and max.
        /// </summary>
        /// <param name="value">The value to be clamped.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <returns>The clamped value.</returns>
        public static float Clamp(this float value, float min, float max)
        {
            if (value > max)
            {
                return max;
            }

            if (value < min)
            {
                return min;
            }

            return value;
        }

        /// <summary>
        /// Clamps the value to the given maximum.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="max">The maximum.</param>
        /// <returns>The clamped value.</returns>
        public static float ClampMax(this float value, float max)
        {
            if (value > max)
            {
                return max;
            }

            return value;
        }

        /// <summary>
        /// Clamps the value to the given minimum.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="min">The minimum.</param>
        /// <returns>The clamped value.</returns>
        public static float ClampMin(this float value, float min)
        {
            if (value < min)
            {
                return min;
            }

            return value;
        }
    }
}