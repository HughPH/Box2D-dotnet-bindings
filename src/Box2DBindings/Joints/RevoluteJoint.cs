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
public sealed class RevoluteJoint : Joint
{
    internal RevoluteJoint(JointId id) : base(id)
    { }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_EnableSpring")]
    private static extern void b2RevoluteJoint_EnableSpring(JointId jointId, byte enableSpring);

    /// <summary>
    /// Enables/disables the revolute joint spring
    /// </summary>
    /// <param name="enableSpring">True to enable the spring, false to disable the spring</param>
    public void EnableSpring(bool enableSpring) => b2RevoluteJoint_EnableSpring(id, enableSpring ? (byte)1 : (byte)0);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_IsSpringEnabled")]
    private static extern byte b2RevoluteJoint_IsSpringEnabled(JointId jointId);

    /// <summary>
    /// The revolute joint spring enabled state
    /// </summary>
    public bool SpringEnabled
    {
        get => b2RevoluteJoint_IsSpringEnabled(id) != 0;
        set => EnableSpring(value);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_SetSpringHertz")]
    private static extern void b2RevoluteJoint_SetSpringHertz(JointId jointId, float hertz);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetSpringHertz")]
    private static extern float b2RevoluteJoint_GetSpringHertz(JointId jointId);

    /// <summary>
    /// The revolute joint spring stiffness in Hertz
    /// </summary>
    public float SpringHertz
    {
        get => b2RevoluteJoint_GetSpringHertz(id);
        set => b2RevoluteJoint_SetSpringHertz(id, value);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_SetSpringDampingRatio")]
    private static extern void b2RevoluteJoint_SetSpringDampingRatio(JointId jointId, float dampingRatio);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetSpringDampingRatio")]
    private static extern float b2RevoluteJoint_GetSpringDampingRatio(JointId jointId);

    /// <summary>
    /// The revolute joint spring damping ratio
    /// </summary>
    public float SpringDampingRatio
    {
        get => b2RevoluteJoint_GetSpringDampingRatio(id);
        set => b2RevoluteJoint_SetSpringDampingRatio(id, value);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetAngle")]
    private static extern float b2RevoluteJoint_GetAngle(JointId jointId);

    /// <summary>
    /// The current joint angle in radians
    /// </summary>
    public float Angle => b2RevoluteJoint_GetAngle(id);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_EnableLimit")]
    private static extern void b2RevoluteJoint_EnableLimit(JointId jointId, byte enableLimit);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_IsLimitEnabled")]
    private static extern byte b2RevoluteJoint_IsLimitEnabled(JointId jointId);

    /// <summary>
    /// The revolute joint limit enabled state
    /// </summary>
    public bool LimitEnabled
    {
        get => b2RevoluteJoint_IsLimitEnabled(id) != 0;
        set => b2RevoluteJoint_EnableLimit(id, value ? (byte)1 : (byte)0);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_SetLimits")]
    private static extern void b2RevoluteJoint_SetLimits(JointId jointId, float lower, float upper);

    /// <summary>
    /// Sets the revolute joint limits in radians. 
    /// </summary>
    /// <param name="lower">The lower limit in radians</param>
    /// <param name="upper">The upper limit in radians</param>
    /// <remarks>It is expected that lower &lt;= upper and that -0.99 * <see cref="System.Math.PI"/> &lt;= lower &amp;&amp; upper &lt;= -0.99 * <see cref="System.Math.PI"/>.</remarks>
    public void SetLimits(float lower, float upper) => b2RevoluteJoint_SetLimits(id, lower, upper);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetLowerLimit")]
    private static extern float b2RevoluteJoint_GetLowerLimit(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetUpperLimit")]
    private static extern float b2RevoluteJoint_GetUpperLimit(JointId jointId);

    /// <summary>
    /// The lower joint limit of this revolute joint in radians
    /// </summary>
    public float LowerLimit => b2RevoluteJoint_GetLowerLimit(id);

    /// <summary>
    /// The upper joint limit of this revolute joint in radians
    /// </summary>
    public float UpperLimit => b2RevoluteJoint_GetUpperLimit(id);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_EnableMotor")]
    private static extern void b2RevoluteJoint_EnableMotor(JointId jointId, byte enableMotor);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_IsMotorEnabled")]
    private static extern byte b2RevoluteJoint_IsMotorEnabled(JointId jointId);

    /// <summary>
    /// The revolute joint motor enabled state
    /// </summary>
    public bool MotorEnabled
    {
        get => b2RevoluteJoint_IsMotorEnabled(id) != 0;
        set => b2RevoluteJoint_EnableMotor(id, value ? (byte)1 : (byte)0);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_SetMotorSpeed")]
    private static extern void b2RevoluteJoint_SetMotorSpeed(JointId jointId, float motorSpeed);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetMotorSpeed")]
    private static extern float b2RevoluteJoint_GetMotorSpeed(JointId jointId);

    /// <summary>
    /// The revolute joint motor speed in radians per second
    /// </summary>
    public float MotorSpeed
    {
        get => b2RevoluteJoint_GetMotorSpeed(id);
        set => b2RevoluteJoint_SetMotorSpeed(id, value);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetMotorTorque")]
    private static extern float b2RevoluteJoint_GetMotorTorque(JointId jointId);

    /// <summary>
    /// The revolute joint current motor torque, usually in newton-meters
    /// </summary>
    public float MotorTorque => b2RevoluteJoint_GetMotorTorque(id);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_SetMaxMotorTorque")]
    private static extern void b2RevoluteJoint_SetMaxMotorTorque(JointId jointId, float torque);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetMaxMotorTorque")]
    private static extern float b2RevoluteJoint_GetMaxMotorTorque(JointId jointId);

    /// <summary>
    /// The revolute joint maximum motor torque, usually in newton-meters
    /// </summary>
    public float MaxMotorTorque
    {
        get => b2RevoluteJoint_GetMaxMotorTorque(id);
        set => b2RevoluteJoint_SetMaxMotorTorque(id, value);
    }
}
