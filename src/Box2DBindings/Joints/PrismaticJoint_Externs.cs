using System.Runtime.InteropServices;

namespace Box2D
{
    partial class PrismaticJoint
    {
        #if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte, void> b2PrismaticJoint_EnableSpring;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte> b2PrismaticJoint_IsSpringEnabled;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2PrismaticJoint_SetSpringHertz;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2PrismaticJoint_GetSpringHertz;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2PrismaticJoint_SetSpringDampingRatio;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2PrismaticJoint_GetSpringDampingRatio;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte, void> b2PrismaticJoint_EnableLimit;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte> b2PrismaticJoint_IsLimitEnabled;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, float, void> b2PrismaticJoint_SetLimits;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2PrismaticJoint_GetLowerLimit;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2PrismaticJoint_GetUpperLimit;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte, void> b2PrismaticJoint_EnableMotor;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte> b2PrismaticJoint_IsMotorEnabled;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2PrismaticJoint_SetMotorSpeed;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2PrismaticJoint_GetMotorSpeed;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2PrismaticJoint_SetMaxMotorForce;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2PrismaticJoint_GetMaxMotorForce;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2PrismaticJoint_GetMotorForce;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2PrismaticJoint_GetTranslation;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2PrismaticJoint_GetSpeed;

    static unsafe PrismaticJoint()
    {
        nint lib = NativeLibrary.Load(libraryName);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_EnableSpring", out var p0);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_IsSpringEnabled", out var p1);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_SetSpringHertz", out var p2);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_GetSpringHertz", out var p3);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_SetSpringDampingRatio", out var p4);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_GetSpringDampingRatio", out var p5);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_EnableLimit", out var p6);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_IsLimitEnabled", out var p7);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_SetLimits", out var p8);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_GetLowerLimit", out var p9);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_GetUpperLimit", out var p10);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_EnableMotor", out var p11);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_IsMotorEnabled", out var p12);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_SetMotorSpeed", out var p13);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_GetMotorSpeed", out var p14);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_SetMaxMotorForce", out var p15);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_GetMaxMotorForce", out var p16);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_GetMotorForce", out var p17);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_GetTranslation", out var p18);
        NativeLibrary.TryGetExport(lib, "b2PrismaticJoint_GetSpeed", out var p19);

        b2PrismaticJoint_EnableSpring = (delegate* unmanaged[Cdecl]<JointId, byte, void>)p0;
        b2PrismaticJoint_IsSpringEnabled = (delegate* unmanaged[Cdecl]<JointId, byte>)p1;
        b2PrismaticJoint_SetSpringHertz = (delegate* unmanaged[Cdecl]<JointId, float, void>)p2;
        b2PrismaticJoint_GetSpringHertz = (delegate* unmanaged[Cdecl]<JointId, float>)p3;
        b2PrismaticJoint_SetSpringDampingRatio = (delegate* unmanaged[Cdecl]<JointId, float, void>)p4;
        b2PrismaticJoint_GetSpringDampingRatio = (delegate* unmanaged[Cdecl]<JointId, float>)p5;
        b2PrismaticJoint_EnableLimit = (delegate* unmanaged[Cdecl]<JointId, byte, void>)p6;
        b2PrismaticJoint_IsLimitEnabled = (delegate* unmanaged[Cdecl]<JointId, byte>)p7;
        b2PrismaticJoint_SetLimits = (delegate* unmanaged[Cdecl]<JointId, float, float, void>)p8;
        b2PrismaticJoint_GetLowerLimit = (delegate* unmanaged[Cdecl]<JointId, float>)p9;
        b2PrismaticJoint_GetUpperLimit = (delegate* unmanaged[Cdecl]<JointId, float>)p10;
        b2PrismaticJoint_EnableMotor = (delegate* unmanaged[Cdecl]<JointId, byte, void>)p11;
        b2PrismaticJoint_IsMotorEnabled = (delegate* unmanaged[Cdecl]<JointId, byte>)p12;
        b2PrismaticJoint_SetMotorSpeed = (delegate* unmanaged[Cdecl]<JointId, float, void>)p13;
        b2PrismaticJoint_GetMotorSpeed = (delegate* unmanaged[Cdecl]<JointId, float>)p14;
        b2PrismaticJoint_SetMaxMotorForce = (delegate* unmanaged[Cdecl]<JointId, float, void>)p15;
        b2PrismaticJoint_GetMaxMotorForce = (delegate* unmanaged[Cdecl]<JointId, float>)p16;
        b2PrismaticJoint_GetMotorForce = (delegate* unmanaged[Cdecl]<JointId, float>)p17;
        b2PrismaticJoint_GetTranslation = (delegate* unmanaged[Cdecl]<JointId, float>)p18;
        b2PrismaticJoint_GetSpeed = (delegate* unmanaged[Cdecl]<JointId, float>)p19;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_EnableSpring")]
    private static extern void b2PrismaticJoint_EnableSpring(JointId jointId, byte enableSpring);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_IsSpringEnabled")]
    private static extern byte b2PrismaticJoint_IsSpringEnabled(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_SetSpringHertz")]
    private static extern void b2PrismaticJoint_SetSpringHertz(JointId jointId, float hertz);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetSpringHertz")]
    private static extern float b2PrismaticJoint_GetSpringHertz(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_SetSpringDampingRatio")]
    private static extern void b2PrismaticJoint_SetSpringDampingRatio(JointId jointId, float dampingRatio);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetSpringDampingRatio")]
    private static extern float b2PrismaticJoint_GetSpringDampingRatio(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_EnableLimit")]
    private static extern void b2PrismaticJoint_EnableLimit(JointId jointId, byte enableLimit);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_IsLimitEnabled")]
    private static extern byte b2PrismaticJoint_IsLimitEnabled(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_SetLimits")]
    private static extern void b2PrismaticJoint_SetLimits(JointId jointId, float lower, float upper);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetLowerLimit")]
    private static extern float b2PrismaticJoint_GetLowerLimit(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetUpperLimit")]
    private static extern float b2PrismaticJoint_GetUpperLimit(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_EnableMotor")]
    private static extern void b2PrismaticJoint_EnableMotor(JointId jointId, byte enableMotor);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_IsMotorEnabled")]
    private static extern byte b2PrismaticJoint_IsMotorEnabled(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_SetMotorSpeed")]
    private static extern void b2PrismaticJoint_SetMotorSpeed(JointId jointId, float motorSpeed);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetMotorSpeed")]
    private static extern float b2PrismaticJoint_GetMotorSpeed(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_SetMaxMotorForce")]
    private static extern void b2PrismaticJoint_SetMaxMotorForce(JointId jointId, float force);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetMaxMotorForce")]
    private static extern float b2PrismaticJoint_GetMaxMotorForce(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetMotorForce")]
    private static extern float b2PrismaticJoint_GetMotorForce(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetTranslation")]
    private static extern float b2PrismaticJoint_GetTranslation(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PrismaticJoint_GetSpeed")]
    private static extern float b2PrismaticJoint_GetSpeed(JointId jointId);
#endif
    }
}
