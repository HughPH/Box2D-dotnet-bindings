using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Low level ray cast or shape-cast output data. Returns a zero fraction and normal in the case of initial overlap.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public readonly struct CastOutput
{
    /// <summary>
    /// The surface normal at the hit point
    /// </summary>
    public readonly Vec2 Normal;

    /// <summary>
    /// The surface hit point
    /// </summary>
    public readonly Vec2 Point;

    /// <summary>
    /// The fraction of the input translation at collision
    /// </summary>
    public readonly float Fraction;

    /// <summary>
    /// The number of iterations used
    /// </summary>
    public readonly int Iterations;

    private readonly byte hit;
    
    /// <summary>
    /// Did the cast hit?
    /// </summary>
    public bool Hit => hit != 0;
}