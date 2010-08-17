using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Graphics;
using Graphics.Cameras;
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
    public class Program
    {
        static void Main()
        {
            using(var window = new Window())
            {
                var positions = CreatePositions();
                var colors = CreateColors();

                Buffer positionBuffer = CreateVertexBuffer(window.Device, positions);
                Buffer colorBuffer = CreateVertexBuffer(window.Device, colors);

                string errors;
                Effect effect = Effect.FromFile(window.Device, "shader.fx", "fx_4_0",
                    ShaderFlags.Debug, EffectFlags.None, null, null, out errors);
                Console.WriteLine(errors);

                var technique = effect.GetTechniqueByIndex(0);
                var pass = technique.GetPassByIndex(0);

                var inputElements = new[]
                {
                    new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                    new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 0, 1)
                };

                var inputLayout = new InputLayout(window.Device, pass.Description.Signature, inputElements);
                //var engine = IronRuby.Ruby.CreateEngine();

                var stand = new Stand { Position = new Vector3(0, 0, 3), Direction = -Vector3.ZAxis };
                var lens = new PerspectiveProjectionLens();
                var camera = new Camera(stand, lens);

                Application.Idle +=
                    delegate
                    {
                        window.ClearRenderTarget();

                        window.Device.InputAssembler.SetInputLayout(inputLayout);
                        window.Device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
                        window.Device.InputAssembler.SetVertexBuffers(0,
                            new VertexBufferBinding(positionBuffer, Marshal.SizeOf(typeof(Vector3)), 0));
                        window.Device.InputAssembler.SetVertexBuffers(1,
                            new VertexBufferBinding(colorBuffer, Marshal.SizeOf(typeof(Vector4)), 0));

                        stand.Position += Vector3.ZAxis;

                        effect.GetVariableBySemantic("WorldViewProjection")
                            .AsMatrix().SetMatrix(camera.ViewProjectionMatrix.ToSlimDX());

                        for (int actualPass = 0; actualPass < technique.Description.PassCount; ++actualPass)
                        {
                            pass.Apply();
                            window.Device.Draw(positions.Length, 0);
                        }

                        window.Present();
                        
                        Application.DoEvents();
                    };

                window.Run();
            }
        }

        static Vector4[] CreateColors()
        {
            var top = new Vector4(1f, 0f, 0f, 0f);
            var left = new Vector4(0f, 1f, 0f, 0f);
            var right = new Vector4(0f, 0f, 1f, 0f);

            return new[] {top, right, left};
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