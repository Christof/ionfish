using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Graphics;
using SlimDX;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Buffer = SlimDX.Direct3D10.Buffer;
using Device = SlimDX.Direct3D10.Device;

namespace Sandbox
{
    public class Program
    {
        private struct Vertex
        {
            public Vector3 Position;
            public Color4 Color;
        }

        static void Main()
        {
            using(var window = new Window())
            {
                Vertex[] vertices = CreateVertices();

                Buffer buffer = CreateBuffer(window.Device, vertices);

                string errors;
                Effect effect = Effect.FromFile(window.Device, "shader.fx", "fx_4_0",
                    ShaderFlags.Debug, EffectFlags.None, null, null, out errors);
                Console.WriteLine(errors);

                var technique = effect.GetTechniqueByIndex(0);
                var pass = technique.GetPassByIndex(0);

                var inputElements = new[]
                {
                    new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                    new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 12, 0)
                };

                var inputLayout = new InputLayout(window.Device, pass.Description.Signature, inputElements);

                //var engine = IronRuby.Ruby.CreateEngine();

                Application.Idle +=
                    delegate
                    {
                        window.ClearRenderTarget();

                        window.Device.InputAssembler.SetInputLayout(inputLayout);
                        window.Device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
                        window.Device.InputAssembler.SetVertexBuffers(0,
                            new VertexBufferBinding(buffer, Marshal.SizeOf(typeof(Vertex)), 0));

                        Matrix view = Matrix.LookAtRH(new Vector3(0, 0, 3), new Vector3(), new Vector3(0, 1, 0));
                        Matrix projection = Matrix.PerspectiveFovRH((float)(Math.PI / 3), 800f / 600.0f, 0.01f, 100f);
                        Matrix world = Matrix.Identity;
                        Matrix worldViewProjection = world * view * projection;

                        effect.GetVariableBySemantic("WorldViewProjection")
                            .AsMatrix().SetMatrix(worldViewProjection);

                        for (int actualPass = 0; actualPass < technique.Description.PassCount; ++actualPass)
                        {
                            pass.Apply();
                            window.Device.Draw(vertices.Length, 0);
                        }

                        window.Present();
                        
                        Application.DoEvents();
                    };

                window.Run();
            }
        }

        private static Buffer CreateBuffer(Device device, Vertex[] vertices)
        {
            var stream = new DataStream(vertices.Length * Marshal.SizeOf(typeof(Vertex)), true, true);

            foreach (var vertex in vertices)
            {
                stream.Write(vertex.Position);
                stream.Write(vertex.Color);
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
                SizeInBytes = (vertices.Length * Marshal.SizeOf(typeof(Vertex))),
                Usage = ResourceUsage.Default
            };

            var buffer = new Buffer(device, stream, bufferDescription);
            stream.Close();
            return buffer;
        }

        private static Vertex[] CreateVertices()
        {
            var top = new Vertex { Position = new Vector3(0f, 1f, 0f), Color = new Color4(01f, 0f, 0f) };
            var left = new Vertex { Position = new Vector3(-1f, -1f, 0f), Color = new Color4(0f, 1f, 0f) };
            var right = new Vertex { Position = new Vector3(1f, -1f, 0f), Color = new Color4(0f, 0f, 1f) };

            return new[] { top, right, left };
        }
    }
}