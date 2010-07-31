using Machine.Specifications;

namespace Math
{
    public class Spec_Vector4
    {
        [Subject(typeof(Vector4))]
        public class initialization_with_the_four_components
        {
            static Vector4 vector;

            Because of = () => vector = new Vector4(1, 2, 3, 4);

            It should_have_the_X_value = () => vector.X.ShouldEqual(1);
            It should_have_the_Y_value = () => vector.Y.ShouldEqual(2);
            It should_have_the_Z_value = () => vector.Z.ShouldEqual(3);
            It should_have_the_W_value = () => vector.W.ShouldEqual(4);
        }

        public abstract class two_vectors_context
        {
            protected static Vector4 vector1;
            protected static Vector4 vector2;

            Establish context = () =>
            {
                vector1 = new Vector4(1, 2, 3, 4);
                vector2 = new Vector4(5, 6, 7, 8);
            };
        }

        [Subject(typeof(Vector4))]
        public class addition : two_vectors_context
        {
            static Vector4 resultVector;

            Because of = () => resultVector = vector1 + vector2;

            It should_have_the_X_value = () => resultVector.X.ShouldEqual(1 + 5);
            It should_have_the_Y_value = () => resultVector.Y.ShouldEqual(2 + 6);
            It should_have_the_Z_value = () => resultVector.Z.ShouldEqual(3 + 7);
            It should_have_the_W_value = () => resultVector.W.ShouldEqual(4 + 8);
        }

        [Subject(typeof(Vector4))]
        public class subtraction : two_vectors_context
        {
            static Vector4 resultVector;

            Because of = () => resultVector = vector1 - vector2;

            It should_have_the_X_value = () => resultVector.X.ShouldEqual(1 - 5);
            It should_have_the_Y_value = () => resultVector.Y.ShouldEqual(2 - 6);
            It should_have_the_Z_value = () => resultVector.Z.ShouldEqual(3 - 7);
            It should_have_the_W_value = () => resultVector.W.ShouldEqual(4 - 8);
        }

        [Subject(typeof(Vector4))]
        public class dot_product : two_vectors_context
        {
            static float result;

            Because of = () => result = vector1.Dot(vector2);

            It should_have_the_value = () => result.ShouldEqual(1*5+2*6+3*7+4*8);
        }
    }
}