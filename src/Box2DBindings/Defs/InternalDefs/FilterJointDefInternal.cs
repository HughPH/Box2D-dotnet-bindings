using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Sequential)]
struct FilterJointDefInternal
{
#if NET5_0_OR_GREATER
    private static unsafe delegate* unmanaged[Cdecl]<FilterJointDefInternal> b2DefaultNullJointDef;

    static unsafe FilterJointDefInternal()
    {
        nint lib = Core.NativeLibHandle;
        NativeLibrary.TryGetExport(lib, "b2DefaultNullJointDef", out var ptr);
        b2DefaultNullJointDef = (delegate* unmanaged[Cdecl]<FilterJointDefInternal>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultNullJointDef")]
    private static extern FilterJointDefInternal b2DefaultNullJointDef();
#endif
    
    internal Body BodyA;
    
    internal Body BodyB;

    internal nint UserData;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly int internalValue;

    private static unsafe FilterJointDefInternal Default => b2DefaultNullJointDef();
    
    public FilterJointDefInternal()
    {
        this = Default;
    }
}