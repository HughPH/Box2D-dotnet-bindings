using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// This is used to filter collision on shapes. It affects shape-vs-shape collision
/// and shape-versus-query collision (such as World.CastRay).
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct Filter
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<Filter> b2DefaultFilter;

    static unsafe Filter()
    {
        nint lib = NativeLibrary.Load(libraryName);
        NativeLibrary.TryGetExport(lib, "b2DefaultFilter", out var ptr);
        b2DefaultFilter = (delegate* unmanaged[Cdecl]<Filter>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultFilter")]
    private static extern Filter b2DefaultFilter();
#endif
    
    /// <summary>
    /// The collision category bits. Normally you would just set one bit. The category bits should
    /// represent your application object types. For example:
    /// <code lang="c">
    /// enum MyCategories
    /// {
    ///    Static  = 0x00000001,
    ///    Dynamic = 0x00000002,
    ///    Debris  = 0x00000004,
    ///    Player  = 0x00000008,
    ///    // etc
    /// };
    /// </code>
    /// </summary>
    public ulong CategoryBits;

    /// <summary>
    /// The collision mask bits. This states the categories that this
    /// shape would accept for collision.
    /// For example, you may want your player to only collide with static objects
    /// and other players.
    /// <code lang="c">
    /// maskBits = Static | Player;
    /// </code>
    /// </summary>
    public ulong MaskBits;

    /// <summary>
    /// Collision groups allow a certain group of objects to never collide (negative)
    /// or always collide (positive). A group index of zero has no effect. Non-zero group filtering
    /// always wins against the mask bits.
    /// For example, you may want ragdolls to collide with other ragdolls but you don't want
    /// ragdoll self-collision. In this case you would give each ragdoll a unique negative group index
    /// and apply that group index to all shapes on the ragdoll.
    /// </summary>
    public int GroupIndex;
    
    /// <summary>
    /// Creates a filter with the default values.
    /// </summary>
    public unsafe Filter()
    {
        this = b2DefaultFilter();
    }
}