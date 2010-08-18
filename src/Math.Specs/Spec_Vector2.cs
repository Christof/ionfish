using System;
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
        public abstract class vector_context
        {
            protected static Vector2 vector;

            Establish context = () => vector = new Vector2(1, 2);
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
        public class unary_minus_operator : vector_context
        {
            static Vector2 resultVector;
            static Vector2 negatedVector = new Vector2(-1, -2);

            Because of = () => resultVector = -vector;

            It should_be_negated = () => resultVector.ShouldEqual(negatedVector);
        }

        [Subject(typeof(Vector2))]
        public class scalar_multiplication : vector_context
        {
            static Vector2 resultVector;

            Because of = () => resultVector = vector * 2;

            It should_have_the_scaled_the_components = () => resultVector.ShouldEqual(new Vector2(2, 4));
        }

        [Subject(typeof(Vector2))]
        public class scalar_multiplication_reverse_operand_order : vector_context
        {
            static Vector2 resultVector;

            Because of = () => resultVector = 2 * vector;

            It should_have_the_scaled_the_components = () => resultVector.ShouldEqual(new Vector2(2, 4));
        }

        [Subject(typeof(Vector2))]
        public class dot_product : two_vectors_context
        {
            static float result;

            Because of = () => result = vector1.Dot(vector2);

            It should_have_the_dot_product = () => result.ShouldEqual(1*3+2*4);
        }

        [Subject(typeof(Vector2))]
        public class vector_length : vector_context
        {
            static float result;

            Because of = () => result = vector.Length;

            It should_return_the_length = () => result.ShouldEqual((float)System.Math.Sqrt(1 * 1 + 2 * 2));
        }

        [Subject(typeof(Vector2))]
        public class normalize : vector_context
        {
            It should_have_length_of_one = () => vector.Normalized().Length.ShouldBeCloseTo(1);
        }

        [Subject(typeof(Vector2))]
        public class equals
        {
            static object vector;

            Because of = () => vector = new Vector2(1, 2);

            It should_return_true_if_the_components_difference_is_less_than_the_default_delta
                = () => vector.Equals(new Vector2(1.0001f, 2f)).ShouldBeTrue();

            It should_return_false_if_the_components_difference_is_greater_than_the_default_delta
                = () => vector.Equals(new Vector2(1.0001f, 3f)).ShouldBeFalse();

            It should_return_false_if_the_argument_has_another_type
                = () => vector.Equals("other type").ShouldBeFalse();
        }

        [Subject(typeof(Vector2))]
        public class equals_with_IEquatable
        {
            static IEquatable<Vector2> vector;

            Because of = () => vector = new Vector2(1, 2);

            It should_return_true_if_the_components_difference_is_less_than_the_default_delta
                = () => vector.Equals(new Vector2(1.0001f, 2f)).ShouldBeTrue();

            It should_return_false_if_the_components_difference_is_greater_than_the_default_delta
                = () => vector.Equals(new Vector2(1.0001f, 3f)).ShouldBeFalse();
        }

        [Subject(typeof(Vector2))]
        public class equals_with_operator
        {
            static Vector2 vector;

            Because of = () => vector = new Vector2(1, 2);

            It should_return_true_if_the_components_difference_is_less_than_the_default_delta
                = () => (vector == new Vector2(1.0001f, 2f)).ShouldBeTrue();

            It should_return_false_if_the_components_difference_is_greater_than_the_default_delta
                = () => (vector == new Vector2(1.0001f, 3f)).ShouldBeFalse();
        }

        [Subject(typeof(Vector2))]
        public class not_equals_with_operator
        {
            static Vector2 vector;

            Because of = () => vector = new Vector2(1, 2);

            It should_return_false_if_the_components_difference_is_less_than_the_default_delta
                = () => (vector != new Vector2(1.0001f, 2f)).ShouldBeFalse();

            It should_return_true_if_the_components_difference_is_greater_than_the_default_delta
                = () => (vector != new Vector2(1.0001f, 3f)).ShouldBeTrue();
        }

        [Subject(typeof(Vector2))]
        public class to_string
        {
            static Vector2 vector = new Vector2(1.1f, 2.2f);

            It should_be_formatted_correctly = () => vector.ToString().ShouldEqual("X: 1.1 Y: 2.2");
        }
    }
}