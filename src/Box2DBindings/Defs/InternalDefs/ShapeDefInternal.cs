using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Used to create a shape.
/// This is a temporary object used to bundle shape creation parameters. You may use
/// the same shape definition to create multiple shapes.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
struct ShapeDefInternal
{
    /// <summary>
    /// Use this to store application specific shape data.
    /// </summary>
    [FieldOffset(0)]
    internal nint UserData;
    
    [FieldOffset(8)]
    internal SurfaceMaterial Material;
    
    /// <summary>
    /// The density, usually in kg/m².
    /// This is not part of the surface material because this is for the interior, which may have
    /// other considerations, such as being hollow. For example a wood barrel may be hollow or full of water.
    /// </summary>
    [FieldOffset(32)]
    internal float Density;

    /// <summary>
    /// Collision filtering data.
    /// </summary>
    [FieldOffset(36)]
    internal Filter Filter; // 20 bytes
    
    /// <summary>
    /// A sensor shape generates overlap events but never generates a collision response.
    /// Sensors do not have continuous collision. Instead, use a ray or shape cast for those scenarios.
    /// Sensors still contribute to the body mass if they have non-zero density.
    /// </summary>
    /// <remarks>
    /// <i>Note: Sensor events are disabled by default.</i><br/>
    /// See <see cref="EnableSensorEvents"/>
    /// </remarks>
    [MarshalAs(UnmanagedType.U1)]
    [FieldOffset(56)]
    internal bool IsSensor;

    /// <summary>
    /// Enable sensor events for this shape. This applies to sensors and non-sensors. False by default, even for sensors.
    /// </summary>
    [MarshalAs(UnmanagedType.U1)]
    [FieldOffset(57)]
    internal bool EnableSensorEvents;
    
    /// <summary>
    /// Enable contact events for this shape. Only applies to kinematic and dynamic bodies. Ignored for sensors.
    /// </summary>
    [MarshalAs(UnmanagedType.U1)]
    [FieldOffset(58)]
    internal bool EnableContactEvents;

    /// <summary>
    /// Enable hit events for this shape. Only applies to kinematic and dynamic bodies. Ignored for sensors.
    /// </summary>
    [MarshalAs(UnmanagedType.U1)]
    [FieldOffset(59)]
    internal bool EnableHitEvents;

    /// <summary>
    /// Enable pre-solve contact events for this shape. Only applies to dynamic bodies. These are expensive
    /// and must be carefully handled due to threading. Ignored for sensors.
    /// </summary>
    [MarshalAs(UnmanagedType.U1)]
    [FieldOffset(60)]
    internal bool EnablePreSolveEvents;

    /// <summary>
    /// Normally shapes on static bodies don't invoke contact creation when they are added to the world. This overrides
    /// that behavior and causes contact creation. This significantly slows down static body creation which can be important
    /// when there are many static shapes.
    /// This is implicitly always true for sensors, dynamic bodies, and kinematic bodies.
    /// </summary>
    [MarshalAs(UnmanagedType.U1)]
    [FieldOffset(61)]
    internal bool InvokeContactCreation;

    /// <summary>
    /// Should the body update the mass properties when this shape is created. Default is true.
    /// </summary>
    [MarshalAs(UnmanagedType.U1)]
    [FieldOffset(62)]
    internal bool UpdateBodyMass;

    /// <summary>
    /// Used internally to detect a valid definition. DO NOT SET.
    /// </summary>
    [FieldOffset(64)]
    private readonly int internalValue;
    
    /// <summary>
    /// The default shape definition.
    /// </summary>
    [DllImport(Core.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultShapeDef")]
    private static extern ShapeDefInternal GetDefault();
    
    /// <summary>
    /// The default shape definition.
    /// </summary>
    public static ShapeDefInternal Default => GetDefault();
    
    public ShapeDefInternal()
    {
        this = Default;
    }
}