using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Sequential)]
struct DistanceJointDefInternal
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<DistanceJointDefInternal> b2DefaultDistanceJointDef;

    static unsafe DistanceJointDefInternal()
    {
        nint lib = Core.NativeLibHandle;
        NativeLibrary.TryGetExport(lib, "b2DefaultDistanceJointDef", out var ptr);
        b2DefaultDistanceJointDef = (delegate* unmanaged[Cdecl]<DistanceJointDefInternal>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultDistanceJointDef")]
    private static extern DistanceJointDefInternal b2DefaultDistanceJointDef();
#endif
    
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
    
    private static unsafe DistanceJointDefInternal Default => b2DefaultDistanceJointDef();

    public DistanceJointDefInternal()
    {
        this = Default;
    }
}