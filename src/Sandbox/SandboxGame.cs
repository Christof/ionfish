using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Graphics.Materials;
using Graphics.Primitives;
using Input;
using Math;

namespace Sandbox
{
    public class SandboxGame : Game
    {
        private Keyboard mKeyboard;
        private InputCommandBinder mInputCommandBinder;
        private Camera mCamera;
        private Material mMaterial;
        private Material mSphereMaterial;
        private MeshMaterialBinding mQuadBinding;
        private MeshMaterialBinding mTriangleBinding;
        private MeshMaterialBinding mCubeBinding;
        private MeshMaterialBinding mSphereBinding;
        private Vector3RandomGenerator mVector3Random;
        private Vector3[] mCubePositions;
        private Vector3[] mTrianglePositions;
        private Vector3[] mQuadPositions;
        private CameraCommandBindingManager mCameraCommandBindingManager;

        protected override void Initialize()
        {
            var quadMesh = new Quad(Window.Device);
            var triangleMesh = new Triangle(Window.Device);
            var cubeMesh = new Cube(Window.Device);
            var sphereMesh = new Sphere(Window.Device);

            mMaterial = new Material("shader.fx", Window.Device);
            mSphereMaterial = new Material("normal_as_color.fx", Window.Device);
            mQuadBinding = new MeshMaterialBinding(Window.Device, mMaterial, quadMesh);
            mTriangleBinding = new MeshMaterialBinding(Window.Device, mMaterial, triangleMesh);
            mCubeBinding = new MeshMaterialBinding(Window.Device, mMaterial, cubeMesh);
            mSphereBinding = new MeshMaterialBinding(Window.Device, mSphereMaterial, sphereMesh);

            mKeyboard = new Keyboard();

            var stand = new FirstPersonStand { Position = new Vector3(0, 0, 3), Direction = -Vector3.ZAxis };
            var lens = new PerspectiveProjectionLens();
            mCamera = new Camera(stand, lens);

            var commands = new CommandManager();

            mInputCommandBinder = new InputCommandBinder(commands, mKeyboard);
            
            mCameraCommandBindingManager = new FirstPersonCameraCommandBindingManager(commands, mInputCommandBinder, stand, this);

            mVector3Random = new Vector3RandomGenerator(new Vector3(-2, -1, -2), new Vector3(2, 1, 2), 1);
            mCubePositions = new[]
            {
                mVector3Random.Next(),
                mVector3Random.Next(),
                mVector3Random.Next()
            };

            mTrianglePositions = new[]
            {
                mVector3Random.Next(),
                mVector3Random.Next(),
                mVector3Random.Next()
            };

            mQuadPositions = new[]
            {
                mVector3Random.Next(),
                mVector3Random.Next(),
                mVector3Random.Next()
            };
        }

        protected override void OnFrame()
        {
            mCameraCommandBindingManager.Update(Frametime);
            mKeyboard.Update();
            mInputCommandBinder.Update();

            foreach (var position in mCubePositions)
            {
                var world = Matrix.CreateTranslation(position);
                mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
                mCubeBinding.Draw();
            }

            foreach (var position in mTrianglePositions)
            {
                var world = Matrix.CreateTranslation(position);
                mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
                mTriangleBinding.Draw();
            }

            foreach (var position in mQuadPositions)
            {
                var world = Matrix.CreateTranslation(position);
                mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
                mQuadBinding.Draw();
            }

            mSphereMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * Matrix.RotateX(Gametime));
            mSphereBinding.Draw();
        }
    }
}
