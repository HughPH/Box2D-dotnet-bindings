using System;
using System.Runtime.InteropServices;

namespace Box2D;

partial struct AABB
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<AABB, byte> b2IsValidAABB;

    static unsafe AABB()
    {
        nint lib = nativeLibrary;
        NativeLibrary.TryGetExport(lib, "b2IsValidAABB", out var ptr);

        if (ptr == IntPtr.Zero)
            throw new EntryPointNotFoundException("b2IsValidAABB");

        b2IsValidAABB = (delegate* unmanaged[Cdecl]<AABB, byte>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2IsValidAABB")]
    private static extern byte b2IsValidAABB(AABB aabb);
#endif
}
