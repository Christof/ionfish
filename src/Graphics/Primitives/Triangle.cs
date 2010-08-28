using Graphics.Streams;
using Math;
using SlimDX.Direct3D10;
using Mesh = Graphics.Streams.Mesh;

namespace Graphics.Primitives
{
    public class Triangle : Mesh
    {
        public Triangle(Device device)
            : base(device)
        {
        }

        static Vector4[] CreateColors()
        {
            var topLeft = new Vector4(1f, 0f, 0f, 0f);
            var topRight = new Vector4(0f, 1f, 0f, 0f);
            var bottomLeft = new Vector4(0f, 0f, 1f, 0f);

            return new[] { topLeft, topRight, bottomLeft };
        }

        static Vector3[] CreatePositions()
        {
            var left = new Vector3(-0.5f, -0.5f, 0f);
            var middle = new Vector3(0f, 0.5f, 0f);
            var right = new Vector3(0.5f, -0.5f, 0f);

            return new[] { left, middle, right };
        }

        private static uint[] CreateIndices()
        {
            return new uint[] { 0, 1, 2 };
        }

        public Mesh GetTriangle()
        {
            return CreateVertexStream(StreamUsage.Position, CreatePositions())
                .CreateVertexStream(StreamUsage.Color, CreateColors())
                .CreateIndexStream(CreateIndices());
        }
    }
}