using System;
using System.Windows.Forms;
using SlimDX;
using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Device = SlimDX.Direct3D10.Device;
using Resource = SlimDX.Direct3D10.Resource;

namespace Graphics
{
    public class Window : IDisposable
    {
        private readonly int mWidth;
        private readonly int mHeight;
        private Form mForm;
        private RenderTargetView mRenderTarget;
        private SwapChain mSwapChain;
        public Device Device { get; private set; }

        public Window(int width = 1600, int height = 900)
        {
            mWidth = width;
            mHeight = height;
            InitializeDevice();
        }

        public void InitializeDevice()
        {
            mForm = new Form { Width = mWidth, Height = mHeight };

            using (var factory = new Factory())
            {
                Device = new Device(factory.GetAdapter(0), DriverType.Hardware, DeviceCreationFlags.None);

                mSwapChain = CreateSwapChain(factory);
            }

            using (var texture = Resource.FromSwapChain<Texture2D>(mSwapChain, 0))
            {
                mRenderTarget = new RenderTargetView(Device, texture);
            }

            var viewport = new Viewport
            {
                X = 0,
                Y = 0,
                Width = mWidth,
                Height = mHeight,
                MinZ = 0.0f,
                MaxZ = 1.0f
            };

            Device.Rasterizer.SetViewports(viewport);
            Device.OutputMerger.SetTargets(mRenderTarget);
        }

        private SwapChain CreateSwapChain(Factory factory)
        {
            var modeDescription = new ModeDescription(mWidth, mHeight, new Rational(60, 1), Format.R8G8B8A8_UNorm);
            var swapChainDescription = new SwapChainDescription
            {
                BufferCount = 2,
                IsWindowed = true,
                Flags = SwapChainFlags.None,
                ModeDescription = modeDescription,
                OutputHandle = mForm.Handle,
                SampleDescription = new SampleDescription(1, 0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };

            return new SwapChain(factory, Device, swapChainDescription);
        }

        public void Dispose()
        {
            mSwapChain.Dispose();
            mRenderTarget.Dispose();
            Device.Dispose();
            mForm.Dispose();
        }

        public void ClearRenderTarget()
        {
            Device.ClearRenderTargetView(mRenderTarget, new Color4(0, 0, 0));
        }

        public void Present()
        {
            mSwapChain.Present(0, PresentFlags.None);
        }

        public void Run()
        {
            Application.Run(mForm);
        }
    }
}
