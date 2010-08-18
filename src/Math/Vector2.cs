using System;
using System.Globalization;

namespace Math
{
    public struct Vector2 : IEquatable<Vector2>
    {
        readonly float mX;
        readonly float mY;

        public Vector2(float x, float y)
        {
            mX = x;
            mY = y;
        }

        public float X
        {
            get { return mX; }
        }

        public float Y
        {
            get { return mY; }
        }

        public float Length
        {
            get { return (float)System.Math.Sqrt(mX * mX + mY * mY); }
        }

        public static Vector2 XAxis
        {
            get { return new Vector2(1, 0); }
        }

            public static Vector2 YAxis
        {
            get { return new Vector2(0, 1); }
        }

        public static Vector2 Zero
        {
            get { return new Vector2(0, 0); }
        }

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        public static Vector2 operator *(Vector2 vector, float scalar)
        {
            return new Vector2(vector.X * scalar, vector.Y * scalar);
        }

        public static Vector2 operator *(float scalar, Vector2 vector)
        {
            return vector * scalar;
        }

        public static Vector2 operator -(Vector2 vector)
        {
            return new Vector2(-vector.X, -vector.Y);
        }

        public float Dot(Vector2 other)
        {
            return mX*other.X + mY*other.Y;
        }

        public Vector2 Normalized()
        {
            float inverseLength = 1 / Length;
            return this * inverseLength;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 && Equals((Vector2)obj);
        }

        public bool Equals(Vector2 other)
        {
            return mX.EqualsWithDelta(other.X) &&
                   mY.EqualsWithDelta(other.Y);
        }

        public override int GetHashCode()
        {
            return mX.GetHashCode() ^ mY.GetHashCode();
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "X: {0} Y: {1}", mX, mY);
        }
    }
}