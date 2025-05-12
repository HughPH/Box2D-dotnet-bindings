using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Input parameters for ShapeCast
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
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
    
    private byte canEncroach;
    
    /// <summary>
    /// Allows shapes with a radius to move slightly closer if already touching
    /// </summary>
    public bool CanEncroach
    {
        get => canEncroach != 0;
        set => canEncroach = value ? (byte)1 : (byte)0;
    }
    
    /// <summary>
    /// Constructs a new ShapeCastPairInput object with the given parameters.
    /// </summary>
    /// <param name="proxyA">The proxy for shape A</param>
    /// <param name="proxyB">The proxy for shape B</param>
    /// <param name="transformA">The world transform for shape A</param>
    /// <param name="transformB">The world transform for shape B</param>
    /// <param name="translationB">The translation of shape B</param>
    /// <param name="maxFraction">The fraction of the translation to consider, typically 1</param>
    /// <param name="canEncroach">Allows shapes with a radius to move slightly closer if already touching</param>
    public ShapeCastPairInput(ShapeProxy proxyA, ShapeProxy proxyB, Transform transformA, Transform transformB, Vec2 translationB, float maxFraction, bool canEncroach)
    {
        ProxyA = proxyA;
        ProxyB = proxyB;
        TransformA = transformA;
        TransformB = transformB;
        TranslationB = translationB;
        MaxFraction = maxFraction;
        this.canEncroach = canEncroach ? (byte)1 : (byte)0;
    }
    
    /// <summary>
    /// Constructs a new ShapeCastPairInput object with default values.
    /// </summary>
    public ShapeCastPairInput()
    {
        ProxyA = new ShapeProxy();
        ProxyB = new ShapeProxy();
        TransformA = new Transform();
        TransformB = new Transform();
        TranslationB = new Vec2(0, 0);
        MaxFraction = 1;
        canEncroach = 0;
    }
}