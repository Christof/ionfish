using System;
using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Graphics.Streams;
using Input;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Vector3 = Math.Vector3;
using Vector4 = Math.Vector4;

namespace Sandbox
{
    public class SandboxGame : Game
    {
        private Keyboard mKeyboard;
        private InputCommandBinder mInputCommandBinder;
        private InputLayout mInputLayout;
        private Stream<Vector3> mPositionStream;
        private Stream<Vector4> mColorStream;
        private IndexStream mIndexStream;
        private Camera mCamera;
        private Effect mEffect;
        private uint[] mIndices;

        private const string MOVE_FORWARD = "move forward";
        private const string MOVE_BACKWARD = "move backward";
        private const string ESCAPE = "escape";

        protected override void Initialize()
        {
            mPositionStream = new VertexStream<Vector3>(Window.Device, CreatePositions(), 0);
            mColorStream = new VertexStream<Vector4>(Window.Device, CreateColors(), 1);
            mIndices = CreateIndices();
            mIndexStream = new IndexStream(Window.Device, mIndices);
            
            string errors;
            mEffect = Effect.FromFile(Window.Device, "shader.fx", "fx_4_0",
                ShaderFlags.Debug, EffectFlags.None, null, null, out errors);
            Console.WriteLine(errors);

            mKeyboard = new Keyboard();

            var stand = new Stand { Position = new Vector3(0, 0, 3), Direction = -Vector3.ZAxis };
            var lens = new PerspectiveProjectionLens();
            mCamera = new Camera(stand, lens);

            var commands = new CommandManager();
            commands.Add(MOVE_FORWARD, () => stand.Position += Vector3.ZAxis * Frametime);
            commands.Add(MOVE_BACKWARD, () => stand.Position -= Vector3.ZAxis * Frametime);
            commands.Add(ESCAPE, Exit);

            mInputCommandBinder = new InputCommandBinder(commands, mKeyboard);
            mInputCommandBinder.Bind(Button.W, MOVE_FORWARD);
            mInputCommandBinder.Bind(Button.S, MOVE_BACKWARD);
            mInputCommandBinder.Bind(Button.Escape, ESCAPE);
        }

        protected override void OnFrame()
        {
            Window.Device.InputAssembler.SetInputLayout(mInputLayout);
            Window.Device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
            mPositionStream.OnFrame();
            mColorStream.OnFrame();
            mIndexStream.OnFrame();

            mKeyboard.Update();
            mInputCommandBinder.Update();

            mEffect.GetVariableBySemantic("WorldViewProjection")
                .AsMatrix().SetMatrix(mCamera.ViewProjectionMatrix.ToSlimDX());

            var technique = mEffect.GetTechniqueByIndex(0);
            var pass = technique.GetPassByIndex(0);

            var inputElements = new[]
            {
                new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 0, 1)
            };

            mInputLayout = new InputLayout(Window.Device, pass.Description.Signature, inputElements);

            for (int actualPass = 0; actualPass < technique.Description.PassCount; ++actualPass)
            {
                pass.Apply();
                Window.Device.DrawIndexed(mIndices.Length, 0, 0);
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
