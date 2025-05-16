using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A distance joint.<br/>
/// This requires defining an anchor point on both
/// bodies and the non-zero distance of the distance joint. The definition uses
/// local anchor points so that the initial configuration can violate the
/// constraint slightly. This helps when saving and loading a game.
/// </summary>
[PublicAPI]
public sealed class DistanceJoint : Joint
{
    internal DistanceJoint(JointId id) : base(id)
    { }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_SetLength")]
    private static extern void b2DistanceJoint_SetLength(JointId jointId, float length);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetLength")]
    private static extern float b2DistanceJoint_GetLength(JointId jointId);
    
    /// <summary>
    /// The rest length of this distance joint
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__distance__joint.html#ga17fcd4784910ddd991f9d0e6c975ceba">b2DistanceJoint_SetLength</a> and <a href="https://box2d.org/documentation/group__distance__joint.html#ga43f571dd774975325214babc9bf11662">b2DistanceJoint_GetLength</a></remarks>
    public float Length
    {
        get => b2DistanceJoint_GetLength(id);
        set => b2DistanceJoint_SetLength(id, value);
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_EnableSpring")]
    private static extern void b2DistanceJoint_EnableSpring(JointId jointId, byte enableSpring);
    
    /// <summary>
    /// Enables/disables the spring on this distance joint
    /// </summary>
    /// <param name="enableSpring">True to enable the spring, false to disable the spring</param>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__distance__joint.html#gabdc83110a2fd49707be8906b94d17153">b2DistanceJoint_EnableSpring</a></remarks>
    public void EnableSpring(bool enableSpring) =>
        b2DistanceJoint_EnableSpring(id, enableSpring ? (byte)1 : (byte)0);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_IsSpringEnabled")]
    private static extern byte b2DistanceJoint_IsSpringEnabled(JointId jointId);
    
    /// <summary>
    /// Gets or sets the spring enabled state on this distance joint
    /// </summary>
    /// <returns>True if the spring is enabled</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__distance__joint.html#gab383facf14041ef867654de80ae0621f">b2DistanceJoint_IsSpringEnabled</a> and <a href="https://box2d.org/documentation/group__distance__joint.html#gabdc83110a2fd49707be8906b94d17153">b2DistanceJoint_EnableSpring</a></remarks>
    public bool SpringEnabled
    {
        get => b2DistanceJoint_IsSpringEnabled(id) != 0;
        set => EnableSpring(value);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_SetSpringHertz")]
    private static extern void b2DistanceJoint_SetSpringHertz(JointId jointId, float hertz);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetSpringHertz")]
    private static extern float b2DistanceJoint_GetSpringHertz(JointId jointId);
    
    /// <summary>
    /// The spring stiffness in Hertz on this distance joint
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__distance__joint.html#ga2ad68a17008f910d5f7eecc291d5803e">b2DistanceJoint_SetSpringHertz</a> and <a href="https://box2d.org/documentation/group__distance__joint.html#gae4a8e1fb45835ab3c5ec065a770e36f9">b2DistanceJoint_GetSpringHertz</a></remarks>
    public float SpringHertz
    {
        get => b2DistanceJoint_GetSpringHertz(id);
        set => b2DistanceJoint_SetSpringHertz(id, value);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_SetSpringDampingRatio")]
    private static extern void b2DistanceJoint_SetSpringDampingRatio(JointId jointId, float dampingRatio);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetSpringDampingRatio")]
    private static extern float b2DistanceJoint_GetSpringDampingRatio(JointId jointId);
    
    /// <summary>
    /// The spring damping ratio on this distance joint
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__distance__joint.html#gafcc357bc246e02d3095fdf6f73960d14">b2DistanceJoint_SetSpringDampingRatio</a> and <a href="https://box2d.org/documentation/group__distance__joint.html#ga51cc2a3e4906f75d3b460a6e2e248704">b2DistanceJoint_GetSpringDampingRatio</a></remarks>
    public float SpringDampingRatio
    {
        get => b2DistanceJoint_GetSpringDampingRatio(id);
        set => b2DistanceJoint_SetSpringDampingRatio(id, value);
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_EnableLimit")]
    private static extern void b2DistanceJoint_EnableLimit(JointId jointId, byte enableLimit);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_IsLimitEnabled")]
    private static extern byte b2DistanceJoint_IsLimitEnabled(JointId jointId);
    
    /// <summary>
    /// The limit enabled state of this distance joint
    /// </summary>
    /// <remarks>The limit only works if the joint spring is enabled. Otherwise the joint is rigid and the limit has no effect<br/>
    /// This wraps <a href="https://box2d.org/documentation/group__distance__joint.html#ga1cbba6cf523da0b02a3332b5140622e9">b2DistanceJoint_EnableLimit</a> and <a href="https://box2d.org/documentation/group__distance__joint.html#ga2641408993ae1e2eed4760afe768b225">b2DistanceJoint_IsLimitEnabled</a>
    /// </remarks>
    public bool LimitEnabled
    {
        get => b2DistanceJoint_IsLimitEnabled(id) != 0;
        set => b2DistanceJoint_EnableLimit(id, value ? (byte)1 : (byte)0);
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_SetLengthRange")]
    private static extern void b2DistanceJoint_SetLengthRange(JointId jointId, float minLength, float maxLength);
    
    /// <summary>
    /// Sets the minimum and maximum length parameters on this distance joint
    /// </summary>
    /// <param name="minLength">The minimum length</param>
    /// <param name="maxLength">The maximum length</param>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__distance__joint.html#ga32670b5806562fdf63187f4c432fe4b6">b2DistanceJoint_SetLengthRange</a></remarks>
    public void SetLengthRange(float minLength, float maxLength) => b2DistanceJoint_SetLengthRange(id, minLength, maxLength);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetMinLength")]
    private static extern float b2DistanceJoint_GetMinLength(JointId jointId);
    
    /// <summary>
    /// Gets the minimum length of this distance joint
    /// </summary>
    /// <returns>The minimum length</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__distance__joint.html#gad62e130b6c21de81f4732def25a0b5bc">b2DistanceJoint_GetMinLength</a></remarks>
    public float MinLength => b2DistanceJoint_GetMinLength(id);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetMaxLength")]
    private static extern float b2DistanceJoint_GetMaxLength(JointId jointId);
    
    /// <summary>
    /// Gets the maximum length of this distance joint
    /// </summary>
    /// <returns>The maximum length</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__distance__joint.html#ga461bd2689440b95a86ff2ecc0c33cbf1">b2DistanceJoint_GetMaxLength</a></remarks>
    public float MaxLength => b2DistanceJoint_GetMaxLength(id);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetCurrentLength")]
    private static extern float b2DistanceJoint_GetCurrentLength(JointId jointId);
    
    /// <summary>
    /// Gets the current length of this distance joint
    /// </summary>
    /// <returns>The current length</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__distance__joint.html#gaab14313fba3dc87b5fbd0b6fd2cd23f4">b2DistanceJoint_GetCurrentLength</a></remarks>
    public float CurrentLength => b2DistanceJoint_GetCurrentLength(id);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_EnableMotor")]
    private static extern void b2DistanceJoint_EnableMotor(JointId jointId, byte enableMotor);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_IsMotorEnabled")]
    private static extern byte b2DistanceJoint_IsMotorEnabled(JointId jointId);

    /// <summary>
    /// The motor enabled state of this distance joint
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__distance__joint.html#ga9cb9c9bdab2c1fa30ffef81e932cf0a7">b2DistanceJoint_EnableMotor</a> and <a href="https://box2d.org/documentation/group__distance__joint.html#gae4a37dd51ddba9b3a788e114c5728889">b2DistanceJoint_IsMotorEnabled</a></remarks>
    public bool MotorEnabled
    {
        get => b2DistanceJoint_IsMotorEnabled(id) != 0;
        set => b2DistanceJoint_EnableMotor(id, value ? (byte)1 : (byte)0);
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_SetMotorSpeed")]
    private static extern void b2DistanceJoint_SetMotorSpeed(JointId jointId, float motorSpeed);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetMotorSpeed")]
    private static extern float b2DistanceJoint_GetMotorSpeed(JointId jointId);

    /// <summary>
    /// The desired motor speed, usually in meters per second
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__distance__joint.html#ga373762ada7aff1e5271aa07fd9758b50">b2DistanceJoint_SetMotorSpeed</a> and <a href="https://box2d.org/documentation/group__distance__joint.html#ga5e0499361b64c578a0464dcbe4fe2868">b2DistanceJoint_GetMotorSpeed</a></remarks>
    public float MotorSpeed
    {
        get => b2DistanceJoint_GetMotorSpeed(id);
        set => b2DistanceJoint_SetMotorSpeed(id, value);
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_SetMaxMotorForce")]
    private static extern void b2DistanceJoint_SetMaxMotorForce(JointId jointId, float force);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetMaxMotorForce")]
    private static extern float b2DistanceJoint_GetMaxMotorForce(JointId jointId);
    
    /// <summary>
    /// The maximum motor force on this distance joint
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__distance__joint.html#gaac05d93d64a7053870b354ed430502a6">b2DistanceJoint_SetMaxMotorForce</a> and <a href="https://box2d.org/documentation/group__distance__joint.html#gad6ef595390c6ba6efad18df3b0d1b448">b2DistanceJoint_GetMaxMotorForce</a></remarks>
    public float MaxMotorForce
    {
        get => b2DistanceJoint_GetMaxMotorForce(id);
        set => b2DistanceJoint_SetMaxMotorForce(id, value);
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetMotorForce")]
    private static extern float b2DistanceJoint_GetMotorForce(JointId jointId);
    
    /// <summary>
    /// Gets the current motor force on this distance joint
    /// </summary>
    /// <returns>The current motor force, usually in Newtons</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__distance__joint.html#ga1a3a35a9458bd0c5b171c21f9bb5c137">b2DistanceJoint_GetMotorForce</a></remarks>
    public float MotorForce => b2DistanceJoint_GetMotorForce(id);
}