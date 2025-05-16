using JetBrains.Annotations;

namespace Box2D;

/// <summary>
/// A motor joint is used to control the relative motion between two bodies<br/>
///
/// A typical usage is to control the movement of a dynamic body with respect to the ground.
/// </summary>
[PublicAPI]
public sealed class MotorJointDef
{
    //! \internal
    internal MotorJointDefInternal _internal = new();
    
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
    public bool CollideConnected
    {
        get => _internal.CollideConnected != 0;
        set => _internal.CollideConnected = (byte)(value ? 1 : 0);
    }
    
    /// <summary>
    /// Use this to store application specific shape data.
    /// </summary>
    public object? UserData
    {
        get => GetObjectAtPointer(_internal.UserData);
        set => SetObjectAtPointer(ref _internal.UserData, value);
    }
    
    /// <summary>
    /// Construct a motor joint definition with the supplied values
    /// </summary>
    /// <param name="bodyA">The first attached body</param>
    /// <param name="bodyB">The second attached body</param>
    /// <param name="linearOffset">Position of bodyB minus the position of bodyA, in bodyA's frame</param>
    /// <param name="angularOffset">The bodyB angle minus bodyA angle in radians</param>
    /// <param name="maxForce">The maximum motor force in newtons</param>
    /// <param name="maxTorque">The maximum motor torque in newton-meters</param>
    /// <param name="correctionFactor">Position correction factor in the range [0,1]</param>
    /// <param name="collideConnected">Set this flag to true if the attached bodies should collide</param>
    /// <param name="userData">User data</param>
    public MotorJointDef(
        Body bodyA,
        Body bodyB,
        Vec2 linearOffset,
        float angularOffset,
        float maxForce = 0.0f,
        float maxTorque = 0.0f,
        float correctionFactor = 0.3f,
        bool collideConnected = false,
        object? userData = null)
    {
        BodyA = bodyA;
        BodyB = bodyB;
        LinearOffset = linearOffset;
        AngularOffset = angularOffset;
        MaxForce = maxForce;
        MaxTorque = maxTorque;
        CorrectionFactor = correctionFactor;
        CollideConnected = collideConnected;
        UserData = userData;
    }
    
    /// <summary>
    /// Construct a motor joint definition with the default values
    /// </summary>
    public MotorJointDef()
    {
        _internal = new();
    }
}
