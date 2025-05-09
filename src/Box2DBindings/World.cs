using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Box2D;

[StructLayout(LayoutKind.Explicit)]
struct WorldId
{
    [FieldOffset(0)]
    internal ushort index1;
    [FieldOffset(2)]
    internal ushort generation;
}

/// <summary>
/// A Box2D World, the container for all bodies, shapes, and constraints.
/// </summary>
[PublicAPI]
public sealed partial class World
{
    private WorldId id;

    internal readonly Dictionary<int, Body> bodies = new();

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateWorld")]
    private static extern WorldId b2CreateWorld(in WorldDefInternal def);

    private static bool initialized;
    
    /// <summary>
    /// Create a world for rigid body simulation. A world contains bodies, shapes, and constraints. You make create up to 128 worlds. Each world is completely independent and may be simulated in parallel.
    /// </summary>
    /// <param name="def">The world definition</param>
    /// <returns>The world</returns>
    public static World CreateWorld(WorldDef def)
    {
        return new(def);
    }

    /// <summary>
    /// Create a world for rigid body simulation. A world contains bodies, shapes, and constraints. You make create up to 128 worlds. Each world is completely independent and may be simulated in parallel.
    /// </summary>
    /// <param name="def">The world definition</param>
    public World(WorldDef def)
    {
        if (!initialized)
        {
            initialized = true;
            SetAssertFunction(Assert);
        }
        
        if (def is { WorkerCount: > 0, EnqueueTask: null, FinishTask: null })
        {
            Console.WriteLine("Setting up parallelism with {0} workers", def.WorkerCount);
            def.EnqueueTask = Parallelism.DefaultEnqueue;
            def.FinishTask = Parallelism.DefaultFinish;
        }
        
        id = b2CreateWorld(def._internal);
        worlds.Add(id.index1, this);
    }
    
    private World()
    { }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DestroyWorld")]
    private static extern void b2DestroyWorld(WorldId worldId);

    /// <summary>
    /// Destroy this world
    /// </summary>
    public void Destroy()
    {
        if (!Valid) return;
        
        foreach (var body in bodies.Values)
            body.Destroy();
        bodies.Clear();

        // dealloc user data
        nint userDataPtr = b2World_GetUserData(id);
        if (userDataPtr != 0)
            FreeHandle(ref userDataPtr);
        b2World_SetUserData(id, 0);

        b2DestroyWorld(id);
        
        worlds.Remove(id.index1);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_IsValid")]
    private static extern byte b2World_IsValid(WorldId worldId);

    /// <summary>
    /// World id validation. Provides validation for up to 64K allocations.
    /// </summary>
    /// <returns>True if the world id is valid</returns>
    public bool Valid => b2World_IsValid(id) != 0;

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_Step")]
    private static extern void b2World_Step(WorldId worldId, float timeStep, int subStepCount);

    /// <summary>
    /// Simulate a world for one time step. This performs collision detection, integration, and constraint solution.
    /// </summary>
    /// <param name="timeStep">The amount of time to simulate, this should be a fixed number. Usually 1/60.</param>
    /// <param name="subStepCount">The number of sub-steps, increasing the sub-step count can increase accuracy. Usually 4.</param>
    public void Step(float timeStep = 0.016666668f, int subStepCount = 4) => b2World_Step(id, timeStep, subStepCount);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_Draw")]
    private static extern void b2World_Draw(WorldId worldId, ref DebugDrawInternal draw);

    /// <summary>
    /// Call this to draw shapes and other debug draw data
    /// </summary>
    /// <param name="draw">The debug draw implementation</param>
    public void Draw(DebugDraw draw)
    {
        b2World_Draw(id, ref draw.Internal);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetBodyEvents")]
    private static extern BodyEvents b2World_GetBodyEvents(WorldId worldId);

    /// <summary>
    /// Get the body events for the current time step. The event data is transient. Do not store a reference to this data.
    /// </summary>
    /// <returns>The body events</returns>
    public BodyEvents BodyEvents => Valid ? b2World_GetBodyEvents(id) : throw new InvalidOperationException("World is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetSensorEvents")]
    private static extern SensorEvents b2World_GetSensorEvents(WorldId worldId);

    /// <summary>
    /// Get sensor events for the current time step. The event data is transient. Do not store a reference to this data.
    /// </summary>
    /// <returns>The sensor events</returns>
    public SensorEvents SensorEvents => Valid ? b2World_GetSensorEvents(id) : throw new InvalidOperationException("World is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetContactEvents")]
    private static extern ContactEvents b2World_GetContactEvents(WorldId worldId);

    /// <summary>
    /// Get the contact events for the current time step. The event data is transient. Do not store a reference to this data.
    /// </summary>
    /// <returns>The contact events</returns>
    public ContactEvents ContactEvents => Valid ? b2World_GetContactEvents(id) : throw new InvalidOperationException("World is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_EnableSleeping")]
    private static extern void b2World_EnableSleeping(WorldId worldId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_IsSleepingEnabled")]
    private static extern byte b2World_IsSleepingEnabled(WorldId worldId);

    /// <summary>
    /// Gets or sets the sleeping enabled status of the world. If your application does not need sleeping, you can gain some performance by disabling sleep completely at the world level.
    /// </summary>
    public bool SleepingEnabled
    {
        get => Valid ? b2World_IsSleepingEnabled(id) != 0 : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            b2World_EnableSleeping(id, value ? (byte)1 : (byte)0);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_EnableContinuous")]
    private static extern void b2World_EnableContinuous(WorldId worldId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_IsContinuousEnabled")]
    private static extern byte b2World_IsContinuousEnabled(WorldId worldId);

    /// <summary>
    /// Gets or sets the continuous collision enabled state of the world.
    /// </summary>
    /// <remarks>Generally you should keep continuous collision enabled to prevent fast moving objects from going through static objects. The performance gain from disabling continuous collision is minor</remarks>
    public bool ContinuousEnabled
    {
        get => Valid ? b2World_IsContinuousEnabled(id) != 0 : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            b2World_EnableContinuous(id, value ? (byte)1 : (byte)0);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetRestitutionThreshold")]
    private static extern void b2World_SetRestitutionThreshold(WorldId worldId, float value);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetRestitutionThreshold")]
    private static extern float b2World_GetRestitutionThreshold(WorldId worldId);

    /// <summary>
    /// The restitution speed threshold.
    /// </summary>
    public float RestitutionThreshold
    {
        get => Valid ? b2World_GetRestitutionThreshold(id) : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            b2World_SetRestitutionThreshold(id, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetHitEventThreshold")]
    private static extern void b2World_SetHitEventThreshold(WorldId worldId, float value);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetHitEventThreshold")]
    private static extern float b2World_GetHitEventThreshold(WorldId worldId);

    /// <summary>
    /// The hit event threshold in meters per second.
    /// </summary>
    public float HitEventThreshold
    {
        get => Valid ? b2World_GetHitEventThreshold(id) : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            b2World_SetHitEventThreshold(id, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetCustomFilterCallback")]
    private static extern void b2World_SetCustomFilterCallback(WorldId worldId, CustomFilterNintCallback fcn, nint context);

    internal static Dictionary<int, World> worlds = new();

    internal static World GetWorld(WorldId world)
    {
        if (!worlds.TryGetValue(world.index1, out var w))
            worlds.Add(world.index1, w = new World { id = world });
        return w;
    }

    private static unsafe bool CustomFilterThunk<TContext>(Shape shapeA, Shape shapeB, nint context) where TContext : class
    {
        var contextBuffer = (nint*)context;
        TContext contextObj = (TContext)GCHandle.FromIntPtr(contextBuffer[0]).Target!;
        var callback = (CustomFilterCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(shapeA, shapeB, contextObj);
    }

    private static unsafe bool CustomFilterRefThunk<TContext>(Shape shapeA, Shape shapeB, nint context) where TContext : unmanaged
    {
        var contextBuffer = (nint*)context;
        ref TContext contextObj = ref *(TContext*)contextBuffer[0];
        var callback = (CustomFilterRefCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(shapeA, shapeB, ref contextObj);
    }

    /// <summary>
    /// Register the custom filter callback. This is optional.
    /// </summary>
    /// <param name="callback">The custom filter callback function</param>
    /// <param name="context">The context to be passed to the callback</param>
    public unsafe void SetCustomFilterCallback<TContext>(CustomFilterCallback<TContext> callback, TContext context) where TContext : class
    {
        nint* contextBuffer = stackalloc nint[2];
        contextBuffer[0] = GCHandle.ToIntPtr(GCHandle.Alloc(context));
        contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            b2World_SetCustomFilterCallback(id, CustomFilterThunk<TContext>, (nint)contextBuffer);
        }
        finally
        {
            GCHandle.FromIntPtr(contextBuffer[0]).Free();
            GCHandle.FromIntPtr(contextBuffer[1]).Free();
        }
    }

    /// <summary>
    /// Register the custom filter callback. This is optional.
    /// </summary>
    /// <param name="callback">The custom filter callback function</param>
    /// <param name="context">The context to be passed to the callback</param>
    public unsafe void SetCustomFilterCallback<TContext>(CustomFilterRefCallback<TContext> callback, ref TContext context) where TContext : unmanaged
    {
        fixed (TContext* contextPtr = &context)
        {
            nint* contextBuffer = stackalloc nint[2];
            contextBuffer[0] = (nint)contextPtr;
            contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
            try
            {
                b2World_SetCustomFilterCallback(id, CustomFilterRefThunk<TContext>, (nint)contextBuffer);
            }
            finally
            {
                GCHandle.FromIntPtr(contextBuffer[1]).Free();
            }
        }
    }

    /// <summary>
    /// Register the custom filter callback. This is optional.
    /// </summary>
    /// <param name="callback">The custom filter callback function</param>
    /// <param name="context">The context to be passed to the callback</param>
    public void SetCustomFilterCallback(CustomFilterNintCallback callback, nint context)
    {
        b2World_SetCustomFilterCallback(id, callback, context);
    }

    private static bool CustomFilterThunk(Shape shapeA, Shape shapeB, nint context)
    {
        var callback = (CustomFilterCallback)GCHandle.FromIntPtr(context).Target!;
        return callback(shapeA, shapeB);
    }

    /// <summary>
    /// Register the custom filter callback. This is optional.
    /// </summary>
    /// <param name="nintCallback">The custom filter callback function</param>
    public void SetCustomFilterCallback(CustomFilterCallback nintCallback)
    {
        nint context = GCHandle.ToIntPtr(GCHandle.Alloc(nintCallback));
        try
        {
            b2World_SetCustomFilterCallback(id, CustomFilterThunk, context);
        }
        finally
        {
            GCHandle.FromIntPtr(context).Free();
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetPreSolveCallback")]
    private static extern void b2World_SetPreSolveCallback(WorldId worldId, PreSolveNintCallback fcn, nint context);

    private static unsafe bool PreSolveCallbackThunk<TContext>(Shape shapeA, Shape shapeB, nint manifold, nint context) where TContext : class
    {
        var contextBuffer = (nint*)context;
        TContext contextObj = (TContext)GCHandle.FromIntPtr(contextBuffer[0]).Target!;
        var callback = (PreSolveCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(shapeA, shapeB, *(Manifold*)manifold, contextObj);
    }

    private static unsafe bool PreSolveCallbackRefThunk<TContext>(Shape shapeA, Shape shapeB, nint manifold, nint context) where TContext : unmanaged
    {
        var contextBuffer = (nint*)context;
        ref TContext contextObj = ref *(TContext*)contextBuffer[0];
        var callback = (PreSolveRefCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(shapeA, shapeB, *(Manifold*)manifold, ref contextObj);
    }

    /// <summary>
    /// Register the pre-solve callback. This is optional.
    /// </summary>
    /// <param name="callback">The pre-solve callback function</param>
    /// <param name="context">The context</param>
    public unsafe void SetPreSolveCallback<TContext>(PreSolveCallback<TContext> callback, TContext context) where TContext : class
    {
        nint* contextBuffer = stackalloc nint[2];
        contextBuffer[0] = GCHandle.ToIntPtr(GCHandle.Alloc(context));
        contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            b2World_SetPreSolveCallback(id, PreSolveCallbackThunk<TContext>, (nint)contextBuffer);
        }
        finally
        {
            GCHandle.FromIntPtr(contextBuffer[0]).Free();
            GCHandle.FromIntPtr(contextBuffer[1]).Free();
        }
    }

    /// <summary>
    /// Register the pre-solve callback. This is optional.
    /// </summary>
    /// <param name="callback">The pre-solve callback function</param>
    /// <param name="context">The context</param>
    public unsafe void SetPreSolveCallback<TContext>(PreSolveRefCallback<TContext> callback, ref TContext context) where TContext : unmanaged
    {
        fixed (TContext* contextPtr = &context)
        {
            nint* contextBuffer = stackalloc nint[2];
            contextBuffer[0] = (nint)contextPtr;
            contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
            try
            {
                b2World_SetPreSolveCallback(id, PreSolveCallbackRefThunk<TContext>, (nint)contextBuffer);
            }
            finally
            {
                GCHandle.FromIntPtr(contextBuffer[1]).Free();
            }
        }
    }

    private static unsafe bool PreSolveCallbackThunk(Shape shapeA, Shape shapeB, nint manifold, nint context)
    {
        var callback = (PreSolveCallback)GCHandle.FromIntPtr(context).Target!;
        return callback(shapeA, shapeB, *(Manifold*)manifold);
    }

    /// <summary>
    /// Register the pre-solve callback. This is optional.
    /// </summary>
    /// <param name="callback">The pre-solve callback function</param>
    public void SetPreSolveCallback(PreSolveCallback callback)
    {
        nint context = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            b2World_SetPreSolveCallback(id, PreSolveCallbackThunk, context);
        }
        finally
        {
            GCHandle.FromIntPtr(context).Free();
        }
    }

    /// <summary>
    /// Register the pre-solve callback. This is optional.
    /// </summary>
    /// <param name="callback">The pre-solve callback function</param>
    /// <param name="context">The context</param>
    public void SetPreSolveCallback(PreSolveNintCallback callback, nint context)
    {
        b2World_SetPreSolveCallback(id, callback, context);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetGravity")]
    private static extern void b2World_SetGravity(WorldId worldId, Vec2 gravity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetGravity")]
    private static extern Vec2 b2World_GetGravity(WorldId worldId);

    /// <summary>
    /// The gravity vector
    /// </summary>
    public Vec2 Gravity
    {
        get => Valid ? b2World_GetGravity(id) : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            b2World_SetGravity(id, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_Explode")]
    private static extern void b2World_Explode(WorldId worldId, in ExplosionDef explosionDef);

    /// <summary>
    /// Apply a radial explosion
    /// </summary>
    /// <param name="explosionDef">The explosion definition</param>
    /// <remarks>Explosions are modeled as a force, not as a collision event</remarks>
    public void Explode(in ExplosionDef explosionDef) => b2World_Explode(id, explosionDef);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetContactTuning")]
    private static extern void b2World_SetContactTuning(WorldId worldId, float hertz, float dampingRatio, float pushSpeed);

    /// <summary>
    /// Adjust contact tuning parameters
    /// </summary>
    /// <param name="hertz">The contact stiffness (cycles per second)</param>
    /// <param name="dampingRatio">The contact bounciness with 1 being critical damping (non-dimensional)</param>
    /// <param name="pushSpeed">The maximum contact constraint push out speed (meters per second)</param>
    /// <remarks><i>Note: Advanced feature</i></remarks>
    public void SetContactTuning(float hertz, float dampingRatio, float pushSpeed) =>
        b2World_SetContactTuning(id, hertz, dampingRatio, pushSpeed);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetJointTuning")]
    private static extern void b2World_SetJointTuning(WorldId worldId, float hertz, float dampingRatio);

    /// <summary>
    /// Adjust joint tuning parameters
    /// </summary>
    /// <param name="hertz">The contact stiffness (cycles per second)</param>
    /// <param name="dampingRatio">The contact bounciness with 1 being critical damping (non-dimensional)</param>
    /// <remarks>Advanced feature</remarks>
    public void SetJointTuning(float hertz, float dampingRatio) => b2World_SetJointTuning(id, hertz, dampingRatio);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetMaximumLinearSpeed")]
    private static extern void b2World_SetMaximumLinearSpeed(WorldId worldId, float maximumLinearSpeed);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetMaximumLinearSpeed")]
    private static extern float b2World_GetMaximumLinearSpeed(WorldId worldId);

    /// <summary>
    /// The maximum linear speed.
    /// </summary>
    public float MaximumLinearSpeed
    {
        get => Valid ? b2World_GetMaximumLinearSpeed(id) : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            b2World_SetMaximumLinearSpeed(id, value);
        }
    }


    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_EnableWarmStarting")]
    private static extern void b2World_EnableWarmStarting(WorldId worldId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_IsWarmStartingEnabled")]
    private static extern byte b2World_IsWarmStartingEnabled(WorldId worldId);

    /// <summary>
    /// Enable/disable constraint warm starting. Advanced feature for testing. Disabling warm starting greatly reduces stability and provides no performance gain.
    /// </summary>
    public bool WarmStartingEnabled
    {
        get => Valid ? b2World_IsWarmStartingEnabled(id) != 0 : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            b2World_EnableWarmStarting(id, value ? (byte)1 : (byte)0);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetAwakeBodyCount")]
    private static extern int b2World_GetAwakeBodyCount(WorldId worldId);

    /// <summary>
    /// Get the number of awake bodies.
    /// </summary>
    /// <returns>The number of awake bodies</returns>
    public int AwakeBodyCount => Valid ? b2World_GetAwakeBodyCount(id) : throw new InvalidOperationException("World is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetProfile")]
    private static extern Profile b2World_GetProfile(WorldId worldId);

    /// <summary>
    /// Get the current world performance profile
    /// </summary>
    /// <returns>The world performance profile</returns>
    public Profile Profile => Valid ? b2World_GetProfile(id) : throw new InvalidOperationException("World is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetCounters")]
    private static extern Counters b2World_GetCounters(WorldId worldId);

    /// <summary>
    /// Get world counters and sizes
    /// </summary>
    /// <returns>The world counters and sizes</returns>
    public Counters Counters => b2World_GetCounters(id);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetUserData")]
    private static extern void b2World_SetUserData(WorldId worldId, nint userData);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetUserData")]
    private static extern nint b2World_GetUserData(WorldId worldId);

    /// <summary>
    /// The user data object for this world.
    /// </summary>
    [PublicAPI]
    public object? UserData
    {
        get => Valid ? GetObjectAtPointer(b2World_GetUserData, id) : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            SetObjectAtPointer(b2World_GetUserData, b2World_SetUserData, id, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetFrictionCallback")]
    private static extern void b2World_SetFrictionCallback(WorldId worldId, FrictionCallback callback);

    /// <summary>
    /// Sets the friction callback.
    /// </summary>
    /// <param name="callback">The friction callback</param>
    /// <remarks>Passing NULL resets to default</remarks>
    public void SetFrictionCallback(FrictionCallback callback) => b2World_SetFrictionCallback(id, callback);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetRestitutionCallback")]
    private static extern void b2World_SetRestitutionCallback(WorldId worldId, RestitutionCallback callback);

    /// <summary>
    /// Sets the restitution callback.
    /// </summary>
    /// <param name="callback">The restitution callback</param>
    /// <remarks>Passing NULL resets to default</remarks>
    public void SetRestitutionCallback(RestitutionCallback callback) => b2World_SetRestitutionCallback(id, callback);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_DumpMemoryStats")]
    private static extern void b2World_DumpMemoryStats(WorldId worldId);

    /// <summary>
    /// Dumps memory stats to box2d_memory.txt
    /// </summary>
    /// <remarks>Memory stats are dumped to box2d_memory.txt</remarks>
    public void DumpMemoryStats() => b2World_DumpMemoryStats(id);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateBody")]
    private static extern Body b2CreateBody(WorldId worldId, in BodyDefInternal def);

    /// <summary>
    /// Creates a rigid body given a definition.
    /// </summary>
    /// <param name="def">The body definition</param>
    /// <returns>The body</returns>
    public Body CreateBody(BodyDef def)
    {
        Body body = b2CreateBody(id, def._internal);
        bodies.Add(body.index1, body);
        return body;
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateDistanceJoint")]
    private static extern JointId b2CreateDistanceJoint(WorldId worldId, in DistanceJointDefInternal def);

    /// <summary>
    /// Creates a distance joint
    /// </summary>
    /// <param name="def">The distance joint definition</param>
    /// <returns>The distance joint</returns>
    public DistanceJoint CreateJoint(DistanceJointDef def) => new(b2CreateDistanceJoint(id, def._internal));

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateMotorJoint")]
    private static extern JointId b2CreateMotorJoint(WorldId worldId, in MotorJointDefInternal def);

    /// <summary>
    /// Creates a motor joint
    /// </summary>
    /// <param name="def">The motor joint definition</param>
    /// <returns>The motor joint</returns>
    public MotorJoint CreateJoint(MotorJointDef def) => new(b2CreateMotorJoint(id, def._internal));

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateMouseJoint")]
    private static extern JointId b2CreateMouseJoint(WorldId worldId, in MouseJointDefInternal def);

    /// <summary>
    /// Creates a mouse joint
    /// </summary>
    /// <param name="def">The mouse joint definition</param>
    /// <returns>The mouse joint</returns>
    public MouseJoint CreateJoint(MouseJointDef def) => new(b2CreateMouseJoint(id, def._internal));

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateFilterJoint")]
    private static extern JointId b2CreateFilterJoint(WorldId worldId, in FilterJointDefInternal def);

    /// <summary>
    /// Creates a filter joint. See <see cref="FilterJointDef"/> for details.
    /// </summary>
    /// <param name="def">The filter joint definition</param>
    /// <returns>The filter joint</returns>
    /// <remarks>The filter joint is used to disable collision between two bodies. As a side effect of being a joint, it also keeps the two bodies in the same simulation island.</remarks>
    public Joint CreateJoint(FilterJointDef def) => new(b2CreateFilterJoint(id, def._internal));

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreatePrismaticJoint")]
    private static extern JointId b2CreatePrismaticJoint(WorldId worldId, in PrismaticJointDefInternal def);

    /// <summary>
    /// Creates a prismatic (slider) joint
    /// </summary>
    /// <param name="def">The prismatic joint definition</param>
    /// <returns>The prismatic joint</returns>
    public PrismaticJoint CreateJoint(PrismaticJointDef def) => new(b2CreatePrismaticJoint(id, def._internal));

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateRevoluteJoint")]
    private static extern JointId b2CreateRevoluteJoint(WorldId worldId, in RevoluteJointDefInternal def);

    /// <summary>
    /// Creates a revolute joint
    /// </summary>
    /// <param name="def">The <see cref="RevoluteJointDef"/></param>
    /// <returns>The revolute joint</returns>
    public RevoluteJoint CreateJoint(RevoluteJointDef def) => new(b2CreateRevoluteJoint(id, def._internal));

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateWeldJoint")]
    private static extern JointId b2CreateWeldJoint(WorldId worldId, in WeldJointDefInternal def);

    /// <summary>
    /// Creates a weld joint
    /// </summary>
    /// <param name="def">The <see cref="WeldJointDef"/></param>
    /// <returns>The weld joint</returns>
    public WeldJoint CreateJoint(WeldJointDef def) => new(b2CreateWeldJoint(id, def._internal));

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateWheelJoint")]
    private static extern JointId b2CreateWheelJoint(WorldId worldId, in WheelJointDefInternal def);

    /// <summary>
    /// Creates a wheel joint
    /// </summary>
    /// <param name="def">The wheel joint definition</param>
    /// <returns>The wheel joint</returns>
    public WheelJoint CreateJoint(WheelJointDef def) => new(b2CreateWheelJoint(id, def._internal));

    /// <summary>
    /// Returns a string representation of this world
    /// </summary>
    public override string ToString() => $"World: {id.index1}:{id.generation}";

    /// <summary>
    /// Gets the bodies in this world
    /// </summary>
    public IEnumerable<Body> Bodies => Valid ? bodies.Values : throw new InvalidOperationException("World is not valid");
}
