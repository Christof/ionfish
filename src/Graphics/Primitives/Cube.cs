using Graphics.Streams;
using Math;
using SlimDX.Direct3D10;
using Mesh = Graphics.Streams.Mesh;

namespace Graphics.Primitives
{
    public class Cube : Mesh
    {
        public Cube(Device device)
            : base(device)
        {
            CreateVertexStream(StreamUsage.Position, CreatePositions());
            CreateVertexStream(StreamUsage.Color, CreateColors());
            CreateIndexStream(CreateIndices());
        }

        private static Vector4[] CreateColors()
        {
            var frontbottomLeft = new Vector4(1f, 0f, 0f, 0f);
            var fronttopLeft = new Vector4(0f, 1f, 0f, 0f);
            var frontbottomRight = new Vector4(0f, 0f, 1f, 0f);
            var fronttopRight = new Vector4(0.5f, 0.5f, 0.5f, 0f);

            var backbottomLeft = new Vector4(1f, 0f, 0f, 0f);
            var backtopLeft = new Vector4(0f, 1f, 0f, 0f);
            var backbottomRight = new Vector4(0f, 0f, 1f, 0f);
            var backtopRight = new Vector4(0.5f, 0.5f, 0.5f, 0f);

            return new[] { frontbottomLeft, fronttopLeft, frontbottomRight, fronttopRight, backbottomRight, backtopRight, backbottomLeft, backtopLeft };
        }

        private static Vector3[] CreatePositions()
        {
            var frontbottomLeft = new Vector3(-0.5f, -0.5f, 0f);
            var fronttopLeft = new Vector3(-0.5f, 0.5f, 0f);
            var frontbottomRight = new Vector3(0.5f, -0.5f, 0f);
            var fronttopRight = new Vector3(0.5f, 0.5f, 0f);

            var backbottomLeft = new Vector3(-0.5f, -0.5f, 0.5f);
            var backtopLeft = new Vector3(-0.5f, 0.5f, 0.5f);
            var backbottomRight = new Vector3(0.5f, -0.5f, 0.5f);
            var backtopRight = new Vector3(0.5f, 0.5f, 0.5f);

            return new[] { frontbottomLeft, fronttopLeft, frontbottomRight, fronttopRight, backbottomRight, backtopRight, backbottomLeft, backtopLeft };
        }

        private static uint[] CreateIndices()
        {
            return new uint[] { 0, 1, 3, 0, 3, 2, 2, 3, 5, 2, 5, 4, 4, 5, 7, 4, 7, 6, 6, 7, 1, 6, 1, 0, 0, 2, 4, 0, 4, 6, 1, 7, 5, 1, 5, 3 };
        }
    }
}