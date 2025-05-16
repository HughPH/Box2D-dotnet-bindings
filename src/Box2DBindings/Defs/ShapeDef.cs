using JetBrains.Annotations;

namespace Box2D;

/// <summary>
/// Used to create a shape.
/// This is a temporary object used to bundle shape creation parameters. You may use
/// the same shape definition to create multiple shapes.
/// </summary>
[PublicAPI]
public sealed class ShapeDef
{
    //! \internal
    internal ShapeDefInternal _internal = new();
    
    /// <summary>
    /// Use this to store application specific shape data.
    /// </summary>
    public object? UserData
    {
        get => GetObjectAtPointer(_internal.UserData);
        set => SetObjectAtPointer(ref _internal.UserData, value);
    }

    /// <summary>
    /// The surface material for this shape.
    /// </summary>
    public ref SurfaceMaterial Material => ref _internal.Material;

    /// <summary>
    /// The density, usually in kg/m².
    /// </summary>
    public ref float Density => ref _internal.Density;

    /// <summary>
    /// Collision filtering data.
    /// </summary>
    public ref Filter Filter => ref _internal.Filter;

    /// <summary>
    /// A sensor shape generates overlap events but never generates a collision response.
    /// Sensors do not have continuous collision. Instead, use a ray or shape cast for those scenarios.
    /// <i>Note: Sensor events are disabled by default.</i>
    /// </summary>
    public bool IsSensor
    {
        get => _internal.IsSensor != 0;
        set => _internal.IsSensor = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// Enable sensor events for this shape. This applies to sensors and non-sensors. False by default, even for sensors.
    /// </summary>
    public bool EnableSensorEvents
    {
        get => _internal.EnableSensorEvents != 0;
        set => _internal.EnableSensorEvents = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// Enable contact events for this shape. Only applies to kinematic and dynamic bodies. Ignored for sensors. False by default.
    /// </summary>
    public bool EnableContactEvents
    {
        get => _internal.EnableContactEvents != 0;
        set => _internal.EnableContactEvents = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// Enable hit events for this shape. Only applies to kinematic and dynamic bodies. Ignored for sensors. False by default.
    /// </summary>
    public bool EnableHitEvents
    {
        get => _internal.EnableHitEvents != 0;
        set => _internal.EnableHitEvents = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// Enable pre-solve contact events for this shape. Only applies to dynamic bodies. These are expensive
    /// and must be carefully handled due to threading. Ignored for sensors.
    /// </summary>
    public bool EnablePreSolveEvents
    {
        get => _internal.EnablePreSolveEvents != 0;
        set => _internal.EnablePreSolveEvents = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// Normally shapes on static bodies don't invoke contact creation when they are added to the world. This overrides
    /// that behavior and causes contact creation. This significantly slows down static body creation which can be important
    /// when there are many static shapes.
    /// This is implicitly always true for sensors, dynamic bodies, and kinematic bodies.
    /// </summary>
    public bool InvokeContactCreation
    {
        get => _internal.InvokeContactCreation != 0;
        set => _internal.InvokeContactCreation = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// Should the body update the mass properties when this shape is created. Default is true.
    /// </summary>
    public bool UpdateBodyMass
    {
        get => _internal.UpdateBodyMass != 0;
        set => _internal.UpdateBodyMass = value ? (byte)1 : (byte)0;
    }
    
    /// <summary>
    /// Construct a shape definition with the supplied values.
    /// </summary>
    /// <param name="material">The surface material for this shape.</param>
    /// <param name="density">The density, usually in kg/m².</param>
    /// <param name="filter">Collision filtering data.</param>
    /// <param name="isSensor">A sensor shape generates overlap events but never generates a collision response. Sensors do not have continuous collision. Instead, use a ray or shape cast for those scenarios. <i>Note: Sensor events are disabled by default.</i></param>
    /// <param name="enableSensorEvents">Enable sensor events for this shape. This applies to sensors and non-sensors. False by default, even for sensors.</param>
    /// <param name="enableContactEvents">Enable contact events for this shape. Only applies to kinematic and dynamic bodies. Ignored for sensors. False by default.</param>
    /// <param name="enableHitEvents">Enable hit events for this shape. Only applies to kinematic and dynamic bodies. Ignored for sensors. False by default.</param>
    /// <param name="enablePreSolveEvents">Enable pre-solve contact events for this shape. Only applies to dynamic bodies. These are expensive and must be carefully handled due to threading. Ignored for sensors.</param>
    /// <param name="invokeContactCreation">Normally shapes on static bodies don't invoke contact creation when they are added to the world. This overrides that behavior and causes contact creation. This significantly slows down static body creation which can be important when there are many static shapes. This is implicitly always true for sensors, dynamic bodies, and kinematic bodies.</param>
    /// <param name="updateBodyMass">Should the body update the mass properties when this shape is created. Default is true.</param>
    /// <param name="userData">User data</param>
    public ShapeDef(
        SurfaceMaterial material,
        float density,
        Filter filter,
        bool isSensor = false,
        bool enableSensorEvents = false,
        bool enableContactEvents = false,
        bool enableHitEvents = false,
        bool enablePreSolveEvents = false,
        bool invokeContactCreation = false,
        bool updateBodyMass = true,
        object? userData = null)
    {
        _internal.Material = material;
        _internal.Density = density;
        _internal.Filter = filter;
        IsSensor = isSensor;
        EnableSensorEvents = enableSensorEvents;
        EnableContactEvents = enableContactEvents;
        EnableHitEvents = enableHitEvents;
        EnablePreSolveEvents = enablePreSolveEvents;
        InvokeContactCreation = invokeContactCreation;
        UpdateBodyMass = updateBodyMass;
        UserData = userData;
    }
    
    /// <summary>
    /// Construct a shape definition with the default values.
    /// </summary>
    public ShapeDef()
    {
        _internal = new();
    }
}