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
    }
}