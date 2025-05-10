using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Low level ray cast input data
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public struct RayCastInput
{
    /// <summary>
    /// Start point of the ray cast
    /// </summary>
    public Vec2 Origin;

    /// <summary>
    /// Translation of the ray cast
    /// </summary>
    public Vec2 Translation;

    /// <summary>
    /// The maximum fraction of the translation to consider, typically 1
    /// </summary>
    public float MaxFraction;

    /// <summary>
    /// Validate ray cast input data (NaN, etc)
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2IsValidRay")]
    private static extern byte IsValidRay(in RayCastInput input);

    /// <summary>
    /// Validate this ray cast input data (NaN, etc)
    /// </summary>
    public bool Valid => IsValidRay(this) != 0;
}
