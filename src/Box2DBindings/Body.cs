using Box2D.Comparers;
using JetBrains.Annotations;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A Box2D physics body. The body's transform is the base transform for all
/// shapes attached to this body.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public partial struct Body : IEquatable<Body>, IComparable<Body>
{


    internal int index1;
    private ushort world0;
    private ushort generation;

    /// <summary>
    /// Create a body in the supplied world using the supplied BodyDef
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
    
    /// <summary>
    /// Destroy this body.
    /// </summary>
    /// <remarks>This destroys all shapes and joints attached to the body. Do not keep references to the associated shapes and joints</remarks>
    public unsafe void Destroy()
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

    /// <summary>
    /// Body identifier validation.
    /// </summary>
    /// <returns>True if the body id is valid</returns>
    /// <remarks>Can be used to detect orphaned ids. Provides validation for up to 64K allocations</remarks>
    public unsafe bool Valid => b2Body_IsValid(this) != 0;

    /// <summary>
    /// The body type: static, kinematic, or dynamic.
    /// </summary>
    public unsafe BodyType Type
    {
        get => Valid ? b2Body_GetType(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetType(this, value);
        }
    }

    /// <summary>
    /// The body name.
    /// </summary>
    public unsafe string? Name
    {
        get => Valid ? Marshal.PtrToStringAnsi(b2Body_GetName(this)) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetName(this, value);
        }
    }

    /// <summary>
    /// The user data object for this body.
    /// </summary>
    public unsafe object? UserData
    {
        get => Valid ? GetObjectAtPointer(b2Body_GetUserData, this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            SetObjectAtPointer(b2Body_GetUserData, b2Body_SetUserData, this, value);
        }
    }

    /// <summary>
    /// The world position of the body.
    /// </summary>
    /// <remarks>This is the location of the body origin</remarks>
    public unsafe Vec2 Position => Valid ? b2Body_GetPosition(this) : throw new InvalidOperationException("Body is not valid");

    /// <summary>
    /// The world rotation of this body as a cosine/sine pair (complex number).
    /// </summary>
    public unsafe Rotation Rotation => Valid ? b2Body_GetRotation(this) : throw new InvalidOperationException("Body is not valid");

    /// <summary>
    /// The world transform of this body.
    /// </summary>
    /// <remarks>Setting this acts as a teleport and is fairly expensive.<br/>
    /// <i>Note: Generally you should create a body with the intended transform.</i></remarks>
    public unsafe Transform Transform
    {
        get => Valid ? b2Body_GetTransform(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetTransform(this, value.Position, value.Rotation);
        }
    }

    /// <summary>
    /// Get a local point on a body given a world point
    /// </summary>
    /// <param name="worldPoint">The world point</param>
    /// <returns>The local point on the body</returns>
    public unsafe Vec2 GetLocalPoint(Vec2 worldPoint) => Valid ? b2Body_GetLocalPoint(this, worldPoint) : throw new InvalidOperationException("Body is not valid");

    /// <summary>
    /// Get a world point on a body given a local point
    /// </summary>
    /// <param name="localPoint">The local point</param>
    /// <returns>The world point on the body</returns>
    public unsafe Vec2 GetWorldPoint(Vec2 localPoint) => Valid ? b2Body_GetWorldPoint(this, localPoint) : throw new InvalidOperationException("Body is not valid");

    /// <summary>
    /// Get a local vector on a body given a world vector
    /// </summary>
    /// <param name="worldVector">The world vector</param>
    /// <returns>The local vector on the body</returns>
    public unsafe Vec2 GetLocalVector(Vec2 worldVector) => Valid ? b2Body_GetLocalVector(this, worldVector) : throw new InvalidOperationException("Body is not valid");

    /// <summary>
    /// Get a world vector on a body given a local vector
    /// </summary>
    /// <param name="localVector">The local vector</param>
    /// <returns>The world vector on the body</returns>
    public unsafe Vec2 GetWorldVector(Vec2 localVector) => Valid ? b2Body_GetWorldVector(this, localVector) : throw new InvalidOperationException("Body is not valid");

    /// <summary>
    /// The linear velocity of the body's center of mass.
    /// </summary>
    /// <remarks>Usually in meters per second</remarks>
    public unsafe Vec2 LinearVelocity
    {
        get => Valid ? b2Body_GetLinearVelocity(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetLinearVelocity(this, value);
        }
    }

    /// <summary>
    /// The angular velocity of the body in radians per second.
    /// </summary>
    /// <remarks>In radians per second</remarks>
    public unsafe float AngularVelocity
    {
        get => Valid ? b2Body_GetAngularVelocity(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetAngularVelocity(this, value);
        }
    }

    /// <summary>
    /// Set the velocity to reach the given transform after a given time step.
    /// The result will be close but maybe not exact. This is meant for kinematic bodies.
    /// The target is not applied if the velocity would be below the sleep threshold.
    /// This will automatically wake the body if asleep.
    /// </summary>
    /// <param name="target">The target transform</param>
    /// <param name="timeStep">The time step</param>
    public unsafe void SetTargetTransform(Transform target, float timeStep)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_SetTargetTransform(this, target, timeStep);
    }

    /// <summary>
    /// Get the linear velocity of a local point attached to a body
    /// </summary>
    /// <param name="localPoint">The local point</param>
    /// <returns>The linear velocity of the local point attached to the body, usually in meters per second</returns>
    /// <remarks>Usually in meters per second</remarks>
    public unsafe Vec2 GetLocalPointVelocity(Vec2 localPoint)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        return b2Body_GetLocalPointVelocity(this, localPoint);
    }

    /// <summary>
    /// Get the linear velocity of a world point attached to a body
    /// </summary>
    /// <param name="worldPoint">The world point</param>
    /// <returns>The linear velocity of the world point attached to the body, usually in meters per second</returns>
    /// <remarks>Usually in meters per second</remarks>
    public unsafe Vec2 GetWorldPointVelocity(Vec2 worldPoint)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        return b2Body_GetWorldPointVelocity(this, worldPoint);
    }

    /// <summary>
    /// Apply a force at a world point
    /// </summary>
    /// <param name="force">The world force vector, usually in newtons (N)</param>
    /// <param name="point">The world position of the point of application</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>If the force is not applied at the center of mass, it will generate a torque and affect the angular velocity. The force is ignored if the body is not awake</remarks>
    public unsafe void ApplyForce(Vec2 force, Vec2 point, bool wake)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_ApplyForce(this, force, point, wake ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// Apply a force to the center of mass
    /// </summary>
    /// <param name="force">The world force vector, usually in newtons (N)</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>This wakes up the body</remarks>
    /// <remarks>If the force is not applied at the center of mass, it will generate a torque and affect the angular velocity. The force is ignored if the body is not awake</remarks>
    public unsafe void ApplyForceToCenter(Vec2 force, bool wake)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_ApplyForceToCenter(this, force, wake ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// Apply a torque
    /// </summary>
    /// <param name="torque">The torque about the z-axis (out of the screen), usually in N*m</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>This affects the angular velocity without affecting the linear velocity. The torque is ignored if the body is not awake</remarks>
    public unsafe void ApplyTorque(float torque, bool wake)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_ApplyTorque(this, torque, wake ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// Apply an impulse at a point
    /// </summary>
    /// <param name="impulse">The world impulse vector, usually in N*s or kg*m/s</param>
    /// <param name="point">The world position of the point of application</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>This immediately modifies the velocity. It also modifies the angular velocity if the point of application is not at the center of mass. The impulse is ignored if the body is not awake
    /// <br/><br/><b>Warning: This should be used for one-shot impulses. If you need a steady force, use a force instead, which will work better with the sub-stepping solver</b></remarks>
    public unsafe void ApplyLinearImpulse(Vec2 impulse, Vec2 point, bool wake)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_ApplyLinearImpulse(this, impulse, point, wake ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// Apply an impulse to the center of mass
    /// </summary>
    /// <param name="impulse">The world impulse vector, usually in N*s or kg*m/s</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>This immediately modifies the velocity. The impulse is ignored if the body is not awake
    /// <br/><br/><b>Warning: This should be used for one-shot impulses. If you need a steady force, use a force instead, which will work better with the sub-stepping solver</b></remarks>
    public unsafe void ApplyLinearImpulseToCenter(Vec2 impulse, bool wake)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_ApplyLinearImpulseToCenter(this, impulse, wake ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// Apply an angular impulse
    /// </summary>
    /// <param name="impulse">The angular impulse, usually in units of kg*m*m/s</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>The impulse is ignored if the body is not awake
    /// <br/><br/><b>Warning: This should be used for one-shot impulses. If you need a steady force, use a force instead, which will work better with the sub-stepping solver</b></remarks>
    public unsafe void ApplyAngularImpulse(float impulse, bool wake)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_ApplyAngularImpulse(this, impulse, wake ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// The mass of the body, usually in kilograms.
    /// </summary>
    public unsafe float Mass => Valid ? b2Body_GetMass(this) : throw new InvalidOperationException("Body is not valid");

    /// <summary>
    /// The rotational inertia of the body, usually in kg*m².
    /// </summary>
    public unsafe float RotationalInertia => Valid ? b2Body_GetRotationalInertia(this) : throw new InvalidOperationException("Body is not valid");

    /// <summary>
    /// The center of mass position of the body in local space.
    /// </summary>
    public unsafe Vec2 LocalCenterOfMass => Valid ? b2Body_GetLocalCenterOfMass(this) : throw new InvalidOperationException("Body is not valid");

    /// <summary>
    /// The center of mass position of the body in world space.
    /// </summary>
    public unsafe Vec2 WorldCenterOfMass => Valid ? b2Body_GetWorldCenterOfMass(this) : throw new InvalidOperationException("Body is not valid");

    /// <summary>
    /// The mass data for this body.
    /// </summary>
    public unsafe MassData MassData
    {
        get => Valid ? b2Body_GetMassData(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetMassData(this, value);
        }
    }

    /// <summary>
    /// This updates the mass properties to the sum of the mass properties of the shapes
    /// </summary>
    /// <remarks>This normally does not need to be called unless you called SetMassData to override the mass, and you later want to reset the mass. You may also use this when automatic mass computation has been disabled. You should call this regardless of body type<br/>
    /// <i>Note: Sensor shapes may have mass.</i>
    /// </remarks>
    public unsafe void ApplyMassFromShapes()
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_ApplyMassFromShapes(this);
    }

    /// <summary>
    /// The linear damping.
    /// </summary>
    /// <remarks>Normally this is set in BodyDef before creation</remarks>
    public unsafe float LinearDamping
    {
        get => Valid ? b2Body_GetLinearDamping(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetLinearDamping(this, value);
        }
    }

    /// <summary>
    /// The angular damping.
    /// </summary>
    /// <remarks>Normally this is set in BodyDef before creation</remarks>
    public unsafe float AngularDamping
    {
        get => Valid ? b2Body_GetAngularDamping(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetAngularDamping(this, value);
        }
    }

    /// <summary>
    /// The gravity scale.
    /// </summary>
    /// <remarks>Normally this is set in BodyDef before creation</remarks>
    public unsafe float GravityScale
    {
        get => Valid ? b2Body_GetGravityScale(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetGravityScale(this, value);
        }
    }

    /// <summary>
    /// The body awake state.
    /// </summary>
    /// <remarks>
    /// This wakes the entire island the body is touching.
    /// <b>Warning: Putting a body to sleep will put the entire island of bodies touching this body to sleep, which can be expensive and possibly unintuitive.</b>
    /// </remarks>
    public unsafe bool Awake
    {
        get => Valid ? b2Body_IsAwake(this) != 0 : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetAwake(this, value ? (byte)1 : (byte)0);
        }
    }

    /// <summary>
    /// Option to enable or disable sleeping for this body.
    /// </summary>
    /// <remarks>If sleeping is disabled the body will wake</remarks>
    public unsafe bool SleepEnabled
    {
        get => Valid ? b2Body_IsSleepEnabled(this) != 0 : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_EnableSleep(this, value ? (byte)1 : (byte)0);
        }
    }

    /// <summary>
    /// The sleep threshold, usually in meters per second.
    /// </summary>
    public unsafe float SleepThreshold
    {
        get => Valid ? b2Body_GetSleepThreshold(this) : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetSleepThreshold(this, value);
        }
    }

    /// <summary>
    /// The body enabled flag. 
    /// </summary>
    public unsafe bool Enabled
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

    /// <summary>
    /// The fixed rotation flag of the body.
    /// </summary>
    /// <remarks>Setting this causes the mass to be reset in all cases</remarks>
    public unsafe bool FixedRotation
    {
        get => Valid ? b2Body_IsFixedRotation(this) != 0 : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetFixedRotation(this, value ? (byte)1 : (byte)0);
        }
    }

    /// <summary>
    /// The bullet flag of the body.
    /// </summary>
    /// <remarks>A bullet does continuous collision detection against dynamic bodies (but not other bullets)</remarks>
    public unsafe bool Bullet
    {
        get => Valid ? b2Body_IsBullet(this) != 0 : throw new InvalidOperationException("Body is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Body is not valid");
            b2Body_SetBullet(this, value ? (byte)1 : (byte)0);
        }
    }

    /// <summary>
    /// Enable/disable contact events on all shapes
    /// </summary>
    /// <param name="flag">Option to enable or disable contact events on all shapes</param>
    /// <remarks><b>Warning: Changing this at runtime may cause mismatched begin/end touch events.</b></remarks>
    public unsafe void EnableContactEvents(bool flag)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_EnableContactEvents(this, flag ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// Enable/disable hit events on all shapes
    /// </summary>
    /// <param name="flag">Option to enable or disable hit events on all shapes</param>
    public unsafe void EnableHitEvents(bool flag)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        b2Body_EnableHitEvents(this, flag ? (byte)1 : (byte)0);
    }

    /// <summary>
    /// The world that owns this body.
    /// </summary>
    public unsafe World World => Valid ? World.GetWorld(b2Body_GetWorld(this)) : throw new InvalidOperationException("Body is not valid");

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

            Shape[] shapes =
#if NET5_0_OR_GREATER
                GC.AllocateUninitializedArray<Shape>(shapeCount);
#else
                new Shape[shapeCount];
#endif

            fixed (Shape* shapeArrayPtr = shapes)
                b2Body_GetShapes(this, shapeArrayPtr, shapeCount);

            return shapes.AsSpan(0, shapeCount);
        }
    }

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

            JointId* jointIds = stackalloc JointId[jointCount];
            b2Body_GetJoints(this, jointIds, jointCount);

            Joint[] jointObjects =
#if NET5_0_OR_GREATER
                GC.AllocateUninitializedArray<Joint>(jointCount);
#else
                new Joint[jointCount];
#endif
            for (int i = 0; i < jointCount; i++)
                jointObjects[i] = Joint.GetJoint(jointIds[i]);

            return jointObjects.AsSpan(0, jointCount);
        }
    }

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

            ContactData[] contactData =
#if NET5_0_OR_GREATER
                GC.AllocateUninitializedArray<ContactData>(needed);
#else
                new ContactData[needed];
#endif
            int written;
            fixed (ContactData* p = contactData)
            {
                written = b2Body_GetContactData(this, p, contactData.Length);
            }
            return contactData.AsSpan(0, written);
        }
    }

    /// <summary>
    /// The current world AABB that contains all the attached shapes.
    /// </summary>
    /// <remarks>Note that this may not encompass the body origin. If there are no shapes attached then the returned AABB is empty and centered on the body origin</remarks>
    public unsafe AABB AABB => Valid ? b2Body_ComputeAABB(this) : throw new InvalidOperationException("Body is not valid");

    /// <summary>
    /// Creates a circle shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="circle">The circle</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public unsafe Shape CreateShape(in ShapeDef def, in Circle circle)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        return b2CreateCircleShape(this, in def._internal, circle);
    }

    /// <summary>
    /// Creates a line segment shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="segment">The segment</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public unsafe Shape CreateShape(in ShapeDef def, in Segment segment)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        return b2CreateSegmentShape(this, in def._internal, segment);
    }

    /// <summary>
    /// Creates a capsule shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="capsule">The capsule</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public unsafe Shape CreateShape(in ShapeDef def, in Capsule capsule)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        return b2CreateCapsuleShape(this, in def._internal, capsule);
    }

    /// <summary>
    /// Creates a polygon shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="polygon">The polygon</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public unsafe Shape CreateShape(in ShapeDef def, in Polygon polygon) => b2CreatePolygonShape(this, in def._internal, polygon);

    /// <summary>
    /// Creates a chain shape
    /// </summary>
    /// <param name="def">The chain definition</param>
    /// <returns>The chain shape</returns>
    public unsafe ChainShape CreateChain(ChainDef def)
    {
        if (!Valid)
            throw new InvalidOperationException("Body is not valid");
        return new(b2CreateChain(this, in def._internal));
    }
}
