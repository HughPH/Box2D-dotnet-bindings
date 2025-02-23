using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Prismatic joint definition<br/>
///
/// This requires defining a line of motion using an axis and an anchor point.
/// The definition uses local anchor points and a local axis so that the initial
/// configuration can violate the constraint slightly. The joint translation is zero
/// when the local anchor points coincide in world space.
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct PrismaticJointDef
{
    /// <summary>
    /// The first attached body
    /// </summary>
    public Body BodyA;

    /// <summary>
    /// The second attached body
    /// </summary>
    public Body BodyB;

    /// <summary>
    /// The local anchor point relative to bodyA's origin
    /// </summary>
    public Vec2 LocalAnchorA;

    /// <summary>
    /// The local anchor point relative to bodyB's origin
    /// </summary>
    public Vec2 LocalAnchorB;

    /// <summary>
    /// The local translation unit axis in bodyA
    /// </summary>
    public Vec2 LocalAxisA;

    /// <summary>
    /// The constrained angle between the bodies: bodyB_angle - bodyA_angle
    /// </summary>
    public float ReferenceAngle;

    /// <summary>
    /// Enable a linear spring along the prismatic joint axis
    /// </summary>
    [MarshalAs(UnmanagedType.U1)]
    public bool EnableSpring;

    /// <summary>
    /// The spring stiffness Hertz, cycles per second
    /// </summary>
    public float Hertz;

    /// <summary>
    /// The spring damping ratio, non-dimensional
    /// </summary>
    public float DampingRatio;

    /// <summary>
    /// Enable/disable the joint limit
    /// </summary>
    [MarshalAs(UnmanagedType.U1)]
    public bool EnableLimit;

    /// <summary>
    /// The lower translation limit
    /// </summary>
    public float LowerTranslation;

    /// <summary>
    /// The upper translation limit
    /// </summary>
    public float UpperTranslation;

    /// <summary>
    /// Enable/disable the joint motor
    /// </summary>
    [MarshalAs(UnmanagedType.U1)]
    public bool EnableMotor;

    /// <summary>
    /// The maximum motor force, typically in newtons
    /// </summary>
    public float MaxMotorForce;

    /// <summary>
    /// The desired motor speed, typically in meters per second
    /// </summary>
    public float MotorSpeed;

    /// <summary>
    /// Set this flag to true if the attached bodies should collide
    /// </summary>
    [MarshalAs(UnmanagedType.U1)]
    public bool CollideConnected;

    /// <summary>
    /// User data pointer
    /// </summary>
    public nint UserData;

    /// <summary>
    /// Used internally to detect a valid definition. DO NOT SET.
    /// </summary>
    private readonly int InternalValue = Box2D.B2_SECRET_COOKIE;
    
    public PrismaticJointDef()
    {
        BodyA = default;
        BodyB = default;
        LocalAnchorA = default;
        LocalAnchorB = default;
        LocalAxisA = default;
        ReferenceAngle = 0;
        EnableSpring = false;
        Hertz = 0;
        DampingRatio = 0;
        EnableLimit = false;
        LowerTranslation = 0;
        UpperTranslation = 0;
        EnableMotor = false;
        MaxMotorForce = 0;
        MotorSpeed = 0;
        CollideConnected = false;
        UserData = 0;
    }
}