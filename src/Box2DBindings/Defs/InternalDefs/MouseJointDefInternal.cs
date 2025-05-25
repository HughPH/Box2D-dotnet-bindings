using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Sequential)]
struct MouseJointDefInternal
{
    #if NET5_0_OR_GREATER
    private static unsafe delegate* unmanaged[Cdecl]<MouseJointDefInternal> b2DefaultMouseJointDef;

    static unsafe MouseJointDefInternal()
    {
        nint lib = nativeLibrary;
        NativeLibrary.TryGetExport(lib, "b2DefaultMouseJointDef", out var ptr);
        b2DefaultMouseJointDef = (delegate* unmanaged[Cdecl]<MouseJointDefInternal>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultMouseJointDef")]
    private static extern MouseJointDefInternal b2DefaultMouseJointDef();
#endif
    
    internal BodyId BodyA;
    
    internal BodyId BodyB;

    internal Vec2 Target;

    internal float Hertz;

    internal float DampingRatio;

    internal float MaxForce;

    internal byte CollideConnected;

    internal nint UserData;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly int internalValue;
    
    private static unsafe MouseJointDefInternal Default => b2DefaultMouseJointDef();
    
    public MouseJointDefInternal()
    {
        this = Default;
    }
}