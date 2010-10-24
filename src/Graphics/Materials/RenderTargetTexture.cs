using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Device = SlimDX.Direct3D10.Device;

namespace Graphics.Materials
{
    public class RenderTargetTexture : Texture
    {
        public RenderTargetTexture(Device device, int width, int height)
            : base(device, new Texture2D(device, CreateDescription(width, height)))
        {
        }

        private static Texture2DDescription CreateDescription(int width, int height)
        {
            return new Texture2DDescription
            {
                ArraySize = 1,
                BindFlags = BindFlags.RenderTarget | BindFlags.ShaderResource,
                CpuAccessFlags = CpuAccessFlags.None,
                Format = Format.R8G8B8A8_UNorm,
                Height = height,
                Width = width,
                MipLevels = 1,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                OptionFlags = ResourceOptionFlags.None
            };
        }

        internal Surface AsSurface()
        {
            return SlimDXTexture.AsSurface();
        }
    }
}