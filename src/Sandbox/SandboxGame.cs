using System;
using System.Runtime.InteropServices;
using Core.Commands;
using Graphics;
using Graphics.Cameras;
using Input;
using SlimDX;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Buffer = SlimDX.Direct3D10.Buffer;
using Device = SlimDX.Direct3D10.Device;
using Vector3 = Math.Vector3;
using Vector4 = Math.Vector4;

namespace Sandbox
{
    public class SandboxGame : Game
    {
        private Keyboard mKeyboard;
        private InputCommandBinder mInputCommandBinder;
        private InputLayout mInputLayout;
        private Buffer mPositionBuffer;
        private Buffer mColorBuffer;
        private Camera mCamera;
        private Effect mEffect;
        private Vector3[] mPositions;

        private const string MOVE_FORWARD = "move forward";
        private const string MOVE_BACKWARD = "move backward";
        private const string ESCAPE = "escape";

        protected override void Initialize()
        {
            mPositions = CreatePositions();
            var colors = CreateColors();

            mPositionBuffer = CreateVertexBuffer(Window.Device, mPositions);
            mColorBuffer = CreateVertexBuffer(Window.Device, colors);

            string errors;
            mEffect = Effect.FromFile(Window.Device, "shader.fx", "fx_4_0",
                ShaderFlags.Debug, EffectFlags.None, null, null, out errors);
            Console.WriteLine(errors);

            mKeyboard = new Keyboard();

            var stand = new Stand { Position = new Vector3(0, 0, 3), Direction = -Vector3.ZAxis };
            var lens = new PerspectiveProjectionLens();
            mCamera = new Camera(stand, lens);

            var commands = new CommandManager();
            commands.Add(MOVE_FORWARD, () => stand.Position += Vector3.ZAxis);
            commands.Add(MOVE_BACKWARD, () => stand.Position -= Vector3.ZAxis);
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
            Window.Device.InputAssembler.SetVertexBuffers(0,
                new VertexBufferBinding(mPositionBuffer, Marshal.SizeOf(typeof(Vector3)), 0));
            Window.Device.InputAssembler.SetVertexBuffers(1,
                new VertexBufferBinding(mColorBuffer, Marshal.SizeOf(typeof(Vector4)), 0));

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
                Window.Device.Draw(mPositions.Length, 0);
            }
        }

        static Vector4[] CreateColors()
        {
            var top = new Vector4(1f, 0f, 0f, 0f);
            var left = new Vector4(0f, 1f, 0f, 0f);
            var right = new Vector4(0f, 0f, 1f, 0f);

            return new[] { top, right, left };
        }

        static Vector3[] CreatePositions()
        {
            var top = new Vector3(0f, 1f, 0f);
            var left = new Vector3(-1f, -1f, 0f);
            var right = new Vector3(1f, -1f, 0f);

            return new[] { top, right, left };
        }

        private static Buffer CreateVertexBuffer<T>(Device device, T[] data)
            where T : struct
        {
            int sizeInBytes = data.Length * Marshal.SizeOf(typeof(T));
            var stream = new DataStream(sizeInBytes, canRead: true, canWrite: true);

            foreach (var element in data)
            {
                stream.Write(element);
            }

            // Important: when specifying initial buffer data like this, the buffer will
            // read from the current DataStream position; we must rewind the stream to 
            // the start of the data we just wrote.
            stream.Position = 0;

            var bufferDescription = new BufferDescription
            {
                BindFlags = BindFlags.VertexBuffer,
                CpuAccessFlags = CpuAccessFlags.None,
                OptionFlags = ResourceOptionFlags.None,
                SizeInBytes = sizeInBytes,
                Usage = ResourceUsage.Default
            };

            var buffer = new Buffer(device, stream, bufferDescription);
            stream.Dispose();

            return buffer;
        }
    }
}