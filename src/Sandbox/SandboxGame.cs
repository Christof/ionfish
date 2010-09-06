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
        private Vector3RandomGenerator mVector3Random;
        private Vector3[] mCubePositions;
        private Vector3[] mTrianglePositions;
        private Vector3[] mQuadPositions;

        private const string MOVE_FORWARD = "move forward";
        private const string MOVE_BACKWARD = "move backward";
        private const string STRAFE_LEFT = "strafe left";
        private const string STRAFE_RIGHT = "strafe right";
        private const string ESCAPE = "escape";
        private const string TAKE_SCREENSHOT = "take screenshot";

        protected override void Initialize()
        {
            var quadMesh = new Quad(Window.Device);
            var triangleMesh = new Triangle(Window.Device);
            var cubeMesh = new Cube(Window.Device);

            mMaterial = new Material("shader.fx", Window.Device);
            mQuadBinding = new MeshMaterialBinding(Window.Device, mMaterial, quadMesh);
            mTriangleBinding = new MeshMaterialBinding(Window.Device, mMaterial, triangleMesh);
            mCubeBinding = new MeshMaterialBinding(Window.Device, mMaterial, cubeMesh);

            mKeyboard = new Keyboard();

            var stand = new Stand { Position = new Vector3(0, 0, 3), Direction = -Vector3.ZAxis };
            var lens = new PerspectiveProjectionLens();
            mCamera = new Camera(stand, lens);

            var commands = new CommandManager();
            commands.Add(MOVE_BACKWARD, () => stand.Position += Vector3.ZAxis * Frametime);
            commands.Add(MOVE_FORWARD, () => stand.Position -= Vector3.ZAxis * Frametime);
            commands.Add(STRAFE_RIGHT, () => stand.Position += Vector3.XAxis * Frametime);
            commands.Add(STRAFE_LEFT, () => stand.Position -= Vector3.XAxis * Frametime);
            commands.Add(ESCAPE, Exit);
            commands.Add(TAKE_SCREENSHOT, Window.TakeScreenshot);

            mInputCommandBinder = new InputCommandBinder(commands, mKeyboard);
            mInputCommandBinder.Bind(Button.W, MOVE_FORWARD);
            mInputCommandBinder.Bind(Button.S, MOVE_BACKWARD);
            mInputCommandBinder.Bind(Button.D, STRAFE_RIGHT);
            mInputCommandBinder.Bind(Button.A, STRAFE_LEFT);
            mInputCommandBinder.Bind(Button.Escape, ESCAPE);
            mInputCommandBinder.Bind(Button.PrintScreen, TAKE_SCREENSHOT);

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
            mKeyboard.Update();
            mInputCommandBinder.Update();

            foreach (var position in mCubePositions)
            {
                var world = Matrix.CreateTranslation(position);
                mMaterial.SetWorldViewProjectionMatrix(world * mCamera.ViewProjectionMatrix);
                mCubeBinding.Draw();
            }

            foreach (var position in mTrianglePositions)
            {
                var world = Matrix.CreateTranslation(position);
                mMaterial.SetWorldViewProjectionMatrix(world * mCamera.ViewProjectionMatrix);
                mTriangleBinding.Draw();
            }

            foreach (var position in mQuadPositions)
            {
                var world = Matrix.CreateTranslation(position);
                mMaterial.SetWorldViewProjectionMatrix(world * mCamera.ViewProjectionMatrix);
                mQuadBinding.Draw();
            }
        }
    }
}
