using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// The contact data for two shapes. By convention the manifold normal points
/// from shape A to shape B.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct ContactData
{
    /// <summary>
    /// The first shape in the contact
    /// </summary>
    public readonly Shape ShapeA;
    /// <summary>
    /// The second shape in the contact
    /// </summary>
    public readonly Shape ShapeB;
    /// <summary>
    /// The contact manifold
    /// </summary>
    public readonly Manifold Manifold;
}