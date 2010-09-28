using Machine.Specifications;

namespace Math
{
    public class Spec_Quaternion
    {
        [Subject(typeof (Quaternion))]
        public class create_from_axis_angle
        {
            static Quaternion q;

            Because of = () => q = new Quaternion(new Vector3(1, 2, 3), Constants.PI);

            It should_have_a_W_value_of_0 = () => q.R.ShouldBeCloseTo(0);
            It should_have_an_I_value_of_1 = () => q.I.ShouldBeCloseTo(1);
            It should_have_an_J_value_of_2 = () => q.J.ShouldBeCloseTo(2);
            It should_have_an_K_value_of_3 = () => q.K.ShouldBeCloseTo(3);
        }
    }
}