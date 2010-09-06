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
        private DepthStencilView mDepthStencilView;
        private DepthStencilState mDepthStencilState;
        private bool mTakeScreenshotInCurrentFrame;
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
            mForm.Closing += (sender, args) => IsClosing = true;

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

            CreateDepthBuffer();
            CreateStencilState();

            Device.Rasterizer.SetViewports(viewport);
            Device.OutputMerger.SetTargets(mDepthStencilView, mRenderTarget);
        }

        private void CreateStencilState()
        {
            var depthStencilStateDescription = new DepthStencilStateDescription
            {
                IsDepthEnabled = true,
                IsStencilEnabled = false,
                DepthWriteMask = DepthWriteMask.All,
                DepthComparison = Comparison.Less
            };

            mDepthStencilState = DepthStencilState.FromDescription(Device, depthStencilStateDescription);
            Device.OutputMerger.DepthStencilState = mDepthStencilState;
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

        private void CreateDepthBuffer()
        {
            var textureDescription = new Texture2DDescription
            {
                ArraySize = 1,
                BindFlags = BindFlags.DepthStencil,
                CpuAccessFlags = CpuAccessFlags.None,
                Format = Format.D32_Float,
                Height = mHeight,
                Width = mWidth,
                MipLevels = 1,
                OptionFlags = ResourceOptionFlags.None,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default
            };

            using (var depthBuffer = new Texture2D(Device, textureDescription))
            {
                mDepthStencilView = new DepthStencilView(Device, depthBuffer);
            }
        }

        public void Dispose()
        {
            mDepthStencilState.Dispose();
            mSwapChain.Dispose();
            mRenderTarget.Dispose();
            mDepthStencilView.Dispose();
            Device.Dispose();
            mForm.Dispose();
        }

        public void ClearRenderTarget()
        {
            Device.ClearRenderTargetView(mRenderTarget, new Color4(0, 0, 0));
            Device.ClearDepthStencilView(mDepthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
        }

        public void Present()
        {
            TakeScreenshotIfRequired();
            mSwapChain.Present(0, PresentFlags.None);
        }
        
        public void TakeScreenshot()
        {
            mTakeScreenshotInCurrentFrame = true;
        }

        public void TakeScreenshotIfRequired()
        {
            if (mTakeScreenshotInCurrentFrame == false)
            {
                return;
            }

            using (var texture = Resource.FromSwapChain<Texture2D>(mSwapChain, 0))
            {
                Texture2D.ToFile(texture, ImageFileFormat.Png, string.Format("Screenshot_{0:yyyy}_{0:MM}_{0:dd}_{0:HH}_{0:mm}_{0:ss}.png", DateTime.Now));
            }

            mTakeScreenshotInCurrentFrame = false;
        }

        public void SetCaption(string text)
        {
            mForm.Text = text;
        }

        public void Run()
        {
            Application.Run(mForm);
        }

        public bool IsClosing { get; private set; }
    }
}
