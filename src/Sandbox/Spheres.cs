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
        private MeshMaterialBinding mColorfulCubeBinding;
        private MeshMaterialBinding mRedCubeBinding;
        private MeshMaterialBinding mBlueCubeBinding;
        private readonly Player mPlayer = new Player();
        private const int TARGETS_COUNT = 4;
        private readonly Kinematic[] mTargetsKinematic = new Kinematic[TARGETS_COUNT];
        private RefugeeSteering[] mTargetsSteering = new RefugeeSteering[TARGETS_COUNT];
        private const int ENEMYS_COUNT = 4;
        private readonly Kinematic[] mEnemysKinematic = new Kinematic[ENEMYS_COUNT];
        private readonly ArrivingSteering[] mEnemysSteering = new ArrivingSteering[ENEMYS_COUNT];
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
            mColorfulCubeBinding = new MeshMaterialBinding(Window.Device, mMaterial, new CubeWithNormals(Window.Device));
            mRedCubeBinding = new MeshMaterialBinding(Window.Device, mMaterial, new CubeWithNormals(Window.Device, new Vector4(0.8f, 0, 0, 0)));
            mBlueCubeBinding = new MeshMaterialBinding(Window.Device, mMaterial, new CubeWithNormals(Window.Device, new Vector4(0, 0, 0.8f, 0)));

            mEnemysKinematic[0] = new Kinematic(new Vector3(2, 0, -2), 0.6f);
            mEnemysKinematic[1] = new Kinematic(new Vector3(2, 0, 2), 0.7f);
            mEnemysKinematic[2] = new Kinematic(new Vector3(1, 0, -1), 0.9f);
            mEnemysKinematic[3] = new Kinematic(new Vector3(1, 0, 1), 0.5f);
            mTargetsKinematic[0] = new Kinematic(new Vector3(-2, 0, -2), 0.5f);
            mTargetsKinematic[1] = new Kinematic(new Vector3(-2, 0, 2), 0.6f);
            mTargetsKinematic[2] = new Kinematic(new Vector3(-2, 0, -1), 0.7f);
            mTargetsKinematic[3] = new Kinematic(new Vector3(-2, 0, 1), 0.8f);

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

            mSphereCorrection = Matrix.CreateTranslation(new Vector3(0, 0.1f, 0));

            for (int i = 0; i < TARGETS_COUNT; i++)
            {
                mTargetsSteering[i] = new RefugeeSteering(mTargetsKinematic[i], mPlayer, 0.7f);
            }

            for (int i = 0; i < ENEMYS_COUNT; i++)
            {
                mEnemysSteering[i] = new ArrivingSteering(mEnemysKinematic[i], mPlayer, maxAcceleration: 0.7f, slowRadius: 0.8f, satisfactionRadius: 0.2f);
            }
        }

        protected override void OnFrame()
        {
            mKeyboard.Update();
            mInputCommandBinder.Update();
            var stand = mCamera.Stand;
            stand.Position = mPlayer.Position + new Vector3(0, 1, 4);
            stand.Direction = (mPlayer.Position + new Vector3(0, 0.5f, 0) - stand.Position).Normalized();

            for (int i = 0; i < TARGETS_COUNT; i++)
            {
                mTargetsKinematic[i].Update(mTargetsSteering[i], Frametime);
            }

            for (int i = 0; i < ENEMYS_COUNT; i++)
            {
                mEnemysKinematic[i].Update(mEnemysSteering[i], Frametime);
            }

            RenderSphere();
            RenderCube();
            RenderNpcs();

            RenderGround();
        }

        private void RenderNpcs()
        {
            for (int i = 0; i < TARGETS_COUNT; i++)
            {
                DrawTarget(mTargetsKinematic[i]);
            }

            for (int i = 0; i < ENEMYS_COUNT; i++)
            {
                DrawEnemy(mEnemysKinematic[i]);
            }
       }

        void DrawTarget(Kinematic target)
        {
            var world = Matrix.CreateTranslation(target.Position) * Matrix.CreateTranslation(new Vector3(0, 0.1f, 0)) * Matrix.Scale(0.2f);
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mBlueCubeBinding.Draw();
        }

        void DrawEnemy(Kinematic enemy)
        {
            Matrix world;
            world = Matrix.CreateTranslation(enemy.Position) * Matrix.CreateTranslation(new Vector3(0, 0.1f, 0)) * Matrix.Scale(0.2f);
            mMaterial.SetWorldViewProjectionMatrix(mCamera.ViewProjectionMatrix * world);
            mRedCubeBinding.Draw();
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

            mColorfulCubeBinding.Draw();
        }
    }
}
