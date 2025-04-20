using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D.Character_Movement;

/// <summary>
/// A plane in 2D space.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
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
    private static extern bool IsValidPlane(Plane a);
    
    /// <summary>
    /// Checks if the plane is valid.
    /// </summary>
    /// <returns>True if the plane is valid, false otherwise.</returns>
    /// <remarks>
    /// A plane is valid if its normal is a unit vector and it is not NaN or infinity.
    /// </remarks>
    [PublicAPI]
    public bool Valid => IsValidPlane(this);
}