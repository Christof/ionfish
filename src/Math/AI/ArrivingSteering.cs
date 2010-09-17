namespace Math.AI
{
    public class ArrivingSteering : SteeringBase
    {
        private readonly float mSatisfactionRadius;
        private readonly float mSlowRadius;
        private const float TIME_TO_TARGET = 0.1f;

        public ArrivingSteering(Kinematic character, IHasPosition target, float maxAcceleration, float slowRadius, float satisfactionRadius)
            : base(character, target, maxAcceleration)
        {
            mSatisfactionRadius = satisfactionRadius;
            mSlowRadius = slowRadius;
        }

        public override Vector3 GetLinearAcceleration()
        {
            var direction = Target.Position - Character.Position;
            var distance = direction.Length;

            if (distance < mSatisfactionRadius)
            {
                return -Character.Velocity;
            }

            var maxSpeed = Character.MaxSpeed;
            var targetSpeed = distance > mSlowRadius ? maxSpeed : maxSpeed * distance / mSlowRadius;

            var targetVelocity = direction.Normalized() * targetSpeed;

            var acceleration = targetVelocity - Character.Velocity;

            acceleration = (1 / TIME_TO_TARGET) * acceleration;

            if (acceleration.Length > MaxAcceleration)
            {
                return acceleration.Normalized() * MaxAcceleration;
            }

            return acceleration;
        }
    }
}