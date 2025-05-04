using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A manifold point is a contact point belonging to a contact manifold.
/// It holds details related to the geometry and dynamics of the contact points.
/// Box2D uses speculative collision so some contact points may be separated.
/// You may use the maxNormalImpulse to determine if there was an interaction during
/// the time step.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public readonly struct ManifoldPoint
{
    /// <summary>
    /// Location of the contact point in world space. Subject to precision loss at large coordinates.
    /// </summary>
    public readonly Vec2 Point;

    /// <summary>
    /// Location of the contact point relative to shapeA's origin in world space
    /// </summary>
    public readonly Vec2 AnchorA;

    /// <summary>
    /// Location of the contact point relative to shapeB's origin in world space
    /// </summary>
    public readonly Vec2 AnchorB;

    /// <summary>
    /// The separation of the contact point, negative if penetrating
    /// </summary>
    public readonly float Separation;

    /// <summary>
    /// The impulse along the manifold normal vector.
    /// </summary>
    public readonly float NormalImpulse;

    /// <summary>
    /// The friction impulse
    /// </summary>
    public readonly float TangentImpulse;

    /// <summary>
    /// The total normal impulse applied across sub-stepping and restitution. This is important
    /// to identify speculative contact points that had an interaction in the time step.
    /// </summary>
    public readonly float TotalNormalImpulse;

    /// <summary>
    /// Relative normal velocity pre-solve. Used for hit events. If the normal impulse is
    /// zero then there was no hit. Negative means shapes are approaching.
    /// </summary>
    public readonly float NormalVelocity;

    /// <summary>
    /// Uniquely identifies a contact point between two shapes
    /// </summary>
    public readonly ushort Id;

    private readonly byte persisted;
    
    /// <summary>
    /// Did this contact point exist the previous step?
    /// </summary>
    public bool Persisted => persisted != 0;
}