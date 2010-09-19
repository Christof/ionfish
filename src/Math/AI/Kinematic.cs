namespace Math.AI
{
    public class Kinematic : IHasPosition
    {
        public Vector3 Velocity { get; private set; }
        public Vector3 Position { get; private set; }
        public float MaxSpeed { get; private set; }

        public Kinematic(Vector3 initialPosition, float maxSpeed, Vector3 initialVelocity)
        {
            Velocity = initialVelocity;
            Position = initialPosition;
            MaxSpeed = maxSpeed;
        }

        public Kinematic(Vector3 initialPosition, float maxSpeed)
            : this(initialPosition, maxSpeed, Vector3.Zero)
        {
        }

        public void Update(ISteering steering, float frametime)
        {
            Position += Velocity * frametime;

            Velocity += steering.GetLinearAcceleration() * frametime;

            if (Velocity.Length > MaxSpeed)
            {
                Velocity = Velocity.Normalized() * MaxSpeed;
            }
        }
    }
}