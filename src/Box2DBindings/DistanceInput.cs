using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Input for ShapeDistance
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct DistanceInput
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

    private byte useRadii;
    
    /// <summary>
    /// Should the proxy radius be considered?
    /// </summary>
    public bool UseRadii
    {
        get => useRadii != 0;
        set => useRadii = value ? (byte)1 : (byte)0;
    }
    
    /// <summary>
    /// Constructs a new DistanceInput object with the given parameters.
    /// </summary>
    /// <param name="proxyA">The proxy for shape A</param>
    /// <param name="proxyB">The proxy for shape B</param>
    /// <param name="transformA">The world transform for shape A</param>
    /// <param name="transformB">The world transform for shape B</param>
    /// <param name="useRadii">Should the proxy radius be considered?</param>
    public DistanceInput(ShapeProxy proxyA, ShapeProxy proxyB, Transform transformA, Transform transformB, bool useRadii)
    {
        ProxyA = proxyA;
        ProxyB = proxyB;
        TransformA = transformA;
        TransformB = transformB;
        this.useRadii = useRadii ? (byte)1 : (byte)0;
    }
    
    /// <summary>
    /// Constructs a new DistanceInput object with default values.
    /// </summary>
    public DistanceInput()
    {
        ProxyA = new ShapeProxy();
        ProxyB = new ShapeProxy();
        TransformA = new Transform();
        TransformB = new Transform();
        useRadii = 0;
    }
}