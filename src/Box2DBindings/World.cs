using Box2D.Comparers;
using JetBrains.Annotations;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.InteropServices;
#if NET9_0_OR_GREATER
using System.Threading;
#endif

namespace Box2D;

/// <summary>
/// A Box2D World, the container for all bodies, shapes, and constraints.
/// </summary>
[PublicAPI]
public sealed partial class World
{
    internal readonly WorldId id;

    /// <summary>
    /// Default comparer for World instances. This is used for hashing and equality checks.
    /// </summary>
    public static IComparer<World> DefaultComparer { get; } = WorldComparer.Instance;

    /// <summary>
    /// Default equality comparer for World instances. This is used for hashing and equality checks.
    /// </summary>
    public static IEqualityComparer<World> DefaultEqualityComparer { get; } = WorldComparer.Instance;

    /// <summary>
    /// Checks if this World and another World refer to the same World
    /// </summary>
    public bool Equals(World other) => id.Equals(other.id);

    /// <summary>
    /// Checks if this World and another World refer to the same World
    /// </summary>
    public override bool Equals(object? obj) => obj is World other && Equals(other);

    /// <summary>
    /// Returns the hash code for this World
    /// </summary>
    public override int GetHashCode() => HashCode.Combine(id.index1, id.generation);

    internal readonly ConcurrentHashSet<Body> bodies = new();

    private static bool initialized;
    
    private bool parallelEvents = false;

    /// <summary>
    /// Create a world for rigid body simulation. A world contains bodies, shapes, and constraints. You may create up to 128 worlds. Each world is completely independent and may be simulated in parallel.
    /// </summary>
    /// <param name="def">The world definition</param>
    /// <returns>The world</returns>
    public static World CreateWorld(WorldDef def)
    {
        return new(def);
    }

    /// <summary>
    /// Create a world for rigid body simulation. A world contains bodies, shapes, and constraints. You may create up to 128 worlds. Each world is completely independent and may be simulated in parallel.
    /// </summary>
    /// <param name="def">The world definition</param>
    public unsafe World(WorldDef def)
    {
        if (!initialized)
        {
            initialized = true;
            SetAssertFunction(Assert);
        }

        if (def is { WorkerCount: > 0, EnqueueTask: null, FinishTask: null })
        {
            def.EnqueueTask = Parallelism.DefaultEnqueue;
            def.FinishTask = Parallelism.DefaultFinish;
        }

        id = b2CreateWorld(def._internal);
        worlds.TryAdd(id, this);

        if (bodyMoveTaskCallbackPointer == 0)
        {
            bodyMoveTaskCallback = BodyMoveTaskCallback;
            bodyMoveTaskCallbackPointer = Marshal.GetFunctionPointerForDelegate(bodyMoveTaskCallback);
        }
        if (sensorBeginTouchTaskCallbackPointer == 0)
        {
            sensorBeginTouchTaskCallback = SensorBeginTouchTaskCallback;
            sensorBeginTouchTaskCallbackPointer = Marshal.GetFunctionPointerForDelegate(sensorBeginTouchTaskCallback);
        }
        if (sensorEndTouchTaskCallbackPointer == 0)
        {
            sensorEndTouchTaskCallback = SensorEndTouchTaskCallback;
            sensorEndTouchTaskCallbackPointer = Marshal.GetFunctionPointerForDelegate(sensorEndTouchTaskCallback);
        }
        if (contactBeginTouchTaskCallbackPointer == 0)
        {
            contactBeginTouchTaskCallback = ContactBeginTouchTaskCallback;
            contactBeginTouchTaskCallbackPointer = Marshal.GetFunctionPointerForDelegate(contactBeginTouchTaskCallback);
        }
        if (contactEndTouchTaskCallbackPointer == 0)
        {
            contactEndTouchTaskCallback = ContactEndTouchTaskCallback;
            contactEndTouchTaskCallbackPointer = Marshal.GetFunctionPointerForDelegate(contactEndTouchTaskCallback);
        }
        if (contactHitTaskCallbackPointer == 0)
        {
            contactHitTaskCallback = ContactHitTaskCallback;
            contactHitTaskCallbackPointer = Marshal.GetFunctionPointerForDelegate(contactHitTaskCallback);
        }
        
        parallelEvents = def.EnableParallelEvents;
    }
    
    /// <summary>
    /// Destroy this world
    /// </summary>
    public unsafe void Destroy()
    {
        if (!Valid) return;

        foreach (var body in bodies)
            body.Destroy();
        bodies.Clear();

        // dealloc user data
        nint userDataPtr = b2World_GetUserData(id);
        if (userDataPtr != 0)
            FreeHandle(ref userDataPtr);
        b2World_SetUserData(id, 0);

        b2DestroyWorld(id);

        worlds.TryRemove(id, out _);
    }
    
    /// <summary>
    /// World id validation. Provides validation for up to 64K allocations.
    /// </summary>
    /// <returns>True if the world id is valid</returns>
    public unsafe bool Valid => b2World_IsValid(id) != 0;

    /// <summary>
    /// A lock object for the world. This is used to synchronize access to the world from multiple threads.
    /// </summary>
    /// <remarks>If you wish to mutate the world, you should use your own Lock object, or use a `lock` statement with this lock object, e.g.<br/>
    /// Thread 1:
    /// <code>
    /// world.Step();
    /// </code>
    /// Thread 2:
    /// <code>
    /// lock (world.WorldLock)
    /// {
    ///     // update world, e.g. add bodies, remove bodies, etc.
    /// }
    /// </code>
    /// <i>Note: It is your responsibility to ensure that the object is unlocked within the time required for another Step.<br/>
    /// Event handlers run within a world lock. Do not lock the world lock in a world event handler. This will cause a deadlock.</i>
    /// </remarks>
#if NET9_0_OR_GREATER
    public Lock WorldLock = new();
#else
    public object WorldLock = new();
#endif

    private readonly TaskCallback bodyMoveTaskCallback = null!; // root the task callback to avoid GC
    private readonly nint bodyMoveTaskCallbackPointer;
    private readonly TaskCallback sensorBeginTouchTaskCallback = null!;
    private readonly nint sensorBeginTouchTaskCallbackPointer;
    private readonly TaskCallback sensorEndTouchTaskCallback = null!;
    private readonly nint sensorEndTouchTaskCallbackPointer;
    private readonly TaskCallback contactBeginTouchTaskCallback = null!;
    private readonly nint contactBeginTouchTaskCallbackPointer;
    private readonly TaskCallback contactEndTouchTaskCallback = null!;
    private readonly nint contactEndTouchTaskCallbackPointer;
    private readonly TaskCallback contactHitTaskCallback = null!;
    private readonly nint contactHitTaskCallbackPointer;
    
    /// <summary>
    /// Simulate a world for one time step. This performs collision detection, integration, and constraint solution.
    /// </summary>
    /// <param name="timeStep">The amount of time to simulate, this should be a fixed number. Usually 1/60.</param>
    /// <param name="subStepCount">The number of sub-steps, increasing the sub-step count can increase accuracy. Usually 4.</param>
    /// <param name="parallelEvents">If true, the events will be processed in parallel.</param>
    public unsafe void Step(float timeStep = 0.016666668f, int subStepCount = 4)
    {
        if (!Valid)
            throw new InvalidOperationException("World is not valid");
        
        Span<nint> tasks = stackalloc nint[6];
        
        lock (WorldLock)
        {
            b2World_Step(id, timeStep, subStepCount);
            if (BodyMove is not null)
            {
                var events = BodyEvents;
                if (parallelEvents)
                {
                    int minRange = Math.Max(events.moveCount / Parallelism.MaxWorkerCount, 1);
                    tasks[0] = Parallelism.DefaultEnqueue(bodyMoveTaskCallbackPointer, events.moveCount, minRange, (nint)events.moveEvents, 0);
                }
                else
                    foreach (BodyMoveEvent e in events.MoveEvents)
                        if (e.Body.Valid)
                            BodyMove(in e);
            }

            if (SensorBeginTouch is not null || SensorEndTouch is not null)
            {
                SensorEvents sensorEvents = SensorEvents;
                if (SensorBeginTouch is not null)
                    if (parallelEvents)
                    {
                        int minRange = Math.Max(sensorEvents.beginCount / Parallelism.MaxWorkerCount, 1);
                        tasks[1] = Parallelism.DefaultEnqueue(sensorBeginTouchTaskCallbackPointer, sensorEvents.beginCount, minRange, (nint)sensorEvents.beginEvents, 0);
                    }
                    else
                        foreach (SensorBeginTouchEvent e in sensorEvents.BeginEvents)
                            if (e.SensorShape.Valid && e.VisitorShape.Valid)
                            {
                                SensorBeginTouch.Invoke(in e);
                            }
                if (SensorEndTouch is not null)
                    if (parallelEvents)
                    {
                        int minRange = Math.Max(sensorEvents.endCount / Parallelism.MaxWorkerCount, 1);
                        tasks[2] = Parallelism.DefaultEnqueue(sensorEndTouchTaskCallbackPointer, sensorEvents.endCount, minRange, (nint)sensorEvents.endEvents, 0);
                    }
                    else
                        foreach (SensorEndTouchEvent e in sensorEvents.EndEvents)
                            if (e.SensorShape.Valid && e.VisitorShape.Valid)
                                SensorEndTouch.Invoke(in e);
            }

            if (ContactBeginTouch is not null || ContactEndTouch is not null || ContactHit is not null)
            {
                ContactEvents contactEvents = ContactEvents;
                if (ContactBeginTouch is not null)
                    if (parallelEvents)
                    {
                        int minRange = Math.Max(contactEvents.beginCount / Parallelism.MaxWorkerCount, 1);
                        tasks[3] = Parallelism.DefaultEnqueue(contactBeginTouchTaskCallbackPointer, contactEvents.beginCount, minRange, (nint)contactEvents.beginEvents, 0);
                    }
                    else
                        foreach (ContactBeginTouchEvent e in contactEvents.BeginEvents)
                            if (e.ShapeA.Valid && e.ShapeB.Valid)
                                ContactBeginTouch.Invoke(in e);
                if (ContactEndTouch is not null)
                    if (parallelEvents)
                    {
                        int minRange = Math.Max(contactEvents.endCount / Parallelism.MaxWorkerCount, 1);
                        tasks[4] = Parallelism.DefaultEnqueue(contactEndTouchTaskCallbackPointer, contactEvents.endCount, minRange, (nint)contactEvents.endEvents, 0);
                    }
                    else
                        foreach (ContactEndTouchEvent e in contactEvents.EndEvents)
                            if (e.ShapeA.Valid && e.ShapeB.Valid)
                                ContactEndTouch.Invoke(in e);
                if (ContactHit is not null)
                    if (parallelEvents)
                    {
                        int minRange = Math.Max(contactEvents.hitCount / Parallelism.MaxWorkerCount, 1);
                        tasks[5] = Parallelism.DefaultEnqueue(contactHitTaskCallbackPointer, contactEvents.hitCount, minRange, (nint)contactEvents.hitEvents, 0);
                    }
                    else
                        foreach (ContactHitEvent e in contactEvents.HitEvents)
                            if (e.ShapeA.Valid && e.ShapeB.Valid)
                                ContactHit.Invoke(in e);
            }
        }
        
        foreach (nint t in tasks)
            if (t != 0) Parallelism.DefaultFinish(t, 0);
    }
    
    /// <summary>
    /// Call this to draw shapes and other debug draw data
    /// </summary>
    /// <param name="draw">The debug draw implementation</param>
    public void Draw(DebugDraw draw)
    {
        b2World_Draw(id, ref draw.Internal);
    }

    /// <summary>
    /// Get the body events for the current time step. The event data is transient. Do not store a reference to this data.
    /// </summary>
    /// <returns>The body events</returns>
    public unsafe BodyEvents BodyEvents => Valid ? b2World_GetBodyEvents(id) : throw new InvalidOperationException("World is not valid");

    /// <summary>
    /// Get sensor events for the current time step. The event data is transient. Do not store a reference to this data.
    /// </summary>
    /// <returns>The sensor events</returns>
    public unsafe SensorEvents SensorEvents => Valid ? b2World_GetSensorEvents(id) : throw new InvalidOperationException("World is not valid");

    /// <summary>
    /// Get the contact events for the current time step. The event data is transient. Do not store a reference to this data.
    /// </summary>
    /// <returns>The contact events</returns>
    public unsafe ContactEvents ContactEvents => Valid ? b2World_GetContactEvents(id) : throw new InvalidOperationException("World is not valid");

    /// <summary>
    /// Gets or sets the sleeping enabled status of the world. If your application does not need sleeping, you can gain some performance by disabling sleep completely at the world level.
    /// </summary>
    public unsafe bool SleepingEnabled
    {
        get => Valid ? b2World_IsSleepingEnabled(id) != 0 : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            b2World_EnableSleeping(id, value ? (byte)1 : (byte)0);
        }
    }

    /// <summary>
    /// Gets or sets the continuous collision enabled state of the world.
    /// </summary>
    /// <remarks>Generally you should keep continuous collision enabled to prevent fast moving objects from going through static objects. The performance gain from disabling continuous collision is minor</remarks>
    public unsafe bool ContinuousEnabled
    {
        get => Valid ? b2World_IsContinuousEnabled(id) != 0 : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            b2World_EnableContinuous(id, value ? (byte)1 : (byte)0);
        }
    }

    /// <summary>
    /// The restitution speed threshold.
    /// </summary>
    public unsafe float RestitutionThreshold
    {
        get => Valid ? b2World_GetRestitutionThreshold(id) : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            b2World_SetRestitutionThreshold(id, value);
        }
    }

    /// <summary>
    /// The hit event threshold in meters per second.
    /// </summary>
    public unsafe float HitEventThreshold
    {
        get => Valid ? b2World_GetHitEventThreshold(id) : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            b2World_SetHitEventThreshold(id, value);
        }
    }

    internal static ConcurrentDictionary<WorldId, World> worlds = new(WorldId.DefaultEqualityComparer);

    internal static unsafe World GetWorld(WorldId world)
    {
        if (b2World_IsValid(world) == 0) throw new InvalidOperationException("World is not valid");
        if (!worlds.TryGetValue(world, out var w))
            throw new InvalidOperationException("World not found");
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

    /// <summary>
    /// The gravity vector
    /// </summary>
    public unsafe Vec2 Gravity
    {
        get => Valid ? b2World_GetGravity(id) : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            b2World_SetGravity(id, value);
        }
    }

    /// <summary>
    /// Apply a radial explosion
    /// </summary>
    /// <param name="explosionDef">The explosion definition</param>
    /// <remarks>Explosions are modeled as a force, not as a collision event</remarks>
    public unsafe void Explode(in ExplosionDef explosionDef) => b2World_Explode(id, explosionDef);

    /// <summary>
    /// Adjust contact tuning parameters
    /// </summary>
    /// <param name="hertz">The contact stiffness (cycles per second)</param>
    /// <param name="dampingRatio">The contact bounciness with 1 being critical damping (non-dimensional)</param>
    /// <param name="pushSpeed">The maximum contact constraint push out speed (meters per second)</param>
    /// <remarks><i>Note: Advanced feature</i></remarks>
    public unsafe void SetContactTuning(float hertz, float dampingRatio, float pushSpeed) =>
        b2World_SetContactTuning(id, hertz, dampingRatio, pushSpeed);

    /// <summary>
    /// Adjust joint tuning parameters
    /// </summary>
    /// <param name="hertz">The contact stiffness (cycles per second)</param>
    /// <param name="dampingRatio">The contact bounciness with 1 being critical damping (non-dimensional)</param>
    /// <remarks>Advanced feature</remarks>
    public unsafe void SetJointTuning(float hertz, float dampingRatio) => b2World_SetJointTuning(id, hertz, dampingRatio);

    /// <summary>
    /// The maximum linear speed.
    /// </summary>
    public unsafe float MaximumLinearSpeed
    {
        get => Valid ? b2World_GetMaximumLinearSpeed(id) : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            b2World_SetMaximumLinearSpeed(id, value);
        }
    }
    
    /// <summary>
    /// Enable/disable constraint warm starting. Advanced feature for testing. Disabling warm starting greatly reduces stability and provides no performance gain.
    /// </summary>
    public unsafe bool WarmStartingEnabled
    {
        get => Valid ? b2World_IsWarmStartingEnabled(id) != 0 : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            b2World_EnableWarmStarting(id, value ? (byte)1 : (byte)0);
        }
    }

    /// <summary>
    /// Get the number of awake bodies.
    /// </summary>
    /// <returns>The number of awake bodies</returns>
    public unsafe int AwakeBodyCount => Valid ? b2World_GetAwakeBodyCount(id) : throw new InvalidOperationException("World is not valid");

    /// <summary>
    /// Get the current world performance profile
    /// </summary>
    /// <returns>The world performance profile</returns>
    public unsafe Profile Profile => Valid ? b2World_GetProfile(id) : throw new InvalidOperationException("World is not valid");

    /// <summary>
    /// Get world counters and sizes
    /// </summary>
    /// <returns>The world counters and sizes</returns>
    public unsafe Counters Counters => b2World_GetCounters(id);

    /// <summary>
    /// The user data object for this world.
    /// </summary>
    public unsafe object? UserData
    {
        get => Valid ? GetObjectAtPointer(b2World_GetUserData, id) : throw new InvalidOperationException("World is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("World is not valid");
            SetObjectAtPointer(b2World_GetUserData, b2World_SetUserData, id, value);
        }
    }

    /// <summary>
    /// Sets the friction callback.
    /// </summary>
    /// <param name="callback">The friction callback</param>
    /// <remarks>Passing NULL resets to default</remarks>
    public unsafe void SetFrictionCallback(FrictionCallback callback) => b2World_SetFrictionCallback(id, callback);

    /// <summary>
    /// Sets the restitution callback.
    /// </summary>
    /// <param name="callback">The restitution callback</param>
    /// <remarks>Passing NULL resets to default</remarks>
    public unsafe void SetRestitutionCallback(RestitutionCallback callback) => b2World_SetRestitutionCallback(id, callback);

    /// <summary>
    /// Dumps memory stats to box2d_memory.txt
    /// </summary>
    /// <remarks>Memory stats are dumped to box2d_memory.txt</remarks>
    public unsafe void DumpMemoryStats() => b2World_DumpMemoryStats(id);

    /// <summary>
    /// Creates a rigid body given a definition.
    /// </summary>
    /// <param name="def">The body definition</param>
    /// <returns>The body</returns>
    public unsafe Body CreateBody(BodyDef def)
    {
        Body body = b2CreateBody(id, def._internal);
        if (!body.Valid)
            return default;
        bodies.Add(body);
        return body;
    }

    /// <summary>
    /// Creates a distance joint
    /// </summary>
    /// <param name="def">The distance joint definition</param>
    /// <returns>The distance joint</returns>
    public unsafe DistanceJoint CreateJoint(DistanceJointDef def) => new(b2CreateDistanceJoint(id, def._internal));

    /// <summary>
    /// Creates a motor joint
    /// </summary>
    /// <param name="def">The motor joint definition</param>
    /// <returns>The motor joint</returns>
    public unsafe MotorJoint CreateJoint(MotorJointDef def) => new(b2CreateMotorJoint(id, def._internal));

    /// <summary>
    /// Creates a mouse joint
    /// </summary>
    /// <param name="def">The mouse joint definition</param>
    /// <returns>The mouse joint</returns>
    public unsafe MouseJoint CreateJoint(MouseJointDef def) => new(b2CreateMouseJoint(id, def._internal));

    /// <summary>
    /// Creates a filter joint. See <see cref="FilterJointDef"/> for details.
    /// </summary>
    /// <param name="def">The filter joint definition</param>
    /// <returns>The filter joint</returns>
    /// <remarks>The filter joint is used to disable collision between two bodies. As a side effect of being a joint, it also keeps the two bodies in the same simulation island.</remarks>
    public unsafe Joint CreateJoint(FilterJointDef def) => new(b2CreateFilterJoint(id, def._internal));

    /// <summary>
    /// Creates a prismatic (slider) joint
    /// </summary>
    /// <param name="def">The prismatic joint definition</param>
    /// <returns>The prismatic joint</returns>
    public unsafe PrismaticJoint CreateJoint(PrismaticJointDef def) => new(b2CreatePrismaticJoint(id, def._internal));

    /// <summary>
    /// Creates a revolute joint
    /// </summary>
    /// <param name="def">The <see cref="RevoluteJointDef"/></param>
    /// <returns>The revolute joint</returns>
    public unsafe RevoluteJoint CreateJoint(RevoluteJointDef def) => new(b2CreateRevoluteJoint(id, def._internal));

    /// <summary>
    /// Creates a weld joint
    /// </summary>
    /// <param name="def">The <see cref="WeldJointDef"/></param>
    /// <returns>The weld joint</returns>
    public unsafe WeldJoint CreateJoint(WeldJointDef def) => new(b2CreateWeldJoint(id, def._internal));

    /// <summary>
    /// Creates a wheel joint
    /// </summary>
    /// <param name="def">The wheel joint definition</param>
    /// <returns>The wheel joint</returns>
    public unsafe WheelJoint CreateJoint(WheelJointDef def) => new(b2CreateWheelJoint(id, def._internal));

    /// <summary>
    /// Returns a string representation of this world
    /// </summary>
    public override string ToString() => $"World: {id.index1}:{id.generation}";

    /// <summary>
    /// Gets the bodies in this world
    /// </summary>
    public IEnumerable<Body> Bodies => Valid ? bodies.Items : throw new InvalidOperationException("World is not valid");
}