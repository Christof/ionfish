using Machine.Specifications;

namespace Math
{
    public class Spec_Quaternion
    {
        [Subject(typeof (Quaternion))]
        public class create_from_axis_angle
        {
            static Quaternion q;

            Because of = () => q = Quaternion.FromAxisAngle(new Vector3(1, 2, 3), Constants.PI);

            It should_have_a_W_value_of_0 = () => q.R.ShouldBeCloseTo(0);
            It should_have_an_I_value_of_1_divided_by_the_length = () => q.I.ShouldBeCloseTo(1f / Functions.Sqrt(14));
            It should_have_an_J_value_of_2_divided_by_the_length = () => q.J.ShouldBeCloseTo(2f / Functions.Sqrt(14));
            It should_have_an_K_value_of_3_divided_by_the_length = () => q.K.ShouldBeCloseTo(3f / Functions.Sqrt(14));
        }

        [Subject(typeof (Quaternion))]
        public class multiplication
        {
            static Quaternion left;
            static Quaternion right;
            static Quaternion result;
            static Quaternion expected;

            Establish context = () =>
            {
                right = Quaternion.FromAxisAngle(Vector3.YAxis, Constants.PI);
                left = Quaternion.FromAxisAngle(Vector3.ZAxis, Constants.HALF_PI);

                expected = Quaternion.FromAxisAngle(new Vector3(-1, 1, 0).Normalized(), Constants.PI);
            };

            Because of = () => result = left * right;

            It should_have_the_expected_R_value = () => result.R.ShouldBeCloseTo(expected.R);
            It should_have_the_expected_I_value = () => result.I.ShouldBeCloseTo(expected.I);
            It should_have_the_expected_J_value = () => result.J.ShouldBeCloseTo(expected.J);
            It should_have_the_expected_K_value = () => result.K.ShouldBeCloseTo(expected.K);
        }

        [Subject(typeof (Quaternion))]
        public class to_matrix
        {
            static Quaternion q;
            static Matrix result;
            static Matrix expected;

            Establish context = () =>
            {
                q = Quaternion.FromAxisAngle(Vector3.XAxis, Constants.HALF_PI);
                expected = Matrix.RotateX(3 * Constants.HALF_PI);
            };

            Because of = () => result = q.ToMatrix();

            private It should_have_converted_the_quaternion_to_a_matrix = () => 
                result.ShouldEqualWithDelta(expected, 0.000001f);
        }
    }
}