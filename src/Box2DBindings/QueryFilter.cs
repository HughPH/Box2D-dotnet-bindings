using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// The query filter is used to filter collisions between queries and shapes. For example,
/// you may want a ray-cast representing a projectile to hit players and the static environment
/// but not debris.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public ref struct QueryFilter
{
    /// <summary>
    /// The collision category bits of this query. Normally you would just set one bit.
    /// </summary>
    public ulong CategoryBits;

    /// <summary>
    /// The collision mask bits. This states the shape categories that this
    /// query would accept for collision.
    /// </summary>
    public ulong MaskBits;

    /// <summary>
    /// Constructor for the query filter.
    /// </summary>
    public QueryFilter(ulong categoryBits, ulong maskBits)
    {
        CategoryBits = categoryBits;
        MaskBits = maskBits;
    }
    
    /// <summary>
    /// The default query filter settings.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultQueryFilter")]
    private static extern QueryFilter DefaultQueryFilter();

    /// <summary>
    /// Default constructor for the query filter. This will set the filter to the default settings.
    /// </summary>
    public QueryFilter()
    {
        this = DefaultQueryFilter();
    }
}