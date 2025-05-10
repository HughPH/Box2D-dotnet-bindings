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
public class WheelJoint : Joint
{
    internal WheelJoint(JointId id) : base(id)
    { }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_EnableSpring")]
    private static extern void b2WheelJoint_EnableSpring(JointId jointId, byte enableSpring);
    
    /// <summary>
    /// Enable/disable the wheel joint spring
    /// </summary>
    /// <param name="enableSpring">True to enable the spring, false to disable the spring</param>
    public void EnableSpring(bool enableSpring) => b2WheelJoint_EnableSpring(id, enableSpring ? (byte)1 : (byte)0);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_IsSpringEnabled")]
    private static extern byte b2WheelJoint_IsSpringEnabled(JointId jointId);
    
    /// <summary>
    /// Gets or sets wheel joint spring enabled state
    /// </summary>
    /// <returns>True if the spring is enabled</returns>
    public bool SpringEnabled
    {
        get => b2WheelJoint_IsSpringEnabled(id) != 0;
        set => EnableSpring(value);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_SetSpringHertz")]
    private static extern void b2WheelJoint_SetSpringHertz(JointId jointId, float hertz);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_GetSpringHertz")]
    private static extern float b2WheelJoint_GetSpringHertz(JointId jointId);
    
    /// <summary>
    /// Set the wheel joint spring frequency in hertz
    /// </summary>
    public float SpringHertz
    {
        get => b2WheelJoint_GetSpringHertz(id);
        set => b2WheelJoint_SetSpringHertz(id, value);
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_SetSpringDampingRatio")]
    private static extern void b2WheelJoint_SetSpringDampingRatio(JointId jointId, float dampingRatio);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_GetSpringDampingRatio")]
    private static extern float b2WheelJoint_GetSpringDampingRatio(JointId jointId);
    
    /// <summary>
    /// The wheel joint damping ratio, non-dimensional
    /// </summary>
    public float SpringDampingRatio
    {
        get => b2WheelJoint_GetSpringDampingRatio(id);
        set => b2WheelJoint_SetSpringDampingRatio(id, value);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_EnableLimit")]
    private static extern void b2WheelJoint_EnableLimit(JointId jointId, byte enableLimit);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_IsLimitEnabled")]
    private static extern byte b2WheelJoint_IsLimitEnabled(JointId jointId);
    
    /// <summary>
    /// The wheel joint limit enabled flag
    /// </summary>
    public bool LimitEnabled
    {
        get => b2WheelJoint_IsLimitEnabled(id) != 0;
        set => b2WheelJoint_EnableLimit(id, value ? (byte)1 : (byte)0);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_SetLimits")]
    private static extern void b2WheelJoint_SetLimits(JointId jointId, float lower, float upper);
    
    /// <summary>
    /// Set the wheel joint limits
    /// </summary>
    /// <param name="lower">The lower limit</param>
    /// <param name="upper">The upper limit</param>
    public void SetLimits(float lower, float upper) => b2WheelJoint_SetLimits(id, lower, upper);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_GetLowerLimit")]
    private static extern float b2WheelJoint_GetLowerLimit(JointId jointId);
    
    /// <summary>
    /// The lower wheel joint limit
    /// </summary>
    public float LowerLimit => b2WheelJoint_GetLowerLimit(id);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_GetUpperLimit")]
    private static extern float b2WheelJoint_GetUpperLimit(JointId jointId);
    
    /// <summary>
    /// The upper wheel joint limit
    /// </summary>
    public float UpperLimit => b2WheelJoint_GetUpperLimit(id);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_EnableMotor")]
    private static extern void b2WheelJoint_EnableMotor(JointId jointId, byte enableMotor);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_IsMotorEnabled")]
    private static extern byte b2WheelJoint_IsMotorEnabled(JointId jointId);
    
    /// <summary>
    /// The wheel joint motor enabled flag
    /// </summary>
    public bool MotorEnabled
    {
        get => b2WheelJoint_IsMotorEnabled(id) != 0;
        set => b2WheelJoint_EnableMotor(id, value ? (byte)1 : (byte)0);
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_SetMotorSpeed")]
    private static extern void b2WheelJoint_SetMotorSpeed(JointId jointId, float motorSpeed);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_GetMotorSpeed")]
    private static extern float b2WheelJoint_GetMotorSpeed(JointId jointId);
    
    /// <summary>
    /// The wheel joint motor speed in radians per second
    /// </summary>
    public float MotorSpeed
    {
        get => b2WheelJoint_GetMotorSpeed(id);
        set => b2WheelJoint_SetMotorSpeed(id, value);
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_SetMaxMotorTorque")]
    private static extern void b2WheelJoint_SetMaxMotorTorque(JointId jointId, float torque);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_GetMaxMotorTorque")]
    private static extern float b2WheelJoint_GetMaxMotorTorque(JointId jointId);
    
    /// <summary>
    /// The wheel joint maximum motor torque, usually in newton-meters
    /// </summary>
    public float MaxMotorTorque
    {
        get => b2WheelJoint_GetMaxMotorTorque(id);
        set => b2WheelJoint_SetMaxMotorTorque(id, value);
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_GetMotorTorque")]
    private static extern float b2WheelJoint_GetMotorTorque(JointId jointId);
    
    /// <summary>
    /// The current wheel joint motor torque, usually in newton-meters
    /// </summary>
    public float MotorTorque => b2WheelJoint_GetMotorTorque(id);
}