namespace Math.AI
{
    public class Kinetic
    {
        private Vector3 mVelocity;
        public Vector3 Position { get; private set; }
        public float MaxSpeed { get; private set; }

        public Kinetic(Vector3 initialPosition, Vector3 initialVelocity)
        {
            mVelocity = initialVelocity;
            Position = initialPosition;
        }

        public Kinetic(Vector3 initialPosition)
            : this(initialPosition, Vector3.Zero)
        {
        }

        public void Update(ISteering steering, float maxSpeed, float frametime)
        {
            MaxSpeed = maxSpeed;
            Position += mVelocity * frametime;

            mVelocity += steering.GetLinearAcceleration() * frametime;

            if (mVelocity.Length > maxSpeed)
            {
                mVelocity = mVelocity.Normalized() * maxSpeed;
            }
        }
    }
}