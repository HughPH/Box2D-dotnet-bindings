using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A solid circle
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public struct Circle
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
    /// Compute mass properties of a circle
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ComputeCircleMass")]
    private static extern MassData ComputeCircleMass(in Circle shape, float density);
    
    /// <summary>
    /// Compute mass properties of this circle
    /// </summary>
    public MassData ComputeMass(float density) => ComputeCircleMass(this, density);
    
    /// <summary>
    /// Compute the bounding box of a transformed circle
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ComputeCircleAABB")]
    private static extern AABB ComputeCircleAABB(in Circle shape, in Transform transform);

    /// <summary>
    /// Compute the bounding box of this transformed circle
    /// </summary>
    public AABB ComputeAABB(in Transform transform) => ComputeCircleAABB(this, transform);
    
    /// <summary>
    /// Test a point for overlap with a circle in local space
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PointInCircle")]
    private static extern byte PointInCircle(in Vec2 point, in Circle shape);
    
    /// <summary>
    /// Test a point for overlap with this circle in local space
    /// </summary>
    public bool TestPoint(in Vec2 point) => PointInCircle(point, this) != 0;

    /// <summary>
    /// Ray cast versus circle shape in local space. Initial overlap is treated as a miss.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RayCastCircle")]
    private static extern CastOutput RayCast(in RayCastInput input, in Circle shape);
    
    /// <summary>
    /// Ray cast versus this circle shape in local space. Initial overlap is treated as a miss.
    /// </summary>
    public CastOutput RayCast(in RayCastInput input) => RayCast(input, this); 

    /// <summary>
    /// Shape cast versus a circle. Initial overlap is treated as a miss.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ShapeCastCircle")]
    private static extern CastOutput ShapeCastCircle(in ShapeCastInput input, in Circle shape);

    /// <summary>
    /// Shape cast versus this circle. Initial overlap is treated as a miss.
    /// </summary>
    public CastOutput ShapeCast(in ShapeCastInput input) => ShapeCastCircle(input, this);
}