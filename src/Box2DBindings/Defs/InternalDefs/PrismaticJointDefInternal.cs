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
[StructLayout(LayoutKind.Explicit)]
struct PrismaticJointDefInternal
{
    /// <summary>
    /// The first attached body
    /// </summary>
    [FieldOffset(0)] 
    internal Body BodyA;

    /// <summary>
    /// The second attached body
    /// </summary>
    [FieldOffset(8)]
    internal Body BodyB;

    /// <summary>
    /// The local anchor point relative to bodyA's origin
    /// </summary>
    [FieldOffset(16)]
    internal Vec2 LocalAnchorA;

    /// <summary>
    /// The local anchor point relative to bodyB's origin
    /// </summary>
    [FieldOffset(24)]
    internal Vec2 LocalAnchorB;

    /// <summary>
    /// The local translation unit axis in bodyA
    /// </summary>
    [FieldOffset(32)]
    internal Vec2 LocalAxisA;

    /// <summary>
    /// The constrained angle between the bodies: bodyB_angle - bodyA_angle
    /// </summary>
    [FieldOffset(40)]
    internal float ReferenceAngle;

    /// <summary>
    /// Enable a linear spring along the prismatic joint axis
    /// </summary>
    [FieldOffset(44)]
    internal byte EnableSpring;

    /// <summary>
    /// The spring stiffness Hertz, cycles per second
    /// </summary>
    [FieldOffset(48)]
    internal float Hertz;

    /// <summary>
    /// The spring damping ratio, non-dimensional
    /// </summary>
    [FieldOffset(52)]
    internal float DampingRatio;

    /// <summary>
    /// Enable/disable the joint limit
    /// </summary>
    [FieldOffset(56)]
    internal byte EnableLimit;

    /// <summary>
    /// The lower translation limit
    /// </summary>
    [FieldOffset(60)]
    internal float LowerTranslation;

    /// <summary>
    /// The upper translation limit
    /// </summary>
    [FieldOffset(64)]
    internal float UpperTranslation;

    /// <summary>
    /// Enable/disable the joint motor
    /// </summary>
    [FieldOffset(68)]
    internal byte EnableMotor;

    /// <summary>
    /// The maximum motor force, typically in newtons
    /// </summary>
    [FieldOffset(72)]
    internal float MaxMotorForce;

    /// <summary>
    /// The desired motor speed, typically in meters per second
    /// </summary>
    [FieldOffset(76)]
    internal float MotorSpeed;

    /// <summary>
    /// Set this flag to true if the attached bodies should collide
    /// </summary>
    [FieldOffset(80)]
    internal byte CollideConnected;

    /// <summary>
    /// User data pointer
    /// </summary>
    [FieldOffset(88)]
    internal nint UserData;

    /// <summary>
    /// Used internally to detect a valid definition. DO NOT SET.
    /// </summary>
    [FieldOffset(96)]
    private readonly int internalValue;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultPrismaticJointDef")]
    private static extern PrismaticJointDefInternal GetDefault();
    
    /// <summary>
    /// The default prismatic joint definition.
    /// </summary>
    private static PrismaticJointDefInternal Default => GetDefault();
    
    /// <summary>
    /// Creates a prismatic joint definition with the default values.
    /// </summary>
    public PrismaticJointDefInternal()
    {
        this = Default;
    }
}