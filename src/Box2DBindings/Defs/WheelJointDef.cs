using JetBrains.Annotations;

namespace Box2D;

/// <summary>
/// Wheel joint definition
///
/// This requires defining a line of motion using an axis and an anchor point.
/// The definition uses local  anchor points and a local axis so that the initial
/// configuration can violate the constraint slightly. The joint translation is zero
/// when the local anchor points coincide in world space.
/// </summary>
[PublicAPI]
public sealed class WheelJointDef
{
    //! \internal
    internal WheelJointDefInternal _internal = new();

    /// <summary>
    /// The first attached body
    /// </summary>
    public Body BodyA
    {
        get => Body.GetBody(_internal.BodyA);
        set => _internal.BodyA = value.id;
    }

    /// <summary>
    /// The second attached body
    /// </summary>
    public Body BodyB
    {
        get => Body.GetBody(_internal.BodyB);
        set => _internal.BodyB = value.id;
    }

    /// <summary>
    /// The local anchor point relative to bodyA's origin
    /// </summary>
    public ref Vec2 LocalAnchorA => ref _internal.LocalAnchorA;

    /// <summary>
    /// The local anchor point relative to bodyB's origin
    /// </summary>
    public ref Vec2 LocalAnchorB => ref _internal.LocalAnchorB;

    /// <summary>
    /// The local translation unit axis in bodyA
    /// </summary>
    public ref Vec2 LocalAxisA => ref _internal.LocalAxisA;

    /// <summary>
    /// Enable a linear spring along the local axis
    /// </summary>
    public bool EnableSpring
    {
        get => _internal.EnableSpring != 0;
        set => _internal.EnableSpring = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// Spring stiffness in Hertz
    /// </summary>
    public ref float Hertz => ref _internal.Hertz;

    /// <summary>
    /// Spring damping ratio, non-dimensional
    /// </summary>
    public ref float DampingRatio => ref _internal.DampingRatio;

    /// <summary>
    /// Enable/disable the joint linear limit
    /// </summary>
    public bool EnableLimit
    {
        get => _internal.EnableLimit != 0;
        set => _internal.EnableLimit = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// The lower translation limit
    /// </summary>
    public ref float LowerTranslation => ref _internal.LowerTranslation;

    /// <summary>
    /// The upper translation limit
    /// </summary>
    public ref float UpperTranslation => ref _internal.UpperTranslation;

    /// <summary>
    /// Enable/disable the joint rotational motor
    /// </summary>
    public bool EnableMotor
    {
        get => _internal.EnableMotor != 0;
        set => _internal.EnableMotor = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// The maximum motor torque, typically in newton-meters
    /// </summary>
    public ref float MaxMotorTorque => ref _internal.MaxMotorTorque;

    /// <summary>
    /// The desired motor speed in radians per second
    /// </summary>
    public ref float MotorSpeed => ref _internal.MotorSpeed;

    /// <summary>
    /// Set this flag to true if the attached bodies should collide
    /// </summary>
    public bool CollideConnected
    {
        get => _internal.CollideConnected != 0;
        set => _internal.CollideConnected = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// User data
    /// </summary>
    public object? UserData
    {
        get => GetObjectAtPointer(_internal.UserData);
        set => SetObjectAtPointer(ref _internal.UserData, value);
    }

    /// <summary>
    /// Construct a wheel joint definition with the supplied values
    /// </summary>
    /// <param name="bodyA">The first attached body</param>
    /// <param name="bodyB">The second attached body</param>
    /// <param name="anchorA">The local anchor point relative to bodyA's origin</param>
    /// <param name="anchorB">The local anchor point relative to bodyB's origin</param>
    /// <param name="axisA">The local translation unit axis in bodyA</param>
    /// <param name="enableSpring">Enable a linear spring along the local axis</param>
    /// <param name="hertz">Spring stiffness in Hertz</param>
    /// <param name="dampingRatio">Spring damping ratio, non-dimensional</param>
    /// <param name="enableLimit">Enable/disable the joint linear limit</param>
    /// <param name="lowerTranslation">The lower translation limit</param>
    /// <param name="upperTranslation">The upper translation limit</param>
    /// <param name="enableMotor">Enable/disable the joint rotational motor</param>
    /// <param name="maxMotorTorque">The maximum motor torque, typically in newton-meters</param>
    /// <param name="motorSpeed">The desired motor speed in radians per second</param>
    /// <param name="collideConnected">Set this flag to true if the attached bodies should collide</param>
    /// <param name="userData">User data</param>
    public WheelJointDef(
        Body bodyA,
        Body bodyB,
        Vec2 anchorA,
        Vec2 anchorB,
        Vec2 axisA,
        bool enableSpring = false,
        float hertz = 0.0f,
        float dampingRatio = 0.0f,
        bool enableLimit = false,
        float lowerTranslation = 0.0f,
        float upperTranslation = 0.0f,
        bool enableMotor = false,
        float maxMotorTorque = 0.0f,
        float motorSpeed = 0.0f,
        bool collideConnected = false,
        object? userData = null)
    {
        BodyA = bodyA;
        BodyB = bodyB;
        _internal.LocalAnchorA = anchorA;
        _internal.LocalAnchorB = anchorB;
        _internal.LocalAxisA = axisA;
        EnableSpring = enableSpring;
        Hertz = hertz;
        DampingRatio = dampingRatio;
        EnableLimit = enableLimit;
        LowerTranslation = lowerTranslation;
        UpperTranslation = upperTranslation;
        EnableMotor = enableMotor;
        MaxMotorTorque = maxMotorTorque;
        MotorSpeed = motorSpeed;
        CollideConnected = collideConnected;
        UserData = userData;
    }
    
    /// <summary>
    /// Construct a wheel joint definition with the default values
    /// </summary>
    public WheelJointDef()
    {
        _internal = new();
    }
    
}