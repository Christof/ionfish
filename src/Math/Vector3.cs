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
    }
}