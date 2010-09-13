using System;

namespace Graphics.Streams
{
    public static class StreamUsageExtensions
    {
        public static string ToInputElementName(this StreamUsage streamUsage)
        {
            switch (streamUsage)
            {
                case StreamUsage.Position:
                    return "POSITION";
                case StreamUsage.Color:
                    return "COLOR";
                case StreamUsage.Normal:
                    return "NORMAL";
                default:
                    throw new ArgumentException();
            }
        }
    }
}