using System;
using SlimDX.Direct3D10;

namespace Graphics.Materials
{
    public class Texture : IDisposable
    {
        protected readonly Texture2D SlimDXTexture;
        internal ShaderResourceView ShaderResourceView { get; private set; }

        public Texture(Device device, Texture2D texture)
        {
            SlimDXTexture = texture;
            ShaderResourceView = new ShaderResourceView(device, SlimDXTexture);
        }

        public Texture(Device device, string filename)
        {
            SlimDXTexture = Texture2D.FromFile(device, filename);
            ShaderResourceView = new ShaderResourceView(device, SlimDXTexture);
        }

        public void Dispose()
        {
            ShaderResourceView.Dispose();
            SlimDXTexture.Dispose();
        }
    }
}