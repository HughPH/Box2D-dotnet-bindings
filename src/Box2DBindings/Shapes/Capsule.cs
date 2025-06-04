using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A solid capsule can be viewed as two semicircles connected
/// by a rectangle.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public partial struct Capsule
{
    /// <summary>
    /// Local center of the first semicircle
    /// </summary>
    public Vec2 Center1;

    /// <summary>
    /// Local center of the second semicircle
    /// </summary>
    public Vec2 Center2;

    /// <summary>
    /// The radius of the semicircles
    /// </summary>
    public float Radius;

    /// <summary>
    /// Returns the total length of the capsule, which is the distance between
    /// the two centers plus twice the radius.
    /// </summary>
    public float Length => (Vec2.Distance(Center1, Center2) + 2 * Radius);
    
    /// <summary>
    /// Construct a capsule shape with two centers and a radius
    /// </summary>
    public Capsule(Vec2 center1, Vec2 center2, float radius)
    {
        Center1 = center1;
        Center2 = center2;
        Radius = radius;
    }
    
    /// <summary>
    /// Compute mass properties of this capsule
    /// </summary>
    public unsafe MassData ComputeMass(float density) => ComputeCapsuleMass(in this, density);
    
    /// <summary>
    /// Compute the bounding box of this transformed capsule
    /// </summary>
    public unsafe AABB ComputeAABB(in Transform transform) => ComputeCapsuleAABB(in this, transform);
    
    /// <summary>
    /// Test a point for overlap with this capsule in local space
    /// </summary>
    public unsafe bool TestPoint(in Vec2 point) => PointInCapsule(point, in this) != 0;
    
    /// <summary>
    /// Ray cast versus this capsule shape in local space. Initial overlap is treated as a miss.
    /// </summary>
    public unsafe CastOutput RayCast(in RayCastInput input) => RayCastCapsule(in input, in this);

    /// <summary>
    /// Shape cast versus this capsule. Initial overlap is treated as a miss.
    /// </summary>
    public unsafe CastOutput ShapeCast(in ShapeCastInput input) => ShapeCastCapsule(in input, in this);

}