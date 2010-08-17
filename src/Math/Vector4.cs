using System;

namespace Math
{
    public struct Vector4 : IEquatable<Vector4>
    {
        readonly float mX;
        readonly float mY;
        readonly float mZ;
        readonly float mW;

        public Vector4(float x, float y, float z, float w)
        {
            mX = x;
            mY = y;
            mZ = z;
            mW = w;
        }


        public float X
        {
            get { return mX; }
        }

        public float Y
        {
            get { return mY; }
        }

        public float Z
        {
            get { return mZ; }
        }

        public float W
        {
            get { return mW; }
        }

        public float Length
        {
            get { return (float)System.Math.Sqrt(mX * mX + mY * mY + mZ * mZ + mW * mW); }
        }

        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }

        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }

        public static Vector4 operator *(Vector4 vector, float scalar)
        {
            return new Vector4(vector.X * scalar, vector.Y * scalar, vector.Z * scalar, vector.W * scalar);
        }

        public static Vector4 operator *(float scalar, Vector4 vector)
        {
            return vector * scalar;
        }

        public float Dot(Vector4 other)
        {
            return mX*other.X + mY*other.Y + mZ*other.Z + mW*other.W;
        }

        public Vector4 Normalized()
        {
            float inverseLength = 1 / Length;
            return this * inverseLength;
        }

        public override bool Equals(object obj)
        {
            return obj is Vector4 && Equals((Vector4)obj);
        }

        public bool Equals(Vector4 other)
        {
            return mX.EqualsWithDelta(other.X) &&
                   mY.EqualsWithDelta(other.Y) &&
                   mZ.EqualsWithDelta(other.Z) &&
                   mW.EqualsWithDelta(other.W);
        }

        public override int GetHashCode()
        {
            return mX.GetHashCode() ^ mY.GetHashCode() ^ mZ.GetHashCode() ^ mW.GetHashCode();
        }

        public static bool operator ==(Vector4 left, Vector4 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector4 left, Vector4 right)
        {
            return !left.Equals(right);
        }
    }
}