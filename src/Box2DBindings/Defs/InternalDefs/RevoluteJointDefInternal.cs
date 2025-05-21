using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Sequential)]
struct RevoluteJointDefInternal
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<RevoluteJointDefInternal> b2DefaultRevoluteJointDef;

    static unsafe RevoluteJointDefInternal()
    {
        nint lib = Core.NativeLibHandle;
        NativeLibrary.TryGetExport(lib, "b2DefaultRevoluteJointDef", out var ptr);
        b2DefaultRevoluteJointDef = (delegate* unmanaged[Cdecl]<RevoluteJointDefInternal>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultRevoluteJointDef")]
    private static extern RevoluteJointDefInternal b2DefaultRevoluteJointDef();
#endif

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
    
    private static unsafe RevoluteJointDefInternal Default => b2DefaultRevoluteJointDef();
    
    public RevoluteJointDefInternal()
    {
        this = Default;
    }
}