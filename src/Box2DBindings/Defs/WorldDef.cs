using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// World definition used to create a simulation world.
/// </summary>
public class WorldDef
{
    internal WorldDefInternal _internal;
    
    public WorldDef()
    {
        _internal = new WorldDefInternal();
    }
    
    public static WorldDef Default => new ();
    
    ~WorldDef()
    {
#if !BOX2D_300
        Box2D.FreeHandle(_internal.UserData);
#endif
        Box2D.FreeHandle(_internal.UserTaskContext);
    }
    
    /// <summary>
    /// Gravity vector. Box2D has no up-vector defined.
    /// </summary>
    public Vec2 Gravity
    {
        get => _internal.Gravity;
        set => _internal.Gravity = value;
    }
    
    /// <summary>
    /// Restitution speed threshold, usually in m/s. Collisions above this
    /// speed have restitution applied (will bounce).
    /// </summary>
    public float RestitutionThreshold
    {
        get => _internal.RestitutionThreshold;
        set => _internal.RestitutionThreshold = value;
    }

#if BOX2D_300
    /// <summary>
    /// This parameter controls how fast overlap is resolved and has units of meters per second
    /// </summary>
    public float ContactPushoutVelocity
    {
        get => _internal.ContactPushoutVelocity;
        set => _internal.ContactPushoutVelocity = value;
    }
#endif
    
    
    /// <summary>
    /// Threshold speed for hit events. Usually meters per second.
    /// </summary>
    public float HitEventThreshold
    {
        get => _internal.HitEventThreshold;
        set => _internal.HitEventThreshold = value;
    }
    
    /// <summary>
    /// Contact stiffness. Cycles per second. Increasing this increases the speed of overlap recovery, but can introduce jitter.
    /// </summary>
    public float ContactHertz
    {
        get => _internal.ContactHertz;
        set => _internal.ContactHertz = value;
    }
    
    /// <summary>
    /// Contact bounciness. Non-dimensional. You can speed up overlap recovery by decreasing this with
    /// the trade-off that overlap resolution becomes more energetic.
    /// </summary>
    public float ContactDampingRatio
    {
        get => _internal.ContactDampingRatio;
        set => _internal.ContactDampingRatio = value;
    }

#if !BOX2D_300
    /// <summary>
    /// This parameter controls how fast overlap is resolved and usually has units of meters per second. This only
    /// puts a cap on the resolution speed. The resolution speed is increased by increasing the hertz and/or
    /// decreasing the damping ratio.
    /// </summary>
    public float ContactPushMaxSpeed
    {
        get => _internal.MaxContactPushSpeed;
        set => _internal.MaxContactPushSpeed = value;
    }
#endif
    
    /// <summary>
    /// Joint stiffness. Cycles per second.
    /// </summary>
    public float JointHertz
    {
        get => _internal.JointHertz;
        set => _internal.JointHertz = value;
    }
    
    /// <summary>
    /// Joint bounciness. Non-dimensional.
    /// </summary>
    public float JointDampingRatio
    {
        get => _internal.JointDampingRatio;
        set => _internal.JointDampingRatio = value;
    }
    
    /// <summary>
    /// Maximum linear speed. Usually meters per second.
    /// </summary>
    public float MaximumLinearSpeed
    {
        get => _internal.MaximumLinearSpeed;
        set => _internal.MaximumLinearSpeed = value;
    }

#if !BOX2D_300
    /// <summary>
    /// Optional mixing callback for friction. The default uses sqrt(frictionA * frictionB).
    /// </summary>
    public FrictionCallback FrictionCallback
    {
        get => _internal.FrictionCallback;
        set => _internal.FrictionCallback = value;
    }
    
    /// <summary>
    /// Optional mixing callback for restitution. The default uses max(restitutionA, restitutionB).
    /// </summary>
    public RestitutionCallback RestitutionCallback
    {
        get => _internal.RestitutionCallback;
        set => _internal.RestitutionCallback = value;
    }
#endif
    /// <summary>
    /// Can bodies go to sleep to improve performance
    /// </summary>
    public bool EnableSleep
    {
        get => _internal.EnableSleep;
        set => _internal.EnableSleep = value;
    }
    
    /// <summary>
    /// Enable continuous collision
    /// </summary>
    public bool EnableContinuous
    {
        get => _internal.EnableContinuous;
        set => _internal.EnableContinuous = value;
    }
    
    /// <summary>
    /// Number of workers to use with the provided task system. Box2D performs best when using only
    /// performance cores and accessing a single L2 cache. Efficiency cores and hyper-threading provide
    /// little benefit and may even harm performance.<br/>
    /// <i>Note: Box2D does not create threads. This is the number of threads your applications has created
    /// that you are allocating to World.Step()</i><br/>
    /// <b>Warning: Do not modify the default value unless you are also providing a task system and providing
    /// task callbacks (enqueueTask and finishTask).</b>
    /// </summary>
    public int WorkerCount
    {
        get => _internal.WorkerCount;
        set => _internal.WorkerCount = value;
    }
    
    /// <summary>
    /// Callback function to spawn tasks
    /// </summary>
    public EnqueueTaskCallback EnqueueTask
    {
        get => _internal.EnqueueTask;
        set => _internal.EnqueueTask = value;
    }
    
    /// <summary>
    /// Callback function to finish a task
    /// </summary>
    public FinishTaskCallback FinishTask
    {
        get => _internal.FinishTask;
        set => _internal.FinishTask = value;
    }
    
#if !BOX2D_300
    /// <summary>
    /// User data pointer
    /// </summary>
    public object? UserData
    {
        get => Box2D.GetObjectAtPointer(_internal.UserData);
        set => Box2D.SetObjectAtPointer(ref _internal.UserData, value);
    }
#endif
    
    /// <summary>
    /// User context that is provided to enqueueTask and finishTask
    /// </summary>
    public object? UserTaskContext
    {
        get => Box2D.GetObjectAtPointer(_internal.UserTaskContext);
        set => Box2D.SetObjectAtPointer(ref _internal.UserTaskContext, value);
    }
}