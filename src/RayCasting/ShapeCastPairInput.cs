using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Input parameters for ShapeCast
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct ShapeCastPairInput
{
    /// <summary>
    /// The proxy for shape A
    /// </summary>
    public ShapeProxy ProxyA;

    /// <summary>
    /// The proxy for shape B
    /// </summary>
    public ShapeProxy ProxyB;

    /// <summary>
    /// The world transform for shape A
    /// </summary>
    public Transform TransformA;

    /// <summary>
    /// The world transform for shape B
    /// </summary>
    public Transform TransformB;

    /// <summary>
    /// The translation of shape B
    /// </summary>
    public Vec2 TranslationB;

    /// <summary>
    /// The fraction of the translation to consider, typically 1
    /// </summary>
    public float MaxFraction;
}