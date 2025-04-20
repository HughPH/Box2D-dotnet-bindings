using System.Runtime.InteropServices;

namespace Box2D.Character_Movement;

/// <summary>
/// These are the collision planes returned from b2World_CollideMover.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct PlaneResult
{
    /// <summary>
    /// The collision plane between the mover and a convex shape.
    /// </summary>
    public Plane Plane;

    /// <summary>
    /// Did the collision register a hit? If not this plane should be ignored.
    /// </summary>
    [MarshalAs(UnmanagedType.I1)]
    public bool Hit;
}