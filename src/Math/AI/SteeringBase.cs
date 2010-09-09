namespace Math.AI
{
    public abstract class SteeringBase : ISteering
    {
        protected Kinematic Character { get; set; }
        public float MaxAcceleration { get; private set; }
        protected Kinematic Target { get; set; }

        protected SteeringBase(Kinematic character, Kinematic target, float maxAcceleration)
        {
            Character = character;
            Target = target;
            MaxAcceleration = maxAcceleration;
        }

        public abstract Vector3 GetLinearAcceleration();
    }
}