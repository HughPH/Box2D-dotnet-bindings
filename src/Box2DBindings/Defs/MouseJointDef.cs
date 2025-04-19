using System;

namespace Box2D;

/// <summary>
/// A mouse joint is used to make a point on a body track a specified world point.<br/>
///
/// This a soft constraint and allows the constraint to stretch without
/// applying huge forces. This also applies rotation constraint heuristic to improve control.
/// </summary>
public class MouseJointDef
{
    internal MouseJointDefInternal _internal;

    public MouseJointDef()
    {
        _internal = MouseJointDefInternal.Default;
    }

    /// <summary>
    /// The first attached body. This is assumed to be static.
    /// </summary>
    public ref Body BodyA => ref _internal.BodyA;

    /// <summary>
    /// The second attached body.
    /// </summary>
    public ref Body BodyB => ref _internal.BodyB;

    /// <summary>
    /// The initial target point in world space
    /// </summary>
    public ref Vec2 Target => ref _internal.Target;

    /// <summary>
    /// Stiffness in hertz
    /// </summary>
    public ref float Hertz => ref _internal.Hertz;

    /// <summary>
    /// Damping ratio, non-dimensional
    /// </summary>
    public ref float DampingRatio => ref _internal.DampingRatio;

    /// <summary>
    /// Maximum force, typically in newtons
    /// </summary>
    public ref float MaxForce => ref _internal.MaxForce;

    /// <summary>
    /// Set this flag to true if the attached bodies should collide.
    /// </summary>
    public ref bool CollideConnected => ref _internal.CollideConnected;

    /// <summary>
    /// User data pointer
    /// </summary>
    public object? UserData
    {
        get => Core.GetObjectAtPointer(_internal.UserData);
        set => Core.SetObjectAtPointer(ref _internal.UserData, value);
    }
}