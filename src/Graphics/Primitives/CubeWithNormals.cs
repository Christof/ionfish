using Core.Helper;
using Graphics.Streams;
using Math;
using SlimDX.Direct3D10;
using Mesh = Graphics.Streams.Mesh;

namespace Graphics.Primitives
{
    public class CubeWithNormals : Mesh
    {
        public CubeWithNormals(Device device, Vector4 color)
            : base(device)
        {
            CreateVertexStream(StreamUsage.Position, CreatePositions());
            CreateVertexStream(StreamUsage.Color, ArrayHelper.Create(8, color));
            CreateVertexStream(StreamUsage.Normal, CreateNormals());
            CreateIndexStream(CreateIndices());
        }

        public CubeWithNormals(Device device)
            : base(device)
        {
            CreateVertexStream(StreamUsage.Position, CreatePositions());
            CreateVertexStream(StreamUsage.Color, CreateColors());
            CreateVertexStream(StreamUsage.Normal, CreateNormals());
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

            return new[] { frontbottomLeft, fronttopLeft, frontbottomRight, fronttopRight, 
                frontbottomRight, fronttopRight, backbottomRight, backtopRight, 
                backbottomRight, backtopRight, backbottomLeft, backtopLeft,
                backbottomLeft, backtopLeft, frontbottomLeft, fronttopLeft,
                backbottomLeft, frontbottomLeft, backbottomRight, frontbottomRight,
                fronttopLeft, backtopLeft, fronttopRight, backtopRight};
        }

        private static Vector3[] CreatePositions()
        {
            var frontbottomLeft = new Vector3(-0.5f, -0.5f, 0.5f);
            var fronttopLeft = new Vector3(-0.5f, 0.5f, 0.5f);
            var frontbottomRight = new Vector3(0.5f, -0.5f, 0.5f);
            var fronttopRight = new Vector3(0.5f, 0.5f, 0.5f);

            var backbottomLeft = new Vector3(-0.5f, -0.5f, -0.5f);
            var backtopLeft = new Vector3(-0.5f, 0.5f, -0.5f);
            var backbottomRight = new Vector3(0.5f, -0.5f, -0.5f);
            var backtopRight = new Vector3(0.5f, 0.5f, -0.5f);

            return new[] { frontbottomLeft, fronttopLeft, frontbottomRight, fronttopRight, 
                frontbottomRight, fronttopRight, backbottomRight, backtopRight, 
                backbottomRight, backtopRight, backbottomLeft, backtopLeft,
                backbottomLeft, backtopLeft, frontbottomLeft, fronttopLeft,
                backbottomLeft, frontbottomLeft, backbottomRight, frontbottomRight,
                fronttopLeft, backtopLeft, fronttopRight, backtopRight};
        }

        private static Vector3[] CreateNormals()
        {
            return new Vector3[]
            {
                Vector3.ZAxis, Vector3.ZAxis, Vector3.ZAxis, Vector3.ZAxis, 
                Vector3.XAxis, Vector3.XAxis, Vector3.XAxis, Vector3.XAxis, 
                -Vector3.ZAxis, -Vector3.ZAxis, -Vector3.ZAxis, -Vector3.ZAxis, 
                -Vector3.XAxis, -Vector3.XAxis, -Vector3.XAxis, -Vector3.XAxis, 
                -Vector3.YAxis, -Vector3.YAxis, -Vector3.YAxis, -Vector3.YAxis, 
                Vector3.YAxis, Vector3.YAxis, Vector3.YAxis, Vector3.YAxis
            };
        }

        private static uint[] CreateIndices()
        {
            return new uint[]
            {
                0, 1, 3, 0, 3, 2,
                4, 5, 7, 4, 7, 6,
                8, 9, 11, 8, 11, 10,
                12, 13, 15, 12, 15, 14,
                16, 17, 19, 16, 19, 18,
                20, 21, 23, 20, 23, 22
            };
        }
    }
}