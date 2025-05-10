using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Explicit)]
struct RevoluteJointDefInternal
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
    internal byte EnableSpring;

    [FieldOffset(40)]
    internal float Hertz;

    [FieldOffset(44)]
    internal float DampingRatio;

    [FieldOffset(48)]
    internal byte EnableLimit;

    [FieldOffset(52)]
    internal float LowerAngle;

    [FieldOffset(56)]
    internal float UpperAngle;

    [FieldOffset(60)]
    internal byte EnableMotor;

    [FieldOffset(64)]
    internal float MaxMotorTorque;

    [FieldOffset(68)]
    internal float MotorSpeed;

    [FieldOffset(72)]
    internal float DrawSize;

    [FieldOffset(76)]
    internal byte CollideConnected;

    [FieldOffset(80)]
    internal nint UserData;

    [FieldOffset(88)]
    private readonly int internalValue;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultRevoluteJointDef")]
    private static extern RevoluteJointDefInternal GetDefault();
    
    private static RevoluteJointDefInternal Default => GetDefault();
    
    public RevoluteJointDefInternal()
    {
        this = Default;
    }
}