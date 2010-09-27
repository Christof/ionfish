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

        [Subject(typeof(float))]
        public class from_0_to_1_range_to_custom_range
        {
            It should_convert_the_value = () => 0.2f.From01ToCustomRange(2, 4).ShouldEqual(2.4f);
        }
    }
}