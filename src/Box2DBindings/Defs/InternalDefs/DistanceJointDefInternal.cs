using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Explicit)]
struct DistanceJointDefInternal
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
    internal float Length;

    [FieldOffset(36)]
    internal byte EnableSpring;

    [FieldOffset(40)]
    internal float Hertz;

    [FieldOffset(44)]
    internal float DampingRatio;

    [FieldOffset(48)]
    internal byte EnableLimit;

    [FieldOffset(52)]
    internal float MinLength;

    [FieldOffset(56)]
    internal float MaxLength;

    [FieldOffset(60)]
    internal byte EnableMotor;

    [FieldOffset(64)]
    internal float MaxMotorForce;

    [FieldOffset(68)]
    internal float MotorSpeed;

    [FieldOffset(72)]
    internal byte CollideConnected;
    
    [FieldOffset(80)]
    internal nint UserData;

    [FieldOffset(88)]
    internal readonly int internalValue;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultDistanceJointDef")]
    private static extern DistanceJointDefInternal GetDefault();
    
    private static DistanceJointDefInternal Default => GetDefault();

    public DistanceJointDefInternal()
    {
        this = Default;
    }
}