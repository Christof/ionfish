using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Graphics.Materials;
using Graphics.Primitives;
using Input;
using Math;
using SlimDX.Direct3D10;

namespace Sandbox
{
    public class TextureDemo : Game
    {
        private Keyboard mKeyboard;
        private InputCommandBinder mInputCommandBinder;
        private Camera mCamera;
        private Material mMaterial;
        private MeshMaterialBinding mQuadBinding;
        private Texture2D mTexture;

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
            mMaterial = new Material("texture.fx", Window.Device);
            mQuadBinding = new MeshMaterialBinding(Window.Device, mMaterial, new Quad(Window.Device, new Vector4(0.6f, 0.6f, 0.6f, 0)));
            mTexture = Texture2D.FromFile(Window.Device, "texture.png");

            mKeyboard = new Keyboard();

            var stand = new OrbitingStand {Radius = 7, Azimuth = Constants.HALF_PI};
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
            commands.Add(STRAFE_RIGHT, () => stand.Azimuth -= Frametime);
            commands.Add(STRAFE_LEFT, () => stand.Azimuth += Frametime);
            commands.Add(UP, () => stand.Declination += Frametime);
            commands.Add(DOWN, () => stand.Declination -= Frametime);

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

            RenderQuad();
        }

        void RenderQuad()
        {
            var world = Matrix.Identity;
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            //mMaterial.SetWorld(Matrix.Identity);
            mMaterial.SetTexture(mTexture);

            mQuadBinding.Draw();
        }
    }
}
