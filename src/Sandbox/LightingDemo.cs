using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Graphics.Materials;
using Graphics.Primitives;
using Input;
using Math;

namespace Sandbox
{
    public class LightingDemo : Game
    {
        private Keyboard mKeyboard;
        private InputCommandBinder mInputCommandBinder;
        private Camera mCamera;
        private Material mMaterial;
        private MeshMaterialBinding mSphereBinding;

        private const string ESCAPE = "escape";
        private const string TAKE_SCREENSHOT = "take screenshot";
        private const string MOVE_FORWARD = "move forward";
        private const string MOVE_BACKWARD = "move backward";
        private const string STRAFE_LEFT = "strafe left";
        private const string STRAFE_RIGHT = "strafe right";
        private const string UP = "up";
        private const string DOWN = "down";

        protected override void Initialize()
        {
            var sphereMesh = new Sphere(Window.Device);

            mMaterial = new Material("lighting.fx", Window.Device);
            mSphereBinding = new MeshMaterialBinding(Window.Device, mMaterial, sphereMesh);

            mKeyboard = new Keyboard();

            var stand = new OrbitingStand { Radius = 5 };
            var lens = new PerspectiveProjectionLens();
            mCamera = new Camera(stand, lens);

            var commands = new CommandManager();
            commands.Add(ESCAPE, Exit);
            commands.Add(TAKE_SCREENSHOT, Window.TakeScreenshot);

            mInputCommandBinder = new InputCommandBinder(commands, mKeyboard);
            mInputCommandBinder.Bind(Button.Escape, ESCAPE);
            mInputCommandBinder.Bind(Button.PrintScreen, TAKE_SCREENSHOT);

            //commands.Add(MOVE_BACKWARD, () => stand.Position += Vector3.ZAxis * mFrameTime * mSpeedFactor);
            //commands.Add(MOVE_FORWARD, () => stand.Position -= Vector3.ZAxis * mFrameTime * mSpeedFactor);
            commands.Add(STRAFE_RIGHT, () => stand.Azimuth += Frametime);
            commands.Add(STRAFE_LEFT, () => stand.Azimuth -= Frametime);
            //commands.Add(UP, () => stand.Position += Vector3.YAxis * mFrameTime * mSpeedFactor);
            //commands.Add(DOWN, () => stand.Position -= Vector3.YAxis * mFrameTime * mSpeedFactor);

            //mInputCommandBinder.Bind(Button.W, MOVE_FORWARD);
            //mInputCommandBinder.Bind(Button.S, MOVE_BACKWARD);
            mInputCommandBinder.Bind(Button.D, STRAFE_RIGHT);
            mInputCommandBinder.Bind(Button.A, STRAFE_LEFT);
            //mInputCommandBinder.Bind(Button.R, UP);
            //mInputCommandBinder.Bind(Button.F, DOWN);
        }

        protected override void OnFrame()
        {
            mKeyboard.Update();
            mInputCommandBinder.Update();

            var world = Matrix.RotateX(Gametime);
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mMaterial.SetWorld(Matrix.RotateX(Gametime));
            mSphereBinding.Draw();
        }
    }
}
