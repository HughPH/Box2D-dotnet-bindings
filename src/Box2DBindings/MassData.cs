using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// This holds the mass data computed for a shape.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public struct MassData
{
    /// <summary>
    /// The mass of the shape, usually in kilograms.
    /// </summary>
    public float Mass;

    /// <summary>
    /// The position of the shape's centroid relative to the shape's origin.
    /// </summary>
    public Vec2 Center;

    /// <summary>
    /// The rotational inertia of the shape about the local origin.
    /// </summary>
    public float RotationalInertia;
    
    /// <summary>
    /// Returns a string representation of the mass data.
    /// </summary>
    public override string ToString()
    {
        return $"MassData(Mass={Mass}, Center={Center}, RotationalInertia={RotationalInertia})";
    }
}