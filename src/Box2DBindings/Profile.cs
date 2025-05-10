using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Profiling data for Box2D. All times are in milliseconds.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly ref struct Profile
{
    /// <summary>
    /// Total time for the entire physics step <see cref="World.Step"/>.
    /// </summary>
    public readonly float Step;

    /// <summary>
    /// Time spent finding and buffering overlapping pairs in the broad-phase.
    /// </summary>
    public readonly float Pairs;

    /// <summary>
    /// Time spent performing narrow-phase collision detection for all contact pairs.
    /// </summary>
    public readonly float Collide;

    /// <summary>
    /// Total time solving all velocity and position constraints.
    /// </summary>
    public readonly float Solve;

    /// <summary>
    /// Time spent merging islands of bodies and contacts.
    /// </summary>
    public readonly float MergeIslands;

    /// <summary>
    /// Time for preparing simulation stages (sorting, partitioning).
    /// </summary>
    public readonly float PrepareStages;

    /// <summary>
    /// Time solving joint and contact constraints.
    /// </summary>
    public readonly float SolveConstraints;

    /// <summary>
    /// Time setting up constraint solver data (effective masses, biases).
    /// </summary>
    public readonly float PrepareConstraints;

    /// <summary>
    /// Time integrating velocities for dynamic bodies.
    /// </summary>
    public readonly float UntegrateVelocities;

    /// <summary>
    /// Time applying cached impulses to warm start the constraint solver.
    /// </summary>
    public readonly float WarmStart;

    /// <summary>
    /// Time computing and applying impulses for velocity constraints.
    /// </summary>
    public readonly float SolveImpulses;

    /// <summary>
    /// Time integrating body positions based on velocities.
    /// </summary>
    public readonly float IntegratePositions;

    /// <summary>
    /// Time relaxing position constraints (position correction).
    /// </summary>
    public readonly float RelaxImpulses;

    /// <summary>
    /// Time applying restitution (bounciness) corrections.
    /// </summary>
    public readonly float ApplyRestitution;

    /// <summary>
    /// Time storing computed impulses for the next warm start.
    /// </summary>
    public readonly float StoreImpulses;

    /// <summary>
    /// Time splitting simulation islands after solving.
    /// </summary>
    public readonly float SplitIslands;

    /// <summary>
    /// Time updating body transform matrices for rendering.
    /// </summary>
    public readonly float Transforms;

    /// <summary>
    /// Time dispatching contact event callbacks and listeners.
    /// </summary>
    public readonly float HitEvents;

    /// <summary>
    /// Time refitting the broad-phase spatial acceleration structures.
    /// </summary>
    public readonly float Refit;

    /// <summary>
    /// Time spent handling continuous collision (bullet) queries.
    /// </summary>
    public readonly float Bullets;

    /// <summary>
    /// Time spent putting bodies to sleep or waking them.
    /// </summary>
    public readonly float SleepIslands;

    /// <summary>
    /// Time spent processing sensor-only fixtures (no collision resolution).
    /// </summary>
    public readonly float Sensors;
}