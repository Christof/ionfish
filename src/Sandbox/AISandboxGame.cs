using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Graphics.Materials;
using Graphics.Primitives;
using Input;
using Math;
using Math.AI;

namespace Sandbox
{
    public class AISandboxGame : Game
    {
        private Keyboard mKeyboard;
        private InputCommandBinder mInputCommandBinder;
        private Camera mCamera;
        private Material mMaterial;
        private MeshMaterialBinding mRefugeeBinding;
        private MeshMaterialBinding mSeekerBinding;
        private MeshMaterialBinding mGroundBinding;
        private readonly Kinetic mRefugeeKinetic = new Kinetic(new Vector3(-5, 0, -5));
        private readonly Kinetic mSeekerKinetic = new Kinetic(new Vector3(5, 0, 5), new Vector3(0, 0, -10));
        private SeekSteering mSeekSteering;
        private RefugeeSteering mRefugeeSteering;
        private CameraCommandManager mCameraCommandManager;

        private const string ESCAPE = "escape";
        private const string TAKE_SCREENSHOT = "take screenshot";

        protected override void Initialize()
        {
            mMaterial = new Material("shader.fx", Window.Device);
            mRefugeeBinding = new MeshMaterialBinding(Window.Device, mMaterial, new Cube(Window.Device, new Vector4(0.8f, 0, 0, 0)));
            mSeekerBinding = new MeshMaterialBinding(Window.Device, mMaterial, new Cube(Window.Device, new Vector4(0, 0, 0.8f, 0)));
            mGroundBinding = new MeshMaterialBinding(Window.Device, mMaterial, new Quad(Window.Device, new Vector4(0.2f, 0.2f, 0.2f, 0)));

            mKeyboard = new Keyboard();

            var stand = new Stand { Position = new Vector3(0, 10, 40) };
            stand.Position = new Vector3(0, 140, 0);
            stand.Direction = -Vector3.YAxis;
            stand.Up = -Vector3.ZAxis;

            var lens = new PerspectiveProjectionLens();
            mCamera = new Camera(stand, lens);

            var commands = new CommandManager();
            commands.Add(ESCAPE, Exit);
            commands.Add(TAKE_SCREENSHOT, Window.TakeScreenshot);
            
            mInputCommandBinder = new InputCommandBinder(commands, mKeyboard);
            mInputCommandBinder.Bind(Button.Escape, ESCAPE);
            mInputCommandBinder.Bind(Button.PrintScreen, TAKE_SCREENSHOT);

            mCameraCommandManager = new CameraCommandManager(commands, mInputCommandBinder, mCamera);

            mSeekSteering = new SeekSteering(mSeekerKinetic, mRefugeeKinetic, 15);
            mRefugeeSteering = new RefugeeSteering(mRefugeeKinetic, mSeekerKinetic, 15);
        }

        protected override void OnFrame()
        {
            mCameraCommandManager.Update(Frametime);
            mKeyboard.Update();
            mInputCommandBinder.Update();
            
            mSeekerKinetic.Update(mSeekSteering, 10, Frametime);
            mRefugeeKinetic.Update(mRefugeeSteering, 7, Frametime);

            var world = Matrix.CreateTranslation(mSeekerKinetic.Position);
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mSeekerBinding.Draw();

            world = Matrix.CreateTranslation(mRefugeeKinetic.Position);
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mRefugeeBinding.Draw();

            world = Matrix.CreateTranslation(new Vector3(0, -0.5f, 0)) * Matrix.Scale(100) * Matrix.RotateX(Constants.HALF_PI);
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mGroundBinding.Draw();
        }
    }
}
