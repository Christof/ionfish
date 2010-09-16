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
        private MeshMaterialBinding mQuadBinding;

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
            mQuadBinding = new MeshMaterialBinding(Window.Device, mMaterial, new Quad(Window.Device, new Vector4(0.6f, 0.6f, 0.6f, 0)));

            mKeyboard = new Keyboard();

            var stand = new OrbitingStand { Radius = 7 };
            var lens = new PerspectiveProjectionLens();
            mCamera = new Camera(stand, lens);

            var commands = new CommandManager();
            commands.Add(ESCAPE, Exit);
            commands.Add(TAKE_SCREENSHOT, Window.TakeScreenshot);

            mInputCommandBinder = new InputCommandBinder(commands, mKeyboard);
            mInputCommandBinder.Bind(Button.Escape, ESCAPE);
            mInputCommandBinder.Bind(Button.PrintScreen, TAKE_SCREENSHOT);

            commands.Add(MOVE_BACKWARD, () => stand.Radius += Frametime);
            commands.Add(MOVE_FORWARD, () => stand.Radius -= Frametime);
            commands.Add(STRAFE_RIGHT, () => stand.Azimuth += Frametime);
            commands.Add(STRAFE_LEFT, () => stand.Azimuth -= Frametime);
            commands.Add(UP, () => stand.Declination -= Frametime);
            commands.Add(DOWN, () => stand.Declination += Frametime);

            mInputCommandBinder.Bind(Button.W, MOVE_FORWARD);
            mInputCommandBinder.Bind(Button.S, MOVE_BACKWARD);
            mInputCommandBinder.Bind(Button.D, STRAFE_RIGHT);
            mInputCommandBinder.Bind(Button.A, STRAFE_LEFT);
            mInputCommandBinder.Bind(Button.R, UP);
            mInputCommandBinder.Bind(Button.F, DOWN);
        }

        protected override void OnFrame()
        {
            mKeyboard.Update();
            mInputCommandBinder.Update();

            RenderSphere();   
            RenderGround();
        }

        void RenderGround()
        {
            var rotation = Matrix.RotateX(-Constants.HALF_PI);
            var world = Matrix.CreateTranslation(new Vector3(0f, -1f, 0f)) * Matrix.Scale(5) * rotation;
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mMaterial.SetWorld(rotation);

            mQuadBinding.Draw();
        }

        void RenderSphere()
        {
            var rotateX = Matrix.RotateX(Gametime);
            var world = rotateX;
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mMaterial.SetWorld(rotateX);

            mSphereBinding.Draw();
        }
    }
}
