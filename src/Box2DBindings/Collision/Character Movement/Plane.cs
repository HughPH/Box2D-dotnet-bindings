using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D.Character_Movement;

/// <summary>
/// A plane in 2D space.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public struct Plane
{
    /// <summary>
    /// The normal vector of the plane.
    /// </summary>
    public Vec2 Normal;

    /// <summary>
    /// The offset of the plane.
    /// </summary>
    public float Offset;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2IsValidPlane")]
    private static extern byte IsValidPlane(Plane a);
    
    /// <summary>
    /// Checks if the plane is valid.
    /// </summary>
    /// <returns>True if the plane is valid, false otherwise.</returns>
    /// <remarks>
    /// A plane is valid if its normal is a unit vector and it is not NaN or infinity.<br/>
    /// This wraps <a href="https://box2d.org/documentation/group__math.html#ga36ebb9b030a14db7747a419a5bd0d29c">b2IsValidPlane</a>.
    /// </remarks>
    public bool Valid => IsValidPlane(this) != 0;
    
    /// <summary>
    /// Constructs a new Plane object with the given parameters.
    /// </summary>
    /// <param name="normal">The normal vector of the plane.</param>
    /// <param name="offset">The offset of the plane.</param>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when the normal vector is not a unit vector.</exception>
    /// <remarks>
    /// A plane is valid if its normal is a unit vector and it is not NaN or infinity.<br/>
    /// </remarks>
    public Plane(Vec2 normal, float offset)
    {
        var lengthSquared = normal.LengthSquared();
        if (lengthSquared is > 1.0001f or < 0.9999f)
            throw new ArgumentOutOfRangeException(nameof(normal), "Normal vector must be a unit vector");
        Normal = normal;
        Offset = offset;
    }
    
    /// <summary>
    /// Constructs a new Plane object with default values.
    /// </summary>
    public Plane()
    {
        Normal = new Vec2(0, 0);
        Offset = 0;
    }
}