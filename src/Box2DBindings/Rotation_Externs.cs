using System;
using System.Runtime.InteropServices;

namespace Box2D;

partial struct Rotation
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<Rotation, byte> b2IsValidRotation;

    static unsafe Rotation()
    {
        nint lib = nativeLibrary;
        NativeLibrary.TryGetExport(lib, "b2IsValidRotation", out var ptr);

        if (ptr == IntPtr.Zero)
            throw new EntryPointNotFoundException("b2IsValidRotation");

        b2IsValidRotation = (delegate* unmanaged[Cdecl]<Rotation, byte>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2IsValidRotation")]
    private static extern byte b2IsValidRotation(Rotation q);
#endif
}
