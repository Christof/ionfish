using System;

namespace Math
{
    public struct Vector4
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

        public static Vector4 operator +(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);
        }

        public static Vector4 operator -(Vector4 left, Vector4 right)
        {
            return new Vector4(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);
        }

        public float Dot(Vector4 other)
        {
            return mX*other.X + mY*other.Y + mZ*other.Z + mW*other.W;
        }
    }
}