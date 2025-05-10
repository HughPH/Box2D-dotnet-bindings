using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Wheel joint definition
///
/// This requires defining a line of motion using an axis and an anchor point.
/// The definition uses local  anchor points and a local axis so that the initial
/// configuration can violate the constraint slightly. The joint translation is zero
/// when the local anchor points coincide in world space.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
struct WheelJointDefInternal
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
    /// Enable a linear spring along the local axis
    /// </summary>
    [FieldOffset(40)]
    internal byte EnableSpring;

    /// <summary>
    /// Spring stiffness in Hertz
    /// </summary>
    [FieldOffset(44)]
    internal float Hertz;

    /// <summary>
    /// Spring damping ratio, non-dimensional
    /// </summary>
    [FieldOffset(48)]
    internal float DampingRatio;

    /// <summary>
    /// Enable/disable the joint linear limit
    /// </summary>
    [FieldOffset(52)]
    internal byte EnableLimit;

    /// <summary>
    /// The lower translation limit
    /// </summary>
    [FieldOffset(56)]
    internal float LowerTranslation;

    /// <summary>
    /// The upper translation limit
    /// </summary>
    [FieldOffset(60)]
    internal float UpperTranslation;

    /// <summary>
    /// Enable/disable the joint rotational motor
    /// </summary>
    [FieldOffset(64)]
    internal byte EnableMotor;

    /// <summary>
    /// The maximum motor torque, typically in newton-meters
    /// </summary>
    [FieldOffset(68)]
    internal float MaxMotorTorque;

    /// <summary>
    /// The desired motor speed in radians per second
    /// </summary>
    [FieldOffset(72)]
    internal float MotorSpeed;

    /// <summary>
    /// Set this flag to true if the attached bodies should collide
    /// </summary>
    [FieldOffset(76)]
    internal byte CollideConnected;

    /// <summary>
    /// User data pointer
    /// </summary>
    [FieldOffset(80)]
    internal nint UserData;

    /// <summary>
    /// Used internally to detect a valid definition. DO NOT SET.
    /// </summary>
    [FieldOffset(88)]
    private readonly int internalValue;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultWheelJointDef")]
    private static extern WheelJointDefInternal GetDefault();
    
    /// <summary>
    /// The default wheel joint definition.
    /// </summary>
    private static WheelJointDefInternal Default => GetDefault();
    
    /// <summary>
    /// Creates a wheel joint definition with the default values.
    /// </summary>
    public WheelJointDefInternal()
    {
        this = Default;
    }
}