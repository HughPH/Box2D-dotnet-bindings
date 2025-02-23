using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Body move events triggered when a body moves.
/// Triggered when a body moves due to simulation. Not reported for bodies moved by the user.
/// This also has a flag to indicate that the body went to sleep so the application can also
/// sleep that actor/entity/object associated with the body.
/// On the other hand if the flag does not indicate the body went to sleep then the application
/// can treat the actor/entity/object associated with the body as awake.
/// This is an efficient way for an application to update game object transforms rather than
/// calling functions such as Body.GetTransform() because this data is delivered as a contiguous array
/// and it is only populated with bodies that have moved.
/// <i>Note: If sleeping is disabled all dynamic and kinematic bodies will trigger move events.</i>
/// </summary>
[StructLayout(LayoutKind.Sequential, Pack = 4)]
public struct BodyMoveEvent
{
    public Transform Transform;
    public Body Body;
    public nint UserData;
    [MarshalAs(UnmanagedType.U1)]
    public bool FellAsleep;
}