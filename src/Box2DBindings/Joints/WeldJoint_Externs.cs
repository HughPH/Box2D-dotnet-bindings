using System.Runtime.InteropServices;

namespace Box2D
{
    public partial class WeldJoint
    {
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2WeldJoint_SetLinearHertz;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2WeldJoint_GetLinearHertz;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2WeldJoint_SetLinearDampingRatio;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2WeldJoint_GetLinearDampingRatio;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2WeldJoint_SetAngularHertz;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2WeldJoint_GetAngularHertz;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2WeldJoint_SetAngularDampingRatio;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2WeldJoint_GetAngularDampingRatio;

    static unsafe WeldJoint()
    {
        nint lib = nativeLibrary;
        NativeLibrary.TryGetExport(lib, "b2WeldJoint_SetLinearHertz", out var p2);
        NativeLibrary.TryGetExport(lib, "b2WeldJoint_GetLinearHertz", out var p3);
        NativeLibrary.TryGetExport(lib, "b2WeldJoint_SetLinearDampingRatio", out var p4);
        NativeLibrary.TryGetExport(lib, "b2WeldJoint_GetLinearDampingRatio", out var p5);
        NativeLibrary.TryGetExport(lib, "b2WeldJoint_SetAngularHertz", out var p6);
        NativeLibrary.TryGetExport(lib, "b2WeldJoint_GetAngularHertz", out var p7);
        NativeLibrary.TryGetExport(lib, "b2WeldJoint_SetAngularDampingRatio", out var p8);
        NativeLibrary.TryGetExport(lib, "b2WeldJoint_GetAngularDampingRatio", out var p9);

        b2WeldJoint_SetLinearHertz = (delegate* unmanaged[Cdecl]<JointId, float, void>)p2;
        b2WeldJoint_GetLinearHertz = (delegate* unmanaged[Cdecl]<JointId, float>)p3;
        b2WeldJoint_SetLinearDampingRatio = (delegate* unmanaged[Cdecl]<JointId, float, void>)p4;
        b2WeldJoint_GetLinearDampingRatio = (delegate* unmanaged[Cdecl]<JointId, float>)p5;
        b2WeldJoint_SetAngularHertz = (delegate* unmanaged[Cdecl]<JointId, float, void>)p6;
        b2WeldJoint_GetAngularHertz = (delegate* unmanaged[Cdecl]<JointId, float>)p7;
        b2WeldJoint_SetAngularDampingRatio = (delegate* unmanaged[Cdecl]<JointId, float, void>)p8;
        b2WeldJoint_GetAngularDampingRatio = (delegate* unmanaged[Cdecl]<JointId, float>)p9;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_SetLinearHertz")]
    private static extern void b2WeldJoint_SetLinearHertz(JointId jointId, float hertz);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_GetLinearHertz")]
    private static extern float b2WeldJoint_GetLinearHertz(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_SetLinearDampingRatio")]
    private static extern void b2WeldJoint_SetLinearDampingRatio(JointId jointId, float dampingRatio);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_GetLinearDampingRatio")]
    private static extern float b2WeldJoint_GetLinearDampingRatio(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_SetAngularHertz")]
    private static extern void b2WeldJoint_SetAngularHertz(JointId jointId, float hertz);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_GetAngularHertz")]
    private static extern float b2WeldJoint_GetAngularHertz(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_SetAngularDampingRatio")]
    private static extern void b2WeldJoint_SetAngularDampingRatio(JointId jointId, float dampingRatio);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WeldJoint_GetAngularDampingRatio")]
    private static extern float b2WeldJoint_GetAngularDampingRatio(JointId jointId);
#endif
    }
}
