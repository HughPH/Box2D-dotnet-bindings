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
                return new ReadOnlySpan<Vec2>(ptr, count);
        }
        set
        {
            if (value.Length > MAX_POLYGON_VERTICES)
                throw new ArgumentException($"Cannot set more than {MAX_POLYGON_VERTICES} points");
            count = value.Length;
            for (int i = 0; i < count; i++)
                points[i * 2] = value[i].X;
            for (int i = 0; i < count; i++)
                points[i * 2 + 1] = value[i].Y;
        }
    }
}
