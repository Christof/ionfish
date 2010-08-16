using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SlimDX;
using SlimDX.D3DCompiler;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Buffer = SlimDX.Direct3D10.Buffer;
using Device = SlimDX.Direct3D10.Device;
using Resource = SlimDX.Direct3D10.Resource;

namespace Sandbox
{
    public class Program
    {
        private struct Vertex
        {
            public Vector3 Position;
            public Color4 Color;
        }

        static void Main(string[] args)
        {
            using (var form = new Form { Width = 800, Height = 600 })
            {
                var factory = new Factory();
                var device = new Device(factory.GetAdapter(0), DriverType.Hardware, DeviceCreationFlags.None);

                SwapChain swapChain = CreateSwapChain(factory, form, device);

                RenderTargetView renderTarget;
                using (var texture = Resource.FromSwapChain<Texture2D>(swapChain, 0))
                {
                    renderTarget = new RenderTargetView(device, texture);
                }

                var viewport = new Viewport
                {
                    X = 0,
                    Y = 0,
                    Width = 800,
                    Height = 600,
                    MinZ = 0.0f,
                    MaxZ = 1.0f
                };

                device.Rasterizer.SetViewports(viewport);
                device.OutputMerger.SetTargets(renderTarget);

                Vertex[] vertices = CreateVertices();

                Buffer buffer = CreateBuffer(device, vertices);

                string errors = string.Empty;
                Effect effect = Effect.FromFile(device, "shader.fx", "fx_4_0",
                    ShaderFlags.Debug, EffectFlags.None, null, null, out errors);
                Console.WriteLine(errors);

                var technique = effect.GetTechniqueByIndex(0);
                var pass = technique.GetPassByIndex(0);

                var inputElements = new[]
                {
                    new InputElement("POSITION", 0, Format.R32G32B32_Float, 0, 0),
                    new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 12, 0)
                };

                var inputLayout = new InputLayout(device, pass.Description.Signature, inputElements);

                //var engine = IronRuby.Ruby.CreateEngine();

                Application.Idle +=
                    delegate
                    {
                        device.ClearRenderTargetView(renderTarget, new Color4(0, 0, 0));

                        device.InputAssembler.SetInputLayout(inputLayout);
                        device.InputAssembler.SetPrimitiveTopology(PrimitiveTopology.TriangleList);
                        device.InputAssembler.SetVertexBuffers(0,
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
                            device.Draw(vertices.Length, 0);
                        }

                        swapChain.Present(0, PresentFlags.None);

                        Application.DoEvents();
                    };

                Application.Run(form);
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

        private static SwapChain CreateSwapChain(Factory factory, Form form, Device device)
        {
            var modeDescription = new ModeDescription(800, 600, new Rational(60, 1), Format.R8G8B8A8_UNorm);
            var swapChainDescription = new SwapChainDescription
            {
                BufferCount = 2,
                IsWindowed = true,
                Flags = SwapChainFlags.None,
                ModeDescription = modeDescription,
                OutputHandle = form.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            return new SwapChain(factory, device, swapChainDescription);
        }
    }
}