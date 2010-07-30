using Machine.Specifications;
namespace Math
{
    [Subject(typeof(Vector3))]
    public class Spec_Vector3
    {
        static Vector3 vector;

        Because the_vector_is_created = () => vector = new Vector3(1, 2, 3);

        It should_have_an_X_value = () => vector.X.ShouldEqual(1);
        It should_have_an_Y_value = () => vector.Y.ShouldEqual(2);
        It should_have_an_Z_value = () => vector.Z.ShouldEqual(3);
    }
}