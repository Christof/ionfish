using SlimDX.Direct3D10;

namespace Graphics.Streams
{
    public class VertexStream<T> : Stream<T>
        where T : struct
    {
        private readonly int mSlot;
        private readonly VertexBufferBinding mVertexBufferBinding;

        public VertexStream(Device device, T[] data, int slot)
            : base(device, data, BindFlags.VertexBuffer)
        {
            mSlot = slot;
            mVertexBufferBinding = new VertexBufferBinding(Buffer, ElementSize, 0);
        }

        public override void OnFrame()
        {
            Device.InputAssembler.SetVertexBuffers(mSlot, mVertexBufferBinding);
        }
    }
}