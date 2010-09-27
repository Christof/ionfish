using Machine.Specifications;

namespace Math.Collision
{
    public class Spec_BoundingSphere
    {
        [Subject(typeof(BoundingSphere))]
        public class CollidesWith
        {
            It shoudl_return_true_if_spheres_collide = () =>
                new BoundingSphere(Vector3.Zero, 2).CollidesWith(new BoundingSphere(new Vector3(0, 3, 0), 3)).
                    ShouldBeTrue();

            It should_return_false_if_spheres_do_not_collide = () =>
                new BoundingSphere(Vector3.Zero, 2).CollidesWith(new BoundingSphere(new Vector3(0, 10, 0), 3)).
                    ShouldBeFalse();
        }
    }
}