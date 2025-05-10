using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Explicit)]
struct DistanceJointDefInternal
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
    /// The rest length of this joint. Clamped to a stable minimum value.
    /// </summary>
    [FieldOffset(32)]
    internal float Length;

    /// <summary>
    /// Enable the distance constraint to behave like a spring. If false
    /// then the distance joint will be rigid, overriding the limit and motor.
    /// </summary>
    [FieldOffset(36)]
    internal byte EnableSpring;

    /// <summary>
    /// The spring linear stiffness Hertz, cycles per second
    /// </summary>
    [FieldOffset(40)]
    internal float Hertz;

    /// <summary>
    /// The spring linear damping ratio, non-dimensional
    /// </summary>
    [FieldOffset(44)]
    internal float DampingRatio;

    /// <summary>
    /// Enable/disable the joint limit
    /// </summary>
    [FieldOffset(48)]
    internal byte EnableLimit;

    /// <summary>
    /// Minimum length. Clamped to a stable minimum value.
    /// </summary>
    [FieldOffset(52)]
    internal float MinLength;

    /// <summary>
    /// Maximum length. Must be greater than or equal to the minimum length.
    /// </summary>
    [FieldOffset(56)]
    internal float MaxLength;

    /// <summary>
    /// Enable/disable the joint motor
    /// </summary>
    [FieldOffset(60)]
    internal byte EnableMotor;

    /// <summary>
    /// The maximum motor force, usually in newtons
    /// </summary>
    [FieldOffset(64)]
    internal float MaxMotorForce;

    /// <summary>
    /// The desired motor speed, usually in meters per second
    /// </summary>
    [FieldOffset(68)]
    internal float MotorSpeed;

    /// <summary>
    /// Set this flag to true if the attached bodies should collide
    /// </summary>
    [FieldOffset(72)]
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
    internal readonly int internalValue;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultDistanceJointDef")]
    private static extern DistanceJointDefInternal GetDefault();
    
    /// <summary>
    /// The default distance joint definition.
    /// </summary>
    private static DistanceJointDefInternal Default => GetDefault();

    /// <summary>
    /// Creates a distance joint definition with the default values.
    /// </summary>
    public DistanceJointDefInternal()
    {
        this = Default;
    }
}