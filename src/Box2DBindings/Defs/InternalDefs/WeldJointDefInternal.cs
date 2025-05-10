using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Explicit)]
struct WeldJointDefInternal
{
    [FieldOffset(0)]
    internal Body BodyA;

    [FieldOffset(8)]
    internal Body BodyB;

    [FieldOffset(16)]
    internal Vec2 LocalAnchorA;

    [FieldOffset(24)]
    internal Vec2 LocalAnchorB;

    [FieldOffset(32)]
    internal float ReferenceAngle;

    [FieldOffset(36)]
    internal float LinearHertz;

    [FieldOffset(40)]
    internal float AngularHertz;

    [FieldOffset(44)]
    internal float LinearDampingRatio;

    [FieldOffset(48)]
    internal float AngularDampingRatio;

    [FieldOffset(52)]
    internal byte CollideConnected;

    [FieldOffset(56)]
    internal nint UserData;

    [FieldOffset(64)]
    private readonly int internalValue;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultWeldJointDef")]
    private static extern WeldJointDefInternal GetDefault();
    
    private static WeldJointDefInternal Default => GetDefault();
    
    public WeldJointDefInternal()
    {
        this = Default;
    }
}