using System;

namespace Math
{
    public class Vector3RandomGenerator
    {
        private readonly Vector3 mMin;
        private readonly Vector3 mMax;
        private readonly Random mRandom;

        /// <summary>
        /// Generates random <see cref="Vector3"/> objects.
        /// </summary>
        /// <param name="min">The minimum vector values.</param>
        /// <param name="max">The maximum vector values.</param>
        /// <param name="seed">The starting seed of the generator.</param>
        public Vector3RandomGenerator(Vector3 min, Vector3 max, int seed)
        {
            mMin = min;
            mMax = max;
            mRandom = new Random(seed);
        }

        /// <summary>
        /// Generates random <see cref="Vector3"/> objects with a default min vector of (-1, -1, -1) and a default max vector of (1, 1, 1).
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Vector3RandomGenerator(int seed)
            : this(new Vector3(-1, -1, -1), new Vector3(1, 1, 1), seed)
        {
        }

        /// <summary>
        /// Generates a new random vector3 with float precision.
        /// </summary>
        public Vector3 Next()
        {
            return new Vector3(GetRandomFloat(mMin.X, mMax.X),
                GetRandomFloat(mMin.Y, mMax.Y),
                GetRandomFloat(mMin.Z, mMax.Z));
        }

        /// <summary>
        /// Caluclates a random float value within the min and max value. 
        /// </summary>
        /// <param name="min">Minimum float.</param>
        /// <param name="max">Maximum float.</param>
        /// <returns>Returns the Random value.</returns>
        private float GetRandomFloat(float min, float max)
        {
            if (min > max)
            {
                throw new ArgumentOutOfRangeException("min", "Min value is higher than max value");
            }

            return (((float)mRandom.NextDouble() * (max - min)) + min);
        }
    }
}