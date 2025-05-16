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
public sealed class WeldJoint : Joint
{
    internal WeldJoint(JointId id) : base(id)
    { }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_GetReferenceAngle")]
    private static extern float b2WeldJoint_GetReferenceAngle(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_SetReferenceAngle")]
    private static extern void b2WeldJoint_SetReferenceAngle(JointId jointId, float angleInRadians);

    /// <summary>
    /// The reference angle in radians on this weld joint
    /// </summary>
    public float ReferenceAngle
    {
        get => b2WeldJoint_GetReferenceAngle(id);
        set
        {
            float angleInRadians = MathF.IEEERemainder(value, MathF.PI * 2);
            b2WeldJoint_SetReferenceAngle(id, angleInRadians);
        }
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_SetLinearHertz")]
    private static extern void b2WeldJoint_SetLinearHertz(JointId jointId, float hertz);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_GetLinearHertz")]
    private static extern float b2WeldJoint_GetLinearHertz(JointId jointId);

    /// <summary>
    /// The weld joint linear stiffness in Hertz.
    /// </summary>
    public float LinearHertz
    {
        get => b2WeldJoint_GetLinearHertz(id);
        set => b2WeldJoint_SetLinearHertz(id, value);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_SetLinearDampingRatio")]
    private static extern void b2WeldJoint_SetLinearDampingRatio(JointId jointId, float dampingRatio);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_GetLinearDampingRatio")]
    private static extern float b2WeldJoint_GetLinearDampingRatio(JointId jointId);

    /// <summary>
    /// The weld joint linear damping ratio.
    /// </summary>
    public float LinearDampingRatio
    {
        get => b2WeldJoint_GetLinearDampingRatio(id);
        set => b2WeldJoint_SetLinearDampingRatio(id, value);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_SetAngularHertz")]
    private static extern void b2WeldJoint_SetAngularHertz(JointId jointId, float hertz);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_GetAngularHertz")]
    private static extern float b2WeldJoint_GetAngularHertz(JointId jointId);

    /// <summary>
    /// The weld joint angular stiffness in Hertz.
    /// </summary>
    public float AngularHertz
    {
        get => b2WeldJoint_GetAngularHertz(id);
        set => b2WeldJoint_SetAngularHertz(id, value);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_SetAngularDampingRatio")]
    private static extern void b2WeldJoint_SetAngularDampingRatio(JointId jointId, float dampingRatio);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_GetAngularDampingRatio")]
    private static extern float b2WeldJoint_GetAngularDampingRatio(JointId jointId);

    /// <summary>
    /// The weld joint angular damping ratio.
    /// </summary>
    public float AngularDampingRatio
    {
        get => b2WeldJoint_GetAngularDampingRatio(id);
        set => b2WeldJoint_SetAngularDampingRatio(id, value);
    }
}
