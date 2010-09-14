using Core.Helper;
using Graphics.Streams;
using Math;
using SlimDX.Direct3D10;
using Mesh = Graphics.Streams.Mesh;

namespace Graphics.Primitives
{
    public class Quad : Mesh
    {
        public Quad(Device device, Vector4 color)
            : base(device)
        {
            CreateVertexStream(StreamUsage.Position, CreatePositions());
            CreateVertexStream(StreamUsage.Color, ArrayHelper.Create(4, color));
            CreateVertexStream(StreamUsage.Normal, CreateNormals());
            CreateIndexStream(CreateIndices());
        }

        public Quad(Device device)
            : base(device)
        {
            CreateVertexStream(StreamUsage.Position, CreatePositions());
            CreateVertexStream(StreamUsage.Color, CreateColors());
            CreateVertexStream(StreamUsage.Normal, CreateNormals());
            CreateIndexStream(CreateIndices());
        }

        private static Vector4[] CreateColors()
        {
            var bottomLeft = new Vector4(0f, 0f, 1f, 0f);
            var topLeft = new Vector4(1f, 0f, 0f, 0f);
            var bottomRight = new Vector4(0.5f, 0.5f, 0.5f, 0f);
            var topRight = new Vector4(0f, 1f, 0f, 0f);

            return new[] { bottomLeft, topLeft, bottomRight, topRight };
        }

        private static Vector3[] CreatePositions()
        {
            var bottomLeft = new Vector3(-0.5f, -0.5f, 0f);
            var topLeft = new Vector3(-0.5f, 0.5f, 0f);
            var bottomRight = new Vector3(0.5f, -0.5f, 0f);
            var topRight = new Vector3(0.5f, 0.5f, 0f);
            
            return new[] { bottomLeft, topLeft, bottomRight, topRight };
        }

        private static Vector3[] CreateNormals()
        {
            var bottomLeft = Vector3.XAxis;
            var topLeft = Vector3.XAxis;
            var bottomRight = Vector3.XAxis;
            var topRight = Vector3.XAxis;

            return new[] { bottomLeft, topLeft, bottomRight, topRight };
        }

        private static uint[] CreateIndices()
        {
            return new uint[] { 0, 1, 3, 0, 3, 2 };
        }
    }
}