using System.Runtime.InteropServices;

namespace Box2D
{
    partial class MotorJoint
    {
        #if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, Vec2, void> b2MotorJoint_SetLinearOffset;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, Vec2> b2MotorJoint_GetLinearOffset;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2MotorJoint_SetAngularOffset;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2MotorJoint_GetAngularOffset;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2MotorJoint_SetMaxForce;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2MotorJoint_GetMaxForce;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2MotorJoint_SetMaxTorque;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2MotorJoint_GetMaxTorque;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2MotorJoint_SetCorrectionFactor;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2MotorJoint_GetCorrectionFactor;

    static unsafe MotorJoint()
    {
        nint lib = Core.NativeLibHandle;
        NativeLibrary.TryGetExport(lib, "b2MotorJoint_SetLinearOffset", out var p0);
        NativeLibrary.TryGetExport(lib, "b2MotorJoint_GetLinearOffset", out var p1);
        NativeLibrary.TryGetExport(lib, "b2MotorJoint_SetAngularOffset", out var p2);
        NativeLibrary.TryGetExport(lib, "b2MotorJoint_GetAngularOffset", out var p3);
        NativeLibrary.TryGetExport(lib, "b2MotorJoint_SetMaxForce", out var p4);
        NativeLibrary.TryGetExport(lib, "b2MotorJoint_GetMaxForce", out var p5);
        NativeLibrary.TryGetExport(lib, "b2MotorJoint_SetMaxTorque", out var p6);
        NativeLibrary.TryGetExport(lib, "b2MotorJoint_GetMaxTorque", out var p7);
        NativeLibrary.TryGetExport(lib, "b2MotorJoint_SetCorrectionFactor", out var p8);
        NativeLibrary.TryGetExport(lib, "b2MotorJoint_GetCorrectionFactor", out var p9);

        b2MotorJoint_SetLinearOffset = (delegate* unmanaged[Cdecl]<JointId, Vec2, void>)p0;
        b2MotorJoint_GetLinearOffset = (delegate* unmanaged[Cdecl]<JointId, Vec2>)p1;
        b2MotorJoint_SetAngularOffset = (delegate* unmanaged[Cdecl]<JointId, float, void>)p2;
        b2MotorJoint_GetAngularOffset = (delegate* unmanaged[Cdecl]<JointId, float>)p3;
        b2MotorJoint_SetMaxForce = (delegate* unmanaged[Cdecl]<JointId, float, void>)p4;
        b2MotorJoint_GetMaxForce = (delegate* unmanaged[Cdecl]<JointId, float>)p5;
        b2MotorJoint_SetMaxTorque = (delegate* unmanaged[Cdecl]<JointId, float, void>)p6;
        b2MotorJoint_GetMaxTorque = (delegate* unmanaged[Cdecl]<JointId, float>)p7;
        b2MotorJoint_SetCorrectionFactor = (delegate* unmanaged[Cdecl]<JointId, float, void>)p8;
        b2MotorJoint_GetCorrectionFactor = (delegate* unmanaged[Cdecl]<JointId, float>)p9;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MotorJoint_SetLinearOffset")]
    private static extern void b2MotorJoint_SetLinearOffset(JointId jointId, Vec2 linearOffset);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MotorJoint_GetLinearOffset")]
    private static extern Vec2 b2MotorJoint_GetLinearOffset(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MotorJoint_SetAngularOffset")]
    private static extern void b2MotorJoint_SetAngularOffset(JointId jointId, float angularOffset);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MotorJoint_GetAngularOffset")]
    private static extern float b2MotorJoint_GetAngularOffset(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MotorJoint_SetMaxForce")]
    private static extern void b2MotorJoint_SetMaxForce(JointId jointId, float maxForce);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MotorJoint_GetMaxForce")]
    private static extern float b2MotorJoint_GetMaxForce(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MotorJoint_SetMaxTorque")]
    private static extern void b2MotorJoint_SetMaxTorque(JointId jointId, float maxTorque);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MotorJoint_GetMaxTorque")]
    private static extern float b2MotorJoint_GetMaxTorque(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MotorJoint_SetCorrectionFactor")]
    private static extern void b2MotorJoint_SetCorrectionFactor(JointId jointId, float factor);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MotorJoint_GetCorrectionFactor")]
    private static extern float b2MotorJoint_GetCorrectionFactor(JointId jointId);
#endif
    }
}
