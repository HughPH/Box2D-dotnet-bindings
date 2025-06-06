using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A solid convex polygon. It is assumed that the interior of the polygon is to
/// the left of each edge.
/// Polygons have a maximum number of vertices equal to B2_MAX_POLYGON_VERTICES.
/// In most cases you should not need many vertices for a convex polygon.
/// <b>Warning: DO NOT fill this out manually, instead use a helper function like
/// MakePolygon or MakeBox.</b>
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public unsafe partial struct Polygon
{
    
    private fixed float vertices[MAX_POLYGON_VERTICES * 2];

    private fixed float normals[MAX_POLYGON_VERTICES * 2];

    /// <summary>
    /// The centroid of the polygon
    /// </summary>
    public Vec2 Centroid;

    /// <summary>
    /// The external radius for rounded polygons
    /// </summary>
    public float Radius;

    /// <summary>
    /// The number of polygon vertices
    /// </summary>
    private int count;

    /// <summary>
    /// Construct a polygon shape with a set of vertices and an optional radius
    /// </summary>
    /// <remarks>
    /// This constructor implicitly creates a Hull from the points. The hull is
    /// computed in the order of the points. The hull is assumed to be convex.<br/>
    /// A radius greater than 0 will create a rounded polygon. A negative radius
    /// is invalid.
    /// </remarks>
    public Polygon(ReadOnlySpan<Vec2> points, float radius = 0f)
    {
        if (points.Length > MAX_POLYGON_VERTICES)
            throw new ArgumentOutOfRangeException(nameof(points), $"Count must be less than {MAX_POLYGON_VERTICES}");
        
        this = MakePolygon(points, radius);
    }

    /// <summary>
    /// The polygon vertices
    /// </summary>
    public ReadOnlySpan<Vec2> Vertices
    {
        get
        {
            fixed (float* ptr = vertices)
                return new(ptr,count);
        }
    }

    /// <summary>
    /// The outward normal vectors of the polygon sides
    /// </summary>
    public ReadOnlySpan<Vec2> Normals
    {
        get
        {
            fixed (float* ptr = normals)
                return new(ptr,count);
        }
    }

    /// <summary>
    /// Make a convex polygon from a set of points. This will create a hull and assert if it is not valid.
    /// </summary>
    public static Polygon MakePolygon(ReadOnlySpan<Vec2> points, float radius)
    {
        if (points.Length > MAX_POLYGON_VERTICES)
            throw new ArgumentOutOfRangeException(nameof(points), $"Count must be less than {MAX_POLYGON_VERTICES}");
        var hull = Hull.Compute(points);
        return MakePolygon_(hull, radius);
    }

    /// <summary>
    /// Make a convex polygon from a convex hull. This will assert if the hull is not valid.
    /// </summary>
    /// <remarks>
    /// <b>Warning: Do not manually fill in the hull data, it must come directly from b2ComputeHull</b>
    /// </remarks>
    public static Polygon MakePolygon(in Hull hull, float radius) => MakePolygon_(in hull, radius);
    
    /// <summary>
    /// Make an offset convex polygon from a set of points. This will create a hull and assert if it is not valid.
    /// </summary>
    public static  Polygon MakeOffsetPolygon(Span<Vec2> points, Vec2 position, Rotation rotation)
    {
        if (points.Length > MAX_POLYGON_VERTICES)
            throw new ArgumentOutOfRangeException(nameof(points), $"Count must be less than {MAX_POLYGON_VERTICES}");
        var hull = Hull.Compute(points);
        return MakeOffsetPolygon_(hull, position, rotation);
    }

    /// <summary>
    /// Make an offset convex polygon from a convex hull. This will assert if the hull is not valid.
    /// </summary>
    /// <remarks>
    /// <b>Warning: Do not manually fill in the hull data, it must come directly from b2ComputeHull</b>
    /// </remarks>
    public static Polygon MakeOffsetPolygon(in Hull hull, Vec2 position, Rotation rotation) => MakeOffsetPolygon_(in hull, position, rotation);
    
    /// <summary>
    /// Make an offset convex polygon from a set of points. This will create a hull and assert if it is not valid.
    /// </summary>
    public static  Polygon MakeOffsetRoundedPolygon(Span<Vec2> points, Vec2 position, Rotation rotation, float radius)
    {
        if (points.Length > MAX_POLYGON_VERTICES)
            throw new ArgumentOutOfRangeException(nameof(points), $"Count must be less than {MAX_POLYGON_VERTICES}");
        var hull = Hull.Compute(points);
        return MakeOffsetRoundedPolygon(hull, position, rotation, radius);
    }
    
    /// <summary>
    /// Make a square polygon, bypassing the need for a convex hull.
    /// </summary>
    /// <param name="halfWidth">the half-width</param>
    public static Polygon MakeSquare(float halfWidth) => MakeSquare_(halfWidth);
    
    /// <summary>
    /// Make a box (rectangle) polygon, bypassing the need for a convex hull.
    /// </summary>
    /// <param name="halfWidth">the half-width (x-axis)</param>
    /// <param name="halfHeight">the half-height (y-axis)</param>
    public static Polygon MakeBox(float halfWidth, float halfHeight) => MakeBox_(halfWidth, halfHeight);
    
    /// <summary>
    /// Make a rounded box, bypassing the need for a convex hull.
    /// </summary>
    /// <param name="halfWidth">the half-width (x-axis)</param>
    /// <param name="halfHeight">the half-height (y-axis)</param>
    /// <param name="radius">the radius of the rounded extension</param>
    public static Polygon MakeRoundedBox(float halfWidth, float halfHeight, float radius) => MakeRoundedBox_(halfWidth, halfHeight, radius);
    
    /// <summary>
    /// Make an offset box, bypassing the need for a convex hull.
    /// </summary>
    /// <param name="halfWidth">the half-width (x-axis)</param>
    /// <param name="halfHeight">the half-height (y-axis)</param>
    /// <param name="center">the local center of the box</param>
    /// <param name="rotation">the local rotation of the box</param>
    public static Polygon MakeOffsetBox(float halfWidth, float halfHeight, Vec2 center, Rotation rotation) => MakeOffsetBox_(halfWidth, halfHeight, center, rotation);
    
    /// <summary>
    /// Make an offset rounded box, bypassing the need for a convex hull.
    /// </summary>
    /// <param name="halfWidth">the half-width (x-axis)</param>
    /// <param name="halfHeight">the half-height (y-axis)</param>
    /// <param name="center">the local center of the box</param>
    /// <param name="rotation">the local rotation of the box</param>
    /// <param name="radius">the radius of the rounded extension</param>
    public static Polygon MakeOffsetRoundedBox(float halfWidth, float halfHeight, Vec2 center, Rotation rotation, float radius) => MakeOffsetRoundedBox_(halfWidth, halfHeight, center, rotation, radius);
    
    /// <summary>
    /// Make an offset convex polygon from a convex hull. This will assert if the hull is not valid.
    /// </summary>
    /// <remarks>
    /// <b>Warning: Do not manually fill in the hull data, it must come directly from b2ComputeHull</b>
    /// </remarks>
    public static Polygon MakeOffsetRoundedPolygon(in Hull hull, Vec2 position, Rotation rotation, float radius) => MakeOffsetRoundedPolygon_(in hull, position, rotation, radius);

    /// <summary>
    /// Compute mass properties of this polygon
    /// </summary>
    public MassData ComputeMass(float density) => ComputePolygonMass_(in this, density);

    /// <summary>
    /// Compute the bounding box of this transformed polygon
    /// </summary>
    public AABB ComputeAABB(in Transform transform) => ComputePolygonAABB_(in this, transform);

    /// <summary>
    /// Test this point for overlap with a convex polygon in local space
    /// </summary>
    public bool TestPoint(in Vec2 point) => PointInPolygon_(point, in this) != 0;
  
    /// <summary>
    /// Ray cast versus this polygon shape in local space. Initial overlap is treated as a miss.
    /// </summary>
    public CastOutput RayCast(in RayCastInput input) => RayCastPolygon_(in input, in this);

    /// <summary>
    /// Shape cast versus this convex polygon. Initial overlap is treated as a miss.
    /// </summary>
    public CastOutput ShapeCast(in ShapeCastInput input) => ShapeCastPolygon_(in input, in this);

}