using JetBrains.Annotations;

namespace Box2D;

/// <summary>
/// The filter joint is used to disable collision between two bodies. As a side effect of being a joint, it also keeps the two bodies in the same simulation island.
/// </summary>
[PublicAPI]
public class FilterJointDef
{
    internal FilterJointDefInternal _internal = new();

    /// <summary>
    /// The first attached body.
    /// </summary>
    public ref Body BodyA => ref _internal.BodyA;

    /// <summary>
    /// The second attached body.
    /// </summary>
    public ref Body BodyB => ref _internal.BodyB;

    /// <summary>
    /// User data pointer
    /// </summary>
    public object? UserData
    {
        get => GetObjectAtPointer(_internal.UserData);
        set => SetObjectAtPointer(ref _internal.UserData, value);
    }
}
