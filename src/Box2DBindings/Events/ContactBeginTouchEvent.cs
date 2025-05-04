using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A begin touch event is generated when two shapes begin touching.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct ContactBeginTouchEvent
{
    /// <summary>
    /// The first shape
    /// </summary>
    public readonly Shape ShapeA;

    /// <summary>
    /// The second shape
    /// </summary>
    public readonly Shape ShapeB;

    /// <summary>
    /// The initial contact manifold. This is recorded before the solver is called,
    /// so all the impulses will be zero.
    /// </summary>
    public readonly Manifold Manifold;
}