using Box2D.Comparers;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A Box2D physics body. The body's transform is the base transform for all
/// shapes attached to this body.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public struct Body : IEquatable<Body>, IComparable<Body>
{
    internal int index1;
    private ushort world0;
    private ushort generation;

    /// <summary>
    /// Create a body in the supplied world using the suppled BodyDef
    /// </summary>
    /// <param name="world">The world in which to create the body</param>
    /// <param name="def">The BodyDef to use to create the body</param>
    public Body(World world, BodyDef def)
    {
        this = world.CreateBody(def);
    }
    
    /// <summary>
    /// Default comparer for Body instances. This is used for sorting and
    /// comparing Body instances. Using the default comparer is recommended
    /// to avoid boxing and unboxing overhead.
    /// </summary>
    public static IComparer<Body> DefaultComparer { get; } = BodyComparer.Instance;
    
    /// <summary>
    /// Default equality comparer for Body instances. This is used for
    /// comparing Body instances. Using the default equality comparer is
    /// recommended to avoid boxing and unboxing overhead.
    /// </summary>
    public static IEqualityComparer<Body> DefaultEqualityComparer { get; } = BodyComparer.Instance;

    /// <summary>
    /// Check if two Body objects represent the same Body.
    /// </summary>
    /// <param name="other">The other Body object to compare with</param>
    /// <returns>True if the two Body objects are equal, false otherwise</returns>
    public bool Equals(Body other) => index1 == other.index1 && world0 == other.world0 && generation == other.generation;
    
    /// <summary>
    /// Check if this Body object is equal to another object.
    /// </summary>
    /// <param name="obj">The object to compare with</param>
    /// <returns>True if the object is a Body and equal to this Body, false otherwise</returns>
    public override bool Equals(object? obj) => obj is Body other && Equals(other);
    
    /// <summary>
    /// Get the hash code for this Body object.
    /// </summary>
    /// <returns>The hash code for this Body object</returns>
    public override int GetHashCode() => HashCode.Combine(index1, world0, generation);

    // equality operator
    /// <summary>
    /// Check if two Body objects are equal.
    /// </summary>
    /// <param name="left">The first Body object</param>
    /// <param name="right">The second Body object</param>
    /// <returns>True if the two Body objects are equal, false otherwise</returns>
    public static bool operator ==(Body left, Body right) => left.Equals(right);
    
    /// <summary>
    /// Check if two Body objects are not equal
    /// </summary>
    /// <param name="left">The first Body object</param>
    /// <param name="right">The second Body object</param>
    /// <returns>True if the two Body objects are not equal, false otherwise</returns>
    public static bool operator !=(Body left, Body right) => !(left == right);
    
    /// <summary>
    /// Check if this Body object refers to the same Body as another Body object.
    /// </summary>
    /// <param name="other">The other Body object to compare with</param>
    /// <returns>True if this Body object refers to the same Body as the other Body object, false otherwise</returns>
    public bool ReferenceEquals(Body other) => this == other;

    /// <summary>
    /// Compare this Body object with another Body object.
    /// </summary>
    /// <param name="other">The other Body object to compare with</param>
    /// <returns>A negative number if this Body object should be ordered before the other Body object,
    /// a positive number if this Body object should be ordered after the other Body object,
    /// or zero if they are equal</returns>
    public int CompareTo(Body other)
    {
        int index1Comparison = index1.CompareTo(other.index1);
        if (index1Comparison != 0)
            return index1Comparison;
        int world0Comparison = world0.CompareTo(other.world0);
        if (world0Comparison != 0)
            return world0Comparison;
        return generation.CompareTo(other.generation);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DestroyBody")]
    private static extern void b2DestroyBody(Body bodyId);

    /// <summary>
    /// Destroy this body.
    /// </summary>
    /// <remarks>This destroys all shapes and joints attached to the body. Do not keep references to the associated shapes and joints</remarks>
    public void Destroy()
    {
        if (!Valid) return;
        // remove self from world
        World.bodies.Remove(this);

        // dealloc user data
        nint userDataPtr = b2Body_GetUserData(this);
        FreeHandle(ref userDataPtr);
        b2Body_SetUserData(this, 0);

        b2DestroyBody(this);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsValid")]
    private static extern byte b2Body_IsValid(Body bodyId);

    /// <summary>
    /// Body identifier validation.
    /// </summary>
    /// <returns>True if the body id is valid</returns>
    /// <remarks>Can be used to detect orphaned ids. Provides validation for up to 64K allocations</remarks>
    public bool Valid => b2Body_IsValid(this) != 0;

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetType")]
    private static extern BodyType b2Body_GetType(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetType")]
    private static extern void b2Body_SetType(Body bodyId, BodyType type);

    /// <summary>
    /// The body type: static, kinematic, or dynamic.
    /// </summary>
    public BodyType Type
    {
        get => Valid ? b2Body_GetType(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetType(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetName")]
    private static extern void b2Body_SetName(Body bodyId, string? name);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetName")]
    private static extern nint b2Body_GetName(Body bodyId);

    /// <summary>
    /// The body name.
    /// </summary>
    public string? Name
    {
        get => Valid ? Marshal.PtrToStringAnsi(b2Body_GetName(this)) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetName(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetUserData")]
    private static extern void b2Body_SetUserData(Body bodyId, nint userData);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetUserData")]
    private static extern nint b2Body_GetUserData(Body bodyId);

    /// <summary>
    /// The user data object for this body.
    /// </summary>
    public object? UserData
    {
        get => Valid ? GetObjectAtPointer(b2Body_GetUserData, this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            SetObjectAtPointer(b2Body_GetUserData, b2Body_SetUserData, this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetPosition")]
    private static extern Vec2 b2Body_GetPosition(Body bodyId);

    /// <summary>
    /// The world position of the body.
    /// </summary>
    /// <remarks>This is the location of the body origin</remarks>
    public Vec2 Position => Valid ? b2Body_GetPosition(this) : throw new InvalidOperationException("Body is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetRotation")]
    private static extern Rotation b2Body_GetRotation(Body bodyId);

    /// <summary>
    /// The world rotation of this body as a cosine/sine pair (complex number).
    /// </summary>
    public Rotation Rotation => Valid ? b2Body_GetRotation(this) : throw new InvalidOperationException("Body is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetTransform")]
    private static extern Transform b2Body_GetTransform(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetTransform")]
    private static extern void b2Body_SetTransform(Body bodyId, Vec2 position, Rotation rotation);

    /// <summary>
    /// The world transform of this body.
    /// </summary>
    /// <remarks>Setting this acts as a teleport and is fairly expensive.<br/>
    /// <i>Note: Generally you should create a body with the intended transform.</i></remarks>
    public Transform Transform
    {
        get => Valid ? b2Body_GetTransform(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetTransform(this, value.Position, value.Rotation);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalPoint")]
    private static extern Vec2 b2Body_GetLocalPoint(Body bodyId, Vec2 worldPoint);

    /// <summary>
    /// Get a local point on a body given a world point
    /// </summary>
    /// <param name="worldPoint">The world point</param>
    /// <returns>The local point on the body</returns>
    public Vec2 GetLocalPoint(Vec2 worldPoint) => Valid ? b2Body_GetLocalPoint(this, worldPoint) : throw new InvalidOperationException("Body is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldPoint")]
    private static extern Vec2 b2Body_GetWorldPoint(Body bodyId, Vec2 localPoint);

    /// <summary>
    /// Get a world point on a body given a local point
    /// </summary>
    /// <param name="localPoint">The local point</param>
    /// <returns>The world point on the body</returns>
    public Vec2 GetWorldPoint(Vec2 localPoint) => Valid ? b2Body_GetWorldPoint(this, localPoint) : throw new InvalidOperationException("Body is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalVector")]
    private static extern Vec2 b2Body_GetLocalVector(Body bodyId, Vec2 worldVector);

    /// <summary>
    /// Get a local vector on a body given a world vector
    /// </summary>
    /// <param name="worldVector">The world vector</param>
    /// <returns>The local vector on the body</returns>
    public Vec2 GetLocalVector(Vec2 worldVector) => Valid ? b2Body_GetLocalVector(this, worldVector) : throw new InvalidOperationException("Body is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldVector")]
    private static extern Vec2 b2Body_GetWorldVector(Body bodyId, Vec2 localVector);

    /// <summary>
    /// Get a world vector on a body given a local vector
    /// </summary>
    /// <param name="localVector">The local vector</param>
    /// <returns>The world vector on the body</returns>
    public Vec2 GetWorldVector(Vec2 localVector) => Valid ? b2Body_GetWorldVector(this, localVector) : throw new InvalidOperationException("Body is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetLinearVelocity")]
    private static extern void b2Body_SetLinearVelocity(Body bodyId, Vec2 linearVelocity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLinearVelocity")]
    private static extern Vec2 b2Body_GetLinearVelocity(Body bodyId);

    /// <summary>
    /// The linear velocity of the body's center of mass.
    /// </summary>
    /// <remarks>Usually in meters per second</remarks>
    public Vec2 LinearVelocity
    {
        get => Valid ? b2Body_GetLinearVelocity(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetLinearVelocity(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetAngularVelocity")]
    private static extern float b2Body_GetAngularVelocity(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetAngularVelocity")]
    private static extern void b2Body_SetAngularVelocity(Body bodyId, float angularVelocity);

    /// <summary>
    /// The angular velocity of the body in radians per second.
    /// </summary>
    /// <remarks>In radians per second</remarks>
    public float AngularVelocity
    {
        get => Valid ? b2Body_GetAngularVelocity(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetAngularVelocity(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetTargetTransform")]
    private static extern void b2Body_SetTargetTransform(Body bodyId, Transform target, float timeStep);

    /// <summary>
    /// Set the velocity to reach the given transform after a given time step.
    /// The result will be close but maybe not exact. This is meant for kinematic bodies.
    /// This will automatically wake the body if asleep.
    /// </summary>
    /// <param name="target">The target transform</param>
    /// <param name="timeStep">The time step</param>
    public void SetTargetTransform(Transform target, float timeStep)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_SetTargetTransform(this, target, timeStep);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalPointVelocity")]
    private static extern Vec2 b2Body_GetLocalPointVelocity(Body bodyId, Vec2 localPoint);

    /// <summary>
    /// Get the linear velocity of a local point attached to a body
    /// </summary>
    /// <param name="localPoint">The local point</param>
    /// <returns>The linear velocity of the local point attached to the body, usually in meters per second</returns>
    /// <remarks>Usually in meters per second</remarks>
    public Vec2 GetLocalPointVelocity(Vec2 localPoint)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        return b2Body_GetLocalPointVelocity(this, localPoint);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldPointVelocity")]
    private static extern Vec2 b2Body_GetWorldPointVelocity(Body bodyId, Vec2 worldPoint);

    /// <summary>
    /// Get the linear velocity of a world point attached to a body
    /// </summary>
    /// <param name="worldPoint">The world point</param>
    /// <returns>The linear velocity of the world point attached to the body, usually in meters per second</returns>
    /// <remarks>Usually in meters per second</remarks>
    public Vec2 GetWorldPointVelocity(Vec2 worldPoint)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        return b2Body_GetWorldPointVelocity(this, worldPoint);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyForce")]
    private static extern void b2Body_ApplyForce(Body bodyId, Vec2 force, Vec2 point, byte wake);

    /// <summary>
    /// Apply a force at a world point
    /// </summary>
    /// <param name="force">The world force vector, usually in newtons (N)</param>
    /// <param name="point">The world position of the point of application</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>If the force is not applied at the center of mass, it will generate a torque and affect the angular velocity. The force is ignored if the body is not awake</remarks>
    public void ApplyForce(Vec2 force, Vec2 point, bool wake)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_ApplyForce(this, force, point, wake ? (byte)1 : (byte)0);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyForceToCenter")]
    private static extern void b2Body_ApplyForceToCenter(Body bodyId, Vec2 force, byte wake);

    /// <summary>
    /// Apply a force to the center of mass
    /// </summary>
    /// <param name="force">The world force vector, usually in newtons (N)</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>This wakes up the body</remarks>
    /// <remarks>If the force is not applied at the center of mass, it will generate a torque and affect the angular velocity. The force is ignored if the body is not awake</remarks>
    public void ApplyForceToCenter(Vec2 force, bool wake)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_ApplyForceToCenter(this, force, wake ? (byte)1 : (byte)0);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyTorque")]
    private static extern void b2Body_ApplyTorque(Body bodyId, float torque, byte wake);

    /// <summary>
    /// Apply a torque
    /// </summary>
    /// <param name="torque">The torque about the z-axis (out of the screen), usually in N*m</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>This affects the angular velocity without affecting the linear velocity. The torque is ignored if the body is not awake</remarks>
    public void ApplyTorque(float torque, bool wake)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_ApplyTorque(this, torque, wake ? (byte)1 : (byte)0);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyLinearImpulse")]
    private static extern void b2Body_ApplyLinearImpulse(Body bodyId, Vec2 impulse, Vec2 point, byte wake);

    /// <summary>
    /// Apply an impulse at a point
    /// </summary>
    /// <param name="impulse">The world impulse vector, usually in N*s or kg*m/s</param>
    /// <param name="point">The world position of the point of application</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>This immediately modifies the velocity. It also modifies the angular velocity if the point of application is not at the center of mass. The impulse is ignored if the body is not awake
    /// <br/><br/><b>Warning: This should be used for one-shot impulses. If you need a steady force, use a force instead, which will work better with the sub-stepping solver</b></remarks>
    public void ApplyLinearImpulse(Vec2 impulse, Vec2 point, bool wake)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_ApplyLinearImpulse(this, impulse, point, wake ? (byte)1 : (byte)0);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyLinearImpulseToCenter")]
    private static extern void b2Body_ApplyLinearImpulseToCenter(Body bodyId, Vec2 impulse, byte wake);

    /// <summary>
    /// Apply an impulse to the center of mass
    /// </summary>
    /// <param name="impulse">The world impulse vector, usually in N*s or kg*m/s</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>This immediately modifies the velocity. The impulse is ignored if the body is not awake
    /// <br/><br/><b>Warning: This should be used for one-shot impulses. If you need a steady force, use a force instead, which will work better with the sub-stepping solver</b></remarks>
    public void ApplyLinearImpulseToCenter(Vec2 impulse, bool wake)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_ApplyLinearImpulseToCenter(this, impulse, wake ? (byte)1 : (byte)0);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyAngularImpulse")]
    private static extern void b2Body_ApplyAngularImpulse(Body bodyId, float impulse, byte wake);

    /// <summary>
    /// Apply an angular impulse
    /// </summary>
    /// <param name="impulse">The angular impulse, usually in units of kg*m*m/s</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>The impulse is ignored if the body is not awake
    /// <br/><br/><b>Warning: This should be used for one-shot impulses. If you need a steady force, use a force instead, which will work better with the sub-stepping solver</b></remarks>
    public void ApplyAngularImpulse(float impulse, bool wake)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_ApplyAngularImpulse(this, impulse, wake ? (byte)1 : (byte)0);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetMass")]
    private static extern float b2Body_GetMass(Body bodyId);

    /// <summary>
    /// The mass of the body, usually in kilograms.
    /// </summary>
    public float Mass => Valid ? b2Body_GetMass(this) : throw new InvalidOperationException("Body is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetRotationalInertia")]
    private static extern float b2Body_GetRotationalInertia(Body bodyId);

    /// <summary>
    /// The rotational inertia of the body, usually in kg*m².
    /// </summary>
    public float RotationalInertia => Valid ? b2Body_GetRotationalInertia(this) : throw new InvalidOperationException("Body is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalCenterOfMass")]
    private static extern Vec2 b2Body_GetLocalCenterOfMass(Body bodyId);

    /// <summary>
    /// The center of mass position of the body in local space.
    /// </summary>
    public Vec2 LocalCenterOfMass => Valid ? b2Body_GetLocalCenterOfMass(this) : throw new InvalidOperationException("Body is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldCenterOfMass")]
    private static extern Vec2 b2Body_GetWorldCenterOfMass(Body bodyId);

    /// <summary>
    /// The center of mass position of the body in world space.
    /// </summary>
    public Vec2 WorldCenterOfMass => Valid ? b2Body_GetWorldCenterOfMass(this) : throw new InvalidOperationException("Body is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetMassData")]
    private static extern void b2Body_SetMassData(Body bodyId, MassData massData);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetMassData")]
    private static extern MassData b2Body_GetMassData(Body bodyId);

    /// <summary>
    /// The mass data for this body.
    /// </summary>
    public MassData MassData
    {
        get => Valid ? b2Body_GetMassData(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetMassData(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyMassFromShapes")]
    private static extern void b2Body_ApplyMassFromShapes(Body bodyId);

    /// <summary>
    /// This updates the mass properties to the sum of the mass properties of the shapes
    /// </summary>
    /// <remarks>This normally does not need to be called unless you called SetMassData to override the mass, and you later want to reset the mass. You may also use this when automatic mass computation has been disabled. You should call this regardless of body type<br/>
    /// <i>Note: Sensor shapes may have mass.</i>
    /// </remarks>
    public void ApplyMassFromShapes()
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_ApplyMassFromShapes(this);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetLinearDamping")]
    private static extern void b2Body_SetLinearDamping(Body bodyId, float linearDamping);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLinearDamping")]
    private static extern float b2Body_GetLinearDamping(Body bodyId);

    /// <summary>
    /// The linear damping.
    /// </summary>
    /// <remarks>Normally this is set in BodyDef before creation</remarks>
    public float LinearDamping
    {
        get => Valid ? b2Body_GetLinearDamping(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetLinearDamping(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetAngularDamping")]
    private static extern void b2Body_SetAngularDamping(Body bodyId, float angularDamping);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetAngularDamping")]
    private static extern float b2Body_GetAngularDamping(Body bodyId);

    /// <summary>
    /// The angular damping.
    /// </summary>
    /// <remarks>Normally this is set in BodyDef before creation</remarks>
    public float AngularDamping
    {
        get => Valid ? b2Body_GetAngularDamping(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetAngularDamping(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetGravityScale")]
    private static extern void b2Body_SetGravityScale(Body bodyId, float gravityScale);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetGravityScale")]
    private static extern float b2Body_GetGravityScale(Body bodyId);

    /// <summary>
    /// The gravity scale.
    /// </summary>
    /// <remarks>Normally this is set in BodyDef before creation</remarks>
    public float GravityScale
    {
        get => Valid ? b2Body_GetGravityScale(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetGravityScale(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsAwake")]
    private static extern byte b2Body_IsAwake(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetAwake")]
    private static extern void b2Body_SetAwake(Body bodyId, byte awake);

    /// <summary>
    /// The body awake state.
    /// </summary>
    /// <remarks>
    /// This wakes the entire island the body is touching.
    /// <b>Warning: Putting a body to sleep will put the entire island of bodies touching this body to sleep, which can be expensive and possibly unintuitive.</b>
    /// </remarks>
    public bool Awake
    {
        get => Valid ? b2Body_IsAwake(this) != 0 : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetAwake(this, value ? (byte)1 : (byte)0);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_EnableSleep")]
    private static extern void b2Body_EnableSleep(Body bodyId, byte enableSleep);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsSleepEnabled")]
    private static extern byte b2Body_IsSleepEnabled(Body bodyId);

    /// <summary>
    /// Option to enable or disable sleeping for this body.
    /// </summary>
    /// <remarks>If sleeping is disabled the body will wake</remarks>
    public bool SleepEnabled
    {
        get => Valid ? b2Body_IsSleepEnabled(this) != 0 : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_EnableSleep(this, value ? (byte)1 : (byte)0);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetSleepThreshold")]
    private static extern void b2Body_SetSleepThreshold(Body bodyId, float sleepThreshold);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetSleepThreshold")]
    private static extern float b2Body_GetSleepThreshold(Body bodyId);

    /// <summary>
    /// The sleep threshold, usually in meters per second.
    /// </summary>
    public float SleepThreshold
    {
        get => Valid ? b2Body_GetSleepThreshold(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetSleepThreshold(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsEnabled")]
    private static extern byte b2Body_IsEnabled(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_Disable")]
    private static extern void b2Body_Disable(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_Enable")]
    private static extern void b2Body_Enable(Body bodyId);

    /// <summary>
    /// The body enabled flag. 
    /// </summary>
    public bool Enabled
    {
        get => Valid ? b2Body_IsEnabled(this) != 0 : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            if (value)
                b2Body_Enable(this);
            else
                b2Body_Disable(this);
        }
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetFixedRotation")]
    private static extern void b2Body_SetFixedRotation(Body bodyId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsFixedRotation")]
    private static extern byte b2Body_IsFixedRotation(Body bodyId);

    /// <summary>
    /// The fixed rotation flag of the body.
    /// </summary>
    /// <remarks>Setting this causes the mass to be reset in all cases</remarks>
    public bool FixedRotation
    {
        get => Valid ? b2Body_IsFixedRotation(this) != 0 : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetFixedRotation(this, value ? (byte)1 : (byte)0);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetBullet")]
    private static extern void b2Body_SetBullet(Body bodyId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsBullet")]
    private static extern byte b2Body_IsBullet(Body bodyId);

    /// <summary>
    /// The bullet flag of the body.
    /// </summary>
    /// <remarks>A bullet does continuous collision detection against dynamic bodies (but not other bullets)</remarks>
    public bool Bullet
    {
        get => Valid ? b2Body_IsBullet(this) != 0 : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetBullet(this, value ? (byte)1 : (byte)0);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_EnableContactEvents")]
    private static extern void b2Body_EnableContactEvents(Body bodyId, byte flag);

    /// <summary>
    /// Enable/disable contact events on all shapes
    /// </summary>
    /// <param name="flag">Option to enable or disable contact events on all shapes</param>
    /// <remarks><b>Warning: Changing this at runtime may cause mismatched begin/end touch events.</b></remarks>
    public void EnableContactEvents(bool flag)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_EnableContactEvents(this, flag ? (byte)1 : (byte)0);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_EnableHitEvents")]
    private static extern void b2Body_EnableHitEvents(Body bodyId, byte flag);

    /// <summary>
    /// Enable/disable hit events on all shapes
    /// </summary>
    /// <param name="flag">Option to enable or disable hit events on all shapes</param>
    public void EnableHitEvents(bool flag)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_EnableHitEvents(this, flag ? (byte)1 : (byte)0);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorld")]
    private static extern WorldId b2Body_GetWorld(Body bodyId);

    /// <summary>
    /// The world that owns this body.
    /// </summary>
    public World World => Valid ? World.GetWorld(b2Body_GetWorld(this)) : throw new InvalidOperationException("Body is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetShapeCount")]
    private static extern int b2Body_GetShapeCount(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetShapes")]
    private static extern unsafe int b2Body_GetShapes(Body bodyId, Shape* shapeArray, int capacity);

    /// <summary>
    /// The shapes attached to this body.
    /// </summary>
    public unsafe ReadOnlySpan<Shape> Shapes
    {
        get
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");

            int shapeCount = b2Body_GetShapeCount(this);
            if (shapeCount == 0)
                return [];

            Shape[] shapes = new Shape[shapeCount];

            fixed (Shape* shapeArrayPtr = shapes)
                b2Body_GetShapes(this, shapeArrayPtr, shapeCount);

            return shapes;
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetJointCount")]
    private static extern int b2Body_GetJointCount(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetJoints")]
    private static extern unsafe int b2Body_GetJoints(Body bodyId, JointId* jointArray, int capacity);

    /// <summary>
    /// The joints attached to this body.
    /// </summary>
    public unsafe ReadOnlySpan<Joint> Joints
    {
        get
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");

            int jointCount = b2Body_GetJointCount(this);
            if (jointCount == 0)
                return [];

            JointId[] jointIds = new JointId[jointCount];
            fixed (JointId* jointIdsArrayPtr = jointIds)
                b2Body_GetJoints(this, jointIdsArrayPtr, jointCount);

            Joint[] jointObjects = new Joint[jointCount];
            for (int i = 0; i < jointCount; i++)
                jointObjects[i] = Joint.GetJoint(jointIds[i]);

            return jointObjects;
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetContactCapacity")]
    private static extern int b2Body_GetContactCapacity(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetContactData")]
    private static extern unsafe int b2Body_GetContactData(Body bodyId, ContactData* contactData, int capacity);

    /// <summary>
    /// The touching contact data for this body.
    /// </summary>
    /// <remarks>
    /// <i>Note: Box2D uses speculative collision so some contact points may be separated.</i>
    /// </remarks>
    public unsafe ReadOnlySpan<ContactData> Contacts
    {
        get
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");

            int needed = b2Body_GetContactCapacity(this);
            if (needed == 0)
                return [];

            ContactData[] contactData = new ContactData[needed];
            int written;
            fixed (ContactData* p = contactData)
            {
                written = b2Body_GetContactData(this, p, contactData.Length);
            }
            return contactData.AsSpan(0, written);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ComputeAABB")]
    private static extern AABB b2Body_ComputeAABB(Body bodyId);

    /// <summary>
    /// The current world AABB that contains all the attached shapes.
    /// </summary>
    /// <remarks>Note that this may not encompass the body origin. If there are no shapes attached then the returned AABB is empty and centered on the body origin</remarks>
    public AABB AABB => Valid ? b2Body_ComputeAABB(this) : throw new InvalidOperationException("Body is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateCircleShape")]
    private static extern Shape b2CreateCircleShape(Body bodyId, in ShapeDefInternal def, in Circle circle);

    /// <summary>
    /// Creates a circle shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="circle">The circle</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public Shape CreateShape(in ShapeDef def, in Circle circle)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        return b2CreateCircleShape(this, in def._internal, circle);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateSegmentShape")]
    private static extern Shape b2CreateSegmentShape(Body bodyId, in ShapeDefInternal def, in Segment segment);

    /// <summary>
    /// Creates a line segment shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="segment">The segment</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public Shape CreateShape(in ShapeDef def, in Segment segment)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        return b2CreateSegmentShape(this, in def._internal, segment);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateCapsuleShape")]
    private static extern Shape b2CreateCapsuleShape(Body bodyId, in ShapeDefInternal def, in Capsule capsule);

    /// <summary>
    /// Creates a capsule shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="capsule">The capsule</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public Shape CreateShape(in ShapeDef def, in Capsule capsule)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        return b2CreateCapsuleShape(this, in def._internal, capsule);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreatePolygonShape")]
    private static extern Shape b2CreatePolygonShape(Body bodyId, in ShapeDefInternal def, in Polygon polygon);

    /// <summary>
    /// Creates a polygon shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="polygon">The polygon</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public Shape CreateShape(in ShapeDef def, in Polygon polygon) => b2CreatePolygonShape(this, in def._internal, polygon);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateChain")]
    private static extern ChainShape b2CreateChain(Body bodyId, in ChainDefInternal def);

    /// <summary>
    /// Creates a chain shape
    /// </summary>
    /// <param name="def">The chain definition</param>
    /// <returns>The chain shape</returns>
    public ChainShape CreateChain(ChainDef def)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        return b2CreateChain(this, in def._internal);
    }
}
