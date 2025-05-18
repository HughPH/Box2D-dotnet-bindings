using System.Runtime.InteropServices;

namespace Box2D
{
    public partial class WheelJoint
    {
        #if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte, void> b2WheelJoint_EnableSpring;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte> b2WheelJoint_IsSpringEnabled;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2WheelJoint_SetSpringHertz;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2WheelJoint_GetSpringHertz;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2WheelJoint_SetSpringDampingRatio;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2WheelJoint_GetSpringDampingRatio;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte, void> b2WheelJoint_EnableLimit;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte> b2WheelJoint_IsLimitEnabled;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, float, void> b2WheelJoint_SetLimits;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2WheelJoint_GetLowerLimit;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2WheelJoint_GetUpperLimit;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte, void> b2WheelJoint_EnableMotor;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte> b2WheelJoint_IsMotorEnabled;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2WheelJoint_SetMotorSpeed;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2WheelJoint_GetMotorSpeed;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2WheelJoint_SetMaxMotorTorque;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2WheelJoint_GetMaxMotorTorque;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2WheelJoint_GetMotorTorque;

    static unsafe WheelJoint()
    {
        nint lib = NativeLibrary.Load(libraryName);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_EnableSpring", out var p0);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_IsSpringEnabled", out var p1);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_SetSpringHertz", out var p2);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_GetSpringHertz", out var p3);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_SetSpringDampingRatio", out var p4);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_GetSpringDampingRatio", out var p5);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_EnableLimit", out var p6);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_IsLimitEnabled", out var p7);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_SetLimits", out var p8);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_GetLowerLimit", out var p9);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_GetUpperLimit", out var p10);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_EnableMotor", out var p11);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_IsMotorEnabled", out var p12);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_SetMotorSpeed", out var p13);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_GetMotorSpeed", out var p14);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_SetMaxMotorTorque", out var p15);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_GetMaxMotorTorque", out var p16);
        NativeLibrary.TryGetExport(lib, "b2WheelJoint_GetMotorTorque", out var p17);

        b2WheelJoint_EnableSpring = (delegate* unmanaged[Cdecl]<JointId, byte, void>)p0;
        b2WheelJoint_IsSpringEnabled = (delegate* unmanaged[Cdecl]<JointId, byte>)p1;
        b2WheelJoint_SetSpringHertz = (delegate* unmanaged[Cdecl]<JointId, float, void>)p2;
        b2WheelJoint_GetSpringHertz = (delegate* unmanaged[Cdecl]<JointId, float>)p3;
        b2WheelJoint_SetSpringDampingRatio = (delegate* unmanaged[Cdecl]<JointId, float, void>)p4;
        b2WheelJoint_GetSpringDampingRatio = (delegate* unmanaged[Cdecl]<JointId, float>)p5;
        b2WheelJoint_EnableLimit = (delegate* unmanaged[Cdecl]<JointId, byte, void>)p6;
        b2WheelJoint_IsLimitEnabled = (delegate* unmanaged[Cdecl]<JointId, byte>)p7;
        b2WheelJoint_SetLimits = (delegate* unmanaged[Cdecl]<JointId, float, float, void>)p8;
        b2WheelJoint_GetLowerLimit = (delegate* unmanaged[Cdecl]<JointId, float>)p9;
        b2WheelJoint_GetUpperLimit = (delegate* unmanaged[Cdecl]<JointId, float>)p10;
        b2WheelJoint_EnableMotor = (delegate* unmanaged[Cdecl]<JointId, byte, void>)p11;
        b2WheelJoint_IsMotorEnabled = (delegate* unmanaged[Cdecl]<JointId, byte>)p12;
        b2WheelJoint_SetMotorSpeed = (delegate* unmanaged[Cdecl]<JointId, float, void>)p13;
        b2WheelJoint_GetMotorSpeed = (delegate* unmanaged[Cdecl]<JointId, float>)p14;
        b2WheelJoint_SetMaxMotorTorque = (delegate* unmanaged[Cdecl]<JointId, float, void>)p15;
        b2WheelJoint_GetMaxMotorTorque = (delegate* unmanaged[Cdecl]<JointId, float>)p16;
        b2WheelJoint_GetMotorTorque = (delegate* unmanaged[Cdecl]<JointId, float>)p17;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_EnableSpring")]
    private static extern void b2WheelJoint_EnableSpring(JointId jointId, byte enableSpring);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_IsSpringEnabled")]
    private static extern byte b2WheelJoint_IsSpringEnabled(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_SetSpringHertz")]
    private static extern void b2WheelJoint_SetSpringHertz(JointId jointId, float hertz);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_GetSpringHertz")]
    private static extern float b2WheelJoint_GetSpringHertz(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_SetSpringDampingRatio")]
    private static extern void b2WheelJoint_SetSpringDampingRatio(JointId jointId, float dampingRatio);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_GetSpringDampingRatio")]
    private static extern float b2WheelJoint_GetSpringDampingRatio(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_EnableLimit")]
    private static extern void b2WheelJoint_EnableLimit(JointId jointId, byte enableLimit);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_IsLimitEnabled")]
    private static extern byte b2WheelJoint_IsLimitEnabled(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_SetLimits")]
    private static extern void b2WheelJoint_SetLimits(JointId jointId, float lower, float upper);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_GetLowerLimit")]
    private static extern float b2WheelJoint_GetLowerLimit(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_GetUpperLimit")]
    private static extern float b2WheelJoint_GetUpperLimit(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_EnableMotor")]
    private static extern void b2WheelJoint_EnableMotor(JointId jointId, byte enableMotor);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_IsMotorEnabled")]
    private static extern byte b2WheelJoint_IsMotorEnabled(JointId jointId);
        
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_SetMotorSpeed")]
    private static extern void b2WheelJoint_SetMotorSpeed(JointId jointId, float motorSpeed);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_GetMotorSpeed")]
    private static extern float b2WheelJoint_GetMotorSpeed(JointId jointId);
        
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_SetMaxMotorTorque")]
    private static extern void b2WheelJoint_SetMaxMotorTorque(JointId jointId, float torque);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_GetMaxMotorTorque")]
    private static extern float b2WheelJoint_GetMaxMotorTorque(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2WheelJoint_GetMotorTorque")]
    private static extern float b2WheelJoint_GetMotorTorque(JointId jointId);

        
#endif
    }
}
