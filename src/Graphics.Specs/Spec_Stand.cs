using Graphics.Cameras;
using Machine.Specifications;
using Math;

namespace Graphics
{
    public class Spec_Stand
    {
        [Subject(typeof (Stand))]
        public class constructor
        {
            static Stand stand;

            Because of = () => stand = new Stand();

            It should_have_set_the_y_axis_as_Up = () => 
                stand.Up.ShouldEqual(Vector3.YAxis);

            It should_have_set_minus_the_z_axis_as_Direction = () =>
                stand.Direction.ShouldEqual(-Vector3.ZAxis);

            It should_have_set_the_origin_as_Position = () =>
                stand.Position.ShouldEqual(Vector3.Zero);
        }

        [Subject(typeof (Stand))]
        public class view_matrix
        {
            static Stand stand;
            static Matrix viewMatrix;

            Establish context = () => stand =
                new Stand
                {
                    Up = new Vector3(1, 2, 3),
                    Direction = new Vector3(4, 5, 6),
                    Position = new Vector3(7, 8, 9)
                };

            Because of = () => viewMatrix = stand.ViewMatrix;

            It should_have_calculated_the_view_matrix = () =>
                viewMatrix.ShouldEqualWithDelta(new Matrix(
                 0.4082483f,  -0.7909116f, -0.4558423f, 0,
                -0.8164966f, -0.09304842f, -0.5698029f, 0.8374357f,
                 0.4082483f,   0.6048148f, -0.6837634f, 13.90319f,
                         0f,           0f,          0f, 1));
        }
    }
}