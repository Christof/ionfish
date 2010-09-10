using System;
using Machine.Specifications;

namespace Math
{
    public class Spec_Vector3RandomGenerator
    {
        [Subject(typeof(Vector3RandomGenerator))]
        public class generating_random_vector3
        {
            static Vector3RandomGenerator generator1;
            static Vector3RandomGenerator generator2;
            static Vector3 minVector = new Vector3(1,1,1);
            static Vector3 maxVector = new Vector3(2,2,2);
            static Vector3 vector1;
            static Vector3 vector2;

            Establish context = () =>
            {
                generator1 = new Vector3RandomGenerator(minVector, maxVector, 1);
                generator2 = new Vector3RandomGenerator(minVector, maxVector, 1);
            };

            Because of = () =>
            {
                vector1 = generator1.Next();
                vector2 = generator2.Next();
            };

            It should_have_the_same_randomvector_when_the_seeds_are_equal =
                () => vector1.ShouldEqual(vector2);
        }

        [Subject(typeof(Vector3RandomGenerator))]
        public class throw_exception_when_the_maxvalue_is_lower_then_the_minvalue
        {
            static Vector3RandomGenerator generator1;
            static Exception resultException;
            static Vector3 minVector = new Vector3(1, 1, 1);
            static Vector3 maxVector = new Vector3(2, 2, 2);

            Establish context = () => generator1 = new Vector3RandomGenerator(maxVector, minVector, 1);

            Because of = () => resultException = Catch.Exception(() => generator1.Next());

            It should_throw_an_exception = () => resultException.ShouldBeOfType<ArgumentOutOfRangeException>();
        }

        [Subject(typeof(Vector3RandomGenerator))]
        public class initializing_the_random_generator_with_default_values
        {
            static Vector3 minVector = new Vector3(-1, -1, -1);
            static Vector3 maxVector = new Vector3(1, 1, 1);
            static Vector3 vector1;

            Because of = () => vector1 = new Vector3RandomGenerator(1).Next();

            It should_have_a_x_value_bigger_then_min = () => vector1.X.ShouldBeGreaterThan(minVector.X);
            It should_have_a_x_value_smaller_then_max = () => vector1.X.ShouldBeLessThan(maxVector.X);
            It should_have_a_y_value_bigger_then_min = () => vector1.Y.ShouldBeGreaterThan(minVector.Y);
            It should_have_a_y_value_smaller_then_max = () => vector1.Y.ShouldBeLessThan(maxVector.Y);
            It should_have_a_z_value_bigger_then_min = () => vector1.Z.ShouldBeGreaterThan(minVector.Z);
            It should_have_a_z_value_smaller_then_max = () => vector1.Z.ShouldBeLessThan(maxVector.Z);
        }
    }
}