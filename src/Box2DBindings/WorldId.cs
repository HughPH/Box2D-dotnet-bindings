using System.Runtime.InteropServices;

namespace Box2D
{
    [StructLayout(LayoutKind.Explicit)]
    struct WorldId
    {
        [FieldOffset(0)]
        internal ushort index1;
        [FieldOffset(2)]
        internal ushort generation;
    }
}