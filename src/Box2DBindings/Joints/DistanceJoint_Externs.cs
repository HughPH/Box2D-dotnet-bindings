using System.Runtime.InteropServices;

namespace Box2D;

partial class DistanceJoint
{
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2DistanceJoint_SetLength;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2DistanceJoint_GetLength;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte, void> b2DistanceJoint_EnableSpring;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte> b2DistanceJoint_IsSpringEnabled;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2DistanceJoint_SetSpringHertz;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2DistanceJoint_GetSpringHertz;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2DistanceJoint_SetSpringDampingRatio;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2DistanceJoint_GetSpringDampingRatio;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte, void> b2DistanceJoint_EnableLimit;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte> b2DistanceJoint_IsLimitEnabled;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, float, void> b2DistanceJoint_SetLengthRange;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2DistanceJoint_GetMinLength;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2DistanceJoint_GetMaxLength;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2DistanceJoint_GetCurrentLength;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte, void> b2DistanceJoint_EnableMotor;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte> b2DistanceJoint_IsMotorEnabled;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2DistanceJoint_SetMotorSpeed;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2DistanceJoint_GetMotorSpeed;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2DistanceJoint_SetMaxMotorForce;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2DistanceJoint_GetMaxMotorForce;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2DistanceJoint_GetMotorForce;

    static unsafe DistanceJoint()
    {
        nint lib = Core.NativeLibHandle;
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_SetLength", out var p0);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_GetLength", out var p1);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_EnableSpring", out var p2);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_IsSpringEnabled", out var p3);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_SetSpringHertz", out var p4);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_GetSpringHertz", out var p5);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_SetSpringDampingRatio", out var p6);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_GetSpringDampingRatio", out var p7);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_EnableLimit", out var p8);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_IsLimitEnabled", out var p9);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_SetLengthRange", out var p10);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_GetMinLength", out var p11);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_GetMaxLength", out var p12);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_GetCurrentLength", out var p13);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_EnableMotor", out var p14);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_IsMotorEnabled", out var p15);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_SetMotorSpeed", out var p16);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_GetMotorSpeed", out var p17);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_SetMaxMotorForce", out var p18);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_GetMaxMotorForce", out var p19);
        NativeLibrary.TryGetExport(lib, "b2DistanceJoint_GetMotorForce", out var p20);

        b2DistanceJoint_SetLength = (delegate* unmanaged[Cdecl]<JointId, float, void>)p0;
        b2DistanceJoint_GetLength = (delegate* unmanaged[Cdecl]<JointId, float>)p1;
        b2DistanceJoint_EnableSpring = (delegate* unmanaged[Cdecl]<JointId, byte, void>)p2;
        b2DistanceJoint_IsSpringEnabled = (delegate* unmanaged[Cdecl]<JointId, byte>)p3;
        b2DistanceJoint_SetSpringHertz = (delegate* unmanaged[Cdecl]<JointId, float, void>)p4;
        b2DistanceJoint_GetSpringHertz = (delegate* unmanaged[Cdecl]<JointId, float>)p5;
        b2DistanceJoint_SetSpringDampingRatio = (delegate* unmanaged[Cdecl]<JointId, float, void>)p6;
        b2DistanceJoint_GetSpringDampingRatio = (delegate* unmanaged[Cdecl]<JointId, float>)p7;
        b2DistanceJoint_EnableLimit = (delegate* unmanaged[Cdecl]<JointId, byte, void>)p8;
        b2DistanceJoint_IsLimitEnabled = (delegate* unmanaged[Cdecl]<JointId, byte>)p9;
        b2DistanceJoint_SetLengthRange = (delegate* unmanaged[Cdecl]<JointId, float, float, void>)p10;
        b2DistanceJoint_GetMinLength = (delegate* unmanaged[Cdecl]<JointId, float>)p11;
        b2DistanceJoint_GetMaxLength = (delegate* unmanaged[Cdecl]<JointId, float>)p12;
        b2DistanceJoint_GetCurrentLength = (delegate* unmanaged[Cdecl]<JointId, float>)p13;
        b2DistanceJoint_EnableMotor = (delegate* unmanaged[Cdecl]<JointId, byte, void>)p14;
        b2DistanceJoint_IsMotorEnabled = (delegate* unmanaged[Cdecl]<JointId, byte>)p15;
        b2DistanceJoint_SetMotorSpeed = (delegate* unmanaged[Cdecl]<JointId, float, void>)p16;
        b2DistanceJoint_GetMotorSpeed = (delegate* unmanaged[Cdecl]<JointId, float>)p17;
        b2DistanceJoint_SetMaxMotorForce = (delegate* unmanaged[Cdecl]<JointId, float, void>)p18;
        b2DistanceJoint_GetMaxMotorForce = (delegate* unmanaged[Cdecl]<JointId, float>)p19;
        b2DistanceJoint_GetMotorForce = (delegate* unmanaged[Cdecl]<JointId, float>)p20;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_SetLength")]
    private static extern void b2DistanceJoint_SetLength(JointId jointId, float length);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetLength")]
    private static extern float b2DistanceJoint_GetLength(JointId jointId);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_EnableSpring")]
    private static extern void b2DistanceJoint_EnableSpring(JointId jointId, byte enableSpring);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_IsSpringEnabled")]
    private static extern byte b2DistanceJoint_IsSpringEnabled(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_SetSpringHertz")]
    private static extern void b2DistanceJoint_SetSpringHertz(JointId jointId, float hertz);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetSpringHertz")]
    private static extern float b2DistanceJoint_GetSpringHertz(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_SetSpringDampingRatio")]
    private static extern void b2DistanceJoint_SetSpringDampingRatio(JointId jointId, float dampingRatio);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetSpringDampingRatio")]
    private static extern float b2DistanceJoint_GetSpringDampingRatio(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_EnableLimit")]
    private static extern void b2DistanceJoint_EnableLimit(JointId jointId, byte enableLimit);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_IsLimitEnabled")]
    private static extern byte b2DistanceJoint_IsLimitEnabled(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_SetLengthRange")]
    private static extern void b2DistanceJoint_SetLengthRange(JointId jointId, float minLength, float maxLength);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetMinLength")]
    private static extern float b2DistanceJoint_GetMinLength(JointId jointId);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetMaxLength")]
    private static extern float b2DistanceJoint_GetMaxLength(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetCurrentLength")]
    private static extern float b2DistanceJoint_GetCurrentLength(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_EnableMotor")]
    private static extern void b2DistanceJoint_EnableMotor(JointId jointId, byte enableMotor);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_IsMotorEnabled")]
    private static extern byte b2DistanceJoint_IsMotorEnabled(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_SetMotorSpeed")]
    private static extern void b2DistanceJoint_SetMotorSpeed(JointId jointId, float motorSpeed);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetMotorSpeed")]
    private static extern float b2DistanceJoint_GetMotorSpeed(JointId jointId);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_SetMaxMotorForce")]
    private static extern void b2DistanceJoint_SetMaxMotorForce(JointId jointId, float force);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetMaxMotorForce")]
    private static extern float b2DistanceJoint_GetMaxMotorForce(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DistanceJoint_GetMotorForce")]
    private static extern float b2DistanceJoint_GetMotorForce(JointId jointId);
#endif
}