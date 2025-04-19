using System;

namespace Box2D;

/// <summary>
/// A motor joint is used to control the relative motion between two bodies<br/>
///
/// A typical usage is to control the movement of a dynamic body with respect to the ground.
/// </summary>
public class MotorJointDef
{
    internal MotorJointDefInternal _internal;

    public MotorJointDef()
    {
        _internal = new MotorJointDefInternal();
    }

    /// <summary>
    /// The first attached body
    /// </summary>
    public ref Body BodyA => ref _internal.BodyA;

    /// <summary>
    /// The second attached body
    /// </summary>
    public ref Body BodyB => ref _internal.BodyB;

    /// <summary>
    /// Position of bodyB minus the position of bodyA, in bodyA's frame
    /// </summary>
    public ref Vec2 LinearOffset => ref _internal.LinearOffset;

    /// <summary>
    /// The bodyB angle minus bodyA angle in radians
    /// </summary>
    public ref float AngularOffset => ref _internal.AngularOffset;

    /// <summary>
    /// The maximum motor force in newtons
    /// </summary>
    public ref float MaxForce => ref _internal.MaxForce;

    /// <summary>
    /// The maximum motor torque in newton-meters
    /// </summary>
    public ref float MaxTorque => ref _internal.MaxTorque;

    /// <summary>
    /// Position correction factor in the range [0,1]
    /// </summary>
    public ref float CorrectionFactor => ref _internal.CorrectionFactor;

    /// <summary>
    /// Set this flag to true if the attached bodies should collide
    /// </summary>
    public ref bool CollideConnected => ref _internal.CollideConnected;

    /// <summary>
    /// Use this to store application specific shape data.
    /// </summary>
    public object? UserData
    {
        get => Core.GetObjectAtPointer(_internal.UserData);
        set => Core.SetObjectAtPointer(ref _internal.UserData, value);
    }
}
