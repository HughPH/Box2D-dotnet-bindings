using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A body definition holds all the data needed to construct a rigid body.
/// You can safely re-use body definitions. Shapes are added to a body after construction.
/// Body definitions are temporary objects used to bundle creation parameters.
/// </summary>
[PublicAPI]
public class BodyDef
{
    //! \internal
    internal BodyDefInternal _internal = new();
    
    //! \internal
    ~BodyDef()
    {
        if (_internal.Name != 0)
        {
            Marshal.FreeHGlobal(_internal.Name);
            _internal.Name = 0;
        }
    }

    /// <summary>
    /// The body type: static, kinematic, or dynamic.
    /// </summary>
    public ref BodyType Type => ref _internal.Type;

    /// <summary>
    /// The initial world position of the body. Bodies should be created with the desired position.
    /// <i>Note: Creating bodies at the origin and then moving them nearly doubles the cost of body creation, especially
    /// if the body is moved after shapes have been added.</i>
    /// </summary>
    public ref Vec2 Position => ref _internal.Position;

    /// <summary>
    /// The initial world rotation of the body.
    /// </summary>
    public ref Rotation Rotation => ref _internal.Rotation;

    /// <summary>
    /// The initial linear velocity of the body's origin. Usually in meters per second.
    /// </summary>
    public ref Vec2 LinearVelocity => ref _internal.LinearVelocity;

    /// <summary>
    /// The initial angular velocity of the body. Radians per second.
    /// </summary>
    public ref float AngularVelocity => ref _internal.AngularVelocity;

    /// <summary>
    /// Linear damping is used to reduce the linear velocity. The damping parameter
    /// can be larger than 1 but the damping effect becomes sensitive to the
    /// time step when the damping parameter is large.
    /// Generally linear damping is undesirable because it makes objects move slowly
    /// as if they are floating.
    /// </summary>
    public ref float LinearDamping => ref _internal.LinearDamping;

    /// <summary>
    /// Angular damping is used to reduce the angular velocity. The damping parameter
    /// can be larger than 1.0f but the damping effect becomes sensitive to the
    /// time step when the damping parameter is large.
    /// Angular damping can be use slow down rotating bodies.
    /// </summary>
    public ref float AngularDamping => ref _internal.AngularDamping;

    /// <summary>
    /// Scale the gravity applied to this body. Non-dimensional.
    /// </summary>
    public ref float GravityScale => ref _internal.GravityScale;

    /// <summary>
    /// Sleep speed threshold, default is 0.05 meters per second
    /// </summary>
    public ref float SleepThreshold => ref _internal.SleepThreshold;

    /// <summary>
    /// Optional body name for debugging. Up to 31 characters (excluding null termination)
    /// </summary>
    public string? Name
    {
        get => Marshal.PtrToStringAnsi(_internal.Name);
        set
        {
            if (_internal.Name != 0)
            {
                Marshal.FreeHGlobal(_internal.Name);
                _internal.Name = 0;
            }
            if (value != null)
            {
                if (value.Length > 31)
                    throw new ArgumentOutOfRangeException(nameof(value), "Name must be 31 characters or less");
                _internal.Name = Marshal.StringToHGlobalAnsi(value);
            }
        }
    }

    /// <summary>
    /// Use this to store application specific body data.
    /// </summary>
    public object? UserData
    {
        get => GetObjectAtPointer(_internal.UserData);
        set => SetObjectAtPointer(ref _internal.UserData, value);
    }

    /// <summary>
    /// Set this flag to false if this body should never fall asleep.
    /// </summary>
    public bool EnableSleep
    {
        get => _internal.EnableSleep != 0;
        set => _internal.EnableSleep = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// Is this body initially awake or sleeping?
    /// </summary>
    public bool IsAwake
    {
        get => _internal.IsAwake != 0;
        set => _internal.IsAwake = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// Should this body be prevented from rotating? Useful for characters.
    /// </summary>
    public bool FixedRotation
    {
        get => _internal.FixedRotation != 0;
        set => _internal.FixedRotation = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// Treat this body as high speed object that performs continuous collision detection
    /// against dynamic and kinematic bodies, but not other bullet bodies.
    /// <b>Warning: Bullets should be used sparingly. They are not a solution for general dynamic-versus-dynamic
    /// continuous collision. They may interfere with joint constraints.</b>
    /// </summary>
    public bool IsBullet
    {
        get => _internal.IsBullet != 0;
        set => _internal.IsBullet = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// Used to disable a body. A disabled body does not move or collide.
    /// </summary>
    public bool IsEnabled
    {
        get => _internal.IsEnabled != 0;
        set => _internal.IsEnabled = value ? (byte)1 : (byte)0;
    }

    /// <summary>
    /// This allows this body to bypass rotational speed limits. Should only be used
    /// for circular objects, like wheels.
    /// </summary>
    public bool AllowFastRotation
    {
        get => _internal.AllowFastRotation != 0;
        set => _internal.AllowFastRotation = value ? (byte)1 : (byte)0;
    }

}