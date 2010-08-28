using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Graphics.Materials;
using Graphics.Streams;
using Input;
using Math;

namespace Sandbox
{
    public class SandboxGame : Game
    {
        private Keyboard mKeyboard;
        private InputCommandBinder mInputCommandBinder;
        private Mesh mMesh;
        private Camera mCamera;
        private Material mMaterial;
        private MeshMaterialBinding mBinding;
        private Vector3RandomGenerator mVector3Random;
        private Vector3[] mPositions;

        private const string MOVE_FORWARD = "move forward";
        private const string MOVE_BACKWARD = "move backward";
        private const string STRAFE_LEFT = "strafe left";
        private const string STRAFE_RIGHT = "strafe right";
        private const string ESCAPE = "escape";

        protected override void Initialize()
        {
            mMesh = new Mesh(Window.Device)
                .CreateVertexStream(StreamUsage.Position, CreatePositions())
                .CreateVertexStream(StreamUsage.Color, CreateColors())
                .CreateIndexStream(CreateIndices());
            
            mMaterial = new Material("shader.fx", Window.Device);
            mBinding = new MeshMaterialBinding(Window.Device, mMaterial, mMesh);

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

            mInputCommandBinder = new InputCommandBinder(commands, mKeyboard);
            mInputCommandBinder.Bind(Button.W, MOVE_FORWARD);
            mInputCommandBinder.Bind(Button.S, MOVE_BACKWARD);
            mInputCommandBinder.Bind(Button.D, STRAFE_RIGHT);
            mInputCommandBinder.Bind(Button.A, STRAFE_LEFT);
            mInputCommandBinder.Bind(Button.Escape, ESCAPE);

            mVector3Random = new Vector3RandomGenerator(new Vector3(-2, -1, -2), new Vector3(2, 1, 2), 1);

            mPositions = new[]
            {
                mVector3Random.GetNextRandomFloat(),
                mVector3Random.GetNextRandomFloat(),
                mVector3Random.GetNextRandomFloat()
            };
        }

        protected override void OnFrame()
        {
            mKeyboard.Update();
            mInputCommandBinder.Update();

            foreach (var position in mPositions)
            {
                var world = Matrix.CreateTranslation(position);
                mMaterial.SetWorldViewProjectionMatrix(world * mCamera.ViewProjectionMatrix);
                mBinding.Draw();
            }
        }

        static Vector4[] CreateColors()
        {
            var topLeft = new Vector4(1f, 0f, 0f, 0f);
            var topRight = new Vector4(0f, 1f, 0f, 0f);
            var bottomLeft = new Vector4(0f, 0f, 1f, 0f);
            var bottomRight = new Vector4(0.5f, 0.5f, 0.5f, 0f);

            return new[] { topLeft, topRight, bottomLeft, bottomRight };
        }

        static Vector3[] CreatePositions()
        {
            var topLeft = new Vector3(-0.5f, 0.5f, 0f);
            var topRight = new Vector3(0.5f, 0.5f, 0f);
            var bottomLeft = new Vector3(-0.5f, -0.5f, 0f);
            var bottomRight = new Vector3(0.5f, -0.5f, 0f);

            return new[] { topLeft, topRight, bottomLeft, bottomRight };
        }

        private static uint[] CreateIndices()
        {
            return new uint[] { 0, 1, 3, 0, 3, 2 };
        }
    }
}
