using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Explicit)]
struct MotorJointDefInternal
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
    /// Position of bodyB minus the position of bodyA, in bodyA's frame
    /// </summary>
    [FieldOffset(16)]
    internal Vec2 LinearOffset;

    /// <summary>
    /// The bodyB angle minus bodyA angle in radians
    /// </summary>
    [FieldOffset(24)]
    internal float AngularOffset;

    /// <summary>
    /// The maximum motor force in newtons
    /// </summary>
    [FieldOffset(28)]
    internal float MaxForce;

    /// <summary>
    /// The maximum motor torque in newton-meters
    /// </summary>
    [FieldOffset(32)]
    internal float MaxTorque;

    /// <summary>
    /// Position correction factor in the range [0,1]
    /// </summary>
    [FieldOffset(36)]
    internal float CorrectionFactor;

    /// <summary>
    /// Set this flag to true if the attached bodies should collide
    /// </summary>
    [FieldOffset(40)]
    internal byte CollideConnected;

    /// <summary>
    /// User data pointer
    /// </summary>
    [FieldOffset(48)]
    internal nint UserData;

    /// <summary>
    /// Used internally to detect a valid definition. DO NOT SET.
    /// </summary>
    [FieldOffset(56)]
    internal readonly int internalValue;

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultMotorJointDef")]
    private static extern MotorJointDefInternal GetDefault();
    
    /// <summary>
    /// The default motor joint definition.
    /// </summary>
    private static MotorJointDefInternal Default => GetDefault();
    
    /// <summary>
    /// Creates a motor joint definition with the default values.
    /// </summary>
    public MotorJointDefInternal()
    {
        this = Default;
    }
}