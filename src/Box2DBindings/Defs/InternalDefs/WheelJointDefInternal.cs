using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Explicit)]
struct WheelJointDefInternal
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
    internal Vec2 LocalAxisA;

    [FieldOffset(40)]
    internal byte EnableSpring;

    [FieldOffset(44)]
    internal float Hertz;

    [FieldOffset(48)]
    internal float DampingRatio;

    [FieldOffset(52)]
    internal byte EnableLimit;

    [FieldOffset(56)]
    internal float LowerTranslation;

    [FieldOffset(60)]
    internal float UpperTranslation;

    [FieldOffset(64)]
    internal byte EnableMotor;

    [FieldOffset(68)]
    internal float MaxMotorTorque;

    [FieldOffset(72)]
    internal float MotorSpeed;

    [FieldOffset(76)]
    internal byte CollideConnected;

    [FieldOffset(80)]
    internal nint UserData;

    [FieldOffset(88)]
    private readonly int internalValue;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultWheelJointDef")]
    private static extern WheelJointDefInternal GetDefault();
    
    private static WheelJointDefInternal Default => GetDefault();
    
    public WheelJointDefInternal()
    {
        this = Default;
    }
}