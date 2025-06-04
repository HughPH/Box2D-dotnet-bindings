using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Surface materials allow chain shapes to have per segment surface properties.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public struct SurfaceMaterial
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<SurfaceMaterial> b2DefaultSurfaceMaterial;

    static unsafe SurfaceMaterial()
    {
        nint lib = nativeLibrary;
        NativeLibrary.TryGetExport(lib, "b2DefaultSurfaceMaterial", out var ptr);
        b2DefaultSurfaceMaterial = (delegate* unmanaged[Cdecl]<SurfaceMaterial>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultSurfaceMaterial")]
    private static extern SurfaceMaterial b2DefaultSurfaceMaterial();
#endif

    /// <summary>
    /// The Coulomb (dry) friction coefficient, usually in the range [0,1].
    /// </summary>
    public float Friction;

    /// <summary>
    /// The coefficient of restitution (bounce) usually in the range [0,1].<br/>
    /// https://en.wikipedia.org/wiki/Coefficient_of_restitution
    /// </summary>
    public float Restitution;

    /// <summary>
    /// The rolling resistance usually in the range [0,1].
    /// </summary>
    public float RollingResistance;

    /// <summary>
    /// The tangent speed for conveyor belts
    /// </summary>
    public float TangentSpeed;

    /// <summary>
    /// User material identifier. This is passed with query results and to friction and restitution
    /// combining functions. It is not used internally.
    /// </summary>
    public int UserMaterialId;

    /// <summary>
    /// Custom debug draw color.
    /// </summary>
    public HexColor CustomColor;
    
    /// <summary>
    /// Construct a surface material with the default values.
    /// </summary>
    public unsafe SurfaceMaterial()
    {
        this = b2DefaultSurfaceMaterial();
    }
}