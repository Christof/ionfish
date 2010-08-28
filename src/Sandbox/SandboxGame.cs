using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Graphics.Materials;
using Graphics.Primitives;
using Graphics.Streams;
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
        private MeshMaterialBinding mBinding;
        private MeshMaterialBinding mTriangleBinding;
        private Vector3RandomGenerator mVector3Random;
        private Vector3[] mPositions;

        private const string MOVE_FORWARD = "move forward";
        private const string MOVE_BACKWARD = "move backward";
        private const string STRAFE_LEFT = "strafe left";
        private const string STRAFE_RIGHT = "strafe right";
        private const string ESCAPE = "escape";

        protected override void Initialize()
        {
            var quadMesh = new Quad(Window.Device);
            var triangleMesh = new Triangle(Window.Device);

            mMaterial = new Material("shader.fx", Window.Device);
            mBinding = new MeshMaterialBinding(Window.Device, mMaterial, quadMesh.GetQuad());
            mTriangleBinding = new MeshMaterialBinding(Window.Device, mMaterial, triangleMesh.GetTriangle());

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
                //mBinding.Draw();
                mTriangleBinding.Draw();
            }
        }
    }
}
