using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// The mouse joint is designed for use in the samples application, but you may find it useful in applications where
/// the user moves a rigid body with a cursor.
/// </summary>
[PublicAPI]
public sealed partial class MouseJoint:Joint
{

    internal MouseJoint(JointId id) : base(id)
    { }
    
    /// <summary>
    /// The target point on this mouse joint
    /// </summary>
    public unsafe Vec2 Target
    {
        get => b2MouseJoint_GetTarget(id);
        set => b2MouseJoint_SetTarget(id, value);
    }

    /// <summary>
    /// The spring frequency on this mouse joint
    /// </summary>
    public unsafe float SpringHertz
    {
        get => b2MouseJoint_GetSpringHertz(id);
        set => b2MouseJoint_SetSpringHertz(id, value);
    }
    /// <summary>
    /// The spring damping ratio on this mouse joint
    /// </summary>
    public unsafe float SpringDampingRatio
    {
        get => b2MouseJoint_GetSpringDampingRatio(id);
        set => b2MouseJoint_SetSpringDampingRatio(id, value);
    }
    
    /// <summary>
    /// The maximum force on this mouse joint
    /// </summary>
    public unsafe float MaxForce
    {
        get => b2MouseJoint_GetMaxForce(id);
        set => b2MouseJoint_SetMaxForce(id, value);
    }
}