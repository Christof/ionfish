using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;

namespace Graphics
{
    public class WindowsMessageLoop
    {
        [StructLayout(LayoutKind.Sequential)]
        private struct NativeMessage
        {
            public IntPtr HWnd;
            public uint Message;
            public IntPtr WParam;
            public IntPtr LParam;
            public uint Time;
            public Point Point;
        }

        private static NativeMessage sMessage;

        [SuppressUnmanagedCodeSecurity]
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool PeekMessage(out NativeMessage message, IntPtr hwnd, uint messageFilterMin, uint messageFilterMax, uint flags);

        public static bool HasNewMessages()
        {
            return PeekMessage(out sMessage, IntPtr.Zero, 0, 0, 0);
        }
    }
}