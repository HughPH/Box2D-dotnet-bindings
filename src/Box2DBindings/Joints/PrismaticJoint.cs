using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A prismatic joint allows for translation along a single axis with no rotation.
///
/// The prismatic joint is useful for things like pistons and moving platforms, where you want a body to translate
/// along an axis and have no rotation. Also called a <i>slider</i> joint.
/// </summary>
[PublicAPI]
public sealed class PrismaticJoint : Joint
{
    internal PrismaticJoint(JointId id) : base(id)
    { }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_EnableSpring")]
    private static extern void b2PrismaticJoint_EnableSpring(JointId jointId, byte enableSpring);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_IsSpringEnabled")]
    private static extern byte b2PrismaticJoint_IsSpringEnabled(JointId jointId);

    /// <summary>
    /// Gets or sets the prismatic joint spring enabled state
    /// </summary>
    /// <returns>True if the prismatic joint spring is enabled</returns>
    public bool SpringEnabled
    {
        get => b2PrismaticJoint_IsSpringEnabled(id) != 0;
        set => b2PrismaticJoint_EnableSpring(id, value ? (byte)1 : (byte)0);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_SetSpringHertz")]
    private static extern void b2PrismaticJoint_SetSpringHertz(JointId jointId, float hertz);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetSpringHertz")]
    private static extern float b2PrismaticJoint_GetSpringHertz(JointId jointId);

    /// <summary>
    /// The spring frequency in Hertz on this prismatic joint
    /// </summary>
    public float SpringHertz
    {
        get => b2PrismaticJoint_GetSpringHertz(id);
        set => b2PrismaticJoint_SetSpringHertz(id, value);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_SetSpringDampingRatio")]
    private static extern void b2PrismaticJoint_SetSpringDampingRatio(JointId jointId, float dampingRatio);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetSpringDampingRatio")]
    private static extern float b2PrismaticJoint_GetSpringDampingRatio(JointId jointId);

    /// <summary>
    /// The spring damping ratio on this prismatic joint
    /// </summary>
    public float SpringDampingRatio
    {
        get => b2PrismaticJoint_GetSpringDampingRatio(id);
        set => b2PrismaticJoint_SetSpringDampingRatio(id, value);
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_SetTargetTranslation")]
    private static extern void b2PrismaticJoint_SetTargetTranslation(JointId jointId, float translation);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetTargetTranslation")]
    private static extern float b2PrismaticJoint_GetTargetTranslation(JointId jointId);
    
    /// <summary>
    /// The prismatic joint spring target translation
    /// </summary>
    public float TargetTranslation
    {
        get => b2PrismaticJoint_GetTargetTranslation(id);
        set => b2PrismaticJoint_SetTargetTranslation(id, value);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_EnableLimit")]
    private static extern void b2PrismaticJoint_EnableLimit(JointId jointId, byte enableLimit);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_IsLimitEnabled")]
    private static extern byte b2PrismaticJoint_IsLimitEnabled(JointId jointId);

    /// <summary>
    /// The limit enabled state of this prismatic joint
    /// </summary>
    public bool LimitEnabled
    {
        get => b2PrismaticJoint_IsLimitEnabled(id) != 0;
        set => b2PrismaticJoint_EnableLimit(id, value ? (byte)1 : (byte)0);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_SetLimits")]
    private static extern void b2PrismaticJoint_SetLimits(JointId jointId, float lower, float upper);

    /// <summary>
    /// Sets the prismatic joint limits
    /// </summary>
    /// <param name="lower">The lower prismatic joint limit</param>
    /// <param name="upper">The upper prismatic joint limit</param>
    public void SetLimits(float lower, float upper) => b2PrismaticJoint_SetLimits(id, lower, upper);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetLowerLimit")]
    private static extern float b2PrismaticJoint_GetLowerLimit(JointId jointId);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetUpperLimit")]
    private static extern float b2PrismaticJoint_GetUpperLimit(JointId jointId);
    
    /// <summary>
    /// The lower joint limit of this prismatic joint
    /// </summary>
    public float LowerLimit => b2PrismaticJoint_GetLowerLimit(id);
    
    /// <summary>
    /// The upper joint limit of this prismatic joint
    /// </summary>
    public float UpperLimit => b2PrismaticJoint_GetUpperLimit(id);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_EnableMotor")]
    private static extern void b2PrismaticJoint_EnableMotor(JointId jointId, byte enableMotor);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_IsMotorEnabled")]
    private static extern byte b2PrismaticJoint_IsMotorEnabled(JointId jointId);

    /// <summary>
    /// The prismatic joint motor enabled state
    /// </summary>
    public bool MotorEnabled
    {
        get => b2PrismaticJoint_IsMotorEnabled(id) != 0;
        set => b2PrismaticJoint_EnableMotor(id, value ? (byte)1 : (byte)0);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_SetMotorSpeed")]
    private static extern void b2PrismaticJoint_SetMotorSpeed(JointId jointId, float motorSpeed);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetMotorSpeed")]
    private static extern float b2PrismaticJoint_GetMotorSpeed(JointId jointId);

    /// <summary>
    /// The prismatic joint motor speed
    /// </summary>
    public float MotorSpeed
    {
        get => b2PrismaticJoint_GetMotorSpeed(id);
        set => b2PrismaticJoint_SetMotorSpeed(id, value);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_SetMaxMotorForce")]
    private static extern void b2PrismaticJoint_SetMaxMotorForce(JointId jointId, float force);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetMaxMotorForce")]
    private static extern float b2PrismaticJoint_GetMaxMotorForce(JointId jointId);

    /// <summary>
    /// The prismatic joint maximum motor force
    /// </summary>
    public float MaxMotorForce
    {
        get => b2PrismaticJoint_GetMaxMotorForce(id);
        set => b2PrismaticJoint_SetMaxMotorForce(id, value);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetMotorForce")]
    private static extern float b2PrismaticJoint_GetMotorForce(JointId jointId);
    
    /// <summary>
    /// The prismatic joint current motor force
    /// </summary>
    public float MotorForce => b2PrismaticJoint_GetMotorForce(id);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetTranslation")]
    private static extern float b2PrismaticJoint_GetTranslation(JointId jointId);

    /// <summary>
    /// The current joint translation
    /// </summary>
    public float Translation =>  b2PrismaticJoint_GetTranslation(id);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetSpeed")]
    private static extern float b2PrismaticJoint_GetSpeed(JointId jointId);

    /// <summary>
    /// The current joint translation speed
    /// </summary>
    public float Speed => b2PrismaticJoint_GetSpeed(id);
}
