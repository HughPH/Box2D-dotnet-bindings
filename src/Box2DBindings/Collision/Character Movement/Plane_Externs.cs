using System;
using System.Runtime.InteropServices;

namespace Box2D.Character_Movement;

partial struct Plane
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<Plane, byte> b2IsValidPlane;

    static unsafe Plane()
    {
        nint lib = nativeLibrary;
        NativeLibrary.TryGetExport(lib, "b2IsValidPlane", out var ptr);

        if (ptr == IntPtr.Zero)
            throw new EntryPointNotFoundException("b2IsValidPlane");

        b2IsValidPlane = (delegate* unmanaged[Cdecl]<Plane, byte>)ptr;
    }
#else
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2IsValidPlane")]
        private static extern byte b2IsValidPlane(Plane a);
#endif
}