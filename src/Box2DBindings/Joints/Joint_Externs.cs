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
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte, void> b2Joint_SetCollideConnected;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, byte> b2Joint_GetCollideConnected;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, nint, void> b2Joint_SetUserData;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, nint> b2Joint_GetUserData;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, void> b2Joint_WakeBodies;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, Vec2> b2Joint_GetConstraintForce;
    private static readonly unsafe delegate* unmanaged[Cdecl]<JointId, float> b2Joint_GetConstraintTorque;

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
#endif
    }
}
