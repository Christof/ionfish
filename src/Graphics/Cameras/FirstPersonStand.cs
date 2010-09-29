using Math;

namespace Graphics.Cameras
{
    public class FirstPersonStand : Stand
    {
        public void Yaw(float angle)
        {
            var q = Quaternion.FromAxisAngle(Up, angle);

            Direction = q.ToMatrix() * Direction;
        }

        public void Pitch(float angle)
        {
            var q = Quaternion.FromAxisAngle(Direction.Cross(Up), angle);
            var matrix = q.ToMatrix();

            Direction = matrix * Direction;
            Up = matrix * Up;
        }

        public void Roll(float angle)
        {
            var q = Quaternion.FromAxisAngle(Direction, angle);
            var matrix = q.ToMatrix();

            Up = matrix * Up;
        }
    }
}