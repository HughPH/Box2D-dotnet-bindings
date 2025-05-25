using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Joints allow you to connect rigid bodies together while allowing various forms of relative motions.
/// </summary>
[PublicAPI]
public partial class Joint
{
    internal JointId id;

    internal Joint(JointId id)
    {
        this.id = id;
    }

    internal static unsafe Joint GetJoint(JointId id)
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
    
    /// <summary>
    /// Destroys this joint
    /// </summary>
    public unsafe void Destroy()
    {
        if (!Valid) return;
        nint userDataPtr = b2Joint_GetUserData(id);
        FreeHandle(ref userDataPtr);
        b2Joint_SetUserData(id, 0);

        b2DestroyJoint(id);
    }

    /// <summary>
    /// Checks if this joint is valid
    /// </summary>
    /// <returns>true if this joint is valid</returns>
    /// <remarks>Provides validation for up to 64K allocations</remarks>
    public unsafe bool Valid => b2Joint_IsValid(id) != 0;

    /// <summary>
    /// Gets the joint type
    /// </summary>
    /// <returns>The joint type</returns>
    public unsafe JointType Type => Valid ? b2Joint_GetType(id) : throw new InvalidOperationException("Joint is not valid");

    /// <summary>
    /// Gets body A on this joint
    /// </summary>
    /// <returns>The body A on this joint</returns>
    public unsafe Body BodyA => Valid ? Body.GetBody(b2Joint_GetBodyA(id)) : throw new InvalidOperationException("Joint is not valid");

    /// <summary>
    /// Gets body B on this joint
    /// </summary>
    /// <returns>The body B on this joint</returns>
    public unsafe Body BodyB => Valid ? Body.GetBody(b2Joint_GetBodyB(id)) : throw new InvalidOperationException("Joint is not valid");

    /// <summary>
    /// Gets the world that owns this joint
    /// </summary>
    public unsafe World World => Valid ? World.GetWorld(b2Joint_GetWorld(id)) : throw new InvalidOperationException("Joint is not valid");

    /// <summary>
    /// Gets the local anchor on body A
    /// </summary>
    /// <returns>The local anchor on body A</returns>
    public unsafe Vec2 LocalAnchorA => Valid ? b2Joint_GetLocalAnchorA(id) : throw new InvalidOperationException("Joint is not valid");

    /// <summary>
    /// Gets the local anchor on body B
    /// </summary>
    /// <returns>The local anchor on body B</returns>
    public unsafe Vec2 LocalAnchorB => Valid ? b2Joint_GetLocalAnchorB(id) : throw new InvalidOperationException("Joint is not valid");

    /// <summary>
    /// Set this flag to true if the attached bodies should collide
    /// </summary>
    public unsafe bool CollideConnected
    {
        get => Valid ? b2Joint_GetCollideConnected(id) != 0 : throw new InvalidOperationException("Joint is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Joint is not valid");
            b2Joint_SetCollideConnected(id, value ? (byte)1 : (byte)0);
        }
    }

    /// <summary>
    /// The user data object for this joint.
    /// </summary>
    public unsafe object? UserData
    {
        get => Valid ? GetObjectAtPointer(b2Joint_GetUserData, id) : throw new InvalidOperationException("Joint is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Joint is not valid");
            SetObjectAtPointer(b2Joint_GetUserData, b2Joint_SetUserData, id, value);
        }
    }

    /// <summary>
    /// Wakes the bodies connected to this joint
    /// </summary>
    public unsafe void WakeBodies() => b2Joint_WakeBodies(id);

    /// <summary>
    /// Gets the current constraint force for this joint
    /// </summary>
    /// <returns>The current constraint force for this joint</returns>
    /// <remarks>Usually in Newtons</remarks>
    public unsafe Vec2 ConstraintForce => Valid ? b2Joint_GetConstraintForce(id) : throw new InvalidOperationException("Joint is not valid");

    /// <summary>
    /// Gets the current constraint torque for this joint
    /// </summary>
    /// <returns>The current constraint torque for this joint</returns>
    /// <remarks>Usually in Newton * meters</remarks>
    public unsafe float ConstraintTorque => Valid ? b2Joint_GetConstraintTorque(id) : throw new InvalidOperationException("Joint is not valid");
}
