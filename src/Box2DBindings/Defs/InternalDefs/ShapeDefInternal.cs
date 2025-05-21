using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Explicit, Size = 80)]
struct ShapeDefInternal
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<ShapeDefInternal> b2DefaultShapeDef;

    static unsafe ShapeDefInternal()
    {
        nint lib = NativeLibrary.Load(libraryName);
        NativeLibrary.TryGetExport(lib, "b2DefaultShapeDef", out var ptr);
        b2DefaultShapeDef = (delegate* unmanaged[Cdecl]<ShapeDefInternal>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultShapeDef")]
    private static extern ShapeDefInternal b2DefaultShapeDef();
#endif
    
    [FieldOffset( 0)]
    internal nint UserData;
    
    [FieldOffset( 8)]
    internal SurfaceMaterial Material;
    
    [FieldOffset(32)]
    internal float Density;

    [FieldOffset(40)]
    internal Filter Filter; // 20 bytes
    
    [FieldOffset(64)]
    internal byte IsSensor;

    [FieldOffset(65)]
    internal byte EnableSensorEvents;
    
    [FieldOffset(66)]
    internal byte EnableContactEvents;

    [FieldOffset(67)]
    internal byte EnableHitEvents;

    [FieldOffset(68)]
    internal byte EnablePreSolveEvents;

    [FieldOffset(69)]
    internal byte InvokeContactCreation;

    [FieldOffset(70)]
    internal byte UpdateBodyMass;

    [FieldOffset(72)]
    internal readonly int internalValue;
    
    private static unsafe ShapeDefInternal Default => b2DefaultShapeDef();
    
    public ShapeDefInternal()
    {
        this = Default;
    }
}