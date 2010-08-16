using System;
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

        [Subject(typeof(Vector4))]
        public abstract class vector_context
        {
            protected static Vector4 vector;

            Establish context = () => vector = new Vector4(1, 2, 3, 4);
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
        public class scalar_multiplication : vector_context
        {
            static Vector4 resultVector;

            Because of = () => resultVector = vector * 2;

            It should_have_the_scaled_the_components = () => resultVector.ShouldEqual(new Vector4(2, 4, 6, 8));
        }

        [Subject(typeof(Vector4))]
        public class scalar_multiplication_reverse_operand_order : vector_context
        {
            static Vector4 resultVector;

            Because of = () => resultVector = 2 * vector;

            It should_have_the_scaled_the_components = () => resultVector.ShouldEqual(new Vector4(2, 4, 6, 8));
        }

        [Subject(typeof(Vector4))]
        public class dot_product : two_vectors_context
        {
            static float result;

            Because of = () => result = vector1.Dot(vector2);

            It should_have_the_value = () => result.ShouldEqual(1*5+2*6+3*7+4*8);
        }

        [Subject(typeof(Vector4))]
        public class vector_length : vector_context
        {
            static float result;

            Because of = () => result = vector.Length;

            It should_return_the_length = () => result.ShouldEqual((float)System.Math.Sqrt(1 * 1 + 2 * 2 + 3 * 3 + 4 * 4));
        }

        [Subject(typeof(Vector4))]
        public class normalize : vector_context
        {
            It should_have_length_of_one = () => vector.Normalized().Length.ShouldBeCloseTo(1);
        }

        [Subject(typeof(Vector4))]
        public class equals
        {
            static object vector;

            Because of = () => vector = new Vector4(1, 2, 3, 4);

            It should_return_true_if_the_components_difference_is_less_than_the_default_delta
                = () => vector.Equals(new Vector4(1.0001f, 2.0009f, 3f, 3.9999f)).ShouldBeTrue();

            It should_return_false_if_the_components_difference_is_greater_than_the_default_delta
                = () => vector.Equals(new Vector4(1.0001f, 2.0009f, 4f, 3.9999f)).ShouldBeFalse();

            It should_return_false_if_the_argument_has_another_type
                = () => vector.Equals("other type").ShouldBeFalse();
        }

        [Subject(typeof(Vector4))]
        public class equals_with_IEquatable
        {
            static IEquatable<Vector4> vector;

            Because of = () => vector = new Vector4(1, 2, 3, 4);

            It should_return_true_if_the_components_difference_is_less_than_the_default_delta
                = () => vector.Equals(new Vector4(1.0001f, 2.0009f, 3f, 3.9999f)).ShouldBeTrue();

            It should_return_false_if_the_components_difference_is_greater_than_the_default_delta
                = () => vector.Equals(new Vector4(1.0001f, 2.0009f, 4f, 3.9999f)).ShouldBeFalse();
        }

        [Subject(typeof(Vector4))]
        public class equals_with_operator
        {
            static Vector4 vector;

            Because of = () => vector = new Vector4(1, 2, 3, 4);

            It should_return_true_if_the_components_difference_is_less_than_the_default_delta
                = () => (vector == new Vector4(1.0001f, 2.0009f, 3f, 3.9999f)).ShouldBeTrue();

            It should_return_false_if_the_components_difference_is_greater_than_the_default_delta
                = () => (vector == new Vector4(1.0001f, 2.0009f, 4f, 3.9999f)).ShouldBeFalse();
        }

        [Subject(typeof(Vector4))]
        public class not_equals_with_operator
        {
            static Vector4 vector;

            Because of = () => vector = new Vector4(1, 2, 3, 4);

            It should_return_false_if_the_components_difference_is_less_than_the_default_delta
                = () => (vector != new Vector4(1.0001f, 2.0009f, 3f, 3.9999f)).ShouldBeFalse();

            It should_return_true_if_the_components_difference_is_greater_than_the_default_delta
                = () => (vector != new Vector4(1.0001f, 2.0009f, 4f, 3.9999f)).ShouldBeTrue();
        }
    }
}