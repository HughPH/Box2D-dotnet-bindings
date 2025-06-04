using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A distance joint.<br/>
/// This requires defining an anchor point on both
/// bodies and the non-zero distance of the distance joint. The definition uses
/// local anchor points so that the initial configuration can violate the
/// constraint slightly. This helps when saving and loading a game.
/// </summary>
[PublicAPI]
public sealed partial class DistanceJoint : Joint
{
    internal DistanceJoint(JointId id) : base(id)
    { }

    /// <summary>
    /// The rest length of this distance joint
    /// </summary>
    public unsafe float Length
    {
        get => b2DistanceJoint_GetLength(id);
        set => b2DistanceJoint_SetLength(id, value);
    }

    /// <summary>
    /// Enables/disables the spring on this distance joint
    /// </summary>
    /// <param name="enableSpring">True to enable the spring, false to disable the spring</param>
    public unsafe void EnableSpring(bool enableSpring) => b2DistanceJoint_EnableSpring(id, enableSpring ? (byte)1 : (byte)0);

    /// <summary>
    /// Gets or sets the spring enabled state on this distance joint
    /// </summary>
    /// <returns>True if the spring is enabled</returns>
    public unsafe bool SpringEnabled
    {
        get => b2DistanceJoint_IsSpringEnabled(id) != 0;
        set => EnableSpring(value);
    }

    /// <summary>
    /// The spring stiffness in Hertz on this distance joint
    /// </summary>
    public unsafe float SpringHertz
    {
        get => b2DistanceJoint_GetSpringHertz(id);
        set => b2DistanceJoint_SetSpringHertz(id, value);
    }

    /// <summary>
    /// The spring damping ratio on this distance joint
    /// </summary>
    public unsafe float SpringDampingRatio
    {
        get => b2DistanceJoint_GetSpringDampingRatio(id);
        set => b2DistanceJoint_SetSpringDampingRatio(id, value);
    }

    /// <summary>
    /// The limit enabled state of this distance joint
    /// </summary>
    /// <remarks>The limit only works if the joint spring is enabled. Otherwise the joint is rigid and the limit has no effect<br/>
    /// </remarks>
    public unsafe bool LimitEnabled
    {
        get => b2DistanceJoint_IsLimitEnabled(id) != 0;
        set => b2DistanceJoint_EnableLimit(id, value ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// Sets the minimum and maximum length parameters on this distance joint
    /// </summary>
    /// <param name="minLength">The minimum length</param>
    /// <param name="maxLength">The maximum length</param>
    public unsafe void SetLengthRange(float minLength, float maxLength) => b2DistanceJoint_SetLengthRange(id, minLength, maxLength);

    /// <summary>
    /// Gets the minimum length of this distance joint
    /// </summary>
    /// <returns>The minimum length</returns>
    public unsafe float MinLength => b2DistanceJoint_GetMinLength(id);

    /// <summary>
    /// Gets the maximum length of this distance joint
    /// </summary>
    /// <returns>The maximum length</returns>
    public unsafe float MaxLength => b2DistanceJoint_GetMaxLength(id);

    /// <summary>
    /// Gets the current length of this distance joint
    /// </summary>
    /// <returns>The current length</returns>
    public unsafe float CurrentLength => b2DistanceJoint_GetCurrentLength(id);

    /// <summary>
    /// The motor enabled state of this distance joint
    /// </summary>
    public unsafe bool MotorEnabled
    {
        get => b2DistanceJoint_IsMotorEnabled(id) != 0;
        set => b2DistanceJoint_EnableMotor(id, value ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// The desired motor speed, usually in meters per second
    /// </summary>
    public unsafe float MotorSpeed
    {
        get => b2DistanceJoint_GetMotorSpeed(id);
        set => b2DistanceJoint_SetMotorSpeed(id, value);
    }

    /// <summary>
    /// The maximum motor force on this distance joint
    /// </summary>
    public unsafe float MaxMotorForce
    {
        get => b2DistanceJoint_GetMaxMotorForce(id);
        set => b2DistanceJoint_SetMaxMotorForce(id, value);
    }

    /// <summary>
    /// Gets the current motor force on this distance joint
    /// </summary>
    /// <returns>The current motor force, usually in Newtons</returns>
    public unsafe float MotorForce => b2DistanceJoint_GetMotorForce(id);
}
