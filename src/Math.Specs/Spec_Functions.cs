using Machine.Specifications;

namespace Math
{
    public class Spec_Functions
    {
        [Subject(typeof(Functions))]
        public class cotan
        {
            It should_return_0_for_half_pi = () => Functions.CoTan(Constants.HALF_PI).ShouldBeCloseTo(0);
            It should_return_1_for_quarter_pi = () => Functions.CoTan(Constants.PI / 4f).ShouldBeCloseTo(1);
            It should_return_infinity_for_0 = () => Functions.CoTan(0).ShouldBeCloseTo(float.PositiveInfinity);
        }

        [Subject(typeof (Functions))]
        public class GetRandom
        {
            It should_return_a_random_number_in_the_given_range = () => 
                Functions.GetRandom(-1, 2).ShouldBeGreaterThan(-1.0001).ShouldBeLessThan(2);
        }

        [Subject(typeof (Functions))]
        public class Sqrt
        {
            It should_return_the_square_root_of_the_given_number = () =>
                Functions.Sqrt(9).ShouldEqual(3);
        }
    }
}