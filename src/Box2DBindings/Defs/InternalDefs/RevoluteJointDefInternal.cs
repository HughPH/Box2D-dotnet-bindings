using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Sequential)]
struct RevoluteJointDefInternal
{
    internal Body BodyA;
    
    internal Body BodyB;

    internal Vec2 LocalAnchorA;

    internal Vec2 LocalAnchorB;

    internal float ReferenceAngle;

    internal byte EnableSpring;

    internal float Hertz;

    internal float DampingRatio;

    internal byte EnableLimit;

    internal float LowerAngle;

    internal float UpperAngle;

    internal byte EnableMotor;

    internal float MaxMotorTorque;

    internal float MotorSpeed;

    internal float DrawSize;

    internal byte CollideConnected;

    internal nint UserData;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly int internalValue;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultRevoluteJointDef")]
    private static extern RevoluteJointDefInternal GetDefault();
    
    private static RevoluteJointDefInternal Default => GetDefault();
    
    public RevoluteJointDefInternal()
    {
        this = Default;
    }
}