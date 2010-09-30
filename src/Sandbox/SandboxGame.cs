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
        private MeshMaterialBinding mQuadBinding;
        private MeshMaterialBinding mTriangleBinding;
        private MeshMaterialBinding mCubeBinding;
        private MeshMaterialBinding mSphereBinding;
        private Vector3RandomGenerator mVector3Random;
        private Vector3[] mCubePositions;
        private Vector3[] mTrianglePositions;
        private Vector3[] mQuadPositions;
        private CameraCommandManager mCameraCommandManager;

        private const string ESCAPE = "escape";
        private const string TAKE_SCREENSHOT = "take screenshot";
        private const string TURN_LEFT = "turn left";
        private const string TURN_RIGHT = "turn right";
        private const string TURN_UP = "turn up";
        private const string TURN_DOWN = "turn down";
        private const string ROLL_LEFT = "roll left";
        private const string ROLL_RIGHT = "roll right";

        protected override void Initialize()
        {
            var quadMesh = new Quad(Window.Device);
            var triangleMesh = new Triangle(Window.Device);
            var cubeMesh = new Cube(Window.Device);
            var sphereMesh = new Sphere(Window.Device);

            mMaterial = new Material("shader.fx", Window.Device);
            mQuadBinding = new MeshMaterialBinding(Window.Device, mMaterial, quadMesh);
            mTriangleBinding = new MeshMaterialBinding(Window.Device, mMaterial, triangleMesh);
            mCubeBinding = new MeshMaterialBinding(Window.Device, mMaterial, cubeMesh);
            mSphereBinding = new MeshMaterialBinding(Window.Device, mMaterial, sphereMesh);

            mKeyboard = new Keyboard();

            var stand = new FirstPersonStand { Position = new Vector3(0, 0, 3), Direction = -Vector3.ZAxis };
            var lens = new PerspectiveProjectionLens();
            mCamera = new Camera(stand, lens);

            var commands = new CommandManager();
            commands.Add(ESCAPE, Exit);
            commands.Add(TAKE_SCREENSHOT, Window.TakeScreenshot);
            commands.Add(TURN_LEFT, () => stand.Yaw(-Frametime));
            commands.Add(TURN_RIGHT, () => stand.Yaw(Frametime));
            commands.Add(TURN_UP, () => stand.Pitch(-Frametime));
            commands.Add(TURN_DOWN, () => stand.Pitch(Frametime));
            commands.Add(ROLL_LEFT, () => stand.Roll(Frametime));
            commands.Add(ROLL_RIGHT, () => stand.Roll(-Frametime));

            mInputCommandBinder = new InputCommandBinder(commands, mKeyboard);
            mInputCommandBinder.Bind(Button.Escape, ESCAPE);
            mInputCommandBinder.Bind(Button.PrintScreen, TAKE_SCREENSHOT);
            mInputCommandBinder.Bind(Button.J, TURN_LEFT);
            mInputCommandBinder.Bind(Button.L, TURN_RIGHT);
            mInputCommandBinder.Bind(Button.I, TURN_UP);
            mInputCommandBinder.Bind(Button.K, TURN_DOWN);
            mInputCommandBinder.Bind(Button.U, ROLL_LEFT);
            mInputCommandBinder.Bind(Button.O, ROLL_RIGHT);
            
            mCameraCommandManager = new CameraCommandManager(commands, mInputCommandBinder, stand);

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
            mCameraCommandManager.Update(Frametime);
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

            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * Matrix.RotateX(Gametime));
            mSphereBinding.Draw();
        }
    }
}
