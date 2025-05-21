using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Sequential)]
unsafe struct ChainDefInternal
{
#if NET5_0_OR_GREATER
    private static delegate* unmanaged[Cdecl]<ChainDefInternal> b2DefaultChainDef;

    static ChainDefInternal()
    {
        nint lib = Core.NativeLibHandle;
        NativeLibrary.TryGetExport(lib, "b2DefaultChainDef", out var ptr);
        b2DefaultChainDef = (delegate* unmanaged[Cdecl]<ChainDefInternal>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultChainDef")]
    private static extern ChainDefInternal b2DefaultChainDef();
#endif
    
    internal nint UserData;

    internal Vec2* Points;
	
    internal int Count;

    internal SurfaceMaterial* Materials;
	
    internal int MaterialCount;
    
    internal Filter Filter; // 20 bytes

    internal byte IsLoop;

    internal byte EnableSensorEvents;
    
    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly int internalValue;
    
    private static ChainDefInternal Default => b2DefaultChainDef();
    
    public ChainDefInternal()
    {
        this = Default;
    }
}