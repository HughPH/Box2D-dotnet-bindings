using System.Runtime.InteropServices;

namespace Box2D;

//! \internal
[StructLayout(LayoutKind.Sequential)]
struct FilterJointDefInternal
{
    internal Body BodyA;
    
    internal Body BodyB;

    internal nint UserData;

    // ReSharper disable once PrivateFieldCanBeConvertedToLocalVariable
    private readonly int internalValue;

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultNullJointDef")]
    private static extern FilterJointDefInternal GetDefault();
    
    private static FilterJointDefInternal Default => GetDefault();
    
    public FilterJointDefInternal()
    {
        this = Default;
    }
}