using JetBrains.Annotations;

namespace Box2D;

/// <summary>
/// World definition used to create a simulation world.
/// </summary>
[PublicAPI]
public class WorldDef
{
    internal WorldDefInternal _internal = new();
    
    /// <summary>
    /// Creates a world definition with the default values.
    /// </summary>
    public WorldDef()
    {
        WorkerCount = Parallelism.MaxWorkerCount;
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
    public ref int WorkerCount => ref _internal.WorkerCount;

    /// <summary>
    /// Callback function to spawn tasks
    /// </summary>
    public ref EnqueueTaskCallback EnqueueTask => ref _internal.EnqueueTask;

    /// <summary>
    /// Callback function to finish a task
    /// </summary>
    public ref FinishTaskCallback FinishTask => ref _internal.FinishTask;

    /// <summary>
    /// User data pointer
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
}