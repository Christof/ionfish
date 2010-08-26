using SlimDX.Direct3D10;
using SlimDX.DXGI;
using Device = SlimDX.Direct3D10.Device;

namespace Graphics.Streams
{
    public class IndexStream : Stream<uint>
    {
        public IndexStream(Device device, uint[] data)
            : base(device, data, BindFlags.IndexBuffer)
        {
        }

        public override void OnFrame()
        {
            Device.InputAssembler.SetIndexBuffer(Buffer, Format.R32_UInt, 0);
        }
    }
}