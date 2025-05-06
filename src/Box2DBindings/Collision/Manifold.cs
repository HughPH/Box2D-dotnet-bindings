using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A contact manifold describes the contact points between colliding shapes.
/// </summary>
/// <remarks>Box2D uses speculative collision so some contact points may be separated.</remarks>
[StructLayout(LayoutKind.Sequential)]
public readonly unsafe struct Manifold
{
    /// <summary>
    /// The unit normal vector in world space, points from shape A to bodyB
    /// </summary>
    public readonly Vec2 Normal;

    /// <summary>
    /// Angular impulse applied for rolling resistance. N * m * s = kg * m² / s
    /// </summary>
    public readonly float RollingImpulse;

    private readonly ManifoldPoint manifoldPoint0;
    private readonly ManifoldPoint manifoldPoint1;

    /// <summary>
    /// The number of contacts points, will be 0, 1, or 2
    /// </summary>
    private readonly int pointCount;
    
    /// <summary>
    /// The manifold points, up to two are possible in 2D
    /// </summary>
    [PublicAPI]
    public ReadOnlySpan<ManifoldPoint> Points
    {
        get
        {
            fixed (ManifoldPoint* points = &manifoldPoint0)
                return new(points, Math.Clamp(pointCount, 0, 2));
        }
    }
}