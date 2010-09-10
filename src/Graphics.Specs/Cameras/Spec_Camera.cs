using Machine.Specifications;
using Math;
using Moq;
using It = Machine.Specifications.It;

namespace Graphics.Cameras
{
    public class Spec_Camera
    {
        [Subject(typeof (Camera))]
        public class constructor_sets_properties
        {
            private static ICamera camera;
            private static Mock<IStand> stand = new Mock<IStand>();
            private static Mock<ILens> lens = new Mock<ILens>();

            Because of = () => camera = new Camera(stand.Object, lens.Object);

            It should_have_assigned_the_Stand_property = () =>
                camera.Stand.ShouldBeTheSameAs(stand.Object);

            It should_have_assigned_the_Lens_property = () =>
                camera.Lens.ShouldBeTheSameAs(lens.Object);
        }

        [Subject(typeof (Camera))]
        public class view_projection_matrix
        {
            private static ICamera camera;

            private static Mock<IStand> stand = new Mock<IStand>();
            private static Matrix viewMatrix = new Matrix(
                1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16);

            private static Mock<ILens> lens = new Mock<ILens>();
            private static Matrix projectionMatrix = new Matrix(
                17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32);

            private static Matrix viewProjectionMatrix;

            Establish context = () => 
            {
                camera = new Camera(stand.Object, lens.Object);

                stand.Setup(x => x.ViewMatrix).Returns(viewMatrix);
                lens.Setup(x => x.ProjectionMatrix).Returns(projectionMatrix);
            };

            Because of = () => viewProjectionMatrix = camera.ViewProjectionMatrix;

            It should_have_calculated_the_view_projection_matrix = () =>
                viewProjectionMatrix.ShouldEqualWithDelta(projectionMatrix * viewMatrix);
        }
    }
}