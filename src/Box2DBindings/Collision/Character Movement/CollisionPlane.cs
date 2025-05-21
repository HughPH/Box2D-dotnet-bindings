using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D.Character_Movement;

/// <summary>
/// These are collision planes that can be fed to b2SolvePlanes. Normally
/// this is assembled by the user from plane results in b2PlaneResult.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public struct CollisionPlane
{
    /// <summary>
    /// The collision plane between the mover and some shape.
    /// </summary>
    public Plane Plane;

    /// <summary>
    /// Setting this to <see cref="float.MaxValue"/> makes the plane as rigid as possible. Lower values can
    /// make the plane collision soft. Usually in meters.
    /// </summary>
    public float PushLimit;

    /// <summary>
    /// The push on the mover determined by b2SolvePlanes. Usually in meters.
    /// </summary>
    public float Push;

    private byte clipVelocity;
    
    /// <summary>
    /// Indicates if b2ClipVector should clip against this plane. Should be false for soft collision.
    /// </summary>
    public bool ClipVelocity
    {
        get => clipVelocity != 0;
        set => clipVelocity = value ? (byte)1 : (byte)0;
    }
    
    /// <summary>
    /// Constructs a new CollisionPlane object with the given parameters.
    /// </summary>
    /// <param name="plane">The collision plane between the mover and some shape.</param>
    /// <param name="pushLimit">The plane rigidity. Setting this to <see cref="float.MaxValue"/> makes the plane as rigid as possible. Lower values can make the plane collision soft. Usually in meters.</param>
    /// <param name="push">The push on the mover determined by b2SolvePlanes. Usually in meters.</param>
    /// <param name="clipVelocity">Indicates if <see cref="Mover.ClipVector(in Vec2,Box2D.Character_Movement.CollisionPlane[])"/> should clip against this plane. Should be false for soft collision.</param>
    public CollisionPlane(Plane plane, float pushLimit, float push, bool clipVelocity)
    {
        Plane = plane;
        PushLimit = pushLimit;
        Push = push;
        this.clipVelocity = clipVelocity ? (byte)1 : (byte)0;
    }
    
    /// <summary>
    /// Constructs a new CollisionPlane object with default values.
    /// </summary>
    public CollisionPlane()
    {
        Plane = default;
        PushLimit = 0;
        Push = 0;
        clipVelocity = 0;
    }
}