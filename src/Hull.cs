using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A convex hull. Used to create convex polygons.
/// </summary>
/// <remarks>
/// <b>Warning: Do not modify these values directly, instead use ComputeHull()</b>
/// </remarks>
[StructLayout(LayoutKind.Explicit)]
public struct Hull
{
    /// <summary>
    /// The final points of the hull
    /// </summary>
    [FieldOffset(0)]
    public Vec2 Points;

    /// <summary>
    /// The number of points
    /// </summary>
    [FieldOffset(8)]
    public int Count;
}