using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Results of a Box2D raycast query, including intersection details and traversal statistics.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly ref struct RayResult
{
    /// <summary>
    /// Reference to the shape hit by the ray.
    /// </summary>
    public readonly Shape Shape; // 8 bytes

    /// <summary>
    /// World coordinates of the intersection point.
    /// </summary>
    public readonly Vec2 Point;

    /// <summary>
    /// Surface normal at the intersection.
    /// </summary>
    public readonly Vec2 Normal;

    /// <summary>
    /// Fraction along the ray length where the hit occurred (0 = start, 1 = end).
    /// </summary>
    public readonly float Fraction;
    
    /// <summary>
    /// Number of broad-phase nodes visited during traversal.
    /// </summary>
    public readonly int NodeVisits;

    /// <summary>
    /// Number of leaf nodes (shapes) visited during traversal.
    /// </summary>
    public readonly int LeafVisits;
    
    private readonly byte hit;
    
    /// <summary>
    /// True if the ray intersects a shape.
    /// </summary>
    public bool Hit => hit != 0;
}
