using System.Runtime.InteropServices;

namespace Box2D
{
    partial class RevoluteJoint
    {
        #if NET5_0_OR_GREATER
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte, void> b2RevoluteJoint_EnableSpring;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte> b2RevoluteJoint_IsSpringEnabled;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2RevoluteJoint_SetSpringHertz;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2RevoluteJoint_GetSpringHertz;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2RevoluteJoint_SetSpringDampingRatio;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2RevoluteJoint_GetSpringDampingRatio;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2RevoluteJoint_GetAngle;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte, void> b2RevoluteJoint_EnableLimit;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte> b2RevoluteJoint_IsLimitEnabled;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, float, void> b2RevoluteJoint_SetLimits;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2RevoluteJoint_GetLowerLimit;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2RevoluteJoint_GetUpperLimit;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte, void> b2RevoluteJoint_EnableMotor;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte> b2RevoluteJoint_IsMotorEnabled;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2RevoluteJoint_SetMotorSpeed;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2RevoluteJoint_GetMotorSpeed;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2RevoluteJoint_GetMotorTorque;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2RevoluteJoint_SetMaxMotorTorque;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2RevoluteJoint_GetMaxMotorTorque;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2RevoluteJoint_SetTargetAngle;
        private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2RevoluteJoint_GetTargetAngle;

        static unsafe RevoluteJoint()
        {
            nint lib = nativeLibrary;
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_EnableSpring", out var p0);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_IsSpringEnabled", out var p1);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_SetSpringHertz", out var p2);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_GetSpringHertz", out var p3);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_SetSpringDampingRatio", out var p4);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_GetSpringDampingRatio", out var p5);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_GetAngle", out var p6);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_EnableLimit", out var p7);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_IsLimitEnabled", out var p8);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_SetLimits", out var p9);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_GetLowerLimit", out var p10);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_GetUpperLimit", out var p11);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_EnableMotor", out var p12);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_IsMotorEnabled", out var p13);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_SetMotorSpeed", out var p14);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_GetMotorSpeed", out var p15);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_GetMotorTorque", out var p16);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_SetMaxMotorTorque", out var p17);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_GetMaxMotorTorque", out var p18);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_SetTargetAngle", out var p19);
            NativeLibrary.TryGetExport(lib, "b2RevoluteJoint_GetTargetAngle", out var p20);

            b2RevoluteJoint_EnableSpring = (delegate* unmanaged[Cdecl]<JointId, byte, void>)p0;
            b2RevoluteJoint_IsSpringEnabled = (delegate* unmanaged[Cdecl]<JointId, byte>)p1;
            b2RevoluteJoint_SetSpringHertz = (delegate* unmanaged[Cdecl]<JointId, float, void>)p2;
            b2RevoluteJoint_GetSpringHertz = (delegate* unmanaged[Cdecl]<JointId, float>)p3;
            b2RevoluteJoint_SetSpringDampingRatio = (delegate* unmanaged[Cdecl]<JointId, float, void>)p4;
            b2RevoluteJoint_GetSpringDampingRatio = (delegate* unmanaged[Cdecl]<JointId, float>)p5;
            b2RevoluteJoint_GetAngle = (delegate* unmanaged[Cdecl]<JointId, float>)p6;
            b2RevoluteJoint_EnableLimit = (delegate* unmanaged[Cdecl]<JointId, byte, void>)p7;
            b2RevoluteJoint_IsLimitEnabled = (delegate* unmanaged[Cdecl]<JointId, byte>)p8;
            b2RevoluteJoint_SetLimits = (delegate* unmanaged[Cdecl]<JointId, float, float, void>)p9;
            b2RevoluteJoint_GetLowerLimit = (delegate* unmanaged[Cdecl]<JointId, float>)p10;
            b2RevoluteJoint_GetUpperLimit = (delegate* unmanaged[Cdecl]<JointId, float>)p11;
            b2RevoluteJoint_EnableMotor = (delegate* unmanaged[Cdecl]<JointId, byte, void>)p12;
            b2RevoluteJoint_IsMotorEnabled = (delegate* unmanaged[Cdecl]<JointId, byte>)p13;
            b2RevoluteJoint_SetMotorSpeed = (delegate* unmanaged[Cdecl]<JointId, float, void>)p14;
            b2RevoluteJoint_GetMotorSpeed = (delegate* unmanaged[Cdecl]<JointId, float>)p15;
            b2RevoluteJoint_GetMotorTorque = (delegate* unmanaged[Cdecl]<JointId, float>)p16;
            b2RevoluteJoint_SetMaxMotorTorque = (delegate* unmanaged[Cdecl]<JointId, float, void>)p17;
            b2RevoluteJoint_GetMaxMotorTorque = (delegate* unmanaged[Cdecl]<JointId, float>)p18;
            b2RevoluteJoint_SetTargetAngle = (delegate* unmanaged[Cdecl]<JointId, float, void>)p19;
            b2RevoluteJoint_GetTargetAngle = (delegate* unmanaged[Cdecl]<JointId, float>)p20;
        }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_EnableSpring")]
    private static extern void b2RevoluteJoint_EnableSpring(JointId jointId, byte enableSpring);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_IsSpringEnabled")]
    private static extern byte b2RevoluteJoint_IsSpringEnabled(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_SetSpringHertz")]
    private static extern void b2RevoluteJoint_SetSpringHertz(JointId jointId, float hertz);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetSpringHertz")]
    private static extern float b2RevoluteJoint_GetSpringHertz(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_SetSpringDampingRatio")]
    private static extern void b2RevoluteJoint_SetSpringDampingRatio(JointId jointId, float dampingRatio);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetSpringDampingRatio")]
    private static extern float b2RevoluteJoint_GetSpringDampingRatio(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetAngle")]
    private static extern float b2RevoluteJoint_GetAngle(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_EnableLimit")]
    private static extern void b2RevoluteJoint_EnableLimit(JointId jointId, byte enableLimit);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_IsLimitEnabled")]
    private static extern byte b2RevoluteJoint_IsLimitEnabled(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_SetLimits")]
    private static extern void b2RevoluteJoint_SetLimits(JointId jointId, float lower, float upper);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetLowerLimit")]
    private static extern float b2RevoluteJoint_GetLowerLimit(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetUpperLimit")]
    private static extern float b2RevoluteJoint_GetUpperLimit(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_EnableMotor")]
    private static extern void b2RevoluteJoint_EnableMotor(JointId jointId, byte enableMotor);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_IsMotorEnabled")]
    private static extern byte b2RevoluteJoint_IsMotorEnabled(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_SetMotorSpeed")]
    private static extern void b2RevoluteJoint_SetMotorSpeed(JointId jointId, float motorSpeed);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetMotorSpeed")]
    private static extern float b2RevoluteJoint_GetMotorSpeed(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetMotorTorque")]
    private static extern float b2RevoluteJoint_GetMotorTorque(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_SetMaxMotorTorque")]
    private static extern void b2RevoluteJoint_SetMaxMotorTorque(JointId jointId, float torque);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetMaxMotorTorque")]
    private static extern float b2RevoluteJoint_GetMaxMotorTorque(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_SetTargetAngle")]
    private static extern void b2RevoluteJoint_SetTargetAngle(JointId jointId, float angle);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RevoluteJoint_GetTargetAngle")]
    private static extern float b2RevoluteJoint_GetTargetAngle(JointId jointId);
#endif
    }
}
