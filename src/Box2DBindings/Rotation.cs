using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Rotation expressed as a cosine and sine.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public struct Rotation : IEquatable<Rotation>
{
    /// <summary>
    /// Cosine component of the rotation.
    /// </summary>
    public float Cos;
    /// <summary>
    /// Sine component of the rotation.
    /// </summary>
    public float Sin;
    
    /// <summary>
    /// Identity rotation (0 degrees).
    /// </summary>
    public static readonly Rotation Identity = new()
            { Cos = 1, Sin = 0 };

    /// <summary>
    /// Constructs a rotation from cosine and sine values.
    /// </summary>
    public Rotation(float cos, float sin)
    {
        Cos = cos;
        Sin = sin;
    }

    /// <summary>
    /// Constructs a rotation from an angle in radians.
    /// </summary>
    public Rotation(float radians)
    {
        Cos = MathF.Cos(radians);
        Sin = MathF.Sin(radians);
    }
     
    /// <summary>
    /// Implicitly converts a tuple of (cos, sin) to a Rotation.
    /// </summary>
    public static implicit operator Rotation((float Cos, float Sin) tuple) => new() { Cos = tuple.Cos, Sin = tuple.Sin };
    
    /// <summary>
    /// Implicitly converts a Rotation to a tuple of (cos, sin).
    /// </summary>
    public static implicit operator (float, float)(Rotation rotation) => (rotation.Cos, rotation.Sin);
    
    /// <summary>
    /// Implicitly converts a float (angle in radians) to a Rotation.
    /// </summary>
    public static implicit operator Rotation(float radians) => new(radians);

    /// <summary>
    /// Implicitly converts a Rotation to a float (angle in radians).
    /// </summary>
    public static implicit operator float(Rotation rotation) => MathF.Atan2(rotation.Sin, rotation.Cos);
    
    /// <summary>
    /// Returns a string representation of the rotation.
    /// </summary>
    public override string ToString()
    {
        return $"Rot(Cos: {Cos}, Sin: {Sin}, Rad: {GetAngle()}, Deg: {GetAngle() * 180 / MathF.PI})";
    }
    
    /// <summary>
    /// Returns the angle of the rotation in radians.
    /// </summary>
    public float GetAngle() =>
        MathF.Atan2(Sin, Cos);

    /// <summary>
    /// Checks if this rotation is equal to another rotation.
    /// </summary>
    public bool Equals(Rotation other) => Cos.Equals(other.Cos) && Sin.Equals(other.Sin);
    
    /// <summary>
    /// Checks if this rotation is equal to another object.
    /// </summary>
    public override bool Equals(object? obj) => obj is Rotation other && Equals(other);
    
    /// <summary>
    /// Returns a hash code for this rotation.
    /// </summary>
    public override int GetHashCode() => HashCode.Combine(Cos, Sin);
}