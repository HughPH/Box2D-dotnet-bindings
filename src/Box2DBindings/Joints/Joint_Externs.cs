using System.Runtime.InteropServices;

namespace Box2D
{
    partial class Joint
    {
        #if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, void> b2DestroyJoint;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte> b2Joint_IsValid;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, JointType> b2Joint_GetType;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, Body> b2Joint_GetBodyA;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, Body> b2Joint_GetBodyB;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, WorldId> b2Joint_GetWorld;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, Vec2> b2Joint_GetLocalAnchorA;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, Vec2> b2Joint_GetLocalAnchorB;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, Vec2, void> b2Joint_SetLocalAnchorA;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, Vec2, void> b2Joint_SetLocalAnchorB;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2Joint_GetReferenceAngle;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, void> b2Joint_SetReferenceAngle;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, Vec2, void> b2Joint_SetLocalAxisA;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, Vec2> b2Joint_GetLocalAxisA;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2Joint_GetLinearSeparation;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2Joint_GetAngularSeparation;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte, void> b2Joint_SetCollideConnected;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte> b2Joint_GetCollideConnected;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, nint, void> b2Joint_SetUserData;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, nint> b2Joint_GetUserData;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, void> b2Joint_WakeBodies;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, Vec2> b2Joint_GetConstraintForce;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2Joint_GetConstraintTorque;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, out float, out float, void> b2Joint_GetConstraintTuning;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float, float, void> b2Joint_SetConstraintTuning;
    
    static unsafe Joint()
    {
        nint lib = nativeLibrary;
        NativeLibrary.TryGetExport(lib, "b2DestroyJoint", out var p0);
        NativeLibrary.TryGetExport(lib, "b2Joint_IsValid", out var p1);
        NativeLibrary.TryGetExport(lib, "b2Joint_GetType", out var p2);
        NativeLibrary.TryGetExport(lib, "b2Joint_GetBodyA", out var p3);
        NativeLibrary.TryGetExport(lib, "b2Joint_GetBodyB", out var p4);
        NativeLibrary.TryGetExport(lib, "b2Joint_GetWorld", out var p5);
        NativeLibrary.TryGetExport(lib, "b2Joint_GetLocalAnchorA", out var p6);
        NativeLibrary.TryGetExport(lib, "b2Joint_GetLocalAnchorB", out var p7);
        NativeLibrary.TryGetExport(lib, "b2Joint_SetCollideConnected", out var p8);
        NativeLibrary.TryGetExport(lib, "b2Joint_GetCollideConnected", out var p9);
        NativeLibrary.TryGetExport(lib, "b2Joint_SetUserData", out var p10);
        NativeLibrary.TryGetExport(lib, "b2Joint_GetUserData", out var p11);
        NativeLibrary.TryGetExport(lib, "b2Joint_WakeBodies", out var p12);
        NativeLibrary.TryGetExport(lib, "b2Joint_GetConstraintForce", out var p13);
        NativeLibrary.TryGetExport(lib, "b2Joint_GetConstraintTorque", out var p14);
        NativeLibrary.TryGetExport(lib, "b2Joint_SetLocalAnchorA", out var p15);
        NativeLibrary.TryGetExport(lib, "b2Joint_SetLocalAnchorB", out var p16);
        NativeLibrary.TryGetExport(lib, "b2Joint_GetReferenceAngle", out var p17);
        NativeLibrary.TryGetExport(lib, "b2Joint_SetReferenceAngle", out var p18);
        NativeLibrary.TryGetExport(lib, "b2Joint_SetLocalAxisA", out var p19);
        NativeLibrary.TryGetExport(lib, "b2Joint_GetLocalAxisA", out var p20);
        NativeLibrary.TryGetExport(lib, "b2Joint_GetLinearSeparation", out var p21);
        NativeLibrary.TryGetExport(lib, "b2Joint_GetAngularSeparation", out var p22);
        NativeLibrary.TryGetExport(lib, "b2Joint_SetConstraintTuning", out var p23);
        NativeLibrary.TryGetExport(lib, "b2Joint_GetConstraintTuning", out var p24);

        b2DestroyJoint = (delegate* unmanaged[Cdecl]<JointId, void>)p0;
        b2Joint_IsValid = (delegate* unmanaged[Cdecl]<JointId, byte>)p1;
        b2Joint_GetType = (delegate* unmanaged[Cdecl]<JointId, JointType>)p2;
        b2Joint_GetBodyA = (delegate* unmanaged[Cdecl]<JointId, Body>)p3;
        b2Joint_GetBodyB = (delegate* unmanaged[Cdecl]<JointId, Body>)p4;
        b2Joint_GetWorld = (delegate* unmanaged[Cdecl]<JointId, WorldId>)p5;
        b2Joint_GetLocalAnchorA = (delegate* unmanaged[Cdecl]<JointId, Vec2>)p6;
        b2Joint_GetLocalAnchorB = (delegate* unmanaged[Cdecl]<JointId, Vec2>)p7;
        b2Joint_SetCollideConnected = (delegate* unmanaged[Cdecl]<JointId, byte, void>)p8;
        b2Joint_GetCollideConnected = (delegate* unmanaged[Cdecl]<JointId, byte>)p9;
        b2Joint_SetUserData = (delegate* unmanaged[Cdecl]<JointId, nint, void>)p10;
        b2Joint_GetUserData = (delegate* unmanaged[Cdecl]<JointId, nint>)p11;
        b2Joint_WakeBodies = (delegate* unmanaged[Cdecl]<JointId, void>)p12;
        b2Joint_GetConstraintForce = (delegate* unmanaged[Cdecl]<JointId, Vec2>)p13;
        b2Joint_GetConstraintTorque = (delegate* unmanaged[Cdecl]<JointId, float>)p14;
        b2Joint_SetLocalAnchorA = (delegate* unmanaged[Cdecl]<JointId, Vec2, void>)p15;
        b2Joint_SetLocalAnchorB = (delegate* unmanaged[Cdecl]<JointId, Vec2, void>)p16;
        b2Joint_GetReferenceAngle = (delegate* unmanaged[Cdecl]<JointId, float>)p17;
        b2Joint_SetReferenceAngle = (delegate* unmanaged[Cdecl]<JointId, float, void>)p18;
        b2Joint_SetLocalAxisA = (delegate* unmanaged[Cdecl]<JointId, Vec2, void>)p19;
        b2Joint_GetLocalAxisA = (delegate* unmanaged[Cdecl]<JointId, Vec2>)p20;
        b2Joint_GetLinearSeparation = (delegate* unmanaged[Cdecl]<JointId, float>)p21;
        b2Joint_GetAngularSeparation = (delegate* unmanaged[Cdecl]<JointId, float>)p22;
        b2Joint_SetConstraintTuning = (delegate* unmanaged[Cdecl]<JointId, float, float, void>)p23;
        b2Joint_GetConstraintTuning = (delegate* unmanaged[Cdecl]<JointId, out float, out float, void>)p24;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DestroyJoint")]
    private static extern void b2DestroyJoint(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_IsValid")]
    private static extern byte b2Joint_IsValid(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetType")]
    private static extern JointType b2Joint_GetType(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetBodyA")]
    private static extern Body b2Joint_GetBodyA(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetBodyB")]
    private static extern Body b2Joint_GetBodyB(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetWorld")]
    private static extern WorldId b2Joint_GetWorld(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetLocalAnchorA")]
    private static extern Vec2 b2Joint_GetLocalAnchorA(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetLocalAnchorB")]
    private static extern Vec2 b2Joint_GetLocalAnchorB(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_SetCollideConnected")]
    private static extern void b2Joint_SetCollideConnected(JointId jointId, byte shouldCollide);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetCollideConnected")]
    private static extern byte b2Joint_GetCollideConnected(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_SetUserData")]
    private static extern void b2Joint_SetUserData(JointId jointId, nint userData);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetUserData")]
    private static extern nint b2Joint_GetUserData(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_WakeBodies")]
    private static extern void b2Joint_WakeBodies(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetConstraintForce")]
    private static extern Vec2 b2Joint_GetConstraintForce(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetConstraintTorque")]
    private static extern float b2Joint_GetConstraintTorque(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_SetLocalAnchorA")]
    private static extern void b2Joint_SetLocalAnchorA(JointId jointId, Vec2 localAnchor);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_SetLocalAnchorB")]
    private static extern void b2Joint_SetLocalAnchorB(JointId jointId, Vec2 localAnchor);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetReferenceAngle")]
    private static extern float b2Joint_GetReferenceAngle(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_SetReferenceAngle")]
    private static extern void b2Joint_SetReferenceAngle(JointId jointId, float angleInRadians);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_SetLocalAxisA")]
    private static extern void b2Joint_SetLocalAxisA(JointId jointId, Vec2 localAxis);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetLocalAxisA")]
    private static extern Vec2 b2Joint_GetLocalAxisA(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetLinearSeparation")]
    private static extern float b2Joint_GetLinearSeparation(JointId jointId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetAngularSeparation")]
    private static extern float b2Joint_GetAngularSeparation(JointId jointId);
        
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_SetConstraintTuning")]
    private static extern void b2Joint_SetConstraintTuning(JointId jointId, float linearTuning, float angularTuning);
        
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Joint_GetConstraintTuning")]
    private static extern void b2Joint_GetConstraintTuning(JointId jointId, out float linearTuning, out float angularTuning);
#endif
    }
}
