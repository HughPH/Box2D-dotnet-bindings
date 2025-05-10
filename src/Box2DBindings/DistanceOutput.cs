using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Output for ShapeDistance
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly ref struct DistanceOutput
{
    /// <summary>
    /// Closest point on shape A
    /// </summary>
    public readonly Vec2 PointA;

    /// <summary>
    /// Closest point on shape B
    /// </summary>
    public readonly Vec2 PointB;

    /// <summary>
    /// Normal vector that points from A to B
    /// </summary>
    public readonly Vec2 Normal;
    
    /// <summary>
    /// The final distance, zero if overlapped
    /// </summary>
    public readonly float Distance;

    /// <summary>
    /// Number of GJK iterations used
    /// </summary>
    public readonly int Iterations;

    /// <summary>
    /// The number of simplexes stored in the simplex array
    /// </summary>
    public readonly int SimplexCount;
}