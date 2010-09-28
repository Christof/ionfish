using System;

namespace Math
{
    public class Quaternion
    {
        public Quaternion(Vector3 axis, float angle)
        {
            var halfAngle = angle * 0.5f;
            var sinOfHalfAngel = Functions.Sin(halfAngle);
            R = Functions.Cos(halfAngle);
            I = axis.X * sinOfHalfAngel;
            J = axis.Y * sinOfHalfAngel;
            K = axis.Z * sinOfHalfAngel;
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
    }
}