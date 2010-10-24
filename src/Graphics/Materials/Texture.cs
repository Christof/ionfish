using System;
using SlimDX.Direct3D10;

namespace Graphics.Materials
{
    public class Texture : IDisposable
    {
        private readonly Texture2D mTexture;
        internal ShaderResourceView ShaderResourceView { get; private set; }

        public Texture(Device device, Texture2D texture)
        {
            mTexture = texture;
            ShaderResourceView = new ShaderResourceView(device, mTexture);
        }

        public Texture(Device device, string filename)
        {
            mTexture = Texture2D.FromFile(device, filename);
            ShaderResourceView = new ShaderResourceView(device, mTexture);
        }

        public void Dispose()
        {
            ShaderResourceView.Dispose();
            mTexture.Dispose();
        }
    }
}