using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// An Axis-Aligned Bounding Box
/// </summary>
/// <remarks>
/// "Axis-Aligned" here means that the box is aligned with the coordinate axes: the sides of the box
/// are parallel to the x and y axes.<br/>
/// The bounding box is defined by two points, the lower bound and upper bound. AABBs are
/// used for broad-phase collision detection. They can also be used to find the shapes that
/// intersect or are within an axis-aligned rectangle.
/// </remarks>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public struct AABB : IEquatable<AABB>
{
    /// <summary>
    /// The lower bound of the AABB
    /// </summary>
    public Vec2 LowerBound;
    /// <summary>
    /// The upper bound of the AABB
    /// </summary>
    public Vec2 UpperBound;

    /// <summary>
    /// Constructs an AABB with the given lower and upper bounds
    /// </summary>
    /// <param name="lowerBound">The lower bound of the AABB</param>
    /// <param name="upperBound">The upper bound of the AABB</param>
    public AABB(Vec2 lowerBound, Vec2 upperBound)
    {
        LowerBound = lowerBound;
        UpperBound = upperBound;
    }
    
    /// <summary>
    /// The width of the AABB
    /// </summary>
    /// <remarks>This is not an editable property. You must modify the lower and upper bounds to change the width.</remarks>
    public float Width => UpperBound.X - LowerBound.X;
    
    /// <summary>
    /// The height of the AABB
    /// </summary>
    /// <remarks>This is not an editable property. You must modify the lower and upper bounds to change the height.</remarks>
    public float Height => UpperBound.Y - LowerBound.Y;
    
    /// <summary>
    /// Returns a string representation of the AABB
    /// </summary>
    /// <returns>A string representation of the AABB</returns>
    public override string ToString()
    {
        return $"AABB(Lower: {LowerBound}, Upper: {UpperBound}, Width: {Width}, Height: {Height})";
    }
    
    /// <summary>
    /// Checks if this AABB is equal to another AABB
    /// </summary>
    /// <param name="other">The other AABB to compare to</param>
    /// <returns>True if the AABBs are equal, false otherwise</returns>
    public bool Equals(AABB other) =>
        LowerBound.Equals(other.LowerBound) && UpperBound.Equals(other.UpperBound);
        
    /// <summary>
    /// Checks if this AABB is equal to another object
    /// </summary>
    /// <param name="obj">The object to compare to</param>
    /// <returns>True if the object is an AABB and is equal to this AABB, false otherwise</returns>
    public override bool Equals(object? obj) => obj is AABB other && Equals(other);
        
    /// <summary>
    /// Returns a hash code for this AABB
    /// </summary>
    /// <returns>A hash code for this AABB</returns>
    public override int GetHashCode() =>
        HashCode.Combine(LowerBound, UpperBound);
    
    /// <summary>
    /// Compute the bounding box of an array of circles
    /// </summary>
    public static AABB MakeAABB(ReadOnlySpan<Vec2> points, float radius)
    {
        if (points is not { Length: not 0 })
            throw new ArgumentNullException(nameof(points));

        var aabb = new AABB
            {
                LowerBound = points[0],
                UpperBound = points[0]
            };

        for (int i = 1; i < points.Length; ++i)
        {
            aabb.LowerBound = Vec2.Min(aabb.LowerBound, points[i]);
            aabb.UpperBound = Vec2.Max(aabb.UpperBound, points[i]);
        }

        var r = new Vec2(radius, radius);
        aabb.LowerBound -= r;
        aabb.UpperBound += r;

        return aabb;
    }

    /// <summary>
    /// Checks if this AABB overlaps with another AABB
    /// </summary>
    /// <param name="other">The other AABB to check for overlap with</param>
    /// <returns>True if the AABBs overlap, false otherwise</returns>
    public bool Overlaps(AABB other)
    {
        return Overlaps(this, other);
    }

    /// <summary>
    /// Checks if two AABBs overlap
    /// </summary>
    /// <param name="a">The first AABB</param>
    /// <param name="b">The second AABB</param>
    /// <returns>True if the AABBs overlap, false otherwise</returns>
    public static bool Overlaps(AABB a, AABB b) =>
        !( b.LowerBound.X > a.UpperBound.X || b.LowerBound.Y > a.UpperBound.Y ||
            a.LowerBound.X > b.UpperBound.X || a.LowerBound.Y > b.UpperBound.Y );
}