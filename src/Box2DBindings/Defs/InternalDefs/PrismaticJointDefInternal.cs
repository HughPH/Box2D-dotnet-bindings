using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Sequential)]
struct PrismaticJointDefInternal
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<PrismaticJointDefInternal> b2DefaultPrismaticJointDef;

    static unsafe PrismaticJointDefInternal()
    {
        nint lib = Core.NativeLibHandle;
        NativeLibrary.TryGetExport(lib, "b2DefaultPrismaticJointDef", out var ptr);
        b2DefaultPrismaticJointDef = (delegate* unmanaged[Cdecl]<PrismaticJointDefInternal>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultPrismaticJointDef")]
    private static extern PrismaticJointDefInternal b2DefaultPrismaticJointDef();
#endif
    
    internal Body BodyA;

    internal Body BodyB;

    internal Vec2 LocalAnchorA;

    internal Vec2 LocalAnchorB;

    internal Vec2 LocalAxisA;

    internal float ReferenceAngle;

    internal byte EnableSpring;

    internal float Hertz;

    internal float DampingRatio;

    internal byte EnableLimit;

    internal float LowerTranslation;

    internal float UpperTranslation;

    internal byte EnableMotor;

    internal float MaxMotorForce;

    internal float MotorSpeed;

    internal byte CollideConnected;

    internal nint UserData;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly int internalValue;
    
    private static unsafe PrismaticJointDefInternal Default => b2DefaultPrismaticJointDef();
    
    public PrismaticJointDefInternal()
    {
        this = Default;
    }
}