namespace Math.AI
{
    public abstract class SteeringBase : ISteering
    {
        protected Kinetic Character { get; set; }
        public float MaxAcceleration { get; private set; }
        protected Kinetic Target { get; set; }

        protected SteeringBase(Kinetic character, Kinetic target, float maxAcceleration)
        {
            Character = character;
            Target = target;
            MaxAcceleration = maxAcceleration;
        }

        public abstract Vector3 GetLinearAcceleration();
    }
}