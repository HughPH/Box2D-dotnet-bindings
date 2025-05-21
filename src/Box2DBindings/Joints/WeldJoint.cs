using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A weld joint fully constrains the relative transform between two bodies while allowing for springiness
/// A weld joint constrains the relative rotation and translation between two bodies. Both rotation and translation
/// can have damped springs.<br/>
/// <b>Note: The accuracy of weld joint is limited by the accuracy of the solver. Long chains of weld joints may flex.</b>
/// </summary>
[PublicAPI]
public sealed partial class WeldJoint : Joint
{
    internal WeldJoint(JointId id) : base(id)
    { }

    /// <summary>
    /// The reference angle in radians on this weld joint
    /// </summary>
    public unsafe float ReferenceAngle
    {
        get => b2WeldJoint_GetReferenceAngle(id);
        set
        {
            float angleInRadians = MathF.IEEERemainder(value, MathF.PI * 2);
            b2WeldJoint_SetReferenceAngle(id, angleInRadians);
        }
    }

    /// <summary>
    /// The weld joint linear stiffness in Hertz.
    /// </summary>
    public unsafe float LinearHertz
    {
        get => b2WeldJoint_GetLinearHertz(id);
        set => b2WeldJoint_SetLinearHertz(id, value);
    }

    /// <summary>
    /// The weld joint linear damping ratio.
    /// </summary>
    public unsafe float LinearDampingRatio
    {
        get => b2WeldJoint_GetLinearDampingRatio(id);
        set => b2WeldJoint_SetLinearDampingRatio(id, value);
    }

    /// <summary>
    /// The weld joint angular stiffness in Hertz.
    /// </summary>
    public unsafe float AngularHertz
    {
        get => b2WeldJoint_GetAngularHertz(id);
        set => b2WeldJoint_SetAngularHertz(id, value);
    }

    /// <summary>
    /// The weld joint angular damping ratio.
    /// </summary>
    public unsafe float AngularDampingRatio
    {
        get => b2WeldJoint_GetAngularDampingRatio(id);
        set => b2WeldJoint_SetAngularDampingRatio(id, value);
    }
}
