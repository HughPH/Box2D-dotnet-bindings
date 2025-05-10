using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Weld joint definition<br/>
///
/// A weld joint connect to bodies together rigidly. This constraint provides springs to mimic
/// soft-body simulation.
/// <i>Note: The approximate solver in Box2D cannot hold many bodies together rigidly</i>.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
struct WeldJointDefInternal
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
    /// The bodyB angle minus bodyA angle in the reference state (radians)
    /// </summary>
    [FieldOffset(32)]
    internal float ReferenceAngle;

    /// <summary>
    /// Linear stiffness expressed as Hertz (cycles per second). Use zero for maximum stiffness.
    /// </summary>
    [FieldOffset(36)]
    internal float LinearHertz;

    /// <summary>
    /// Angular stiffness as Hertz (cycles per second). Use zero for maximum stiffness.
    /// </summary>
    [FieldOffset(40)]
    internal float AngularHertz;

    /// <summary>
    /// Linear damping ratio, non-dimensional. Use 1 for critical damping.
    /// </summary>
    [FieldOffset(44)]
    internal float LinearDampingRatio;

    /// <summary>
    /// Linear damping ratio, non-dimensional. Use 1 for critical damping.
    /// </summary>
    [FieldOffset(48)]
    internal float AngularDampingRatio;

    /// <summary>
    /// Set this flag to true if the attached bodies should collide
    /// </summary>
    [FieldOffset(52)]
    internal byte CollideConnected;

    /// <summary>
    /// User data pointer
    /// </summary>
    [FieldOffset(56)]
    internal nint UserData;

    /// <summary>
    /// Used internally to detect a valid definition. DO NOT SET.
    /// </summary>
    [FieldOffset(64)]
    private readonly int internalValue;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultWeldJointDef")]
    private static extern WeldJointDefInternal GetDefault();
    
    /// <summary>
    /// The default weld joint definition.
    /// </summary>
    private static WeldJointDefInternal Default => GetDefault();
    
    public WeldJointDefInternal()
    {
        this = Default;
    }
}