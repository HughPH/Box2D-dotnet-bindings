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
    
    /// <summary>
    /// Get the body type: static, kinematic, or dynamic
    /// </summary>
    /// <returns>The body type</returns>
    public BodyType GetBodyType() => b2Body_GetType(this);

    public BodyType Type => GetBodyType();

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetType")]
    private static extern void b2Body_SetType(Body bodyId, BodyType type);
    
    /// <summary>
    /// Change the body type.
    /// </summary>
    /// <param name="type">The body type</param>
    /// <remarks>This is an expensive operation. This automatically updates the mass properties regardless of the automatic mass setting</remarks>
    public void SetType(BodyType type) => b2Body_SetType(this, type);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetName")]
    private static extern void b2Body_SetName(Body bodyId, string name);
    
    /// <summary>
    /// Set the body name.
    /// </summary>
    /// <param name="name">The body name</param>
    /// <remarks>Up to 31 characters</remarks>
    public void SetName(string name) => b2Body_SetName(this, name);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetName")]
    private static extern string b2Body_GetName(Body bodyId);
    
    /// <summary>
    /// Get the body name.
    /// </summary>
    /// <returns>The body name</returns>
    /// <remarks>May be null</remarks>
    public string GetName() => b2Body_GetName(this);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetUserData")]
    private static extern void b2Body_SetUserData(Body bodyId, nint userData);
    
    /// <summary>
    /// Set the user data object for a body
    /// </summary>
    /// <param name="userData">The user data object</param>
    public void SetUserData<T>(ref T userData)
    {
        GCHandle handle = GCHandle.Alloc(userData);
        nint userDataPtr = GCHandle.ToIntPtr(handle);
        b2Body_SetUserData(this, userDataPtr);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetUserData")]
    private static extern nint b2Body_GetUserData(Body bodyId);
    
    /// <summary>
    /// Get the user data object for a body
    /// </summary>
    /// <returns>The user data object</returns>
    public T? GetUserData<T>()
    {
        nint userDataPtr = b2Body_GetUserData(this);
        if (userDataPtr == 0) return default;
        GCHandle handle = GCHandle.FromIntPtr(userDataPtr);
        if (!handle.IsAllocated) return default;
        T? userData = (T?)handle.Target;
        return userData;
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetPosition")]
    private static extern Vec2 b2Body_GetPosition(Body bodyId);
    
    /// <summary>
    /// Get the world position of a body.
    /// </summary>
    /// <returns>The world position of the body</returns>
    /// <remarks>This is the location of the body origin</remarks>
    public Vec2 GetPosition() => b2Body_GetPosition(this);

    public Vec2 Position => GetPosition();

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetRotation")]
    private static extern Rotation b2Body_GetRotation(Body bodyId);
    
    /// <summary>
    /// Get the world rotation of a body as a cosine/sine pair (complex number)
    /// </summary>
    /// <returns>The world rotation of the body as a cosine/sine pair (complex number)</returns>
    public Rotation GetRotation() => b2Body_GetRotation(this);

    public Rotation Rotation => GetRotation();

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetTransform")]
    private static extern Transform b2Body_GetTransform(Body bodyId);
    
    /// <summary>
    /// Get the world transform of a body.
    /// </summary>
    /// <returns>The world transform of the body</returns>
    public Transform GetTransform() => b2Body_GetTransform(this);

    public Transform Transform => GetTransform();
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetTransform")]
    private static extern void b2Body_SetTransform(Body bodyId, Vec2 position, Rotation rotation);
    
    /// <summary>
    /// Set the world transform of a body.
    /// </summary>
    /// <param name="position">The position</param>
    /// <param name="rotation">The rotation</param>
    /// <remarks>This acts as a teleport and is fairly expensive.<br/>
    /// <i>Note: Generally you should create a body with the intended transform.</i></remarks>
    public void SetTransform(Vec2 position, Rotation rotation) => b2Body_SetTransform(this, position, rotation);

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

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLinearVelocity")]
    private static extern Vec2 b2Body_GetLinearVelocity(Body bodyId);
    
    /// <summary>
    /// Get the linear velocity of a body's center of mass
    /// </summary>
    /// <returns>The linear velocity of the body's center of mass, usually in meters per second</returns>
    /// <remarks>Usually in meters per second</remarks>
    public Vec2 GetLinearVelocity() => b2Body_GetLinearVelocity(this);

    public Vec2 LinearVelocity => GetLinearVelocity();
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetAngularVelocity")]
    private static extern float b2Body_GetAngularVelocity(Body bodyId);
    
    /// <summary>
    /// Get the angular velocity of a body in radians per second
    /// </summary>
    /// <returns>The angular velocity of the body in radians per second</returns>
    public float GetAngularVelocity() => b2Body_GetAngularVelocity(this);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetLinearVelocity")]
    private static extern void b2Body_SetLinearVelocity(Body bodyId, Vec2 linearVelocity);
    
    /// <summary>
    /// Set the linear velocity of a body
    /// </summary>
    /// <param name="linearVelocity">The linear velocity, usually in meters per second</param>
    /// <remarks>Usually in meters per second</remarks>
    public void SetLinearVelocity(Vec2 linearVelocity) => b2Body_SetLinearVelocity(this, linearVelocity);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetAngularVelocity")]
    private static extern void b2Body_SetAngularVelocity(Body bodyId, float angularVelocity);
    
    /// <summary>
    /// Set the angular velocity of a body in radians per second
    /// </summary>
    /// <param name="angularVelocity">The angular velocity in radians per second</param>
    /// <remarks>Usually in meters per second</remarks>
    public void SetAngularVelocity(float angularVelocity) => b2Body_SetAngularVelocity(this, angularVelocity);

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
    /// Get the mass of the body
    /// </summary>
    /// <returns>The mass of the body, usually in kilograms</returns>
    public float GetMass() => b2Body_GetMass(this);

    public float Mass => GetMass();

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetRotationalInertia")]
    private static extern float b2Body_GetRotationalInertia(Body bodyId);
    
    /// <summary>
    /// Get the rotational inertia of the body
    /// </summary>
    /// <returns>The rotational inertia of the body, usually in kg*m^2</returns>
    public float GetRotationalInertia() => b2Body_GetRotationalInertia(this);

    public float RotationalInertia => GetRotationalInertia();
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalCenterOfMass")]
    private static extern Vec2 b2Body_GetLocalCenterOfMass(Body bodyId);
    
    /// <summary>
    /// Get the center of mass position of the body in local space
    /// </summary>
    /// <returns>The center of mass position of the body in local space</returns>
    public Vec2 GetLocalCenterOfMass() => b2Body_GetLocalCenterOfMass(this);

    public Vec2 LocalCenterOfMass => GetLocalCenterOfMass();

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldCenterOfMass")]
    private static extern Vec2 b2Body_GetWorldCenterOfMass(Body bodyId);
    
    /// <summary>
    /// Get the center of mass position of the body in world space
    /// </summary>
    /// <returns>The center of mass position of the body in world space</returns>
    public Vec2 GetWorldCenterOfMass() => b2Body_GetWorldCenterOfMass(this);

    public Vec2 WorldCenterOfMass => GetWorldCenterOfMass();

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetMassData")]
    private static extern void b2Body_SetMassData(Body bodyId, MassData massData);
    
    /// <summary>
    /// Override the body's mass properties
    /// </summary>
    /// <param name="massData">The mass data</param>
    /// <remarks>Normally this is computed automatically using the shape geometry and density. This information is lost if a shape is added or removed or if the body type changes</remarks>
    public void SetMassData(MassData massData) => b2Body_SetMassData(this, massData);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetMassData")]
    private static extern MassData b2Body_GetMassData(Body bodyId);
    
    /// <summary>
    /// Get the mass data for a body
    /// </summary>
    /// <returns>The mass data for the body</returns>
    public MassData GetMassData() => b2Body_GetMassData(this);

    public MassData MassData
    {
        get => GetMassData();
        set => SetMassData(value);
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
    
    /// <summary>
    /// Adjust the linear damping
    /// </summary>
    /// <param name="linearDamping">The linear damping</param>
    /// <remarks>Normally this is set in b2BodyDef before creation</remarks>
    public void SetLinearDamping(float linearDamping) => b2Body_SetLinearDamping(this, linearDamping);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLinearDamping")]
    private static extern float b2Body_GetLinearDamping(Body bodyId);
    
    /// <summary>
    /// Get the current linear damping
    /// </summary>
    /// <returns>The current linear damping</returns>
    public float GetLinearDamping() => b2Body_GetLinearDamping(this);

    public float LinearDamping
    {
        get => GetLinearDamping();
        set => SetLinearDamping(value);
    }

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetAngularDamping")]
    private static extern void b2Body_SetAngularDamping(Body bodyId, float angularDamping);
    
    /// <summary>
    /// Adjust the angular damping
    /// </summary>
    /// <param name="angularDamping">The angular damping</param>
    /// <remarks>Normally this is set in b2BodyDef before creation</remarks>
    public void SetAngularDamping(float angularDamping) => b2Body_SetAngularDamping(this, angularDamping);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetAngularDamping")]
    private static extern float b2Body_GetAngularDamping(Body bodyId);
    
    /// <summary>
    /// Get the current angular damping
    /// </summary>
    /// <returns>The current angular damping</returns>
    public float GetAngularDamping() => b2Body_GetAngularDamping(this);

    public float AngularDamping
    {
        get => GetAngularDamping();
        set => SetAngularDamping(value);
    }

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetGravityScale")]
    private static extern void b2Body_SetGravityScale(Body bodyId, float gravityScale);
    
    /// <summary>
    /// Adjust the gravity scale
    /// </summary>
    /// <param name="gravityScale">The gravity scale</param>
    /// <remarks>Normally this is set in b2BodyDef before creation</remarks>
    public void SetGravityScale(float gravityScale) => b2Body_SetGravityScale(this, gravityScale);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetGravityScale")]
    private static extern float b2Body_GetGravityScale(Body bodyId);
    
    /// <summary>
    /// Get the gravity scale
    /// </summary>
    /// <returns>The gravity scale</returns>
    public float GetGravityScale() => b2Body_GetGravityScale(this);

    public float GravityScale
    {
        get => GetGravityScale();
        set => SetGravityScale(value);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsAwake")]
    private static extern bool b2Body_IsAwake(Body bodyId);
    
    /// <summary>
    /// Check if this body is awake
    /// </summary>
    /// <returns>true if this body is awake</returns>
    public bool IsAwake() => b2Body_IsAwake(this);
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetAwake")]
    private static extern void b2Body_SetAwake(Body bodyId, bool awake);
    
    /// <summary>
    /// Wake a body from sleep
    /// </summary>
    /// <param name="awake">Option to wake up the body</param>
    /// <remarks>
    /// This wakes the entire island the body is touching.
    /// <b>Warning: Putting a body to sleep will put the entire island of bodies touching this body to sleep, which can be expensive and possibly unintuitive.</b>
    /// </remarks>
    public void SetAwake(bool awake) => b2Body_SetAwake(this, awake);

    public bool Awake
    {
        get => IsAwake();
        set => SetAwake(value);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_EnableSleep")]
    private static extern void b2Body_EnableSleep(Body bodyId, bool enableSleep);
    
    /// <summary>
    /// Enable or disable sleeping for this body
    /// </summary>
    /// <param name="enableSleep">Option to enable or disable sleeping</param>
    /// <remarks>If sleeping is disabled the body will wake</remarks>
    public void EnableSleep(bool enableSleep) => b2Body_EnableSleep(this, enableSleep);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsSleepEnabled")]
    private static extern bool b2Body_IsSleepEnabled(Body bodyId);
    
    /// <summary>
    /// Check if sleeping is enabled for this body
    /// </summary>
    /// <returns>true if sleeping is enabled for this body</returns>
    public bool IsSleepEnabled() => b2Body_IsSleepEnabled(this);

    public bool SleepEnabled
    {
        get => IsSleepEnabled();
        set => EnableSleep(value);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetSleepThreshold")]
    private static extern void b2Body_SetSleepThreshold(Body bodyId, float sleepThreshold);
    
    /// <summary>
    /// Set the sleep threshold
    /// </summary>
    /// <param name="sleepThreshold">The sleep threshold, usually in meters per second</param>
    public void SetSleepThreshold(float sleepThreshold) => b2Body_SetSleepThreshold(this, sleepThreshold);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetSleepThreshold")]
    private static extern float b2Body_GetSleepThreshold(Body bodyId);
    
    /// <summary>
    /// Get the sleep threshold
    /// </summary>
    /// <returns>The sleep threshold, usually in meters per second</returns>
    public float GetSleepThreshold() => b2Body_GetSleepThreshold(this);

    public float SleepThreshold
    {
        get => GetSleepThreshold();
        set => SetSleepThreshold(value);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsEnabled")]
    private static extern bool b2Body_IsEnabled(Body bodyId);
    
    /// <summary>
    /// Check if this body is enabled
    /// </summary>
    /// <returns>true if this body is enabled</returns>
    public bool IsEnabled() => b2Body_IsEnabled(this);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_Disable")]
    private static extern void b2Body_Disable(Body bodyId);
    
    /// <summary>
    /// Disable a body
    /// </summary>
    /// <remarks>Disable a body by removing it completely from the simulation. <b>This is expensive</b></remarks>
    public void Disable() => b2Body_Disable(this);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_Enable")]
    private static extern void b2Body_Enable(Body bodyId);
    
    /// <summary>
    /// Enable a body
    /// </summary>
    /// <remarks>Enable a body by adding it to the simulation. <b>This is expensive</b></remarks>
    public void Enable() => b2Body_Enable(this);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetFixedRotation")]
    private static extern void b2Body_SetFixedRotation(Body bodyId, bool flag);
    
    /// <summary>
    /// Set this body to have fixed rotation
    /// </summary>
    /// <param name="flag">Option to set the body to have fixed rotation</param>
    /// <remarks>This causes the mass to be reset in all cases</remarks>
    public void SetFixedRotation(bool flag) => b2Body_SetFixedRotation(this, flag);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsFixedRotation")]
    private static extern bool b2Body_IsFixedRotation(Body bodyId);
    
    /// <summary>
    /// Check if this body has fixed rotation
    /// </summary>
    /// <returns>true if this body has fixed rotation</returns>
    public bool IsFixedRotation() => b2Body_IsFixedRotation(this);

    public bool FixedRotation
    {
        get => IsFixedRotation();
        set => SetFixedRotation(value);
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetBullet")]
    private static extern void b2Body_SetBullet(Body bodyId, bool flag);
    
    /// <summary>
    /// Set this body to be a bullet
    /// </summary>
    /// <param name="flag">Option to set the body to be a bullet</param>
    /// <remarks>A bullet does continuous collision detection against dynamic bodies (but not other bullets)</remarks>
    public void SetBullet(bool flag) => b2Body_SetBullet(this, flag);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsBullet")]
    private static extern bool b2Body_IsBullet(Body bodyId);
    
    /// <summary>
    /// Check if this body is a bullet
    /// </summary>
    /// <returns>true if this body is a bullet</returns>
    public bool IsBullet() => b2Body_IsBullet(this);

    public bool Bullet
    {
        get => IsBullet();
        set => SetBullet(value);
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
    /// Get the world that owns this body
    /// </summary>
    /// <returns>The world that owns this body</returns>
    public World GetWorld() => b2Body_GetWorld(this);

    public World World => GetWorld();
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetShapeCount")]
    private static extern int b2Body_GetShapeCount(Body bodyId);
    
    /// <summary>
    /// Get the number of shapes on this body
    /// </summary>
    /// <returns>The number of shapes on this body</returns>
    public int GetShapeCount() => b2Body_GetShapeCount(this);

    public int ShapeCount => GetShapeCount();
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetShapes")]
    private static extern int b2Body_GetShapes(Body bodyId, nint shapeArray, int capacity);
    
    /// <summary>
    /// Get the shape ids for all shapes on this body, up to the provided capacity
    /// </summary>
    /// <param name="shapeArray">The shape array</param>
    /// <returns>The number of shape ids stored in the user array</returns>
    public int GetShapes(ref Shape[] shapeArray)
    {
        int capacity = shapeArray.Length;
        nint shapeArrayPtr = Marshal.UnsafeAddrOfPinnedArrayElement(shapeArray, 0);
        int count = b2Body_GetShapes(this, shapeArrayPtr, capacity);
        return count;
    }
    
    public Shape[] Shapes
    {
        get
        {
            int count = GetShapeCount();
            Shape[] shapes = new Shape[count];
            GetShapes(ref shapes);
            return shapes;
        }
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetJointCount")]
    private static extern int b2Body_GetJointCount(Body bodyId);
    
    /// <summary>
    /// Get the number of joints on this body
    /// </summary>
    /// <returns>The number of joints on this body</returns>
    public int GetJointCount() => b2Body_GetJointCount(this);

    public int JointCount => GetJointCount();

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetJoints")]
    private static extern int b2Body_GetJoints(Body bodyId, nint jointArray, int capacity);
    
    /// <summary>
    /// Get the joint ids for all joints on this body, up to the provided capacity
    /// </summary>
    /// <param name="jointArray">The joint array</param>
    /// <returns>The number of joint ids stored in the user array</returns>
    public int GetJoints(ref Joint[] jointArray)
    {
        int capacity = jointArray.Length;
        nint jointArrayPtr = Marshal.UnsafeAddrOfPinnedArrayElement(jointArray, 0);
        int count = b2Body_GetJoints(this, jointArrayPtr, capacity);
        return count;
    }
    
    public Joint[] Joints
    {
        get
        {
            int count = GetJointCount();
            Joint[] joints = new Joint[count];
            GetJoints(ref joints);
            return joints;
        }
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetContactCapacity")]
    private static extern int b2Body_GetContactCapacity(Body bodyId);
    
    /// <summary>
    /// Get the maximum capacity required for retrieving all the touching contacts on a body
    /// </summary>
    /// <returns>The maximum capacity required for retrieving all the touching contacts on a body</returns>
    public int GetContactCapacity() => b2Body_GetContactCapacity(this);

    public int ContactCapacity => GetContactCapacity();
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetContactData")]
    private static extern int b2Body_GetContactData(Body bodyId, nint contactData, int capacity);
    
    /// <summary>
    /// Get the touching contact data for a body
    /// </summary>
    /// <param name="contactData">The contact data</param>
    /// <returns>The number of elements filled in the provided array</returns>
    public int GetContactData(ref ContactData[] contactData)
    {
        int capacity = contactData.Length;
        nint contactDataPtr = Marshal.UnsafeAddrOfPinnedArrayElement(contactData, 0);
        int count = b2Body_GetContactData(this, contactDataPtr, capacity);
        return count;
    }
    
    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ComputeAABB")]
    private static extern AABB b2Body_ComputeAABB(Body bodyId);
    
    /// <summary>
    /// Get the current world AABB that contains all the attached shapes
    /// </summary>
    /// <returns>The current world AABB that contains all the attached shapes</returns>
    /// <remarks>Note that this may not encompass the body origin. If there are no shapes attached then the returned AABB is empty and centered on the body origin</remarks>
    public AABB ComputeAABB() => b2Body_ComputeAABB(this);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateCircleShape")]
    private static extern Shape b2CreateCircleShape(Body bodyId, in ShapeDef def, in Circle circle);
    
    /// <summary>
    /// Creates a circle shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="circle">The circle</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public Shape CreateShape(ShapeDef def, Circle circle) => b2CreateCircleShape(this, def, circle);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateSegmentShape")]
    private static extern Shape b2CreateSegmentShape(Body bodyId, in ShapeDef def, in Segment segment);
    
    /// <summary>
    /// Creates a line segment shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="segment">The segment</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public Shape CreateShape(in ShapeDef def, in Segment segment) => b2CreateSegmentShape(this, def, segment);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateCapsuleShape")]
    private static extern Shape b2CreateCapsuleShape(Body bodyId, in ShapeDef def, in Capsule capsule);
    
    /// <summary>
    /// Creates a capsule shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="capsule">The capsule</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public Shape CreateShape(ref ShapeDef def, ref Capsule capsule) => b2CreateCapsuleShape(this, def, capsule);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreatePolygonShape")]
    private static extern Shape b2CreatePolygonShape(Body bodyId, in ShapeDef def, in Polygon polygon);
    
    /// <summary>
    /// Creates a polygon shape and attaches it to this body
    /// </summary>
    /// <param name="def">The shape definition</param>
    /// <param name="polygon">The polygon</param>
    /// <returns>The shape</returns>
    /// <remarks>The shape definition and geometry are fully cloned. Contacts are not created until the next time step</remarks>
    public Shape CreateShape(ref ShapeDef def, ref Polygon polygon) => b2CreatePolygonShape(this, def, polygon);

    [DllImport(Box2D.libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateChain")]
    private static extern ChainShape b2CreateChain(Body bodyId, in ChainDef def);
    
    /// <summary>
    /// Creates a chain shape
    /// </summary>
    /// <param name="def">The chain definition</param>
    /// <returns>The chain shape</returns>
    public ChainShape CreateChain(ref ChainDef def) => b2CreateChain(this, def);

}