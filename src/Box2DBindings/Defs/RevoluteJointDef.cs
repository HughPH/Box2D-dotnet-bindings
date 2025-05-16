using JetBrains.Annotations;

namespace Box2D;

/// <summary>
/// Revolute joint definition<br/>
///
/// This requires defining an anchor point where the bodies are joined.<br/>
/// The definition uses local anchor points so that the initial configuration can violate the constraint slightly. You also need to specify the initial relative angle for joint limits. This helps when saving and loading a game.<br/>
/// The local anchor points are measured from the body's origin rather than the center of mass because:<br/>
/// 1. you might not know where the center of mass will be<br/>
/// 2. if you add/remove shapes from a body and recompute the mass, the joints will be broken
/// </summary>
[PublicAPI]
public sealed class RevoluteJointDef
{
    //! \internal
    internal RevoluteJointDefInternal _internal = new();
    
    /// <summary>
    /// The first attached body
    /// </summary>
    public ref Body BodyA => ref _internal.BodyA;

    /// <summary>
    /// The second attached body
    /// </summary>
    public ref Body BodyB => ref _internal.BodyB;

    /// <summary>
    /// The local anchor point relative to bodyA's origin
    /// </summary>
    public ref Vec2 LocalAnchorA => ref _internal.LocalAnchorA;

    /// <summary>
    /// The local anchor point relative to bodyB's origin
    /// </summary>
    public ref Vec2 LocalAnchorB => ref _internal.LocalAnchorB;

    /// <summary>
    /// The bodyB angle minus bodyA angle in the reference state (radians).
    /// This defines the zero angle for the joint limit.
    /// </summary>
    public ref float ReferenceAngle => ref _internal.ReferenceAngle;

    /// <summary>
    /// Enable a rotational spring on the revolute hinge axis
    /// </summary>
    public bool EnableSpring
    {
        get => _internal.EnableSpring != 0;
        set => _internal.EnableSpring = value ? (byte)1 : (byte)0;
    }
    
    /// <summary>
    /// The spring stiffness Hertz, cycles per second
    /// </summary>
    public ref float Hertz => ref _internal.Hertz;

    /// <summary>
    /// The spring damping ratio, non-dimensional
    /// </summary>
    public ref float DampingRatio => ref _internal.DampingRatio;

    /// <summary>
    /// A flag to enable joint limits
    /// </summary>
    public bool EnableLimit
    {
        get => _internal.EnableLimit != 0;
        set => _internal.EnableLimit = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// The lower angle for the joint limit in radians. Minimum of -0.95*pi radians.
    /// </summary>
    public ref float LowerAngle => ref _internal.LowerAngle;

    /// <summary>
    /// The upper angle for the joint limit in radians. Maximum of 0.95*pi radians.
    /// </summary>
    public ref float UpperAngle => ref _internal.UpperAngle;

    /// <summary>
    /// A flag to enable the joint motor
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
    /// Scale the debug draw
    /// </summary>
    public ref float DrawSize => ref _internal.DrawSize;

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
    /// Construct a revolute joint definition with the supplied values
    /// </summary>
    /// <param name="bodyA">The first attached body</param>
    /// <param name="bodyB">The second attached body</param>
    /// <param name="anchorA">The local anchor point relative to bodyA's origin</param>
    /// <param name="anchorB">The local anchor point relative to bodyB's origin</param>
    /// <param name="referenceAngle">The bodyB angle minus bodyA angle in the reference state (radians)</param>
    /// <param name="enableSpring">Enable a rotational spring on the revolute hinge axis</param>
    /// <param name="hertz">The spring stiffness Hertz, cycles per second</param>
    /// <param name="dampingRatio">The spring damping ratio, non-dimensional</param>
    /// <param name="enableLimit">A flag to enable joint limits</param>
    /// <param name="lowerAngle">The lower angle for the joint limit in radians</param>
    /// <param name="upperAngle">The upper angle for the joint limit in radians</param>
    /// <param name="enableMotor">A flag to enable the joint motor</param>
    /// <param name="maxMotorTorque">The maximum motor torque, typically in newton-meters</param>
    /// <param name="motorSpeed">The desired motor speed in radians per second</param>
    /// <param name="collideConnected">Set this flag to true if the attached bodies should collide</param>
    /// <param name="userData">User data</param>
    public RevoluteJointDef(
        Body bodyA,
        Body bodyB,
        Vec2 anchorA,
        Vec2 anchorB,
        float referenceAngle = 0.0f,
        bool enableSpring = false,
        float hertz = 0.0f,
        float dampingRatio = 0.0f,
        bool enableLimit = false,
        float lowerAngle = 0.0f,
        float upperAngle = 0.0f,
        bool enableMotor = false,
        float maxMotorTorque = 0.0f,
        float motorSpeed = 0.0f,
        bool collideConnected = false,
        object? userData = null)
    {
        BodyA = bodyA;
        BodyB = bodyB;
        LocalAnchorA = anchorA;
        LocalAnchorB = anchorB;
        ReferenceAngle = referenceAngle;
        EnableSpring = enableSpring;
        Hertz = hertz;
        DampingRatio = dampingRatio;
        EnableLimit = enableLimit;
        LowerAngle = lowerAngle;
        UpperAngle = upperAngle;
        EnableMotor = enableMotor;
        MaxMotorTorque = maxMotorTorque;
        MotorSpeed = motorSpeed;
        CollideConnected = collideConnected;
        
        UserData = userData;
    }
    
    /// <summary>
    /// Construct a revolute joint definition with the default values
    /// </summary>
    public RevoluteJointDef()
    {
        _internal = new();
    }
}