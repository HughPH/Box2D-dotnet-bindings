using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Sequential)]
struct MouseJointDefInternal
{
    internal Body BodyA;
    
    internal Body BodyB;

    internal Vec2 Target;

    internal float Hertz;

    internal float DampingRatio;

    internal float MaxForce;

    internal byte CollideConnected;

    internal nint UserData;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly int internalValue;
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultMouseJointDef")]
    private static extern MouseJointDefInternal GetDefault();
    
    private static MouseJointDefInternal Default => GetDefault();
    
    public MouseJointDefInternal()
    {
        this = Default;
    }
}