using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A line segment with one-sided collision. Only collides on the right side.
/// Several of these are generated for a chain shape.<br/>
/// ghost1 -> point1 -> point2 -> ghost2
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct ChainSegment
{
    /// <summary>
    /// The tail ghost vertex
    /// </summary>
    public Vec2 Ghost1;

    /// <summary>
    /// The line segment
    /// </summary>
    public Segment Segment;

    /// <summary>
    /// The head ghost vertex
    /// </summary>
    public Vec2 Ghost2;

    /// <summary>
    /// The owning chain shape index (internal usage only)
    /// </summary>
    public int ChainId;
}