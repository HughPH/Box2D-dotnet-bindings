using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A revolute joint allows for relative rotation in the 2D plane with no relative translation.
///
/// The revolute joint is probably the most common joint. It can be used for ragdolls and chains.
/// Also called a <i>hinge</i> or <i>pin</i> joint.
/// </summary>
[PublicAPI]
public sealed partial class RevoluteJoint : Joint
{
    internal RevoluteJoint(JointId id) : base(id)
    { }

    /// <summary>
    /// Enables/disables the revolute joint spring
    /// </summary>
    /// <param name="enableSpring">True to enable the spring, false to disable the spring</param>
    public unsafe void EnableSpring(bool enableSpring) => b2RevoluteJoint_EnableSpring(id, enableSpring ? (byte)1 : (byte)0);

    /// <summary>
    /// The revolute joint spring enabled state
    /// </summary>
    public unsafe bool SpringEnabled
    {
        get => b2RevoluteJoint_IsSpringEnabled(id) != 0;
        set => EnableSpring(value);
    }

    /// <summary>
    /// The revolute joint spring stiffness in Hertz
    /// </summary>
    public unsafe float SpringHertz
    {
        get => b2RevoluteJoint_GetSpringHertz(id);
        set => b2RevoluteJoint_SetSpringHertz(id, value);
    }

    /// <summary>
    /// The revolute joint spring damping ratio
    /// </summary>
    public unsafe float SpringDampingRatio
    {
        get => b2RevoluteJoint_GetSpringDampingRatio(id);
        set => b2RevoluteJoint_SetSpringDampingRatio(id, value);
    }

    /// <summary>
    /// The current joint angle in radians
    /// </summary>
    public unsafe float Angle => b2RevoluteJoint_GetAngle(id);

    /// <summary>
    /// The revolute joint limit enabled state
    /// </summary>
    public unsafe bool LimitEnabled
    {
        get => b2RevoluteJoint_IsLimitEnabled(id) != 0;
        set => b2RevoluteJoint_EnableLimit(id, value ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// Sets the revolute joint limits in radians
    /// </summary>
    /// <param name="lower">The lower limit in radians</param>
    /// <param name="upper">The upper limit in radians</param>
    public unsafe void SetLimits(float lower, float upper) => b2RevoluteJoint_SetLimits(id, lower, upper);

    /// <summary>
    /// The lower joint limit of this revolute joint in radians
    /// </summary>
    public unsafe float LowerLimit => b2RevoluteJoint_GetLowerLimit(id);

    /// <summary>
    /// The upper joint limit of this revolute joint in radians
    /// </summary>
    public unsafe float UpperLimit => b2RevoluteJoint_GetUpperLimit(id);

    /// <summary>
    /// The revolute joint motor enabled state
    /// </summary>
    public unsafe bool MotorEnabled
    {
        get => b2RevoluteJoint_IsMotorEnabled(id) != 0;
        set => b2RevoluteJoint_EnableMotor(id, value ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// The revolute joint motor speed in radians per second
    /// </summary>
    public unsafe float MotorSpeed
    {
        get => b2RevoluteJoint_GetMotorSpeed(id);
        set => b2RevoluteJoint_SetMotorSpeed(id, value);
    }

    /// <summary>
    /// The revolute joint current motor torque, usually in newton-meters
    /// </summary>
    public unsafe float MotorTorque => b2RevoluteJoint_GetMotorTorque(id);

    /// <summary>
    /// The revolute joint maximum motor torque, usually in newton-meters
    /// </summary>
    public unsafe float MaxMotorTorque
    {
        get => b2RevoluteJoint_GetMaxMotorTorque(id);
        set => b2RevoluteJoint_SetMaxMotorTorque(id, value);
    }
}
