using Machine.Specifications;
using Math;

namespace Graphics.Cameras
{
    public class Spec_PerspectiveProjectionLens
    {
        [Subject(typeof (PerspectiveProjectionLens))]
        public class projection_matrix
        {
            static PerspectiveProjectionLens lens;
            static Matrix projectionMatrix;

            Establish context = () => lens = new PerspectiveProjectionLens();

            Because of = () => projectionMatrix = lens.ProjectionMatrix;

            private It should_have_calculated_the_view_matrix = () =>
                projectionMatrix.ShouldEqualWithDelta(
                    new Matrix(
                        2.626998f, 0, 0, 0,
                        0, 4.670219f, 0, 0,
                        0, 0, -1, -0.001f,
                        0, 0, -1, 0), 0.00001f);
        }
    }
}