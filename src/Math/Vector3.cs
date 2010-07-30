using System;

namespace Math
{
    public struct Vector3
    {
        private readonly float mX;
        private readonly float mY;
        private readonly float mZ;

        public Vector3(float x, float y, float z)
        {
            mX = x;
            mY = y;
            mZ = z;
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

        public static Vector3 operator +(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X + right.X, left.Y + right.Y, left.Z + right.Z);
        }

        public static Vector3 operator -(Vector3 left, Vector3 right)
        {
            return new Vector3(left.X - right.X, left.Y - right.Y, left.Z - right.Z);
        }

        public float Dot(Vector3 other)
        {
            return mX * other.X + mY * other.Y + mZ * other.Z;
        }

        public Vector3 Cross(Vector3 other)
        {
            return new Vector3(
                mY * other.Z - mZ * other.mY,
                mZ * other.X - mX * other.Z,
                mX * other.Y - mY * other.X);
        }
    }
}