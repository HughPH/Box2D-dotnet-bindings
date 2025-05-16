using JetBrains.Annotations;

namespace Box2D;

/// <summary>
/// World definition used to create a simulation world.
/// </summary>
[PublicAPI]
public sealed class WorldDef
{
    //! \internal
    internal WorldDefInternal _internal = new();
    
    /// <summary>
    /// Creates a world definition with the default values.
    /// </summary>
    public WorldDef()
    {
        
    }
    
    /// <summary>
    /// Creates a world definition with the default values.
    /// </summary>
    public static WorldDef Default => new ();
    
    /// <summary>
    /// Gravity vector. Box2D has no up-vector defined.
    /// </summary>
    public ref Vec2 Gravity => ref _internal.Gravity;

    /// <summary>
    /// Restitution speed threshold, usually in m/s. Collisions above this
    /// speed have restitution applied (will bounce).
    /// </summary>
    public ref float RestitutionThreshold => ref _internal.RestitutionThreshold;

    /// <summary>
    /// Threshold speed for hit events. Usually meters per second.
    /// </summary>
    public ref float HitEventThreshold => ref _internal.HitEventThreshold;

    /// <summary>
    /// Contact stiffness. Cycles per second. Increasing this increases the speed of overlap recovery, but can introduce jitter.
    /// </summary>
    public ref float ContactHertz => ref _internal.ContactHertz;

    /// <summary>
    /// Contact bounciness. Non-dimensional. You can speed up overlap recovery by decreasing this with
    /// the trade-off that overlap resolution becomes more energetic.
    /// </summary>
    public ref float ContactDampingRatio => ref _internal.ContactDampingRatio;

    /// <summary>
    /// This parameter controls how fast overlap is resolved and usually has units of meters per second. This only
    /// puts a cap on the resolution speed. The resolution speed is increased by increasing the hertz and/or
    /// decreasing the damping ratio.
    /// </summary>
    public ref float MaxContactPushSpeed => ref _internal.MaxContactPushSpeed;

    /// <summary>
    /// Joint stiffness. Cycles per second.
    /// </summary>
    public ref float JointHertz => ref _internal.JointHertz;

    /// <summary>
    /// Joint bounciness. Non-dimensional.
    /// </summary>
    public ref float JointDampingRatio => ref _internal.JointDampingRatio;

    /// <summary>
    /// Maximum linear speed. Usually meters per second.
    /// </summary>
    public ref float MaximumLinearSpeed => ref _internal.MaximumLinearSpeed;

    /// <summary>
    /// Optional mixing callback for friction. The default uses sqrt(frictionA * frictionB).
    /// </summary>
    public ref FrictionCallback FrictionCallback => ref _internal.FrictionCallback;

    /// <summary>
    /// Optional mixing callback for restitution. The default uses max(restitutionA, restitutionB).
    /// </summary>
    public ref RestitutionCallback RestitutionCallback => ref _internal.RestitutionCallback;

    /// <summary>
    /// Can bodies go to sleep to improve performance
    /// </summary>
    public bool EnableSleep
    {
        get => _internal.EnableSleep != 0;
        set => _internal.EnableSleep = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// Enable continuous collision
    /// </summary>
    public bool EnableContinuous
    {
        get => _internal.EnableContinuous != 0;
        set => _internal.EnableContinuous = value ? (byte)1 : (byte)0;
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
        get => Parallelism.MaxWorkerCount;
        set => Parallelism.MaxWorkerCount = value;
    }

    /// <summary>
    /// Callback function to spawn tasks
    /// </summary>
    public ref EnqueueTaskCallback EnqueueTask => ref _internal.EnqueueTask;

    /// <summary>
    /// Callback function to finish a task
    /// </summary>
    public ref FinishTaskCallback FinishTask => ref _internal.FinishTask;

    /// <summary>
    /// User data
    /// </summary>
    public object? UserData
    {
        get => GetObjectAtPointer(_internal.UserData);
        set => SetObjectAtPointer(ref _internal.UserData, value);
    }
    
    /// <summary>
    /// User context that is provided to enqueueTask and finishTask
    /// </summary>
    public object? UserTaskContext
    {
        get => GetObjectAtPointer(_internal.UserTaskContext);
        set => SetObjectAtPointer(ref _internal.UserTaskContext, value);
    }
    
    /// <summary>
    /// Construct a new world definition with the supplied values
    /// </summary>
    /// <param name="gravity">The gravity vector</param>
    /// <param name="restitutionThreshold">Restitution speed threshold</param>
    /// <param name="hitEventThreshold">Hit event threshold</param>
    /// <param name="contactHertz">Contact stiffness</param>
    /// <param name="contactDampingRatio">Contact bounciness</param>
    /// <param name="maxContactPushSpeed">Overlap resolution speed</param>
    /// <param name="jointHertz">Joint stiffness</param>
    /// <param name="jointDampingRatio">Joint bounciness</param>
    /// <param name="maximumLinearSpeed">Maximum linear speed</param>
    /// <param name="enableSleep">Enable sleep</param>
    /// <param name="enableContinuous">Enable continuous collision</param>
    /// <param name="userData">User data</param>
    /// <param name="frictionCallback">Friction callback</param>
    /// <param name="restitutionCallback">Restitution callback</param>
    /// <param name="enqueueTask">Enqueue task callback</param>
    /// <param name="finishTask">Finish task callback</param>
    /// <param name="userTaskContext">User task context</param>
    public  WorldDef(  
        Vec2 gravity,
        float restitutionThreshold = 0.0f,
        float hitEventThreshold = 0.0f,
        float contactHertz = 0.0f,
        float contactDampingRatio = 0.0f,
        float maxContactPushSpeed = 0.0f,
        float jointHertz = 0.0f,
        float jointDampingRatio = 0.0f,
        float maximumLinearSpeed = 0.0f,
        bool enableSleep = true,
        bool enableContinuous = true,
        object? userData = null,
        FrictionCallback? frictionCallback = null,
        RestitutionCallback? restitutionCallback = null,
        EnqueueTaskCallback? enqueueTask = null,
        FinishTaskCallback? finishTask = null,
        object? userTaskContext = null)
    {
        Gravity = gravity;
        RestitutionThreshold = restitutionThreshold;
        HitEventThreshold = hitEventThreshold;
        ContactHertz = contactHertz;
        ContactDampingRatio = contactDampingRatio;
        MaxContactPushSpeed = maxContactPushSpeed;
        JointHertz = jointHertz;
        JointDampingRatio = jointDampingRatio;
        MaximumLinearSpeed = maximumLinearSpeed;
        EnableSleep = enableSleep;
        EnableContinuous = enableContinuous;
        UserData = userData;
        if (frictionCallback != null)
            FrictionCallback = frictionCallback;
        if (restitutionCallback != null)
            RestitutionCallback = restitutionCallback;
        
        if (enqueueTask != null)
            EnqueueTask = enqueueTask;
        if (finishTask != null)
            FinishTask = finishTask;
        
        UserTaskContext = userTaskContext;
    }
}