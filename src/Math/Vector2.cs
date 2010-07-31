using System;

namespace Math
{
    public struct Vector2
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

        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        public float Dot(Vector2 other)
        {
            return mX*other.X + mY*other.Y;
        }
    }
}