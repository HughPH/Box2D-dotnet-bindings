using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Sequential)]
struct PrismaticJointDefInternal
{
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
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultPrismaticJointDef")]
    private static extern PrismaticJointDefInternal GetDefault();
    
    private static PrismaticJointDefInternal Default => GetDefault();
    
    public PrismaticJointDefInternal()
    {
        this = Default;
    }
}