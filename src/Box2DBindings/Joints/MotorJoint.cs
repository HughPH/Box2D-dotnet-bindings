using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// The motor joint is used to drive the relative transform between two bodies. It takes
/// a relative position and rotation and applies the forces and torques needed to achieve
/// that relative transform over time.
/// </summary>
[PublicAPI]
public sealed partial class MotorJoint : Joint
{
    internal MotorJoint(JointId id) : base(id)
    { }
    
    /// <summary>
    /// The linear offset target on this motor joint
    /// </summary>
    public unsafe Vec2 LinearOffset
    {
        get => b2MotorJoint_GetLinearOffset(id);
        set => b2MotorJoint_SetLinearOffset(id, value);
    }

    /// <summary>
    /// The angular offset target in radians on this motor joint
    /// </summary>
    public unsafe float AngularOffset
    {
        get => b2MotorJoint_GetAngularOffset(id);
        set => b2MotorJoint_SetAngularOffset(id, value);
    }

    /// <summary>
    /// The maximum force on this motor joint
    /// </summary>
    public unsafe float MaxForce
    {
        get => b2MotorJoint_GetMaxForce(id);
        set => b2MotorJoint_SetMaxForce(id, value);
    }

    /// <summary>
    /// The maximum torque on this motor joint
    /// </summary>
    public unsafe float MaxTorque
    {
        get => b2MotorJoint_GetMaxTorque(id);
        set => b2MotorJoint_SetMaxTorque(id, value);
    }
    
    /// <summary>
    /// The correction factor on this motor joint
    /// </summary>
    /// <remarks>0 means no correction, 1 means full correction</remarks>
    public unsafe float CorrectionFactor
    {
        get => b2MotorJoint_GetCorrectionFactor(id);
        set => b2MotorJoint_SetCorrectionFactor(id, value);
    }
}
