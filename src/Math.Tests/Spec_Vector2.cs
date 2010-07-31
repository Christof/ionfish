using Machine.Specifications;

namespace Math
{
    public class Spec_Vector2
    {
        [Subject(typeof(Vector2))]
        public class initialization_with_the_two_components
        {
            static Vector2 vector;

            Because of = () => vector = new Vector2(1, 2);

            It should_have_an_X_value = () => vector.X.ShouldEqual(1);
            It should_have_an_Y_value = () => vector.Y.ShouldEqual(2);
        }

        public abstract class two_vectors_context
        {
            protected static Vector2 vector1;
            protected static Vector2 vector2;

            Establish context = () =>
            {
                vector1 = new Vector2(1, 2);
                vector2 = new Vector2(3, 4);
            };
        }

        [Subject(typeof(Vector2))]
        public class addition : two_vectors_context
        {
            static Vector2 resultVector;

            Because of = () => resultVector = vector1 + vector2;

            It should_have_the_X_value = () => resultVector.X.ShouldEqual(1 + 3);
            It should_have_the_Y_value = () => resultVector.Y.ShouldEqual(2 + 4);
        }

        [Subject(typeof(Vector2))]
        public class subtraction : two_vectors_context
        {
            static Vector2 resultVector;

            Because of = () => resultVector = vector1 - vector2;

            It should_have_the_X_value = () => resultVector.X.ShouldEqual(1 - 3);
            It should_have_the_Y_value = () => resultVector.Y.ShouldEqual(2 - 4);
        }

        [Subject(typeof(Vector2))]
        public class dot_product : two_vectors_context
        {
            static float result;

            Because of = () => result = vector1.Dot(vector2);

            It should_have_the_dot_product = () => result.ShouldEqual(1*3+2*4);
        }
    }
}