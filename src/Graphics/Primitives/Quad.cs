using Graphics.Streams;
using Math;
using SlimDX.Direct3D10;
using Mesh = Graphics.Streams.Mesh;

namespace Graphics.Primitives
{
    public class Quad : Mesh
    {
        public Quad(Device device)
            : base(device)
        {
            CreateVertexStream(StreamUsage.Position, CreatePositions());
            CreateVertexStream(StreamUsage.Color, CreateColors());
            CreateIndexStream(CreateIndices());
        }

        private static Vector4[] CreateColors()
        {
            var topLeft = new Vector4(1f, 0f, 0f, 0f);
            var topRight = new Vector4(0f, 1f, 0f, 0f);
            var bottomLeft = new Vector4(0f, 0f, 1f, 0f);
            var bottomRight = new Vector4(0.5f, 0.5f, 0.5f, 0f);

            return new[] { topLeft, topRight, bottomLeft, bottomRight };
        }

        private static Vector3[] CreatePositions()
        {
            var bottomLeft = new Vector3(-0.5f, -0.5f, 0f);
            var topLeft = new Vector3(-0.5f, 0.5f, 0f);
            var bottomRight = new Vector3(0.5f, -0.5f, 0f);
            var topRight = new Vector3(0.5f, 0.5f, 0f);
            
            return new[] { bottomLeft, topLeft, bottomRight, topRight};
        }

        private static uint[] CreateIndices()
        {
            return new uint[] { 0, 1, 3, 0, 3, 2 };
        }
    }
}