using System;
using SlimDX.DXGI;

namespace Graphics.Streams
{
    public class FormatHelper
    {
        public static Format GetFormatForType<T>()
        {
            var typeName = typeof(T).Name;
            switch (typeName)
            {
                case "Vector2":
                    return Format.R32G32_Float;
                case "Vector3":
                    return Format.R32G32B32_Float;
                case "Vector4":
                    return Format.R32G32B32A32_Float;
                default:
                    throw new ArgumentException(string.Format("{0} does not correspond to a Format.", typeName));
            }
        }
    }
}