using Machine.Specifications;
using Math;

namespace Graphics.Cameras
{
    public class Spec_OrbitingStand
    {
        [Subject(typeof (OrbitingStand))]
        public class position_and_direction_are_calculated_from_angles_and_radius
        {
            static OrbitingStand stand = new OrbitingStand(2, Constants.HALF_PI, Constants.HALF_PI * 0.5f);

            It should_calculate_the_position = () =>
                stand.Position.ShouldEqual(new Vector3(0, Functions.Sqrt(2), Functions.Sqrt(2)));

            It should_calculate_the_direction = () =>
                stand.Direction.ShouldEqual(new Vector3(0, -1 / Functions.Sqrt(2), -1 / Functions.Sqrt(2)));
        }

        [Subject(typeof (OrbitingStand))]
        public class Up
        {
            static OrbitingStand stand = new OrbitingStand();

            It should_return_the_y_axis_for_as_Up = () =>
                stand.Up.ShouldEqual(Vector3.YAxis);
        }

        [Subject(typeof (OrbitingStand))]
        public class setting_and_getting_the_angles
        {
            static OrbitingStand stand = new OrbitingStand();

            Because of = () =>
            {
                stand.Azimuth = 0.1f;
                stand.Declination = 0.2f;
                stand.MaxDeclination = 0.5f;
                stand.MinDeclination = -0.3f;
            };

            It should_have_changed_the_Azimuth= () =>
                stand.Azimuth.ShouldEqual(0.1f);

            It should_have_changed_the_Declination = () =>
                stand.Declination = 0.2f;

            It should_have_changed_the_MaxDeclination = () =>
                stand.MaxDeclination.ShouldEqual(0.5f);

            It should_have_changed_the_MinDeclination = () =>
                stand.MinDeclination.ShouldEqual(-0.3f);
        }

        [Subject(typeof (OrbitingStand))]
        public class Declination_clamping
        {
            static OrbitingStand stand = new OrbitingStand();

            Because of = () =>
            {
                stand.MaxDeclination = 0.3f;
                stand.MinDeclination = -0.3f;
            };

            It should_clamp_a_larger_Declination = () =>
            {
                stand.Declination = 0.4f;
                stand.Declination.ShouldEqual(0.3f);
            };

            It should_clamp_a_smaller_Declination = () =>
            {
                stand.Declination = -0.4f;
                stand.Declination.ShouldEqual(-0.3f);
            };
        }

        [Subject(typeof (OrbitingStand))]
        public class Declination_max_and_min_clamping
        {
            static OrbitingStand stand = new OrbitingStand();

            It should_clamp_a_larger_MaxDeclination = () =>
            {
                stand.MaxDeclination = 2;
                stand.MaxDeclination.ShouldEqual(Constants.HALF_PI - Constants.DELTA);
            };

            It should_clamp_a_smaller_MinDeclination = () =>
            {
                stand.MinDeclination = -2;
                stand.MinDeclination.ShouldEqual(-Constants.HALF_PI + Constants.DELTA);
            };
        }

        [Subject(typeof (OrbitingStand))]
        public class ViewMatrix
        {
            static OrbitingStand stand = new OrbitingStand();
            static Matrix viewMatrix;

            Because of = () => viewMatrix = stand.ViewMatrix;

            // using the default position of 0|0|10
            It should_have_calculated_the_ViewMatrix = () =>
                viewMatrix.ShouldEqualWithDelta(new Matrix(
                    0, 0, -1, 0, // new x-axis
                    0, 1, 0, 0, // new y-axis which has not changed
                    1, 0, 0, -10, // new z-axis and translation
                    0, 0, 0, 1));
        }
    }
}