using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A convex hull. Used to create convex polygons.
/// </summary>
/// <remarks>
/// <b>Warning: Do not modify these values directly, instead use <see cref="M:Box2D.Hull.Compute(System.ReadOnlySpan{Box2D.Vec2})"/></b>
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public ref struct Hull
{
    private unsafe fixed float points[MAX_POLYGON_VERTICES * 2];

    private int count;
    
    /// <summary>
    /// Computes a hull from a set of points. This is a copy constructor.
    /// </summary>
    /// <param name="points">
    /// The points to compute the hull from
    /// </param>
    /// <remarks>
    /// This is a copy constructor. The hull will be computed and stored in this instance.
    /// </remarks>
    public Hull(Span<Vec2> points)
    {
        this = Compute(points);
    }
    
    /// <summary>
    /// The final points of the hull
    /// </summary>
    public unsafe ReadOnlySpan<Vec2> Points
    {
        get
        {
            fixed (float* ptr = points)
            {
                if (count > MAX_POLYGON_VERTICES)
                    throw new ArgumentOutOfRangeException(nameof(count), $"Count cannot be greater than {MAX_POLYGON_VERTICES}");
                
                return new(ptr, count);
            }
        }
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ComputeHull")]
    private static extern unsafe Hull Compute(Vec2* points, int count);

    /// <summary>
    /// Compute the convex hull of a set of points. Returns an empty hull if it fails.
    /// Some failure cases:
    /// <ul>
    /// <li>all points very close together</li>
    /// <li>all points on a line</li>
    /// <li>less than 3 points</li>
    /// <li>more than B2_MAX_POLYGON_VERTICES points</li>
    /// </ul>
    /// This welds close points and removes collinear points.
    /// </summary>
    /// <remarks>
    /// <b>Warning: Do not modify a hull once it has been computed</b>
    /// </remarks>
    public static unsafe Hull Compute(ReadOnlySpan<Vec2> points)
    {
        if (points.Length > MAX_POLYGON_VERTICES)
            throw new ArgumentException($"Hull can only contain up to {MAX_POLYGON_VERTICES} points");
        
        fixed (Vec2* pointsPtr = points)
            return Compute(pointsPtr, points.Length);
    }
    
    /// <summary>
    /// This determines if a hull is valid. Checks for:
    /// <ul>
    /// <li>convexity</li>
    /// <li>collinear points</li>
    /// </ul>
    /// This is expensive and should not be called at runtime.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ValidateHull")]
    private static extern byte Validate(in Hull hull);
    
    /// <summary>
    /// Determines if this hull is valid. Checks for:
    /// <ul>
    /// <li>convexity</li>
    /// <li>collinear points</li>
    /// </ul>
    /// This is expensive and should not be called at runtime.
    /// </summary>
    public bool Valid => Validate(in this) != 0;
}