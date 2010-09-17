namespace Math.AI
{
    public class SeekSteering : SteeringBase
    {
        public SeekSteering(Kinematic character, IHasPosition target, float maxAcceleration)
            : base(character, target, maxAcceleration)
        {
        }

        public override Vector3 GetLinearAcceleration()
        {
            var direction = Target.Position - Character.Position;
            return direction.Normalized() * MaxAcceleration;
        }
    }
}