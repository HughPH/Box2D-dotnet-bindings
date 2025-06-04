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
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<in RayCastInput, byte> b2IsValidRay;

    static unsafe RayCastInput()
    {
        nint lib = nativeLibrary;
        NativeLibrary.TryGetExport(lib, "b2IsValidRay", out var ptr);
        b2IsValidRay = (delegate* unmanaged[Cdecl]<in RayCastInput, byte>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2IsValidRay")]
    private static extern byte b2IsValidRay(in RayCastInput input);
#endif

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
    /// Validate this ray cast input data (NaN, etc)
    /// </summary>
    public unsafe bool Valid => b2IsValidRay(this) != 0;
    
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
