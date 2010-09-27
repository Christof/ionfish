namespace Math.Collision
{
    public class BoundingSphere
    {
        public float Radius { get; private set; }
        public Vector3 Center { get; private set; }

        public BoundingSphere(Vector3 center, float radius)
        {
            Radius = radius;
            Center = center;
        }

        public bool CollidesWith(BoundingSphere other)
        {
            float distance = Radius + other.Radius;
            return distance * distance >= (Center - other.Center).LengthSquared;
        }
    }
}