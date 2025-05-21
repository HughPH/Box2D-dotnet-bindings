using System.Runtime.InteropServices;

namespace Box2D
{
    public partial class MouseJoint
    {
        #if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, Vec2, void> b2MouseJoint_SetTarget;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, Vec2> b2MouseJoint_GetTarget;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2MouseJoint_SetSpringHertz;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2MouseJoint_GetSpringHertz;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2MouseJoint_SetSpringDampingRatio;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2MouseJoint_GetSpringDampingRatio;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2MouseJoint_SetMaxForce;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2MouseJoint_GetMaxForce;

    static unsafe MouseJoint()
    {
        nint lib = Core.NativeLibHandle;
        NativeLibrary.TryGetExport(lib, "b2MouseJoint_SetTarget", out var p0);
        NativeLibrary.TryGetExport(lib, "b2MouseJoint_GetTarget", out var p1);
        NativeLibrary.TryGetExport(lib, "b2MouseJoint_SetSpringHertz", out var p2);
        NativeLibrary.TryGetExport(lib, "b2MouseJoint_GetSpringHertz", out var p3);
        NativeLibrary.TryGetExport(lib, "b2MouseJoint_SetSpringDampingRatio", out var p4);
        NativeLibrary.TryGetExport(lib, "b2MouseJoint_GetSpringDampingRatio", out var p5);
        NativeLibrary.TryGetExport(lib, "b2MouseJoint_SetMaxForce", out var p6);
        NativeLibrary.TryGetExport(lib, "b2MouseJoint_GetMaxForce", out var p7);

        b2MouseJoint_SetTarget = (delegate* unmanaged[Cdecl]<JointId, Vec2, void>)p0;
        b2MouseJoint_GetTarget = (delegate* unmanaged[Cdecl]<JointId, Vec2>)p1;
        b2MouseJoint_SetSpringHertz = (delegate* unmanaged[Cdecl]<JointId, float, void>)p2;
        b2MouseJoint_GetSpringHertz = (delegate* unmanaged[Cdecl]<JointId, float>)p3;
        b2MouseJoint_SetSpringDampingRatio = (delegate* unmanaged[Cdecl]<JointId, float, void>)p4;
        b2MouseJoint_GetSpringDampingRatio = (delegate* unmanaged[Cdecl]<JointId, float>)p5;
        b2MouseJoint_SetMaxForce = (delegate* unmanaged[Cdecl]<JointId, float, void>)p6;
        b2MouseJoint_GetMaxForce = (delegate* unmanaged[Cdecl]<JointId, float>)p7;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MouseJoint_SetTarget")]
    private static extern void b2MouseJoint_SetTarget(JointId jointId, Vec2 target);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MouseJoint_GetTarget")]
    private static extern Vec2 b2MouseJoint_GetTarget(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MouseJoint_SetSpringHertz")]
    private static extern void b2MouseJoint_SetSpringHertz(JointId jointId, float hertz);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MouseJoint_GetSpringHertz")]
    private static extern float b2MouseJoint_GetSpringHertz(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MouseJoint_SetSpringDampingRatio")]
    private static extern void b2MouseJoint_SetSpringDampingRatio(JointId jointId, float ratio);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MouseJoint_GetSpringDampingRatio")]
    private static extern float b2MouseJoint_GetSpringDampingRatio(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MouseJoint_SetMaxForce")]
    private static extern void b2MouseJoint_SetMaxForce(JointId jointId, float maxForce);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MouseJoint_GetMaxForce")]
    private static extern float b2MouseJoint_GetMaxForce(JointId jointId);
#endif
    }
}
