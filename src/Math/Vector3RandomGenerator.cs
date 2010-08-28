using System;

namespace Math
{
    public class Vector3RandomGenerator
    {
        readonly Vector3 mMin = new Vector3(-1, -1, -1);
        readonly Vector3 mMax = new Vector3(1, 1, 1);
        readonly Random mRandom;

        /// <summary>
        /// Generates random Vetcor 3 objects.
        /// </summary>
        /// <param name="min">The minimum vector values. The values will be converted into an integer.</param>
        /// <param name="max">The maximum vector values. The values will be converted into an integer</param>
        /// <param name="seed">The starting point of the generator.</param>
        public Vector3RandomGenerator(Vector3 min , Vector3 max, int seed)
        {
            mMin = min;
            mMax = max;
            mRandom = new Random(seed);
        }

        /// <summary>
        /// Generates a new random vector3 with float precision.
        /// </summary>
        public Vector3 GetNextRandomFloat()
        {
            return new Vector3
            (
                GetRandomFloat(mMin.X, mMax.X),
                GetRandomFloat(mMin.Y, mMax.Y),
                GetRandomFloat(mMin.Z, mMax.Z)
            );
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
                throw new ArgumentOutOfRangeException("Min value is higher than max value");
            }

            return (((float)mRandom.NextDouble() * (max - min)) + min);
        }

        /// <summary>
        /// Generates a new random vector3 with int precision.
        /// </summary>
        /// <returns>The random vector 3.</returns>
        public Vector3 GetNextRandom()
        {
            return new Vector3
            (
                mRandom.Next(Convert.ToInt32(mMin.X), Convert.ToInt32(mMax.X)),
                mRandom.Next(Convert.ToInt32(mMin.Y), Convert.ToInt32(mMax.Y)),
                mRandom.Next(Convert.ToInt32(mMin.Z), Convert.ToInt32(mMax.Z))
            );
        }
    }
}