using System;

namespace Math
{
    public class Quaternion
    {
        public Quaternion(float r, float i, float j, float k)
        {
            R = r;
            I = i;
            J = j;
            K = k;
        }

        public static Quaternion FromAxisAngle(Vector3 axis, float angle)
        {
            var halfAngle = angle * 0.5f;
            var sinOfHalfAngel = Functions.Sin(halfAngle);
            var r = Functions.Cos(halfAngle);
            var i = axis.X * sinOfHalfAngel;
            var j = axis.Y * sinOfHalfAngel;
            var k = axis.Z * sinOfHalfAngel;

            return new Quaternion(r, i, j, k).Normalized();
        }

        /// <summary>
        /// Real component.
        /// </summary>
        public float R { get; set; }

        /// <summary>
        /// First complex component.
        /// </summary>
        public float I { get; set; }

        /// <summary>
        /// Second complex component.
        /// </summary>
        public float J { get; set; }
        
        /// <summary>
        /// Third complex component
        /// </summary>
        public float K { get; set; }

        public Quaternion Normalized()
        {
            var invertedLength = 1 / Functions.Sqrt(R * R + I * I + J * J + K * K);
            return new Quaternion(R * invertedLength, I * invertedLength, J * invertedLength, K * invertedLength);
        }

        public static Quaternion operator *(Quaternion left, Quaternion right)
        {
            return new Quaternion(
                left.R * right.R - left.I * right.I - left.J * right.J - left.K * right.K,
                left.R * right.I + left.I * right.R - left.J * right.K - left.K * right.J,
                left.R * right.J - left.I * right.K + left.J * right.R - left.K * right.I,
                left.R * right.K + left.I * right.J - left.J * right.I + left.K * right.R);    
        }

        public Matrix ToMatrix()
        {
            return new Matrix(
                1 - (2 * J * J + 2 * K * K), 2 * I * J + 2 * K * R, 2 * I * K - 2 * J * R, 0,
                2 * I * J - 2 * K * R, 1 - (2 * I * I + 2 * K * K), 2 * J * K + 2 * I * R, 0,
                2 * I * K + 2 * J * R, 2 * J * K - 2 * I * R, 1 - (2 * I * I + 2 * J * J), 0,
                0, 0, 0, 1);
        }
    }
}