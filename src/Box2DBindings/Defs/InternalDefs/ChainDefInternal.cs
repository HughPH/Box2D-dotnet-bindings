using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Sequential)]
unsafe struct ChainDefInternal
{
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
 
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultChainDef")]
    private static extern ChainDefInternal GetDefault();
    
    private static ChainDefInternal Default => GetDefault();
    
    public ChainDefInternal()
    {
        this = Default;
    }
}