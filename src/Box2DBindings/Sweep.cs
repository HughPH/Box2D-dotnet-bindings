using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// This describes the motion of a body/shape for TOI computation. Shapes are defined with respect to the body origin,
/// which may not coincide with the center of mass. However, to support dynamics we must interpolate the center of mass
/// position.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public struct Sweep
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<in Sweep, float, Transform> b2GetSweepTransform;

    static unsafe Sweep()
    {
        nint lib = nativeLibrary;
        NativeLibrary.TryGetExport(lib, "b2GetSweepTransform", out var ptr);
        b2GetSweepTransform = (delegate* unmanaged[Cdecl]<in Sweep, float, Transform>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2GetSweepTransform")]
    private static extern Transform b2GetSweepTransform(in Sweep sweep, float time);
#endif

    /// <summary>
    /// Local center of mass position
    /// </summary>
    public Vec2 LocalCenter;

    /// <summary>
    /// Starting center of mass world position
    /// </summary>
    public Vec2 C1;

    /// <summary>
    /// Ending center of mass world position
    /// </summary>
    public Vec2 C2;

    /// <summary>
    /// Starting world rotation
    /// </summary>
    public Rotation Q1;

    /// <summary>
    /// Ending world rotation
    /// </summary>
    public Rotation Q2;
    
    /// <summary>
    /// Get the transform at a specific time.
    /// </summary>
    public unsafe Transform GetTransform(float time) => b2GetSweepTransform(this, time);

}