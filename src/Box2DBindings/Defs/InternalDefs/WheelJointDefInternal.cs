using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Explicit)]
struct WheelJointDefInternal
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<WheelJointDefInternal> b2DefaultWheelJointDef;

    static unsafe WheelJointDefInternal()
    {
        nint lib = Core.NativeLibHandle;
        NativeLibrary.TryGetExport(lib, "b2DefaultWheelJointDef", out var ptr);
        b2DefaultWheelJointDef = (delegate* unmanaged[Cdecl]<WheelJointDefInternal>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultWheelJointDef")]
    private static extern WheelJointDefInternal b2DefaultWheelJointDef();
#endif
    
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
    
    private static unsafe WheelJointDefInternal Default => b2DefaultWheelJointDef();
    
    public WheelJointDefInternal()
    {
        this = Default;
    }
}