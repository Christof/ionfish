using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Graphics.Materials;
using Graphics.Primitives;
using Input;
using Math;

namespace Sandbox
{
    public class AISandboxGame : Game
    {
        private Keyboard mKeyboard;
        private InputCommandBinder mInputCommandBinder;
        private Camera mCamera;
        private Material mMaterial;
        private MeshMaterialBinding mTriangleBinding;
        private MeshMaterialBinding mCubeBinding;
        private readonly Kinetic mRefugeeKinetic = new Kinetic(new Vector3(-5, 0, -5));
        private readonly Kinetic mSeekerKinetic = new Kinetic(new Vector3(10, 0, 10));
        private SeekSteering mSeekSteering;
        private RefugeeSteering mRefugeeSteering;

        private const string MOVE_FORWARD = "move forward";
        private const string MOVE_BACKWARD = "move backward";
        private const string STRAFE_LEFT = "strafe left";
        private const string STRAFE_RIGHT = "strafe right";
        private const string ESCAPE = "escape";
        private const string TAKE_SCREENSHOT = "take screenshot";

        protected override void Initialize()
        {
            var triangleMesh = new Triangle(Window.Device);
            var cubeMesh = new Cube(Window.Device);

            mMaterial = new Material("shader.fx", Window.Device);
            mTriangleBinding = new MeshMaterialBinding(Window.Device, mMaterial, triangleMesh);
            mCubeBinding = new MeshMaterialBinding(Window.Device, mMaterial, cubeMesh);

            mKeyboard = new Keyboard();

            var stand = new Stand { Position = new Vector3(0, 10, 40) };
            stand.Direction = -stand.Position.Normalized();
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

            mSeekSteering = new SeekSteering(mSeekerKinetic, mRefugeeKinetic);
            mRefugeeSteering = new RefugeeSteering(mRefugeeKinetic, mSeekerKinetic);
        }

        protected override void OnFrame()
        {
            mKeyboard.Update();
            mInputCommandBinder.Update();
            mSeekerKinetic.Update(mSeekSteering, 3, Frametime);
            mRefugeeKinetic.Update(mRefugeeSteering, 2, Frametime);

            var world = Matrix.CreateTranslation(mSeekerKinetic.Position);
            mMaterial.SetWorldViewProjectionMatrix(world * mCamera.ViewProjectionMatrix);
            mCubeBinding.Draw();

            world = Matrix.CreateTranslation(mRefugeeKinetic.Position);
            mMaterial.SetWorldViewProjectionMatrix(world * mCamera.ViewProjectionMatrix);
            mTriangleBinding.Draw();
        }
    }

    class Kinetic
    {
        public Vector3 Position { get; private set; }
        private Vector3 mVelocity;

        public Kinetic(Vector3 initialPosition)
        {
            mVelocity = Vector3.Zero;
            Position = initialPosition;
        }

        public void Update(ISteering steering, float maxSpeed, float time)
        {
            Position += mVelocity * time;

            mVelocity += steering.GetLinearAcceleration() * time;

            if (mVelocity.Length > maxSpeed)
            {
                mVelocity = mVelocity.Normalized() * maxSpeed;
            }
        }
    }

    class SeekSteering : ISteering
    {
        private readonly Kinetic mCharacter;
        private readonly Kinetic mTarget;
        private const float MAX_ACCELERATION = 2;

        public SeekSteering(Kinetic character, Kinetic target)
        {
            mCharacter = character;
            mTarget = target;
        }

        public Vector3 GetLinearAcceleration()
        {
            var direction = mTarget.Position - mCharacter.Position;
            return direction.Normalized() * MAX_ACCELERATION;
        }
    }

    internal interface ISteering
    {
        Vector3 GetLinearAcceleration();
    }

    class RefugeeSteering : ISteering
    {
        private readonly Kinetic mCharacter;
        private readonly Kinetic mTarget;
        private const float MAX_ACCELERATION = 2;

        public RefugeeSteering(Kinetic character, Kinetic target)
        {
            mCharacter = character;
            mTarget = target;
        }

        public Vector3 GetLinearAcceleration()
        {
            var direction = mCharacter.Position - mTarget.Position;
            return direction.Normalized() * MAX_ACCELERATION;
        }
    }
}
