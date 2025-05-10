using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Sequential)]
struct DistanceJointDefInternal
{
    internal Body BodyA;

    internal Body BodyB;

    internal Vec2 LocalAnchorA;

    internal Vec2 LocalAnchorB;

    internal float Length;

    internal byte EnableSpring;

    internal float Hertz;

    internal float DampingRatio;

    internal byte EnableLimit;

    internal float MinLength;

    internal float MaxLength;

    internal byte EnableMotor;

    internal float MaxMotorForce;

    internal float MotorSpeed;

    internal byte CollideConnected;
    
    internal nint UserData;

    internal readonly int internalValue;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultDistanceJointDef")]
    private static extern DistanceJointDefInternal GetDefault();
    
    private static DistanceJointDefInternal Default => GetDefault();

    public DistanceJointDefInternal()
    {
        this = Default;
    }
}