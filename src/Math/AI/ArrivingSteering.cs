namespace Math.AI
{
    public class ArrivingSteering : SteeringBase
    {
        private readonly float mSatisfactionRadius;
        private readonly float mSlowRadius = 18;
        private const float TIME_TO_TARGET = 0.1f;
        private float mMaxSpeed = 10;

        public ArrivingSteering(Kinematic character, Kinematic target, float maxAcceleration, float satisfactionRadius)
            : base(character, target, maxAcceleration)
        {
            mSatisfactionRadius = satisfactionRadius;
        }

        public override Vector3 GetLinearAcceleration()
        {
            var direction = Target.Position - Character.Position;
            var distance = direction.Length;

            if (distance < mSatisfactionRadius)
            {
                return -Character.Velocity;
            }

            var targetSpeed = distance > mSlowRadius ? mMaxSpeed : mMaxSpeed * distance / mSlowRadius;

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