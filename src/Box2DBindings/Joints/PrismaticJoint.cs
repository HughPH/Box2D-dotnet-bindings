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
public sealed partial class PrismaticJoint : Joint
{
    internal PrismaticJoint(JointId id) : base(id)
    { }


    /// <summary>
    /// Gets or sets the prismatic joint spring enabled state
    /// </summary>
    /// <returns>True if the prismatic joint spring is enabled</returns>
    public unsafe bool SpringEnabled
    {
        get => b2PrismaticJoint_IsSpringEnabled(id) != 0;
        set => b2PrismaticJoint_EnableSpring(id, value ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// The spring frequency in Hertz on this prismatic joint
    /// </summary>
    public unsafe float SpringHertz
    {
        get => b2PrismaticJoint_GetSpringHertz(id);
        set => b2PrismaticJoint_SetSpringHertz(id, value);
    }

    /// <summary>
    /// The spring damping ratio on this prismatic joint
    /// </summary>
    public unsafe float SpringDampingRatio
    {
        get => b2PrismaticJoint_GetSpringDampingRatio(id);
        set => b2PrismaticJoint_SetSpringDampingRatio(id, value);
    }
    
    
    /// <summary>
    /// The prismatic joint spring target translation
    /// </summary>
    public unsafe float TargetTranslation
    {
        get => b2PrismaticJoint_GetTargetTranslation(id);
        set => b2PrismaticJoint_SetTargetTranslation(id, value);
    }

    /// <summary>
    /// The limit enabled state of this prismatic joint
    /// </summary>
    public unsafe bool LimitEnabled
    {
        get => b2PrismaticJoint_IsLimitEnabled(id) != 0;
        set => b2PrismaticJoint_EnableLimit(id, value ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// Sets the prismatic joint limits
    /// </summary>
    /// <param name="lower">The lower prismatic joint limit</param>
    /// <param name="upper">The upper prismatic joint limit</param>
    public unsafe void SetLimits(float lower, float upper) => b2PrismaticJoint_SetLimits(id, lower, upper);
    
    /// <summary>
    /// The lower joint limit of this prismatic joint
    /// </summary>
    public unsafe float LowerLimit => b2PrismaticJoint_GetLowerLimit(id);
    
    /// <summary>
    /// The upper joint limit of this prismatic joint
    /// </summary>
    public unsafe float UpperLimit => b2PrismaticJoint_GetUpperLimit(id);

    /// <summary>
    /// The prismatic joint motor enabled state
    /// </summary>
    public unsafe bool MotorEnabled
    {
        get => b2PrismaticJoint_IsMotorEnabled(id) != 0;
        set => b2PrismaticJoint_EnableMotor(id, value ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// The prismatic joint motor speed
    /// </summary>
    public unsafe float MotorSpeed
    {
        get => b2PrismaticJoint_GetMotorSpeed(id);
        set => b2PrismaticJoint_SetMotorSpeed(id, value);
    }

    /// <summary>
    /// The prismatic joint maximum motor force
    /// </summary>
    public unsafe float MaxMotorForce
    {
        get => b2PrismaticJoint_GetMaxMotorForce(id);
        set => b2PrismaticJoint_SetMaxMotorForce(id, value);
    }
    
    /// <summary>
    /// The prismatic joint current motor force
    /// </summary>
    public unsafe float MotorForce => b2PrismaticJoint_GetMotorForce(id);

    /// <summary>
    /// The current joint translation
    /// </summary>
    public unsafe float Translation =>  b2PrismaticJoint_GetTranslation(id);

    /// <summary>
    /// The current joint translation speed
    /// </summary>
    public unsafe float Speed => b2PrismaticJoint_GetSpeed(id);
}
