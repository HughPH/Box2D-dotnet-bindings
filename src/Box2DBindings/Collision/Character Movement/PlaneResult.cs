using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D.Character_Movement;

/// <summary>
/// These are the collision planes returned from b2World_CollideMover.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public readonly struct PlaneResult
{
    /// <summary>
    /// The collision plane between the mover and a convex shape.
    /// </summary>
    public readonly Plane Plane;

    /// <summary>
    /// The collision point on the shape.
    /// </summary>
    public readonly Vec2 Point;
    
    private readonly byte hit;

    /// <summary>
    /// Did the collision register a hit? If not this plane should be ignored.
    /// </summary>
    public bool Hit => hit != 0;
}