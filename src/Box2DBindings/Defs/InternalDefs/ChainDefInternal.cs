using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Explicit)]
unsafe struct ChainDefInternal
{
    [FieldOffset(0)]
    internal nint UserData;

    [FieldOffset(8)]
    internal Vec2* Points;
	
    [FieldOffset(16)]
    internal int Count;

    [FieldOffset(24)]
    internal SurfaceMaterial* Materials;
	
    [FieldOffset(28)]
    internal int MaterialCount;
    
    [FieldOffset(32)]
    internal Filter Filter; // 20 bytes

    [FieldOffset(64)]
    internal byte IsLoop;

    [FieldOffset(65)]
    internal byte EnableSensorEvents;
    
    [FieldOffset(68)]
    private readonly int internalValue;
 
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultChainDef")]
    private static extern ChainDefInternal GetDefault();
    
    private static ChainDefInternal Default => GetDefault();
    
    public ChainDefInternal()
    {
        this = Default;
    }
}