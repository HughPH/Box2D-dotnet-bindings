using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Input parameters for TimeOfImpact
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public struct TOIInput
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
    /// The movement of shape A
    /// </summary>
    public Sweep SweepA; // 40 bytes

    /// <summary>
    /// The movement of shape B
    /// </summary>
    public Sweep SweepB;

    /// <summary>
    /// Defines the sweep interval [0, maxFraction]
    /// </summary>
    public float MaxFraction;
    
    /// <summary>
    /// Constructs a new TOIInput object with the given parameters.
    /// </summary>
    /// <param name="proxyA">The proxy for shape A</param>
    /// <param name="proxyB">The proxy for shape B</param>
    /// <param name="sweepA">The movement of shape A</param>
    /// <param name="sweepB">>The movement of shape B</param>
    /// <param name="maxFraction">Defines the sweep interval [0, maxFraction]</param>
    public TOIInput(ShapeProxy proxyA, ShapeProxy proxyB, Sweep sweepA, Sweep sweepB, float maxFraction)
    {
        ProxyA = proxyA;
        ProxyB = proxyB;
        SweepA = sweepA;
        SweepB = sweepB;
        MaxFraction = maxFraction;
    }
    
    /// <summary>
    /// Constructs a new TOIInput object with default values.
    /// </summary>
    public TOIInput()
    {
        ProxyA = new ShapeProxy();
        ProxyB = new ShapeProxy();
        SweepA = new Sweep();
        SweepB = new Sweep();
        MaxFraction = 1;
    }
}