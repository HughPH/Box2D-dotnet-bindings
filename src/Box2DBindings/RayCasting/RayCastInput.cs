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
    
    /// <summary>
    /// Constructs a new RayCastInput object with the given parameters.
    /// </summary>
    /// <param name="origin">Start point of the ray cast</param>
    /// <param name="translation">Translation of the ray cast</param>
    /// <param name="maxFraction">The maximum fraction of the translation to consider, typically 1</param>
    public RayCastInput(Vec2 origin, Vec2 translation, float maxFraction)
    {
        Origin = origin;
        Translation = translation;
        MaxFraction = maxFraction;
    }
    
    /// <summary>
    /// Constructs a new RayCastInput object with default values.
    /// </summary>
    public RayCastInput()
    {
        Origin = new Vec2(0, 0);
        Translation = new Vec2(0, 0);
        MaxFraction = 1;
    }
}
