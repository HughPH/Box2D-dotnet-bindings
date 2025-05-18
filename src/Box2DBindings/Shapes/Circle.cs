using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A solid circle
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public partial struct Circle
{
    /// <summary>
    /// The local center
    /// </summary>
    public Vec2 Center;

    /// <summary>
    /// The radius
    /// </summary>
    public float Radius;

    /// <summary>
    /// Construct a circle shape with a center and radius
    /// </summary>
    public Circle(Vec2 center, float radius)
    {
        Center = center;
        Radius = radius;
    }
    
    /// <summary>
    /// Compute mass properties of this circle
    /// </summary>
    public unsafe MassData ComputeMass(float density) => b2ComputeCircleMass(this, density);
    
    /// <summary>
    /// Compute the bounding box of this transformed circle
    /// </summary>
    public unsafe AABB ComputeAABB(in Transform transform) => b2ComputeCircleAABB(this, transform);
    
    /// <summary>
    /// Test a point for overlap with this circle in local space
    /// </summary>
    public unsafe bool TestPoint(in Vec2 point) => b2PointInCircle(point, this) != 0;

    /// <summary>
    /// Ray cast versus this circle shape in local space. Initial overlap is treated as a miss.
    /// </summary>
    public unsafe CastOutput RayCast(in RayCastInput input) => b2RayCastCircle(input, this); 

    /// <summary>
    /// Shape cast versus this circle. Initial overlap is treated as a miss.
    /// </summary>
    public unsafe CastOutput ShapeCast(in ShapeCastInput input) => b2ShapeCastCircle(input, this);
}