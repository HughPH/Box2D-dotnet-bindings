using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Explicit)]
struct MouseJointDefInternal
{
    [FieldOffset(0)]
    internal Body BodyA;
    
    [FieldOffset(8)]
    internal Body BodyB;

    [FieldOffset(16)]
    internal Vec2 Target;

    [FieldOffset(24)]
    internal float Hertz;

    [FieldOffset(28)]
    internal float DampingRatio;

    [FieldOffset(32)]
    internal float MaxForce;

    [FieldOffset(36)]
    internal byte CollideConnected;

    [FieldOffset(40)]
    internal nint UserData;

    [FieldOffset(48)]
    private readonly int internalValue;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultMouseJointDef")]
    private static extern MouseJointDefInternal GetDefault();
    
    private static MouseJointDefInternal Default => GetDefault();
    
    public MouseJointDefInternal()
    {
        this = Default;
    }
}