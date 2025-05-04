using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Result of computing the distance between two line segments
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct SegmentDistanceResult
{
    /// <summary>
    /// The closest point on the first segment
    /// </summary>
    public readonly Vec2 Closest1;

    /// <summary>
    /// The closest point on the second segment
    /// </summary>
    public readonly Vec2 Closest2;

    /// <summary>
    /// The barycentric coordinate on the first segment
    /// </summary>
    public readonly float Fraction1;

    /// <summary>
    /// The barycentric coordinate on the second segment
    /// </summary>
    public readonly float Fraction2;

    /// <summary>
    /// The squared distance between the closest points
    /// </summary>
    public readonly float DistanceSquared;
}