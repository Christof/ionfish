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
    public class Player : IHasPosition
    {
        public Vector3 Position { get; set; }
    }

    public class Spheres : Game
    {
        private Keyboard mKeyboard;
        private InputCommandBinder mInputCommandBinder;
        private Camera mCamera;
        private Material mMaterial;
        private MeshMaterialBinding mSphereBinding;
        private MeshMaterialBinding mQuadBinding;
        private MeshMaterialBinding mCubeBinding;
        private readonly Player mPlayer = new Player();
        private readonly Kinematic mTargetKinematic = new Kinematic(new Vector3(-2, 0, -2), 0.2f);
        private readonly Kinematic mEnemyKinematic = new Kinematic(new Vector3(2, 0, -2), 0.3f);
        private RefugeeSteering mTargetSteering;
        private ArrivingSteering mEnemySteering;
        private Matrix mSphereCorrection;

        private const string ESCAPE = "escape";
        private const string TAKE_SCREENSHOT = "take screenshot";
        private const string MOVE_FORWARD = "move forward";
        private const string MOVE_BACKWARD = "move backward";
        private const string STRAFE_LEFT = "strafe left";
        private const string STRAFE_RIGHT = "strafe right";

        protected override void Initialize()
        {
            mMaterial = new Material("lighting.fx", Window.Device);
            mSphereBinding = new MeshMaterialBinding(Window.Device, mMaterial, new Sphere(Window.Device, radius: 0.1f));
            mQuadBinding = new MeshMaterialBinding(Window.Device, mMaterial, new Quad(Window.Device, new Vector4(0.6f, 0.6f, 0.6f, 0)));
            mCubeBinding = new MeshMaterialBinding(Window.Device, mMaterial, new CubeWithNormals(Window.Device));

            mKeyboard = new Keyboard();

            var stand = new Stand();
            stand.Position = new Vector3(0, 1, 4);
            stand.Direction = (new Vector3(0, 0.5f, 0) - stand.Position).Normalized();
            stand.Up = Vector3.XAxis.Cross(stand.Direction);

            var lens = new PerspectiveProjectionLens();
            mCamera = new Camera(stand, lens);

            var commands = new CommandManager();
            commands.Add(ESCAPE, Exit);
            commands.Add(TAKE_SCREENSHOT, Window.TakeScreenshot);
            commands.Add(MOVE_FORWARD, () => mPlayer.Position += -Vector3.ZAxis * Frametime);
            commands.Add(MOVE_BACKWARD, () => mPlayer.Position -= -Vector3.ZAxis * Frametime);
            commands.Add(STRAFE_LEFT, () => mPlayer.Position += -Vector3.XAxis * Frametime);
            commands.Add(STRAFE_RIGHT, () => mPlayer.Position -= -Vector3.XAxis * Frametime);

            mInputCommandBinder = new InputCommandBinder(commands, mKeyboard);
            mInputCommandBinder.Bind(Button.Escape, ESCAPE);
            mInputCommandBinder.Bind(Button.PrintScreen, TAKE_SCREENSHOT);
            mInputCommandBinder.Bind(Button.W, MOVE_FORWARD);
            mInputCommandBinder.Bind(Button.S, MOVE_BACKWARD);
            mInputCommandBinder.Bind(Button.A, STRAFE_LEFT);
            mInputCommandBinder.Bind(Button.D, STRAFE_RIGHT);

            mEnemySteering = new ArrivingSteering(mEnemyKinematic, mPlayer, maxAcceleration: 0.7f, slowRadius: 0.8f, satisfactionRadius: 0.2f);
            mTargetSteering = new RefugeeSteering(mTargetKinematic, mPlayer, 0.7f);
            mSphereCorrection = Matrix.CreateTranslation(new Vector3(0, 0.1f, 0));
        }

        protected override void OnFrame()
        {
            mKeyboard.Update();
            mInputCommandBinder.Update();
            var stand = mCamera.Stand;
            stand.Position = mPlayer.Position + new Vector3(0, 1, 4);
            stand.Direction = (mPlayer.Position + new Vector3(0, 0.5f, 0) - stand.Position).Normalized();

            mTargetKinematic.Update(mTargetSteering, Frametime);
            mEnemyKinematic.Update(mEnemySteering, Frametime);

            RenderSphere();
            RenderCube();
            RenderNpcs();

            RenderGround();
        }

        private void RenderNpcs()
        {
            var world = Matrix.CreateTranslation(mTargetKinematic.Position) * mSphereCorrection;
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mSphereBinding.Draw();

            world = Matrix.CreateTranslation(mEnemyKinematic.Position) * mSphereCorrection;
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mSphereBinding.Draw();
        }

        void RenderGround()
        {
            var rotation = Matrix.RotateX(-Constants.HALF_PI);
            var world = Matrix.Scale(50) * rotation;
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mMaterial.SetWorld(rotation);

            mQuadBinding.Draw();
        }

        void RenderSphere()
        {
            var rotateX = Matrix.RotateX(Gametime);
            var world = Matrix.CreateTranslation(mPlayer.Position) * mSphereCorrection * rotateX;
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mMaterial.SetWorld(rotateX);

            mSphereBinding.Draw();
        }

        void RenderCube()
        {
            var rotateX = Matrix.Identity;
            var world = Matrix.CreateTranslation(new Vector3(1, 0, -2)) * rotateX;
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mMaterial.SetWorld(rotateX);

            mCubeBinding.Draw();
        }
    }
}
