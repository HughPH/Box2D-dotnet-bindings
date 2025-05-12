using JetBrains.Annotations;

namespace Box2D;

/// <summary>
/// Prismatic joint definition<br/>
///
/// This requires defining a line of motion using an axis and an anchor point.
/// The definition uses local anchor points and a local axis so that the initial
/// configuration can violate the constraint slightly. The joint translation is zero
/// when the local anchor points coincide in world space.
/// </summary>
[PublicAPI]
public class PrismaticJointDef
{
    //! \internal
    internal PrismaticJointDefInternal _internal = new();
    
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
    /// The local translation unit axis in bodyA
    /// </summary>
    public ref Vec2 LocalAxisA => ref _internal.LocalAxisA;

    /// <summary>
    /// The constrained angle between the bodies: bodyB_angle - bodyA_angle
    /// </summary>
    public ref float ReferenceAngle => ref _internal.ReferenceAngle;

    /// <summary>
    /// Enable a linear spring along the prismatic joint axis
    /// </summary>
    public bool EnableSpring
    {
        get => _internal.EnableSpring != 0;
        set => _internal.EnableSpring = (byte)(value ? 1 : 0);
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
    /// Enable/disable the joint limit
    /// </summary>
    public bool EnableLimit
    {
        get => _internal.EnableLimit != 0;
        set => _internal.EnableLimit = (byte)(value ? 1 : 0);
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
    /// Enable/disable the joint motor
    /// </summary>
    public bool EnableMotor
    {
        get => _internal.EnableMotor != 0;
        set => _internal.EnableMotor = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// The maximum motor force, typically in newtons
    /// </summary>
    public ref float MaxMotorForce => ref _internal.MaxMotorForce;

    /// <summary>
    /// The desired motor speed, typically in meters per second
    /// </summary>
    public ref float MotorSpeed => ref _internal.MotorSpeed;

    /// <summary>
    /// Set this flag to true if the attached bodies should collide
    /// </summary>
    public bool CollideConnected
    {
        get => _internal.CollideConnected != 0;
        set => _internal.CollideConnected = (byte)(value ? 1 : 0);
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
    /// Construct a prismatic joint definition with the supplied values
    /// </summary>
    /// <param name="bodyA">The first attached body</param>
    /// <param name="bodyB">The second attached body</param>
    /// <param name="localAnchorA">The local anchor point relative to bodyA's origin</param>
    /// <param name="localAnchorB">The local anchor point relative to bodyB's origin</param>
    /// <param name="localAxisA">The local translation unit axis in bodyA</param>
    /// <param name="referenceAngle">The constrained angle between the bodies: bodyB_angle - bodyA_angle</param>
    /// <param name="enableSpring">Enable a linear spring along the prismatic joint axis</param>
    /// <param name="hertz">The spring stiffness Hertz, cycles per second</param>
    /// <param name="dampingRatio">The spring damping ratio, non-dimensional</param>
    /// <param name="enableLimit">Enable/disable the joint limit</param>
    /// <param name="lowerTranslation">The lower translation limit</param>
    /// <param name="upperTranslation">The upper translation limit</param>
    /// <param name="enableMotor">Enable/disable the joint motor</param>
    /// <param name="maxMotorForce">The maximum motor force, typically in newtons</param>
    /// <param name="motorSpeed">The desired motor speed, typically in meters per second</param>
    /// <param name="collideConnected">Set this flag to true if the attached bodies should collide</param>
    /// <param name="userData">User data</param>
    public PrismaticJointDef(
        Body bodyA,
        Body bodyB,
        Vec2 localAnchorA,
        Vec2 localAnchorB,
        Vec2 localAxisA,
        float referenceAngle = 0.0f,
        bool enableSpring = false,
        float hertz = 0.0f,
        float dampingRatio = 0.0f,
        bool enableLimit = false,
        float lowerTranslation = 0.0f,
        float upperTranslation = 0.0f,
        bool enableMotor = false,
        float maxMotorForce = 0.0f,
        float motorSpeed = 0.0f,
        bool collideConnected = false,
        object? userData = null)
    {
        BodyA = bodyA;
        BodyB = bodyB;
        LocalAnchorA = localAnchorA;
        LocalAnchorB = localAnchorB;
        LocalAxisA = localAxisA;
        ReferenceAngle = referenceAngle;
        EnableSpring = enableSpring;
        Hertz = hertz;
        DampingRatio = dampingRatio;
        EnableLimit = enableLimit;
        LowerTranslation = lowerTranslation;
        UpperTranslation = upperTranslation;
        EnableMotor = enableMotor;
        MaxMotorForce = maxMotorForce;
        MotorSpeed = motorSpeed;
        CollideConnected = collideConnected;
        UserData = userData;
    }
    
    /// <summary>
    /// Construct a prismatic joint definition with the default values
    /// </summary>
    public PrismaticJointDef()
    {
        _internal = new();
    }
}