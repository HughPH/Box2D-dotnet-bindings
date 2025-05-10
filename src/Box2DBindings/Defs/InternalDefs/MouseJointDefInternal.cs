using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A mouse joint is used to make a point on a body track a specified world point.<br/>
///
/// This a soft constraint and allows the constraint to stretch without
/// applying huge forces. This also applies rotation constraint heuristic to improve control.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
struct MouseJointDefInternal
{
    /// <summary>
    /// The first attached body. This is assumed to be static.
    /// </summary>
    [FieldOffset(0)]
    internal Body BodyA;
    
    /// <summary>
    /// The second attached body.
    /// </summary>
    [FieldOffset(8)]
    internal Body BodyB;

    /// <summary>
    /// The initial target point in world space
    /// </summary>
    [FieldOffset(16)]
    internal Vec2 Target;

    /// <summary>
    /// Stiffness in hertz
    /// </summary>
    [FieldOffset(24)]
    internal float Hertz;

    /// <summary>
    /// Damping ratio, non-dimensional
    /// </summary>
    [FieldOffset(28)]
    internal float DampingRatio;

    /// <summary>
    /// Maximum force, typically in newtons
    /// </summary>
    [FieldOffset(32)]
    internal float MaxForce;

    /// <summary>
    /// Set this flag to true if the attached bodies should collide.
    /// </summary>
    [FieldOffset(36)]
    internal byte CollideConnected;

    /// <summary>
    /// User data pointer
    /// </summary>
    [FieldOffset(40)]
    internal nint UserData;

    /// <summary>
    /// Used internally to detect a valid definition. DO NOT SET.
    /// </summary>
    [FieldOffset(48)]
    private readonly int internalValue;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultMouseJointDef")]
    private static extern MouseJointDefInternal GetDefault();
    
    /// <summary>
    /// The default mouse joint definition.
    /// </summary>
    private static MouseJointDefInternal Default => GetDefault();
    
    /// <summary>
    /// Creates a mouse joint definition with the default values.
    /// </summary>
    public MouseJointDefInternal()
    {
        this = Default;
    }
}