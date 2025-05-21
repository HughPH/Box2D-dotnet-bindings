using System.Runtime.InteropServices;

namespace Box2D
{
    partial struct ChainShape
    {
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShape, void> b2DestroyChain;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShape, WorldId> b2Chain_GetWorld;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShape, int> b2Chain_GetSegmentCount;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShape, Shape*, int, int> b2Chain_GetSegments;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShape, float, void> b2Chain_SetFriction;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShape, float> b2Chain_GetFriction;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShape, float, void> b2Chain_SetRestitution;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShape, float> b2Chain_GetRestitution;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShape, int, void> b2Chain_SetMaterial;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShape, int> b2Chain_GetMaterial;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShape, byte> b2Chain_IsValid;

    static unsafe ChainShape()
    {
        nint lib = Core.NativeLibHandle;
        NativeLibrary.TryGetExport(lib, "b2DestroyChain", out var p0);
        NativeLibrary.TryGetExport(lib, "b2Chain_GetWorld", out var p1);
        NativeLibrary.TryGetExport(lib, "b2Chain_GetSegmentCount", out var p2);
        NativeLibrary.TryGetExport(lib, "b2Chain_GetSegments", out var p3);
        NativeLibrary.TryGetExport(lib, "b2Chain_SetFriction", out var p4);
        NativeLibrary.TryGetExport(lib, "b2Chain_GetFriction", out var p5);
        NativeLibrary.TryGetExport(lib, "b2Chain_SetRestitution", out var p6);
        NativeLibrary.TryGetExport(lib, "b2Chain_GetRestitution", out var p7);
        NativeLibrary.TryGetExport(lib, "b2Chain_SetMaterial", out var p8);
        NativeLibrary.TryGetExport(lib, "b2Chain_GetMaterial", out var p9);
        NativeLibrary.TryGetExport(lib, "b2Chain_IsValid", out var p10);

        b2DestroyChain = (delegate* unmanaged[Cdecl]<ChainShape, void>)p0;
        b2Chain_GetWorld = (delegate* unmanaged[Cdecl]<ChainShape, WorldId>)p1;
        b2Chain_GetSegmentCount = (delegate* unmanaged[Cdecl]<ChainShape, int>)p2;
        b2Chain_GetSegments = (delegate* unmanaged[Cdecl]<ChainShape, Shape*, int, int>)p3;
        b2Chain_SetFriction = (delegate* unmanaged[Cdecl]<ChainShape, float, void>)p4;
        b2Chain_GetFriction = (delegate* unmanaged[Cdecl]<ChainShape, float>)p5;
        b2Chain_SetRestitution = (delegate* unmanaged[Cdecl]<ChainShape, float, void>)p6;
        b2Chain_GetRestitution = (delegate* unmanaged[Cdecl]<ChainShape, float>)p7;
        b2Chain_SetMaterial = (delegate* unmanaged[Cdecl]<ChainShape, int, void>)p8;
        b2Chain_GetMaterial = (delegate* unmanaged[Cdecl]<ChainShape, int>)p9;
        b2Chain_IsValid = (delegate* unmanaged[Cdecl]<ChainShape, byte>)p10;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DestroyChain")]
    private static extern void b2DestroyChain(ChainShape chainId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetWorld")]
    private static extern WorldId b2Chain_GetWorld(ChainShape chainId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetSegmentCount")]
    private static extern int b2Chain_GetSegmentCount(ChainShape chainId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetSegments")]
    private static extern unsafe int b2Chain_GetSegments(ChainShape chainId, [In] Shape* segmentArray, int capacity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_SetFriction")]
    private static extern void b2Chain_SetFriction(ChainShape chainId, float friction);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetFriction")]
    private static extern float b2Chain_GetFriction(ChainShape chainId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_SetRestitution")]
    private static extern void b2Chain_SetRestitution(ChainShape chainId, float restitution);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetRestitution")]
    private static extern float b2Chain_GetRestitution(ChainShape chainId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_SetMaterial")]
    private static extern void b2Chain_SetMaterial(ChainShape chainId, int material);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetMaterial")]
    private static extern int b2Chain_GetMaterial(ChainShape chainId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_IsValid")]
    private static extern byte b2Chain_IsValid(ChainShape chainId);
#endif
    }
}
