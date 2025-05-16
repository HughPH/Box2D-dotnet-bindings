using JetBrains.Annotations;

namespace Box2D;

/// <summary>
/// Weld joint definition<br/>
///
/// A weld joint connect to bodies together rigidly. This constraint provides springs to mimic
/// soft-body simulation.
/// <i>Note: The approximate solver in Box2D cannot hold many bodies together rigidly</i>.
/// </summary>
[PublicAPI]
public sealed class WeldJointDef
{
    //! \internal
    internal WeldJointDefInternal _internal = new();
    
    /// <summary>
    /// The first attached body
    /// </summary>
    public ref Body BodyA => ref _internal.BodyA;

    /// <summary>
    /// The second attached body
    /// </summary>
    public ref Body BodyB => ref _internal.BodyB;

    /// <summary>
    /// The local anchor point relative to bodyA's origin
    /// </summary>
    public ref Vec2 LocalAnchorA => ref _internal.LocalAnchorA;

    /// <summary>
    /// The local anchor point relative to bodyB's origin
    /// </summary>
    public ref Vec2 LocalAnchorB => ref _internal.LocalAnchorB;

    /// <summary>
    /// The bodyB angle minus bodyA angle in the reference state (radians)
    /// </summary>
    public ref float ReferenceAngle => ref _internal.ReferenceAngle;

    /// <summary>
    /// Linear stiffness expressed as Hertz (cycles per second). Use zero for maximum stiffness.
    /// </summary>
    public ref float LinearHertz => ref _internal.LinearHertz;

    /// <summary>
    /// Angular stiffness as Hertz (cycles per second). Use zero for maximum stiffness.
    /// </summary>
    public ref float AngularHertz => ref _internal.AngularHertz;

    /// <summary>
    /// Linear damping ratio, non-dimensional. Use 1 for critical damping.
    /// </summary>
    public ref float LinearDampingRatio => ref _internal.LinearDampingRatio;

    /// <summary>
    /// Linear damping ratio, non-dimensional. Use 1 for critical damping.
    /// </summary>
    public ref float AngularDampingRatio => ref _internal.AngularDampingRatio;

    /// <summary>
    /// Set this flag to true if the attached bodies should collide
    /// </summary>
    public bool CollideConnected
    {
        get => _internal.CollideConnected != 0;
        set => _internal.CollideConnected = (byte)(value ? 1 : 0);
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
    /// Construct a weld joint definition with the supplied values
    /// </summary>
    /// <param name="bodyA">The first attached body</param>
    /// <param name="bodyB">The second attached body</param>
    /// <param name="anchorA">The local anchor point relative to bodyA's origin</param>
    /// <param name="anchorB">The local anchor point relative to bodyB's origin</param>
    /// <param name="referenceAngle">The bodyB angle minus bodyA angle in the reference state (radians)</param>
    /// <param name="linearHertz">Linear stiffness expressed as Hertz (cycles per second). Use zero for maximum stiffness</param>
    /// <param name="angularHertz">Angular stiffness as Hertz (cycles per second). Use zero for maximum stiffness</param>
    /// <param name="linearDampingRatio">Linear damping ratio, non-dimensional. Use 1 for critical damping</param>
    /// <param name="angularDampingRatio">Angular damping ratio, non-dimensional. Use 1 for critical damping</param>
    /// <param name="collideConnected">Set this flag to true if the attached bodies should collide</param>
    /// <param name="userData">User data</param>
    public WeldJointDef(
        Body bodyA,
        Body bodyB,
        Vec2 anchorA,
        Vec2 anchorB,
        float referenceAngle = 0.0f,
        float linearHertz = 0.0f,
        float angularHertz = 0.0f,
        float linearDampingRatio = 0.0f,
        float angularDampingRatio = 0.0f,
        bool collideConnected = false,
        object? userData = null)
    {
        BodyA = bodyA;
        BodyB = bodyB;
        LocalAnchorA = anchorA;
        LocalAnchorB = anchorB;
        ReferenceAngle = referenceAngle;
        LinearHertz = linearHertz;
        AngularHertz = angularHertz;
        LinearDampingRatio = linearDampingRatio;
        AngularDampingRatio = angularDampingRatio;
        CollideConnected = collideConnected;
        UserData = userData;
    }
    
    /// <summary>
    /// Construct a weld joint definition with the default values
    /// </summary>
    public WeldJointDef()
    {
        _internal = new();
    }
}