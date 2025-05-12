using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A distance proxy is used by the GJK algorithm. It encapsulates any shape.
/// You can provide between 1 and <see cref="Constants.MAX_POLYGON_VERTICES" /> and a radius.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public unsafe struct ShapeProxy
{
    private fixed float points[MAX_POLYGON_VERTICES*2];
    private int count;
    
    /// <summary>
    /// The radius of the shape.
    /// </summary>
    public float Radius;

    /// <summary>
    /// The points of the shape in local coordinates.
    /// </summary>
    public ReadOnlySpan<Vec2> Points
    {
        get
        {
            fixed (float* ptr = points)
                return new(ptr, count);
        }
        set
        {
            if (value.Length > MAX_POLYGON_VERTICES)
                throw new ArgumentException($"Cannot set more than {MAX_POLYGON_VERTICES} points");
            count = value.Length;
            fixed (float* ptr = points)
                value.CopyTo(new(ptr, count * 2));
        }
    }
    
    
    /// <summary>
    /// Constructs a new ShapeProxy object with the given parameters.
    /// </summary>
    /// <param name="points">The points of the shape in local coordinates.</param>
    /// <param name="radius">The radius of the shape.</param>
    /// <exception cref="ArgumentException">Thrown when the number of points exceeds the maximum allowed.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the points array is null.</exception>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the number of points is less than 1.</exception>
    public ShapeProxy(ReadOnlySpan<Vec2> points, float radius)
    {
        if (points.Length > MAX_POLYGON_VERTICES)
            throw new ArgumentException($"Cannot set more than {MAX_POLYGON_VERTICES} points");
        if (points.Length < 1)
            throw new ArgumentOutOfRangeException(nameof(points), "Must have at least 1 point");
        if (points == null)
            throw new ArgumentNullException(nameof(points));
        
        Points = points;
        Radius = radius;
    }
    
    /// <summary>
    /// Constructs a new ShapeProxy object with default values.
    /// </summary>
    public ShapeProxy()
    {
        count = 0;
        Radius = 0;
    }
}
