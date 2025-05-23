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

    /// <summary>
    /// Decompose a polygon using a simplified version of Mark Bayazit's algorithm.
    /// The input can have any number of points. The result is a set of convex
    /// polygons, each containing no more than <see cref="Constants.MAX_POLYGON_VERTICES"/>
    /// points.
    /// </summary>
    public static Polygon[] Decompose(ReadOnlySpan<Vec2> points)
    {
        if (points.Length < 3)
            throw new ArgumentException("At least three points are required", nameof(points));

        Span<Vec2> scratch = stackalloc Vec2[points.Length];
        points.CopyTo(scratch);

        int maxPolys = points.Length - 2;
        Span<Polygon> polys = stackalloc Polygon[maxPolys];

        int count = DecomposeRecursive(scratch, polys, 0);
        return polys.Slice(0, count).ToArray();
    }

    private static int DecomposeRecursive(ReadOnlySpan<Vec2> vertices, Span<Polygon> result, int index)
    {
        if (vertices.Length < 3 || MathF.Abs(Area(vertices)) < float.Epsilon)
            return index;

        if (vertices.Length <= MAX_POLYGON_VERTICES && IsConvex(vertices))
        {
            result[index++] = MakePolygon(vertices, 0f);
            return index;
        }

        int count = vertices.Length;
        for (int i = 0; i < count; ++i)
        {
            if (IsReflex(i, vertices))
            {
                for (int j = 0; j < count; ++j)
                {
                    if (CanSee(i, j, vertices))
                    {
                        Span<Vec2> lower = stackalloc Vec2[count];
                        int lowerCount = CopyPolygon(vertices, i, j, lower);
                        index = DecomposeRecursive(lower.Slice(0, lowerCount), result, index);

                        Span<Vec2> upper = stackalloc Vec2[count];
                        int upperCount = CopyPolygon(vertices, j, i, upper);
                        index = DecomposeRecursive(upper.Slice(0, upperCount), result, index);

                        return index;
                    }
                }
            }
        }

        result[index++] = MakePolygon(vertices, 0f);
        return index;
    }

    private static int CopyPolygon(ReadOnlySpan<Vec2> vertices, int i, int j, Span<Vec2> result)
    {
        int count = 0;
        int start = i;
        while (start != j)
        {
            result[count++] = vertices[start];
            start = (start + 1) % vertices.Length;
        }
        result[count++] = vertices[j];
        return count;
    }

    private static bool IsConvex(ReadOnlySpan<Vec2> vertices)
    {
        bool sign = Cross(vertices[^2], vertices[^1], vertices[0]) > 0f;
        for (int i = 0; i < vertices.Length; ++i)
        {
            int i1 = (i + 1) % vertices.Length;
            int i2 = (i + 2) % vertices.Length;
            if ((Cross(vertices[i], vertices[i1], vertices[i2]) > 0f) != sign)
                return false;
        }
        return true;
    }

    private static bool IsReflex(int index, ReadOnlySpan<Vec2> vertices)
    {
        int prev = (index - 1 + vertices.Length) % vertices.Length;
        int next = (index + 1) % vertices.Length;
        return Cross(vertices[prev], vertices[index], vertices[next]) < 0f;
    }

    private static bool CanSee(int i, int j, ReadOnlySpan<Vec2> vertices)
    {
        int count = vertices.Length;

        if (i == j || (i + 1) % count == j || i == (j + 1) % count)
            return false;

        Vec2 a = vertices[i];
        Vec2 b = vertices[j];

        int iprev = (i - 1 + count) % count;
        int inext = (i + 1) % count;
        int jprev = (j - 1 + count) % count;
        int jnext = (j + 1) % count;

        if (Cross(vertices[iprev], a, vertices[inext]) < 0f)
        {
            if (Cross(a, vertices[inext], b) > 0f || Cross(vertices[iprev], a, b) > 0f)
                return false;
        }
        else
        {
            if (Cross(a, vertices[iprev], b) < 0f || Cross(vertices[inext], a, b) < 0f)
                return false;
        }

        if (Cross(vertices[jprev], b, vertices[jnext]) < 0f)
        {
            if (Cross(b, vertices[jnext], a) > 0f || Cross(vertices[jprev], b, a) > 0f)
                return false;
        }
        else
        {
            if (Cross(b, vertices[jprev], a) < 0f || Cross(vertices[jnext], b, a) < 0f)
                return false;
        }

        for (int k = 0; k < count; ++k)
        {
            int k1 = (k + 1) % count;
            if (k == i || k1 == i || k == j || k1 == j)
                continue;

            if (LineIntersect(a, b, vertices[k], vertices[k1]))
                return false;
        }

        return true;
    }

    private static bool LineIntersect(Vec2 a1, Vec2 a2, Vec2 b1, Vec2 b2)
    {
        float d1 = Cross(a1, a2, b1);
        float d2 = Cross(a1, a2, b2);
        float d3 = Cross(b1, b2, a1);
        float d4 = Cross(b1, b2, a2);

        if (((d1 > 0f && d2 < 0f) || (d1 < 0f && d2 > 0f)) && ((d3 > 0f && d4 < 0f) || (d3 < 0f && d4 > 0f)))
            return true;

        if (MathF.Abs(d1) < float.Epsilon && OnSegment(a1, a2, b1)) return true;
        if (MathF.Abs(d2) < float.Epsilon && OnSegment(a1, a2, b2)) return true;
        if (MathF.Abs(d3) < float.Epsilon && OnSegment(b1, b2, a1)) return true;
        if (MathF.Abs(d4) < float.Epsilon && OnSegment(b1, b2, a2)) return true;

        return false;
    }

    private static bool OnSegment(Vec2 a, Vec2 b, Vec2 c)
    {
        return c.X >= MathF.Min(a.X, b.X) - float.Epsilon && c.X <= MathF.Max(a.X, b.X) + float.Epsilon &&
               c.Y >= MathF.Min(a.Y, b.Y) - float.Epsilon && c.Y <= MathF.Max(a.Y, b.Y) + float.Epsilon;
    }

    private static float Cross(Vec2 a, Vec2 b, Vec2 c)
    {
        return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
    }

    private static float Area(ReadOnlySpan<Vec2> vertices)
    {
        float area = 0f;
        for (int i = 0; i < vertices.Length; ++i)
        {
            int j = (i + 1) % vertices.Length;
            area += vertices[i].X * vertices[j].Y - vertices[i].Y * vertices[j].X;
        }
        return 0.5f * area;
    }

}
