using System.Runtime.InteropServices;

namespace Box2D
{
    partial class ChainShape
    {
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShapeId, void> b2DestroyChain;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShapeId, WorldId> b2Chain_GetWorld;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShapeId, int> b2Chain_GetSegmentCount;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShapeId, Shape*, int, int> b2Chain_GetSegments;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShapeId, float, void> b2Chain_SetFriction;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShapeId, float> b2Chain_GetFriction;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShapeId, float, void> b2Chain_SetRestitution;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShapeId, float> b2Chain_GetRestitution;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShapeId, int, void> b2Chain_SetMaterial;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShapeId, int> b2Chain_GetMaterial;
    private static readonly unsafe delegate* unmanaged[Cdecl]<ChainShapeId, byte> b2Chain_IsValid;

    static unsafe ChainShape()
    {
        nint lib = nativeLibrary;
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

        b2DestroyChain =          (delegate* unmanaged[Cdecl]<ChainShapeId, void>)p0;
        b2Chain_GetWorld =        (delegate* unmanaged[Cdecl]<ChainShapeId, WorldId>)p1;
        b2Chain_GetSegmentCount = (delegate* unmanaged[Cdecl]<ChainShapeId, int>)p2;
        b2Chain_GetSegments =     (delegate* unmanaged[Cdecl]<ChainShapeId, Shape*, int, int>)p3;
        b2Chain_SetFriction =     (delegate* unmanaged[Cdecl]<ChainShapeId, float, void>)p4;
        b2Chain_GetFriction =     (delegate* unmanaged[Cdecl]<ChainShapeId, float>)p5;
        b2Chain_SetRestitution =  (delegate* unmanaged[Cdecl]<ChainShapeId, float, void>)p6;
        b2Chain_GetRestitution =  (delegate* unmanaged[Cdecl]<ChainShapeId, float>)p7;
        b2Chain_SetMaterial =     (delegate* unmanaged[Cdecl]<ChainShapeId, int, void>)p8;
        b2Chain_GetMaterial =     (delegate* unmanaged[Cdecl]<ChainShapeId, int>)p9;
        b2Chain_IsValid =         (delegate* unmanaged[Cdecl]<ChainShapeId, byte>)p10;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DestroyChain")]
    private static extern void b2DestroyChain(ChainShapeId chainId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetWorld")]
    private static extern WorldId b2Chain_GetWorld(ChainShapeId chainId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetSegmentCount")]
    private static extern int b2Chain_GetSegmentCount(ChainShapeId chainId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetSegments")]
    private static extern unsafe int b2Chain_GetSegments(ChainShapeId chainId, [In] Shape* segmentArray, int capacity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_SetFriction")]
    private static extern void b2Chain_SetFriction(ChainShapeId chainId, float friction);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetFriction")]
    private static extern float b2Chain_GetFriction(ChainShapeId chainId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_SetRestitution")]
    private static extern void b2Chain_SetRestitution(ChainShapeId chainId, float restitution);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetRestitution")]
    private static extern float b2Chain_GetRestitution(ChainShapeId chainId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_SetMaterial")]
    private static extern void b2Chain_SetMaterial(ChainShapeId chainId, int material);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_GetMaterial")]
    private static extern int b2Chain_GetMaterial(ChainShapeId chainId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Chain_IsValid")]
    private static extern byte b2Chain_IsValid(ChainShapeId chainId);
#endif
    }
}
