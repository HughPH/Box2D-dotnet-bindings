using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Explicit)]
struct ChainDefInternal
{
#if BOX2D_300

    [FieldOffset(0)]
    internal nint UserData;

    [FieldOffset(8)]
    internal nint Points;
	
    /// <summary>
    /// The point count, must be 4 or more.
    /// </summary>
    [FieldOffset(16)]
    internal int Count;

    [FieldOffset(20)]
    internal float Friction;

    [FieldOffset(24)]
    internal float Restitution;
    
    [FieldOffset(28)]
    internal Filter Filter; // 20 bytes

    [MarshalAs(UnmanagedType.U1)]
    [FieldOffset(48)]
    internal bool IsLoop;

    /// <summary>
    /// Used internally to detect a valid definition. DO NOT SET.
    /// </summary>
    [FieldOffset(52)]
    private readonly int internalValue;
    
#else
    [FieldOffset(0)]
    internal nint UserData;

    [FieldOffset(8)]
    internal nint Points;
	
    /// <summary>
    /// The point count, must be 4 or more.
    /// </summary>
    [FieldOffset(16)]
    internal int Count;

    [FieldOffset(20)]
    internal nint Materials;
	
    /// <summary>
    /// The material count. Must be 1 or count. This allows you to provide one
    /// material for all segments or a unique material per segment.
    /// </summary>
    [FieldOffset(28)]
    internal int MaterialCount;
    
    [FieldOffset(32)]
    internal Filter Filter; // 20 bytes

    [MarshalAs(UnmanagedType.U1)]
    [FieldOffset(52)]
    internal bool IsLoop;

    [MarshalAs(UnmanagedType.U1)]
    [FieldOffset(53)]
    internal bool EnableSensorEvents;
    
    /// <summary>
    /// Used internally to detect a valid definition. DO NOT SET.
    /// </summary>
    [FieldOffset(56)]
    private readonly int internalValue;

#endif
 
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultChainDef")]
    private static extern ChainDefInternal GetDefault();
    
    /// <summary>
    /// The default chain definition.
    /// </summary>
    internal static ChainDefInternal Default => GetDefault();
    
    public ChainDefInternal()
    {
        this = Default;
    }
}