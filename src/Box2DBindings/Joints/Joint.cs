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
    public unsafe Body BodyA => Valid ? b2Joint_GetBodyA(id) : throw new InvalidOperationException("Joint is not valid");

    /// <summary>
    /// Gets body B on this joint
    /// </summary>
    /// <returns>The body B on this joint</returns>
    public unsafe Body BodyB => Valid ? b2Joint_GetBodyB(id) : throw new InvalidOperationException("Joint is not valid");

    /// <summary>
    /// Gets the world that owns this joint
    /// </summary>
    public unsafe World World => Valid ? World.GetWorld(b2Joint_GetWorld(id)) : throw new InvalidOperationException("Joint is not valid");

    /// <summary>
    /// The local anchor on body A
    /// </summary>
    public unsafe Vec2 LocalAnchorA
    {
        get => Valid ? b2Joint_GetLocalAnchorA(id) : throw new InvalidOperationException("Joint is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Joint is not valid");
            b2Joint_SetLocalAnchorA(id, value);
        }
    }

    /// <summary>
    /// The local anchor on body B
    /// </summary>
    public unsafe Vec2 LocalAnchorB
    {
        get => Valid ? b2Joint_GetLocalAnchorB(id) : throw new InvalidOperationException("Joint is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Joint is not valid");
            b2Joint_SetLocalAnchorB(id, value);
        }
    }

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

    /// <summary>
    /// Gets the current linear separation error for this joint
    /// </summary>
    /// <remarks>Does not consider admissible movement. Usually in meters.</remarks>
    public unsafe float LinearSeparation => Valid ? b2Joint_GetLinearSeparation(id) : throw new InvalidOperationException("Joint is not valid");

    /// <summary>
    /// Gets the current angular separation error for this joint
    /// </summary>
    /// <remarks>Does not consider admissible movement. Usually in meters.</remarks>
    public unsafe float AngularSeparation => Valid ? b2Joint_GetAngularSeparation(id) : throw new InvalidOperationException("Joint is not valid");

    /// <summary>
    /// The reference angle in radians for joints that support it.
    /// </summary>
    public unsafe float ReferenceAngle
    {
        get => Valid ? b2Joint_GetReferenceAngle(id) : throw new InvalidOperationException("Joint is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Joint is not valid");
            float angleInRadians = MathF.IEEERemainder(value, MathF.PI * 2);
            b2Joint_SetReferenceAngle(id, angleInRadians);
        }
    }

    /// <summary>
    /// The local axis on body A for joints that support it.
    /// </summary>
    public unsafe Vec2 LocalAxisA
    {
        get => Valid ? b2Joint_GetLocalAxisA(id) : throw new InvalidOperationException("Joint is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Joint is not valid");
            b2Joint_SetLocalAxisA(id, value);
        }
    }

    /// <summary>
    /// Gets or sets the joint constraint tuning in Hertz and damping ratio. Advanced feature.
    /// </summary>
    /// <exception cref="InvalidOperationException">If the joint is not valid</exception>
    public unsafe (float hertz, float dampingRatio) ConstraintTuning
    {
        get 
        {
            if (!Valid)
                throw new InvalidOperationException("Joint is not valid");
            b2Joint_GetConstraintTuning(id, out float hertz, out float dampingRatio);
            return (hertz, dampingRatio);
        }
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Joint is not valid");
            b2Joint_SetConstraintTuning(id, value.hertz, value.dampingRatio);
        }
    }
    
}
