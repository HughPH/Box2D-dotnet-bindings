using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Output parameters for TimeOfImpact.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct TOIOutput
{
    /// <summary>
    /// The type of result
    /// </summary>
    public readonly TOIState State;

    /// <summary>
    /// The sweep time of the collision
    /// </summary>
    public readonly float Fraction;
}