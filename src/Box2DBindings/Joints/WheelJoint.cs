using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// The wheel joint can be used to simulate wheels on vehicles.
///
/// The wheel joint restricts body B to move along a local axis in body A. Body B is free to
/// rotate. Supports a linear spring, linear limits, and a rotational motor.
/// </summary>
[PublicAPI]
public sealed partial class WheelJoint : Joint
{
    internal WheelJoint(JointId id) : base(id)
    { }
    
    /// <summary>
    /// Enable/disable the wheel joint spring
    /// </summary>
    /// <param name="enableSpring">True to enable the spring, false to disable the spring</param>
    public unsafe void EnableSpring(bool enableSpring) => b2WheelJoint_EnableSpring(id, enableSpring ? (byte)1 : (byte)0);
    
    /// <summary>
    /// Gets or sets wheel joint spring enabled state
    /// </summary>
    /// <returns>True if the spring is enabled</returns>
    public unsafe bool SpringEnabled
    {
        get => b2WheelJoint_IsSpringEnabled(id) != 0;
        set => EnableSpring(value);
    }

    /// <summary>
    /// Set the wheel joint spring frequency in hertz
    /// </summary>
    public unsafe float SpringHertz
    {
        get => b2WheelJoint_GetSpringHertz(id);
        set => b2WheelJoint_SetSpringHertz(id, value);
    }
    
    /// <summary>
    /// The wheel joint damping ratio, non-dimensional
    /// </summary>
    public unsafe float SpringDampingRatio
    {
        get => b2WheelJoint_GetSpringDampingRatio(id);
        set => b2WheelJoint_SetSpringDampingRatio(id, value);
    }
    
    /// <summary>
    /// The wheel joint limit enabled flag
    /// </summary>
    public unsafe bool LimitEnabled
    {
        get => b2WheelJoint_IsLimitEnabled(id) != 0;
        set => b2WheelJoint_EnableLimit(id, value ? (byte)1 : (byte)0);
    }
    
    /// <summary>
    /// Set the wheel joint limits
    /// </summary>
    /// <param name="lower">The lower limit</param>
    /// <param name="upper">The upper limit</param>
    public unsafe void SetLimits(float lower, float upper) => b2WheelJoint_SetLimits(id, lower, upper);
    
    /// <summary>
    /// The lower wheel joint limit
    /// </summary>
    public unsafe float LowerLimit => b2WheelJoint_GetLowerLimit(id);
    
    /// <summary>
    /// The upper wheel joint limit
    /// </summary>
    public unsafe float UpperLimit => b2WheelJoint_GetUpperLimit(id);
    
    /// <summary>
    /// The wheel joint motor enabled flag
    /// </summary>
    public unsafe bool MotorEnabled
    {
        get => b2WheelJoint_IsMotorEnabled(id) != 0;
        set => b2WheelJoint_EnableMotor(id, value ? (byte)1 : (byte)0);
    }
    
    /// <summary>
    /// The wheel joint motor speed in radians per second
    /// </summary>
    public unsafe float MotorSpeed
    {
        get => b2WheelJoint_GetMotorSpeed(id);
        set => b2WheelJoint_SetMotorSpeed(id, value);
    }
    
    /// <summary>
    /// The wheel joint maximum motor torque, usually in newton-meters
    /// </summary>
    public unsafe float MaxMotorTorque
    {
        get => b2WheelJoint_GetMaxMotorTorque(id);
        set => b2WheelJoint_SetMaxMotorTorque(id, value);
    }
    
    /// <summary>
    /// The current wheel joint motor torque, usually in newton-meters
    /// </summary>
    public unsafe float MotorTorque => b2WheelJoint_GetMotorTorque(id);
}