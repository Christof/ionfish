using System;
using System.Collections.Generic;
using SlimDX.Direct3D10;

namespace Graphics.Streams
{
    public class Mesh : IDisposable
    {
        private readonly Device mDevice;
        private readonly List<IStream> mStreams = new List<IStream>();
        private readonly List<InputElement> mInputElements = new List<InputElement>();
        private int mSlot;
        private int mIndexCount;

        public Mesh(Device device)
        {
            mDevice = device;
        }

        public Mesh CreateVertexStream<T>(StreamUsage streamUsage, T[] data)
            where T : struct
        {
            mStreams.Add(new VertexStream<T>(mDevice, data, mSlot));
            mInputElements.Add(new InputElement(streamUsage.ToInputElementName(), 0, FormatHelper.GetFormatForType<T>(), 0, mSlot));

            mSlot++;

            return this;
        }

        public Mesh CreateIndexStream(uint[] data)
        {
            mIndexCount = data.Length;
            mStreams.Add(new IndexStream(mDevice, data));

            return this;
        }

        public InputElement[] GetInputElements()
        {
            return mInputElements.ToArray();
        }

        public void OnFrame()
        {
            foreach (var stream in mStreams)
            {
                stream.OnFrame();
            }
        }

        public void Draw()
        {
            mDevice.DrawIndexed(mIndexCount, 0, 0);
        }

        public void Dispose()
        {
            foreach (var stream in mStreams)
            {
                stream.Dispose();
            }
        }
    }
}