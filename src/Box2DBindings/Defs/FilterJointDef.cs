using JetBrains.Annotations;

namespace Box2D;

/// <summary>
/// The filter joint is used to disable collision between two bodies. As a side effect of being a joint, it also keeps the two bodies in the same simulation island.
/// </summary>
[PublicAPI]
public sealed class FilterJointDef
{
    //! \internal
    internal FilterJointDefInternal _internal = new();

    /// <summary>
    /// The first attached body.
    /// </summary>
    public Body BodyA
    {
        get => Body.GetBody(_internal.BodyA);
        set => _internal.BodyA = value.id;
    }

    /// <summary>
    /// The second attached body.
    /// </summary>
    public Body BodyB
    {
        get => Body.GetBody(_internal.BodyB);
        set => _internal.BodyB = value.id;
    }

    /// <summary>
    /// User data
    /// </summary>
    public object? UserData
    {
        get => GetObjectAtPointer(_internal.UserData);
        set => SetObjectAtPointer(ref _internal.UserData, value);
    }
    
    /// <summary>
    /// Constructs a new filter joint definition with the supplied values.
    /// </summary>
    /// <param name="bodyA">The first attached body.</param>
    /// <param name="bodyB">The second attached body.</param>
    /// <param name="userData">User data</param>
    public FilterJointDef(
        Body bodyA,
        Body bodyB,
        object? userData = null)
    {
        BodyA = bodyA;
        BodyB = bodyB;
        UserData = userData;
    }
    
    /// <summary>
    /// Constructs a filter joint definition with the default values.
    /// </summary>
    public FilterJointDef()
    {
        _internal = new ();
    }
}
