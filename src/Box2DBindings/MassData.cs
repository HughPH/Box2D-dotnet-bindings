using JetBrains.Annotations;
using System.Collections.Specialized;
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
    
    /// <summary>
    /// Constructs a new mass data object.
    /// </summary>
    /// <param name="mass">The mass of the shape, usually in kilograms.</param>
    /// <param name="center">The position of the shape's centroid relative to the shape's origin.</param>
    /// <param name="rotationalInertia">The rotational inertia of the shape about the local origin.</param>
    public MassData(float mass, Vec2? center = null, float rotationalInertia = 0)
    {
        Mass = mass;
        Center = center ?? Vec2.Zero;
        RotationalInertia = rotationalInertia;
    }
    
    /// <summary>
    /// Constructs a new mass data object with default values.
    /// </summary>
    public MassData()
    {
        Mass = 0;
        Center = Vec2.Zero;
        RotationalInertia = 0;
    }
}