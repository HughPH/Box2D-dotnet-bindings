using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Joints allow you to connect rigid bodies together while allowing various forms of relative motions.
/// </summary>
[PublicAPI]
public class Joint
{
    internal JointId id;

    internal Joint(JointId id)
    {
        this.id = id;
    }

    internal static Joint GetJoint(JointId id)
    {
        JointType t = b2Joint_GetType(id);
        switch (t)
        {
            case JointType.Distance:
                return new DistanceJoint(id);
            case JointType.Motor:
                return new MotorJoint(id);
            case JointType.Mouse:
                return new MouseJoint(id);
            case JointType.Prismatic:
                return new PrismaticJoint(id);
            case JointType.Revolute:
                return new RevoluteJoint(id);
            case JointType.Weld:
                return new WeldJoint(id);
            case JointType.Wheel:
                return new WheelJoint(id);
            case JointType.Filter:
                return new(id);
            default:
                throw new NotSupportedException($"Joint type {t} is not supported");

        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DestroyJoint")]
    private static extern void b2DestroyJoint(JointId jointId);

    /// <summary>
    /// Destroys this joint
    /// </summary>
    public void Destroy()
    {
        nint userDataPtr = b2Joint_GetUserData(id);
        FreeHandle(ref userDataPtr);
        b2Joint_SetUserData(id, 0);

        b2DestroyJoint(id);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_IsValid")]
    private static extern byte b2Joint_IsValid(JointId jointId);

    /// <summary>
    /// Checks if this joint is valid
    /// </summary>
    /// <returns>true if this joint is valid</returns>
    /// <remarks>Provides validation for up to 64K allocations</remarks>
    public bool Valid => b2Joint_IsValid(id) != 0;

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetType")]
    private static extern JointType b2Joint_GetType(JointId jointId);

    /// <summary>
    /// Gets the joint type
    /// </summary>
    /// <returns>The joint type</returns>
    public JointType Type => Valid ? b2Joint_GetType(id) : throw new InvalidOperationException("Joint is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetBodyA")]
    private static extern Body b2Joint_GetBodyA(JointId jointId);

    /// <summary>
    /// Gets body A on this joint
    /// </summary>
    /// <returns>The body A on this joint</returns>
    public Body BodyA => Valid ? b2Joint_GetBodyA(id) : throw new InvalidOperationException("Joint is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetBodyB")]
    private static extern Body b2Joint_GetBodyB(JointId jointId);

    /// <summary>
    /// Gets body B on this joint
    /// </summary>
    /// <returns>The body B on this joint</returns>
    public Body BodyB => Valid ? b2Joint_GetBodyB(id) : throw new InvalidOperationException("Joint is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetWorld")]
    private static extern WorldId b2Joint_GetWorld(JointId jointId);

    /// <summary>
    /// Gets the world that owns this joint
    /// </summary>
    public World World => Valid ? World.GetWorld(b2Joint_GetWorld(id)) : throw new InvalidOperationException("Joint is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetLocalAnchorA")]
    private static extern Vec2 b2Joint_GetLocalAnchorA(JointId jointId);

    /// <summary>
    /// Gets the local anchor on body A
    /// </summary>
    /// <returns>The local anchor on body A</returns>
    public Vec2 LocalAnchorA => Valid ? b2Joint_GetLocalAnchorA(id) : throw new InvalidOperationException("Joint is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetLocalAnchorB")]
    private static extern Vec2 b2Joint_GetLocalAnchorB(JointId jointId);

    /// <summary>
    /// Gets the local anchor on body B
    /// </summary>
    /// <returns>The local anchor on body B</returns>
    public Vec2 LocalAnchorB => Valid ? b2Joint_GetLocalAnchorB(id) : throw new InvalidOperationException("Joint is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_SetCollideConnected")]
    private static extern void b2Joint_SetCollideConnected(JointId jointId, byte shouldCollide);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetCollideConnected")]
    private static extern byte b2Joint_GetCollideConnected(JointId jointId);

    /// <summary>
    /// Set this flag to true if the attached bodies should collide
    /// </summary>
    public bool CollideConnected
    {
        get => Valid ? b2Joint_GetCollideConnected(id) != 0 : throw new InvalidOperationException("Joint is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Joint is not valid");
            b2Joint_SetCollideConnected(id, value ? (byte)1 : (byte)0);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_SetUserData")]
    private static extern void b2Joint_SetUserData(JointId jointId, nint userData);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetUserData")]
    private static extern nint b2Joint_GetUserData(JointId jointId);

    /// <summary>
    /// The user data object for this joint.
    /// </summary>
    public object? UserData
    {
        get => Valid ? GetObjectAtPointer(b2Joint_GetUserData, id) : throw new InvalidOperationException("Joint is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Joint is not valid");
            SetObjectAtPointer(b2Joint_GetUserData, b2Joint_SetUserData, id, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_WakeBodies")]
    private static extern void b2Joint_WakeBodies(JointId jointId);

    /// <summary>
    /// Wakes the bodies connected to this joint
    /// </summary>
    public void WakeBodies() => b2Joint_WakeBodies(id);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetConstraintForce")]
    private static extern Vec2 b2Joint_GetConstraintForce(JointId jointId);

    /// <summary>
    /// Gets the current constraint force for this joint
    /// </summary>
    /// <returns>The current constraint force for this joint</returns>
    /// <remarks>Usually in Newtons</remarks>
    public Vec2 ConstraintForce => Valid ? b2Joint_GetConstraintForce(id) : throw new InvalidOperationException("Joint is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetConstraintTorque")]
    private static extern float b2Joint_GetConstraintTorque(JointId jointId);

    /// <summary>
    /// Gets the current constraint torque for this joint
    /// </summary>
    /// <returns>The current constraint torque for this joint</returns>
    /// <remarks>Usually in Newton * meters</remarks>
    public float ConstraintTorque => Valid ? b2Joint_GetConstraintTorque(id) : throw new InvalidOperationException("Joint is not valid");
}
