using System;
using System.Runtime.InteropServices;

namespace Box2D.Character_Movement;

partial struct Plane
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<Plane, byte> IsValidPlane;

    static unsafe Plane()
    {
        nint lib = NativeLibrary.Load(libraryName);
        NativeLibrary.TryGetExport(lib, "b2IsValidPlane", out var ptr);

        if (ptr == IntPtr.Zero)
            throw new EntryPointNotFoundException("b2IsValidPlane");

        IsValidPlane = (delegate* unmanaged[Cdecl]<Plane, byte>)ptr;
    }
#else
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2IsValidPlane")]
        private static extern byte IsValidPlane(Plane a);
#endif
}