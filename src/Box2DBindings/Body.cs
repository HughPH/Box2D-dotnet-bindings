using System;
using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Explicit)]
public struct Body
{
    [FieldOffset(0)]
    internal int index1;
    [FieldOffset(4)]
    internal ushort world0;
    [FieldOffset(6)]
    internal ushort generation;
        
    public bool Equals(Body other) => index1 == other.index1 && world0 == other.world0 && generation == other.generation;
    public override bool Equals(object? obj) => obj is Body other && Equals(other);
    public override int GetHashCode() => HashCode.Combine(index1, world0, generation);
    
    // equality operator
    public static bool operator ==(Body left, Body right) => left.Equals(right);
    public static bool operator !=(Body left, Body right) => !(left == right);
    public bool ReferenceEquals(Body other) => this == other;

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DestroyBody")]
    private static extern void b2DestroyBody(Body bodyId);
    
    /// <summary>
    /// Destroy this body.
    /// </summary>
    /// <remarks>This destroys all shapes and joints attached to the body. Do not keep references to the associated shapes and joints</remarks>
    public void Destroy()
    {
        World._bodies[world0+1].Remove(index1);
        
        // dealloc user data
        nint userDataPtr = b2Body_GetUserData(this);
        Box2D.FreeHandle(ref userDataPtr);
        b2Body_SetUserData(this, 0);
        
        b2DestroyBody(this);
    }

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsValid")]
    private static extern bool b2Body_IsValid(Body bodyId);
    
    /// <summary>
    /// Body identifier validation.
    /// </summary>
    /// <returns>True if the body id is valid</returns>
    /// <remarks>Can be used to detect orphaned ids. Provides validation for up to 64K allocations</remarks>
    public bool IsValid() => b2Body_IsValid(this);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetType")]
    private static extern BodyType b2Body_GetType(Body bodyId);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetType")]
    private static extern void b2Body_SetType(Body bodyId, BodyType type);

    /// <summary>
    /// The body type: static, kinematic, or dynamic.
    /// </summary>
    public BodyType Type
    {
        get => b2Body_GetType(this);
        set => b2Body_SetType(this, value);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetName")]
    private static extern void b2Body_SetName(Body bodyId, string name);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetName")]
    private static extern string b2Body_GetName(Body bodyId);
    
    /// <summary>
    /// The body name.
    /// </summary>
    public string Name
    {
        get => b2Body_GetName(this);
        set => b2Body_SetName(this, value);
    }

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetUserData")]
    private static extern void b2Body_SetUserData(Body bodyId, nint userData);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetUserData")]
    private static extern nint b2Body_GetUserData(Body bodyId);
    
    /// <summary>
    /// The user data object for this body.
    /// </summary>
    public object? UserData
    {
        get => Box2D.GetObjectAtPointer(b2Body_GetUserData,this);
        set => Box2D.SetObjectAtPointer(b2Body_GetUserData, b2Body_SetUserData, this, value);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetPosition")]
    private static extern Vec2 b2Body_GetPosition(Body bodyId);
    
    /// <summary>
    /// The world position of the body.
    /// </summary>
    /// <remarks>This is the location of the body origin</remarks>
    public Vec2 Position => b2Body_GetPosition(this);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetRotation")]
    private static extern Rotation b2Body_GetRotation(Body bodyId);
    
    /// <summary>
    /// The world rotation of this body as a cosine/sine pair (complex number).
    /// </summary>
    public Rotation Rotation => b2Body_GetRotation(this);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetTransform")]
    private static extern Transform b2Body_GetTransform(Body bodyId);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetTransform")]
    private static extern void b2Body_SetTransform(Body bodyId, Vec2 position, Rotation rotation);
    
    /// <summary>
    /// The world transform of this body.
    /// </summary>
    /// <remarks>Setting this acts as a teleport and is fairly expensive.<br/>
    /// <i>Note: Generally you should create a body with the intended transform.</i></remarks>
    public Transform Transform 
    {
        get => b2Body_GetTransform(this);
        set => b2Body_SetTransform(this, value.Position, value.Rotation);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalPoint")]
    private static extern Vec2 b2Body_GetLocalPoint(Body bodyId, Vec2 worldPoint);

    /// <summary>
    /// Get a local point on a body given a world point
    /// </summary>
    /// <param name="worldPoint">The world point</param>
    /// <returns>The local point on the body</returns>
    public Vec2 GetLocalPoint(Vec2 worldPoint) => b2Body_GetLocalPoint(this, worldPoint);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldPoint")]
    private static extern Vec2 b2Body_GetWorldPoint(Body bodyId, Vec2 localPoint);

    /// <summary>
    /// Get a world point on a body given a local point
    /// </summary>
    /// <param name="localPoint">The local point</param>
    /// <returns>The world point on the body</returns>
    public Vec2 GetWorldPoint(Vec2 localPoint) => b2Body_GetWorldPoint(this, localPoint);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalVector")]
    private static extern Vec2 b2Body_GetLocalVector(Body bodyId, Vec2 worldVector);
    
    /// <summary>
    /// Get a local vector on a body given a world vector
    /// </summary>
    /// <param name="worldVector">The world vector</param>
    /// <returns>The local vector on the body</returns>
    public Vec2 GetLocalVector(Vec2 worldVector) => b2Body_GetLocalVector(this, worldVector);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldVector")]
    private static extern Vec2 b2Body_GetWorldVector(Body bodyId, Vec2 localVector);
    
    /// <summary>
    /// Get a world vector on a body given a local vector
    /// </summary>
    /// <param name="localVector">The local vector</param>
    /// <returns>The world vector on the body</returns>
    public Vec2 GetWorldVector(Vec2 localVector) => b2Body_GetWorldVector(this, localVector);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetLinearVelocity")]
    private static extern void b2Body_SetLinearVelocity(Body bodyId, Vec2 linearVelocity);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLinearVelocity")]
    private static extern Vec2 b2Body_GetLinearVelocity(Body bodyId);
    
    /// <summary>
    /// The linear velocity of the body's center of mass.
    /// </summary>
    /// <remarks>Usually in meters per second</remarks>
    public Vec2 LinearVelocity
    {
        get => b2Body_GetLinearVelocity(this);
        set => b2Body_SetLinearVelocity(this, value);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetAngularVelocity")]
    private static extern float b2Body_GetAngularVelocity(Body bodyId);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetAngularVelocity")]
    private static extern void b2Body_SetAngularVelocity(Body bodyId, float angularVelocity);
    
    /// <summary>
    /// The angular velocity of the body in radians per second.
    /// </summary>
    /// <remarks>In radians per second</remarks>
    public float AngularVelocity
    {
        get => b2Body_GetAngularVelocity(this);
        set => b2Body_SetAngularVelocity(this, value);
    }

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetKinematicTarget")]
    private static extern void b2Body_SetKinematicTarget(Body bodyId, Transform target, float timeStep);
    
    /// <summary>
    /// Set the velocity to reach the given transform after a given time step.
    /// The result will be close but maybe not exact. This is meant for kinematic bodies.
    /// This will automatically wake the body if asleep.
    /// </summary>
    /// <param name="target">The target transform</param>
    /// <param name="timeStep">The time step</param>
    public void SetKinematicTarget(Transform target, float timeStep) => b2Body_SetKinematicTarget(this, target, timeStep);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalPointVelocity")]
    private static extern Vec2 b2Body_GetLocalPointVelocity(Body bodyId, Vec2 localPoint);
    
    /// <summary>
    /// Get the linear velocity of a local point attached to a body
    /// </summary>
    /// <param name="localPoint">The local point</param>
    /// <returns>The linear velocity of the local point attached to the body, usually in meters per second</returns>
    /// <remarks>Usually in meters per second</remarks>
    public Vec2 GetLocalPointVelocity(Vec2 localPoint) => b2Body_GetLocalPointVelocity(this, localPoint);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldPointVelocity")]
    private static extern Vec2 b2Body_GetWorldPointVelocity(Body bodyId, Vec2 worldPoint);
    
    /// <summary>
    /// Get the linear velocity of a world point attached to a body
    /// </summary>
    /// <param name="worldPoint">The world point</param>
    /// <returns>The linear velocity of the world point attached to the body, usually in meters per second</returns>
    /// <remarks>Usually in meters per second</remarks>
    public Vec2 GetWorldPointVelocity(Vec2 worldPoint) => b2Body_GetWorldPointVelocity(this, worldPoint);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyForce")]
    private static extern void b2Body_ApplyForce(Body bodyId, Vec2 force, Vec2 point, bool wake);
    
    /// <summary>
    /// Apply a force at a world point
    /// </summary>
    /// <param name="force">The world force vector, usually in newtons (N)</param>
    /// <param name="point">The world position of the point of application</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>If the force is not applied at the center of mass, it will generate a torque and affect the angular velocity. The force is ignored if the body is not awake</remarks>
    public void ApplyForce(Vec2 force, Vec2 point, bool wake) => b2Body_ApplyForce(this, force, point, wake);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyForceToCenter")]
    private static extern void b2Body_ApplyForceToCenter(Body bodyId, Vec2 force, bool wake);
    
    /// <summary>
    /// Apply a force to the center of mass
    /// </summary>
    /// <param name="force">The world force vector, usually in newtons (N)</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>This wakes up the body</remarks>
    /// <remarks>If the force is not applied at the center of mass, it will generate a torque and affect the angular velocity. The force is ignored if the body is not awake</remarks>
    public void ApplyForceToCenter(Vec2 force, bool wake) => b2Body_ApplyForceToCenter(this, force, wake);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyTorque")]
    private static extern void b2Body_ApplyTorque(Body bodyId, float torque, bool wake);
    
    /// <summary>
    /// Apply a torque
    /// </summary>
    /// <param name="torque">The torque about the z-axis (out of the screen), usually in N*m</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>This affects the angular velocity without affecting the linear velocity. The torque is ignored if the body is not awake</remarks>
    public void ApplyTorque(float torque, bool wake) => b2Body_ApplyTorque(this, torque, wake);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyLinearImpulse")]
    private static extern void b2Body_ApplyLinearImpulse(Body bodyId, Vec2 impulse, Vec2 point, bool wake);
    
    /// <summary>
    /// Apply an impulse at a point
    /// </summary>
    /// <param name="impulse">The world impulse vector, usually in N*s or kg*m/s</param>
    /// <param name="point">The world position of the point of application</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>This immediately modifies the velocity. It also modifies the angular velocity if the point of application is not at the center of mass. The impulse is ignored if the body is not awake
    /// <br/><br/><b>Warning: This should be used for one-shot impulses. If you need a steady force, use a force instead, which will work better with the sub-stepping solver</b></remarks>
    public void ApplyLinearImpulse(Vec2 impulse, Vec2 point, bool wake) => b2Body_ApplyLinearImpulse(this, impulse, point, wake);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyLinearImpulseToCenter")]
    private static extern void b2Body_ApplyLinearImpulseToCenter(Body bodyId, Vec2 impulse, bool wake);
    
    /// <summary>
    /// Apply an impulse to the center of mass
    /// </summary>
    /// <param name="impulse">The world impulse vector, usually in N*s or kg*m/s</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>This immediately modifies the velocity. The impulse is ignored if the body is not awake
    /// <br/><br/><b>Warning: This should be used for one-shot impulses. If you need a steady force, use a force instead, which will work better with the sub-stepping solver</b></remarks>
    public void ApplyLinearImpulseToCenter(Vec2 impulse, bool wake) => b2Body_ApplyLinearImpulseToCenter(this, impulse, wake);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyAngularImpulse")]
    private static extern void b2Body_ApplyAngularImpulse(Body bodyId, float impulse, bool wake);
    
    /// <summary>
    /// Apply an angular impulse
    /// </summary>
    /// <param name="impulse">The angular impulse, usually in units of kg*m*m/s</param>
    /// <param name="wake">Option to wake up the body</param>
    /// <remarks>The impulse is ignored if the body is not awake
    /// <br/><br/><b>Warning: This should be used for one-shot impulses. If you need a steady force, use a force instead, which will work better with the sub-stepping solver</b></remarks>
    public void ApplyAngularImpulse(float impulse, bool wake) => b2Body_ApplyAngularImpulse(this, impulse, wake);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetMass")]
    private static extern float b2Body_GetMass(Body bodyId  );
    
    /// <summary>
    /// The mass of the body, usually in kilograms.
    /// </summary>
    public float Mass => b2Body_GetMass(this);

    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetRotationalInertia")]
    private static extern float b2Body_GetRotationalInertia(Body bodyId);
    
    /// <summary>
    /// The rotational inertia of the body, usually in kg*m².
    /// </summary>
    public float RotationalInertia => b2Body_GetRotationalInertia(this);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalCenterOfMass")]
    private static extern Vec2 b2Body_GetLocalCenterOfMass(Body bodyId);
    
    /// <summary>
    /// The center of mass position of the body in local space.
    /// </summary>
    public Vec2 LocalCenterOfMass => b2Body_GetLocalCenterOfMass(this);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldCenterOfMass")]
    private static extern Vec2 b2Body_GetWorldCenterOfMass(Body bodyId);
    
    /// <summary>
    /// The center of mass position of the body in world space.
    /// </summary>
    public Vec2 WorldCenterOfMass => b2Body_GetWorldCenterOfMass(this);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetMassData")]
    private static extern void b2Body_SetMassData(Body bodyId, MassData massData);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetMassData")]
    private static extern MassData b2Body_GetMassData(Body bodyId);
    
    /// <summary>
    /// The mass data for this body.
    /// </summary>
    public MassData MassData
    {
        get => b2Body_GetMassData(this);
        set => b2Body_SetMassData(this, value);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyMassFromShapes")]
    private static extern void b2Body_ApplyMassFromShapes(Body bodyId);
    
    /// <summary>
    /// This updates the mass properties to the sum of the mass properties of the shapes
    /// </summary>
    /// <remarks>This normally does not need to be called unless you called SetMassData to override the mass and you later want to reset the mass. You may also use this when automatic mass computation has been disabled. You should call this regardless of body type</remarks>
    public void ApplyMassFromShapes() => b2Body_ApplyMassFromShapes(this);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetLinearDamping")]
    private static extern void b2Body_SetLinearDamping(Body bodyId, float linearDamping);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLinearDamping")]
    private static extern float b2Body_GetLinearDamping(Body bodyId);
    
    /// <summary>
    /// The linear damping.
    /// </summary>
    /// <remarks>Normally this is set in BodyDef before creation</remarks>
    public float LinearDamping
    {
        get => b2Body_GetLinearDamping(this);
        set => b2Body_SetLinearDamping(this, value);
    }

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetAngularDamping")]
    private static extern void b2Body_SetAngularDamping(Body bodyId, float angularDamping);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetAngularDamping")]
    private static extern float b2Body_GetAngularDamping(Body bodyId);
    
    /// <summary>
    /// The angular damping.
    /// </summary>
    /// <remarks>Normally this is set in BodyDef before creation</remarks>
    public float AngularDamping
    {
        get => b2Body_GetAngularDamping(this);
        set => b2Body_SetAngularDamping(this, value);
    }

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetGravityScale")]
    private static extern void b2Body_SetGravityScale(Body bodyId, float gravityScale);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetGravityScale")]
    private static extern float b2Body_GetGravityScale(Body bodyId);
    
    /// <summary>
    /// The gravity scale.
    /// </summary>
    /// <remarks>Normally this is set in BodyDef before creation</remarks>
    public float GravityScale
    {
        get => b2Body_GetGravityScale(this);
        set => b2Body_SetGravityScale(this, value);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsAwake")]
    private static extern bool b2Body_IsAwake(Body bodyId);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetAwake")]
    private static extern void b2Body_SetAwake(Body bodyId, bool awake);
    
    /// <summary>
    /// The body awake state.
    /// </summary>
    /// <remarks>
    /// This wakes the entire island the body is touching.
    /// <b>Warning: Putting a body to sleep will put the entire island of bodies touching this body to sleep, which can be expensive and possibly unintuitive.</b>
    /// </remarks>
    public bool Awake
    {
        get => b2Body_IsAwake(this);
        set => b2Body_SetAwake(this, value);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_EnableSleep")]
    private static extern void b2Body_EnableSleep(Body bodyId, bool enableSleep);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsSleepEnabled")]
    private static extern bool b2Body_IsSleepEnabled(Body bodyId);

    /// <summary>
    /// Option to enable or disable sleeping for this body.
    /// </summary>
    /// <remarks>If sleeping is disabled the body will wake</remarks>
    public bool SleepEnabled
    {
        get => b2Body_IsSleepEnabled(this);
        set => b2Body_EnableSleep(this, value);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetSleepThreshold")]
    private static extern void b2Body_SetSleepThreshold(Body bodyId, float sleepThreshold);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetSleepThreshold")]
    private static extern float b2Body_GetSleepThreshold(Body bodyId);
    
    /// <summary>
    /// The sleep threshold, usually in meters per second.
    /// </summary>
    public float SleepThreshold
    {
        get => b2Body_GetSleepThreshold(this);
        set => b2Body_SetSleepThreshold(this, value);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsEnabled")]
    private static extern bool b2Body_IsEnabled(Body bodyId);
    
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_Disable")]
    private static extern void b2Body_Disable(Body bodyId);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_Enable")]
    private static extern void b2Body_Enable(Body bodyId);
    
    /// <summary>
    /// The body enabled flag. 
    /// </summary>
    public bool Enabled
    {
        get => b2Body_IsEnabled(this);
        set
        {
            if (value)
                b2Body_Enable(this);
            else
                b2Body_Disable(this);
        }
    }


    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetFixedRotation")]
    private static extern void b2Body_SetFixedRotation(Body bodyId, bool flag);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsFixedRotation")]
    private static extern bool b2Body_IsFixedRotation(Body bodyId);
    
    /// <summary>
    /// The fixed rotation flag of the body.
    /// </summary>
    /// <remarks>Setting this causes the mass to be reset in all cases</remarks>
    public bool FixedRotation
    {
        get => b2Body_IsFixedRotation(this);
        set => b2Body_SetFixedRotation(this, value);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetBullet")]
    private static extern void b2Body_SetBullet(Body bodyId, bool flag);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsBullet")]
    private static extern bool b2Body_IsBullet(Body bodyId);
    
    /// <summary>
    /// The bullet flag of the body.
    /// </summary>
    /// <remarks>A bullet does continuous collision detection against dynamic bodies (but not other bullets)</remarks>
    public bool Bullet
    {
        get => b2Body_IsBullet(this);
        set => b2Body_SetBullet(this, value);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_EnableContactEvents")]
    private static extern void b2Body_EnableContactEvents(Body bodyId, bool flag);

    /// <summary>
    /// Enable/disable contact events on all shapes
    /// </summary>
    /// <param name="flag">Option to enable or disable contact events on all shapes</param>
    /// <remarks><b>Warning: Changing this at runtime may cause mismatched begin/end touch events.</b></remarks>
    public void EnableContactEvents(bool flag) => b2Body_EnableContactEvents(this, flag);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_EnableHitEvents")]
    private static extern void b2Body_EnableHitEvents(Body bodyId, bool flag);
    
    /// <summary>
    /// Enable/disable hit events on all shapes
    /// </summary>
    /// <param name="flag">Option to enable or disable hit events on all shapes</param>
    public void EnableHitEvents(bool flag) => b2Body_EnableHitEvents(this, flag);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorld")]
    private static extern World b2Body_GetWorld(Body bodyId);
    
    /// <summary>
    /// The world that owns this body.
    /// </summary>
    public World World => b2Body_GetWorld(this);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetShapeCount")]
    private static extern int b2Body_GetShapeCount(Body bodyId);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetShapes")]
    private static extern int b2Body_GetShapes(Body bodyId, nint shapeArray, int capacity);
    
    /// <summary>
    /// The shapes attached to this body.
    /// </summary>
    public Shape[] Shapes
    {
        get
        {
            int shapeCount = b2Body_GetShapeCount(this);
            if (shapeCount == 0)
                return Array.Empty<Shape>();

            Shape[] shapes = new Shape[shapeCount];
            GCHandle handle = GCHandle.Alloc(shapes, GCHandleType.Pinned);
            try
            {
                IntPtr shapeArrayPtr = handle.AddrOfPinnedObject();
                b2Body_GetShapes(this, shapeArrayPtr, shapeCount);
            }
            finally
            {
                handle.Free();
            }

            return shapes;
        }
    }

    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetJointCount")]
    private static extern int b2Body_GetJointCount(Body bodyId);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetJoints")]
    private static extern int b2Body_GetJoints(Body bodyId, nint jointArray, int capacity);
    
    /// <summary>
    /// The joints attached to this body.
    /// </summary>
    public Joint[] Joints
    {
        get
        {
            int jointCount = b2Body_GetJointCount(this);
            if (jointCount == 0)
                return Array.Empty<Joint>();
            
            Joint[] joints = new Joint[jointCount];
            GCHandle handle = GCHandle.Alloc(joints, GCHandleType.Pinned);
            try
            {
                IntPtr jointArrayPtr = handle.AddrOfPinnedObject();
                b2Body_GetJoints(this, jointArrayPtr, jointCount);
            }
            finally
            {
                handle.Free();
            }
            
            return joints;
        }
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetContactCapacity")]
    private static extern int b2Body_GetContactCapacity(Body bodyId);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetContactData")]
    private static extern int b2Body_GetContactData(Body bodyId, nint contactData, int capacity);
    
    /// <summary>
    /// The touching contact data for this body.
    /// </summary>
    /// <remarks>
    /// <i>Note: Box2D uses speculative collision so some contact points may be separated.</i>
    /// </remarks>
    public ContactData[] Contacts
    {
        get
        {
            int contactCapacity = b2Body_GetContactCapacity(this);
            if (contactCapacity == 0)
                return Array.Empty<ContactData>();
            
            ContactData[] contactData = new ContactData[contactCapacity];
            GCHandle handle = GCHandle.Alloc(contactData, GCHandleType.Pinned);
            try
            {
                IntPtr contactDataPtr = handle.AddrOfPinnedObject();
                b2Body_GetContactData(this, contactDataPtr, contactCapacity);
            }
            finally
            {
                handle.Free();
            }
            
            return contactData;
        }
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ComputeAABB")]
    private static extern AABB b2Body_ComputeAABB(Body bodyId);

    /// <summary>
    /// The current world AABB that contains all the attached shapes.
    /// </summary>
    /// <remarks>Note that this may not encompass the body origin. If there are no shapes attached then the returned AABB is empty and centered on the body origin</remarks>
    public AABB AABB => b2Body_ComputeAABB(this);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateCircleShape")]
    private static extern Shape b2CreateCircleShape(Body bodyId, in ShapeDefInternal def, in Circle circle);
    
    /// <summary>
    /// Creates a circle shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="circle">The circle</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public Shape CreateShape(ShapeDef def, in Circle circle) => b2CreateCircleShape(this, in def._internal, circle);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateSegmentShape")]
    private static extern Shape b2CreateSegmentShape(Body bodyId, in ShapeDefInternal def, in Segment segment);
    
    /// <summary>
    /// Creates a line segment shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="segment">The segment</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public Shape CreateShape(ShapeDef def, in Segment segment) => b2CreateSegmentShape(this, in def._internal, segment);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateCapsuleShape")]
    private static extern Shape b2CreateCapsuleShape(Body bodyId, in ShapeDefInternal def, in Capsule capsule);
    
    /// <summary>
    /// Creates a capsule shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="capsule">The capsule</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public Shape CreateShape(ShapeDef def, in Capsule capsule) => b2CreateCapsuleShape(this, in def._internal, capsule);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreatePolygonShape")]
    private static extern Shape b2CreatePolygonShape(Body bodyId, in ShapeDefInternal def, in Polygon polygon);
    
    /// <summary>
    /// Creates a polygon shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="polygon">The polygon</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public Shape CreateShape(ShapeDef def, in Polygon polygon) => b2CreatePolygonShape(this, in def._internal, polygon);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateChain")]
    private static extern ChainShape b2CreateChain(Body bodyId, in ChainDefInternal def);
    
    /// <summary>
    /// Creates a chain shape
    /// </summary>
    /// <param name="def">The chain definition</param>
    /// <returns>The chain shape</returns>
    public ChainShape CreateChain(ChainDef def) => b2CreateChain(this, in def._internal);

}