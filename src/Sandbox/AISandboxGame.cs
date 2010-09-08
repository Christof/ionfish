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
        private MeshMaterialBinding mRefugeeBinding;
        private MeshMaterialBinding mSeekerBinding;
        private MeshMaterialBinding mGroundBinding;
        private readonly Kinetic mRefugeeKinetic = new Kinetic(new Vector3(-5, 0, -5));
        private readonly Kinetic mSeekerKinetic = new Kinetic(new Vector3(5, 0, 5), new Vector3(0, 0, -10));
        private SeekSteering mSeekSteering;
        private RefugeeSteering mRefugeeSteering;

        private const string MOVE_FORWARD = "move forward";
        private const string MOVE_BACKWARD = "move backward";
        private const string STRAFE_LEFT = "strafe left";
        private const string STRAFE_RIGHT = "strafe right";
        private const string ESCAPE = "escape";
        private const string TAKE_SCREENSHOT = "take screenshot";
        private const string UP = "up";
        private const string DOWN = "down";

        protected override void Initialize()
        {
            mMaterial = new Material("shader.fx", Window.Device);
            mRefugeeBinding = new MeshMaterialBinding(Window.Device, mMaterial, new Cube(Window.Device, new Vector4(0.8f, 0, 0, 0)));
            mSeekerBinding = new MeshMaterialBinding(Window.Device, mMaterial, new Cube(Window.Device, new Vector4(0, 0, 0.8f, 0)));
            mGroundBinding = new MeshMaterialBinding(Window.Device, mMaterial, new Quad(Window.Device, new Vector4(0.2f, 0.2f, 0.2f, 0)));

            mKeyboard = new Keyboard();

            var stand = new Stand { Position = new Vector3(0, 10, 40) };
            //stand.Direction = -stand.Position.Normalized();
            stand.Position = new Vector3(0, 140, 0);
            stand.Direction = -Vector3.YAxis;
            stand.Up = -Vector3.ZAxis;

            var lens = new PerspectiveProjectionLens();
            mCamera = new Camera(stand, lens);

            var commands = new CommandManager();
            commands.Add(MOVE_BACKWARD, () => stand.Position += Vector3.ZAxis * Frametime);
            commands.Add(MOVE_FORWARD, () => stand.Position -= Vector3.ZAxis * Frametime);
            commands.Add(STRAFE_RIGHT, () => stand.Position += Vector3.XAxis * Frametime);
            commands.Add(STRAFE_LEFT, () => stand.Position -= Vector3.XAxis * Frametime);
            commands.Add(ESCAPE, Exit);
            commands.Add(TAKE_SCREENSHOT, Window.TakeScreenshot);
            commands.Add(UP, () => stand.Position += Vector3.YAxis * Frametime);
            commands.Add(DOWN, () => stand.Position -= Vector3.YAxis * Frametime);

            mInputCommandBinder = new InputCommandBinder(commands, mKeyboard);
            mInputCommandBinder.Bind(Button.W, MOVE_FORWARD);
            mInputCommandBinder.Bind(Button.S, MOVE_BACKWARD);
            mInputCommandBinder.Bind(Button.D, STRAFE_RIGHT);
            mInputCommandBinder.Bind(Button.A, STRAFE_LEFT);
            mInputCommandBinder.Bind(Button.Escape, ESCAPE);
            mInputCommandBinder.Bind(Button.PrintScreen, TAKE_SCREENSHOT);
            mInputCommandBinder.Bind(Button.R, UP);
            mInputCommandBinder.Bind(Button.F, DOWN);

            mSeekSteering = new SeekSteering(mSeekerKinetic, mRefugeeKinetic, 15);
            mRefugeeSteering = new RefugeeSteering(mRefugeeKinetic, mSeekerKinetic, 15);
        }

        protected override void OnFrame()
        {
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

    class Kinetic
    {
        public Vector3 Position { get; private set; }
        private Vector3 mVelocity;

        public Kinetic(Vector3 initialPosition, Vector3 initialVelocity)
        {
            mVelocity = initialVelocity;
            Position = initialPosition;
        }

        public Kinetic(Vector3 initialPosition)
            : this(initialPosition, Vector3.Zero)
        {
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
        private readonly float mMaxAcceleration;

        public SeekSteering(Kinetic character, Kinetic target, float maxAcceleration)
        {
            mCharacter = character;
            mTarget = target;
            mMaxAcceleration = maxAcceleration;
        }

        public Vector3 GetLinearAcceleration()
        {
            var direction = mTarget.Position - mCharacter.Position;
            return direction.Normalized() * mMaxAcceleration;
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
        private readonly float mMaxAcceleration;

        public RefugeeSteering(Kinetic character, Kinetic target, float maxAcceleration)
        {
            mCharacter = character;
            mTarget = target;
            mMaxAcceleration = maxAcceleration;
        }

        public Vector3 GetLinearAcceleration()
        {
            var direction = mCharacter.Position - mTarget.Position;
            return direction.Normalized() * mMaxAcceleration;
        }
    }
}
