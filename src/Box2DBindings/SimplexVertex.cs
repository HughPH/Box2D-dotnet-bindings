using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Simplex vertex for debugging the GJK algorithm
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct SimplexVertex
{
    /// <summary>
    /// Support point in proxyA
    /// </summary>
    public Vec2 WA;

    /// <summary>
    /// Support point in proxyB
    /// </summary>
    public Vec2 WB;

    /// <summary>
    /// wB - wA
    /// </summary>
    public Vec2 W;

    /// <summary>
    /// Barycentric coordinate for closest point
    /// </summary>
    public float A;

    /// <summary>
    /// wA index
    /// </summary>
    public int IndexA;

    /// <summary>
    /// wB index
    /// </summary>
    public int IndexB;
}