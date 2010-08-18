using System;
using Machine.Specifications;

namespace Math
{
    public class Spec_Vector3
    {
        [Subject(typeof(Vector3))]
        public class initialization_with_the_three_components
        {
            static Vector3 vector;

            Because of = () => vector = new Vector3(1, 2, 3);

            It should_have_an_X_value = () => vector.X.ShouldEqual(1);
            It should_have_an_Y_value = () => vector.Y.ShouldEqual(2);
            It should_have_an_Z_value = () => vector.Z.ShouldEqual(3);
        }

        [Subject(typeof(Vector3))]
        public abstract class vector_context
        {
            protected static Vector3 vector;

            Establish context = () => vector = new Vector3(1, 2, 3);
        }

        public abstract class two_vectors_context
        {
            protected static Vector3 vector1;
            protected static Vector3 vector2;

            Establish context = () =>
            {
                vector1 = new Vector3(1, 2, 3);
                vector2 = new Vector3(4, 5, 6);
            };
        }

        [Subject(typeof(Vector3))]
        public class addition : two_vectors_context
        {
            static Vector3 resultVector;

            Because of = () => resultVector = vector1 + vector2;

            It should_have_added_the_X_components = () =>
                resultVector.X.ShouldEqual(1 + 4);

            It should_have_added_the_Y_components = () =>
                resultVector.Y.ShouldEqual(2 + 5);

            It should_have_added_the_Z_components = () =>
                resultVector.Z.ShouldEqual(3 + 6);
        }

        [Subject(typeof(Vector3))]
        public class subtraction : two_vectors_context
        {
            static Vector3 resultVector;

            Because of = () => resultVector = vector1 - vector2;

            It should_have_subtracted_the_X_components = () =>
                resultVector.X.ShouldEqual(1 - 4);

            It should_have_subtracted_the_Y_components = () =>
                resultVector.Y.ShouldEqual(2 - 5);

            It should_have_subtracted_the_Z_components = () =>
                resultVector.Z.ShouldEqual(3 - 6);
        }

        [Subject(typeof(Vector3))]
        public class unary_minus_operator : vector_context
        {
            static Vector3 resultVector;
            static Vector3 negatedVector = new Vector3(-1, -2, -3);

            Because of = () => resultVector = -vector;

            It should_be_negated = () => resultVector.ShouldEqual(negatedVector);
        }

        [Subject(typeof(Vector3))]
        public class scalar_multiplication : vector_context
        {
            static Vector3 resultVector;

            Because of = () => resultVector = vector * 2;

            It should_have_the_scaled_the_components = () => resultVector.ShouldEqual(new Vector3(2, 4, 6));
        }

        [Subject(typeof(Vector3))]
        public class scalar_multiplication_reverse_operand_order : vector_context
        {
            static Vector3 resultVector;

            Because of = () => resultVector = 2 * vector;

            It should_have_the_scaled_the_components = () => resultVector.ShouldEqual(new Vector3(2, 4, 6));
        }

        [Subject(typeof(Vector3))]
        public class dot_product : two_vectors_context
        {
            static float result;

            Because of = () => result = vector1.Dot(vector2);

            It should_return_the_dot_product = () =>
                result.ShouldEqual(1 * 4 + 2 * 5 + 3 * 6);
        }

        [Subject(typeof(Vector3))]
        public class cross_product : two_vectors_context
        {
            static Vector3 resultVector;

            Because of = () => resultVector = vector1.Cross(vector2);

            It should_return_the_cross_product = () =>
                resultVector = new Vector3(2 * 6 - 3 * 5, -(1 * 6 - 3 * 4), 1 * 5 - 2 * 4);
        }

        [Subject(typeof(Vector3))]
        public class vector_length : vector_context
        {
            static float result;

            Because of = () => result = vector.Length;

            It should_return_the_length = () => result.ShouldEqual((float)System.Math.Sqrt(1 * 1 + 2 * 2 + 3 * 3));
        }

        [Subject(typeof(Vector3))]
        public class normalize : vector_context
        {
            It should_have_length_of_one = () => vector.Normalized().Length.ShouldBeCloseTo(1);
        }

        [Subject(typeof(Vector3))]
        public class equals
        {
            static object vector;

            Because of = () => vector = new Vector3(1, 2, 3);

            It should_return_true_if_the_components_difference_is_less_than_the_default_delta
                = () => vector.Equals(new Vector3(1.0001f, 2.0009f, 3f)).ShouldBeTrue();

            It should_return_false_if_the_components_difference_is_greater_than_the_default_delta
                = () => vector.Equals(new Vector3(1.0001f, 2.0009f, 4f)).ShouldBeFalse();

            It should_return_false_if_the_argument_has_another_type
                = () => vector.Equals("other type").ShouldBeFalse();
        }

        [Subject(typeof(Vector3))]
        public class equals_with_IEquatable
        {
            static IEquatable<Vector3> vector;

            Because of = () => vector = new Vector3(1, 2, 3);

            It should_return_true_if_the_components_difference_is_less_than_the_default_delta
                = () => vector.Equals(new Vector3(1.0001f, 2.0009f, 3f)).ShouldBeTrue();

            It should_return_false_if_the_components_difference_is_greater_than_the_default_delta
                = () => vector.Equals(new Vector3(1.0001f, 2.0009f, 4f)).ShouldBeFalse();
        }

        [Subject(typeof(Vector3))]
        public class equals_with_operator
        {
            static Vector3 vector;

            Because of = () => vector = new Vector3(1, 2, 3);

            It should_return_true_if_the_components_difference_is_less_than_the_default_delta
                = () => (vector == new Vector3(1.0001f, 2.0009f, 3f)).ShouldBeTrue();

            It should_return_false_if_the_components_difference_is_greater_than_the_default_delta
                = () => (vector == new Vector3(1.0001f, 2.0009f, 4f)).ShouldBeFalse();
        }

        [Subject(typeof(Vector3))]
        public class not_equals_with_operator
        {
            static Vector3 vector;

            Because of = () => vector = new Vector3(1, 2, 3);

            It should_return_false_if_the_components_difference_is_less_than_the_default_delta
                = () => (vector != new Vector3(1.0001f, 2.0009f, 3f)).ShouldBeFalse();

            It should_return_true_if_the_components_difference_is_greater_than_the_default_delta
                = () => (vector != new Vector3(1.0001f, 2.0009f, 4f)).ShouldBeTrue();
        }

        [Subject(typeof(Vector3))]
        public class to_string
        {
            static Vector3 vector = new Vector3(1.1f, 2.2f, 3.3f);

            It should_be_formatted_correctly = () => vector.ToString().ShouldEqual("X: 1.1 Y: 2.2 Z: 3.3");
        }
    }
}