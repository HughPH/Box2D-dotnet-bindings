using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Revolute joint definition<br/>
///
/// This requires defining an anchor point where the bodies are joined.
/// The definition uses local anchor points so that the
/// initial configuration can violate the constraint slightly. You also need to
/// specify the initial relative angle for joint limits. This helps when saving
/// and loading a game.
/// The local anchor points are measured from the body's origin
/// rather than the center of mass because:
/// 1. you might not know where the center of mass will be
/// 2. if you add/remove shapes from a body and recompute the mass, the joints will be broken
/// </summary>
[StructLayout(LayoutKind.Explicit)]
struct RevoluteJointDefInternal
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
    /// The bodyB angle minus bodyA angle in the reference state (radians).
    /// This defines the zero angle for the joint limit.
    /// </summary>
    [FieldOffset(32)]
    internal float ReferenceAngle;

    /// <summary>
    /// Enable a rotational spring on the revolute hinge axis
    /// </summary>
    [FieldOffset(36)]
    internal byte EnableSpring;

    /// <summary>
    /// The spring stiffness Hertz, cycles per second
    /// </summary>
    [FieldOffset(40)]
    internal float Hertz;

    /// <summary>
    /// The spring damping ratio, non-dimensional
    /// </summary>
    [FieldOffset(44)]
    internal float DampingRatio;

    /// <summary>
    /// A flag to enable joint limits
    /// </summary>
    [FieldOffset(48)]
    internal byte EnableLimit;

    /// <summary>
    /// The lower angle for the joint limit in radians
    /// </summary>
    [FieldOffset(52)]
    internal float LowerAngle;

    /// <summary>
    /// The upper angle for the joint limit in radians
    /// </summary>
    [FieldOffset(56)]
    internal float UpperAngle;

    /// <summary>
    /// A flag to enable the joint motor
    /// </summary>
    [FieldOffset(60)]
    internal byte EnableMotor;

    /// <summary>
    /// The maximum motor torque, typically in newton-meters
    /// </summary>
    [FieldOffset(64)]
    internal float MaxMotorTorque;

    /// <summary>
    /// The desired motor speed in radians per second
    /// </summary>
    [FieldOffset(68)]
    internal float MotorSpeed;

    /// <summary>
    /// Scale the debug draw
    /// </summary>
    [FieldOffset(72)]
    internal float DrawSize;

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
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultRevoluteJointDef")]
    private static extern RevoluteJointDefInternal GetDefault();
    
    /// <summary>
    /// The default revolute joint definition.
    /// </summary>
    private static RevoluteJointDefInternal Default => GetDefault();
    
    public RevoluteJointDefInternal()
    {
        this = Default;
    }
}