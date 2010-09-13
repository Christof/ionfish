using System.Collections.Generic;
using Core.Helper;
using Graphics.Streams;
using Math;
using SlimDX.Direct3D10;
using Mesh = Graphics.Streams.Mesh;

namespace Graphics.Primitives
{
    public class Sphere : Mesh
    {
        public Sphere(Device device, float radius = 1, int rings = 50, int columns = 50)
            : base(device)
        {
            var positionCount = (rings + 1) * (columns + 1);
            var positions = new Vector3[positionCount];
            var normals = new Vector3[positionCount];
            var colors = new Vector4[positionCount];
            var indices = new uint[2 * rings * (columns + 1)];

            var deltaRingAngle = Constants.PI / rings;
            var deltaColumnAngle = 2 * Constants.PI / columns;
            uint vertexIndex = 0;
            for (uint ring = 0; ring < rings + 1; ring++)
            {
                var r0 = Functions.Sin(ring * deltaRingAngle);
                var y0 = Functions.Cos(ring * deltaRingAngle);

                for (uint column = 0; column < columns + 1; column++)
                {
                    var normal = new Vector3(r0 * Functions.Sin(column * deltaColumnAngle), y0,
                        r0 * Functions.Cos(column * deltaColumnAngle));
                    //var uv = new Vector2(column / (float) columns, 1 - ring / (float) rings);

                    var index = ring * (columns + 1) + column;
                    positions[index] = normal * radius;
                    normals[index] = normal;
                    colors[index] = new Vector4(System.Math.Abs(normal.X), System.Math.Abs(normal.Y),
                        System.Math.Abs(normal.Z), 1);

                    if (ring < rings)
                    {
                        indices[vertexIndex * 2] = vertexIndex + (uint) columns + 1;
                        indices[vertexIndex * 2 + 1] = vertexIndex;
                        ++vertexIndex;
                    }
                }
            }

            CreateVertexStream(StreamUsage.Position, positions);
            CreateIndexStream(indices, PrimitiveTopology.TriangleStrip);
            CreateVertexStream(StreamUsage.Normal, normals);
            CreateVertexStream(StreamUsage.Color, colors);
            //CreateVertexStream(StreamUsage.Color, ArrayHelper.Create(positions.Count, new Vector4(0.5f, 0.5f, 1, 1)));
        }
    }
}