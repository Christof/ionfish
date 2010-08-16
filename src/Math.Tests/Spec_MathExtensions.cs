using Machine.Specifications;

namespace Math
{
    public class Spec_MathExtensions
    {
        [Subject(typeof(float))]
        public class equals_with_delta
        {
            It should_return_true_if_the_difference_between_the_values_is_less_than_or_equal_the_given_delta
                = () => 1.0f.EqualsWithDelta(1.1f, 0.11f).ShouldBeTrue();

            It should_return_false_if_the_difference_between_the_values_is_larger_than_the_given_delta
                = () => 1.0f.EqualsWithDelta(1.2f, 0.11f).ShouldBeFalse();

            It should_return_true_if_the_difference_between_the_values_is_less_than_or_equal_to_the_default_delta
                = () => 2.0f.EqualsWithDelta(2.001f).ShouldBeTrue();

            It should_return_true_if_the_difference_between_the_values_is_less_than_the_default_delta
                = () => 2.0f.EqualsWithDelta(2.0011f).ShouldBeFalse();
        }
    }
}