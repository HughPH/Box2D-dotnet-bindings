using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Explicit)]
struct PrismaticJointDefInternal
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
    internal float ReferenceAngle;

    [FieldOffset(44)]
    internal byte EnableSpring;

    [FieldOffset(48)]
    internal float Hertz;

    [FieldOffset(52)]
    internal float DampingRatio;

    [FieldOffset(56)]
    internal byte EnableLimit;

    [FieldOffset(60)]
    internal float LowerTranslation;

    [FieldOffset(64)]
    internal float UpperTranslation;

    [FieldOffset(68)]
    internal byte EnableMotor;

    [FieldOffset(72)]
    internal float MaxMotorForce;

    [FieldOffset(76)]
    internal float MotorSpeed;

    [FieldOffset(80)]
    internal byte CollideConnected;

    [FieldOffset(88)]
    internal nint UserData;

    [FieldOffset(96)]
    private readonly int internalValue;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultPrismaticJointDef")]
    private static extern PrismaticJointDefInternal GetDefault();
    
    private static PrismaticJointDefInternal Default => GetDefault();
    
    public PrismaticJointDefInternal()
    {
        this = Default;
    }
}