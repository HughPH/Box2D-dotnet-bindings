using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Sequential)]
struct MotorJointDefInternal
{
    /// <summary>
    /// The first attached body
    /// </summary>
    internal Body BodyA;

    /// <summary>
    /// The second attached body
    /// </summary>
    internal Body BodyB;

    /// <summary>
    /// Position of bodyB minus the position of bodyA, in bodyA's frame
    /// </summary>
    internal Vec2 LinearOffset;

    /// <summary>
    /// The bodyB angle minus bodyA angle in radians
    /// </summary>
    internal float AngularOffset;

    /// <summary>
    /// The maximum motor force in newtons
    /// </summary>
    internal float MaxForce;

    /// <summary>
    /// The maximum motor torque in newton-meters
    /// </summary>
    internal float MaxTorque;

    /// <summary>
    /// Position correction factor in the range [0,1]
    /// </summary>
    internal float CorrectionFactor;

    /// <summary>
    /// Set this flag to true if the attached bodies should collide
    /// </summary>
    internal byte CollideConnected;

    /// <summary>
    /// User data
    /// </summary>
    internal nint UserData;

    /// <summary>
    /// Used internally to detect a valid definition. DO NOT SET.
    /// </summary>
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