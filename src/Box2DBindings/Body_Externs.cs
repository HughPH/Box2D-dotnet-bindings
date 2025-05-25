using System.Runtime.InteropServices;

namespace Box2D
{
    partial class Body
    {
#if NET5_0_OR_GREATER
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, void> b2DestroyBody;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, byte> b2Body_IsValid;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, BodyType> b2Body_GetType;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, BodyType, void> b2Body_SetType;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, string?, void> b2Body_SetName;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, nint> b2Body_GetName;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, nint, void> b2Body_SetUserData;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, nint> b2Body_GetUserData;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2> b2Body_GetPosition;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Rotation> b2Body_GetRotation;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Transform> b2Body_GetTransform;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2, Rotation, void> b2Body_SetTransform;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2> b2Body_GetLocalPoint;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2> b2Body_GetWorldPoint;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2> b2Body_GetLocalVector;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2> b2Body_GetWorldVector;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2, void> b2Body_SetLinearVelocity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2> b2Body_GetLinearVelocity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, float> b2Body_GetAngularVelocity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, float, void> b2Body_SetAngularVelocity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Transform, float, void> b2Body_SetTargetTransform;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2> b2Body_GetLocalPointVelocity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2> b2Body_GetWorldPointVelocity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2, byte, void> b2Body_ApplyForce;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2, byte, void> b2Body_ApplyForceToCenter;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, float, byte, void> b2Body_ApplyTorque;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2, byte, void> b2Body_ApplyLinearImpulse;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2, byte, void> b2Body_ApplyLinearImpulseToCenter;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, float, byte, void> b2Body_ApplyAngularImpulse;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, float> b2Body_GetMass;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, float> b2Body_GetRotationalInertia;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2> b2Body_GetLocalCenterOfMass;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Vec2> b2Body_GetWorldCenterOfMass;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, MassData, void> b2Body_SetMassData;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, MassData> b2Body_GetMassData;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, void> b2Body_ApplyMassFromShapes;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, float, void> b2Body_SetLinearDamping;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, float> b2Body_GetLinearDamping;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, float, void> b2Body_SetAngularDamping;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, float> b2Body_GetAngularDamping;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, float, void> b2Body_SetGravityScale;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, float> b2Body_GetGravityScale;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, byte> b2Body_IsAwake;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, byte, void> b2Body_SetAwake;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, byte, void> b2Body_EnableSleep;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, byte> b2Body_IsSleepEnabled;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, float, void> b2Body_SetSleepThreshold;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, float> b2Body_GetSleepThreshold;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, byte> b2Body_IsEnabled;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, void> b2Body_Disable;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, void> b2Body_Enable;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, byte, void> b2Body_SetFixedRotation;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, byte> b2Body_IsFixedRotation;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, byte, void> b2Body_SetBullet;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, byte> b2Body_IsBullet;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, byte, void> b2Body_EnableContactEvents;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, byte, void> b2Body_EnableHitEvents;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, WorldId> b2Body_GetWorld;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, int> b2Body_GetShapeCount;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, Shape*, int, int> b2Body_GetShapes;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, int> b2Body_GetJointCount;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, JointId*, int, int> b2Body_GetJoints;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, int> b2Body_GetContactCapacity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, ContactData*, int, int> b2Body_GetContactData;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, AABB> b2Body_ComputeAABB;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, in ShapeDefInternal, in Circle, Shape> b2CreateCircleShape;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, in ShapeDefInternal, in Segment, Shape> b2CreateSegmentShape;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, in ShapeDefInternal, in Capsule, Shape> b2CreateCapsuleShape;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, in ShapeDefInternal, in Polygon, Shape> b2CreatePolygonShape;
        private static readonly unsafe delegate* unmanaged[Cdecl]<BodyId, in ChainDefInternal, ChainShapeId> b2CreateChain;

        static unsafe Body()
        {
            nint lib = nativeLibrary;
            NativeLibrary.TryGetExport(lib, "b2DestroyBody", out var p0);
            NativeLibrary.TryGetExport(lib, "b2Body_IsValid", out var p1);
            NativeLibrary.TryGetExport(lib, "b2Body_GetType", out var p2);
            NativeLibrary.TryGetExport(lib, "b2Body_SetType", out var p3);
            NativeLibrary.TryGetExport(lib, "b2Body_SetName", out var p4);
            NativeLibrary.TryGetExport(lib, "b2Body_GetName", out var p5);
            NativeLibrary.TryGetExport(lib, "b2Body_SetUserData", out var p6);
            NativeLibrary.TryGetExport(lib, "b2Body_GetUserData", out var p7);
            NativeLibrary.TryGetExport(lib, "b2Body_GetPosition", out var p8);
            NativeLibrary.TryGetExport(lib, "b2Body_GetRotation", out var p9);
            NativeLibrary.TryGetExport(lib, "b2Body_GetTransform", out var p10);
            NativeLibrary.TryGetExport(lib, "b2Body_SetTransform", out var p11);
            NativeLibrary.TryGetExport(lib, "b2Body_GetLocalPoint", out var p12);
            NativeLibrary.TryGetExport(lib, "b2Body_GetWorldPoint", out var p13);
            NativeLibrary.TryGetExport(lib, "b2Body_GetLocalVector", out var p14);
            NativeLibrary.TryGetExport(lib, "b2Body_GetWorldVector", out var p15);
            NativeLibrary.TryGetExport(lib, "b2Body_SetLinearVelocity", out var p16);
            NativeLibrary.TryGetExport(lib, "b2Body_GetLinearVelocity", out var p17);
            NativeLibrary.TryGetExport(lib, "b2Body_GetAngularVelocity", out var p18);
            NativeLibrary.TryGetExport(lib, "b2Body_SetAngularVelocity", out var p19);
            NativeLibrary.TryGetExport(lib, "b2Body_SetTargetTransform", out var p20);
            NativeLibrary.TryGetExport(lib, "b2Body_GetLocalPointVelocity", out var p21);
            NativeLibrary.TryGetExport(lib, "b2Body_GetWorldPointVelocity", out var p22);
            NativeLibrary.TryGetExport(lib, "b2Body_ApplyForce", out var p23);
            NativeLibrary.TryGetExport(lib, "b2Body_ApplyForceToCenter", out var p24);
            NativeLibrary.TryGetExport(lib, "b2Body_ApplyTorque", out var p25);
            NativeLibrary.TryGetExport(lib, "b2Body_ApplyLinearImpulse", out var p26);
            NativeLibrary.TryGetExport(lib, "b2Body_ApplyLinearImpulseToCenter", out var p27);
            NativeLibrary.TryGetExport(lib, "b2Body_ApplyAngularImpulse", out var p28);
            NativeLibrary.TryGetExport(lib, "b2Body_GetMass", out var p29);
            NativeLibrary.TryGetExport(lib, "b2Body_GetRotationalInertia", out var p30);
            NativeLibrary.TryGetExport(lib, "b2Body_GetLocalCenterOfMass", out var p31);
            NativeLibrary.TryGetExport(lib, "b2Body_GetWorldCenterOfMass", out var p32);
            NativeLibrary.TryGetExport(lib, "b2Body_SetMassData", out var p33);
            NativeLibrary.TryGetExport(lib, "b2Body_GetMassData", out var p34);
            NativeLibrary.TryGetExport(lib, "b2Body_ApplyMassFromShapes", out var p35);
            NativeLibrary.TryGetExport(lib, "b2Body_SetLinearDamping", out var p36);
            NativeLibrary.TryGetExport(lib, "b2Body_GetLinearDamping", out var p37);
            NativeLibrary.TryGetExport(lib, "b2Body_SetAngularDamping", out var p38);
            NativeLibrary.TryGetExport(lib, "b2Body_GetAngularDamping", out var p39);
            NativeLibrary.TryGetExport(lib, "b2Body_SetGravityScale", out var p40);
            NativeLibrary.TryGetExport(lib, "b2Body_GetGravityScale", out var p41);
            NativeLibrary.TryGetExport(lib, "b2Body_IsAwake", out var p42);
            NativeLibrary.TryGetExport(lib, "b2Body_SetAwake", out var p43);
            NativeLibrary.TryGetExport(lib, "b2Body_EnableSleep", out var p44);
            NativeLibrary.TryGetExport(lib, "b2Body_IsSleepEnabled", out var p45);
            NativeLibrary.TryGetExport(lib, "b2Body_SetSleepThreshold", out var p46);
            NativeLibrary.TryGetExport(lib, "b2Body_GetSleepThreshold", out var p47);
            NativeLibrary.TryGetExport(lib, "b2Body_IsEnabled", out var p48);
            NativeLibrary.TryGetExport(lib, "b2Body_Disable", out var p49);
            NativeLibrary.TryGetExport(lib, "b2Body_Enable", out var p50);
            NativeLibrary.TryGetExport(lib, "b2Body_SetFixedRotation", out var p51);
            NativeLibrary.TryGetExport(lib, "b2Body_IsFixedRotation", out var p52);
            NativeLibrary.TryGetExport(lib, "b2Body_SetBullet", out var p53);
            NativeLibrary.TryGetExport(lib, "b2Body_IsBullet", out var p54);
            NativeLibrary.TryGetExport(lib, "b2Body_EnableContactEvents", out var p55);
            NativeLibrary.TryGetExport(lib, "b2Body_EnableHitEvents", out var p56);
            NativeLibrary.TryGetExport(lib, "b2Body_GetWorld", out var p57);
            NativeLibrary.TryGetExport(lib, "b2Body_GetShapeCount", out var p58);
            NativeLibrary.TryGetExport(lib, "b2Body_GetShapes", out var p59);
            NativeLibrary.TryGetExport(lib, "b2Body_GetJointCount", out var p60);
            NativeLibrary.TryGetExport(lib, "b2Body_GetJoints", out var p61);
            NativeLibrary.TryGetExport(lib, "b2Body_GetContactCapacity", out var p62);
            NativeLibrary.TryGetExport(lib, "b2Body_GetContactData", out var p63);
            NativeLibrary.TryGetExport(lib, "b2Body_ComputeAABB", out var p64);
            NativeLibrary.TryGetExport(lib, "b2CreateCircleShape", out var p65);
            NativeLibrary.TryGetExport(lib, "b2CreateSegmentShape", out var p66);
            NativeLibrary.TryGetExport(lib, "b2CreateCapsuleShape", out var p67);
            NativeLibrary.TryGetExport(lib, "b2CreatePolygonShape", out var p68);
            NativeLibrary.TryGetExport(lib, "b2CreateChain", out var p69);

            b2DestroyBody = (delegate* unmanaged[Cdecl]<BodyId, void>)p0;
            b2Body_IsValid = (delegate* unmanaged[Cdecl]<BodyId, byte>)p1;
            b2Body_GetType = (delegate* unmanaged[Cdecl]<BodyId, BodyType>)p2;
            b2Body_SetType = (delegate* unmanaged[Cdecl]<BodyId, BodyType, void>)p3;
            b2Body_SetName = (delegate* unmanaged[Cdecl]<BodyId, string?, void>)p4;
            b2Body_GetName = (delegate* unmanaged[Cdecl]<BodyId, nint>)p5;
            b2Body_SetUserData = (delegate* unmanaged[Cdecl]<BodyId, nint, void>)p6;
            b2Body_GetUserData = (delegate* unmanaged[Cdecl]<BodyId, nint>)p7;
            b2Body_GetPosition = (delegate* unmanaged[Cdecl]<BodyId, Vec2>)p8;
            b2Body_GetRotation = (delegate* unmanaged[Cdecl]<BodyId, Rotation>)p9;
            b2Body_GetTransform = (delegate* unmanaged[Cdecl]<BodyId, Transform>)p10;
            b2Body_SetTransform = (delegate* unmanaged[Cdecl]<BodyId, Vec2, Rotation, void>)p11;
            b2Body_GetLocalPoint = (delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2>)p12;
            b2Body_GetWorldPoint = (delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2>)p13;
            b2Body_GetLocalVector = (delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2>)p14;
            b2Body_GetWorldVector = (delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2>)p15;
            b2Body_SetLinearVelocity = (delegate* unmanaged[Cdecl]<BodyId, Vec2, void>)p16;
            b2Body_GetLinearVelocity = (delegate* unmanaged[Cdecl]<BodyId, Vec2>)p17;
            b2Body_GetAngularVelocity = (delegate* unmanaged[Cdecl]<BodyId, float>)p18;
            b2Body_SetAngularVelocity = (delegate* unmanaged[Cdecl]<BodyId, float, void>)p19;
            b2Body_SetTargetTransform = (delegate* unmanaged[Cdecl]<BodyId, Transform, float, void>)p20;
            b2Body_GetLocalPointVelocity = (delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2>)p21;
            b2Body_GetWorldPointVelocity = (delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2>)p22;
            b2Body_ApplyForce = (delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2, byte, void>)p23;
            b2Body_ApplyForceToCenter = (delegate* unmanaged[Cdecl]<BodyId, Vec2, byte, void>)p24;
            b2Body_ApplyTorque = (delegate* unmanaged[Cdecl]<BodyId, float, byte, void>)p25;
            b2Body_ApplyLinearImpulse = (delegate* unmanaged[Cdecl]<BodyId, Vec2, Vec2, byte, void>)p26;
            b2Body_ApplyLinearImpulseToCenter = (delegate* unmanaged[Cdecl]<BodyId, Vec2, byte, void>)p27;
            b2Body_ApplyAngularImpulse = (delegate* unmanaged[Cdecl]<BodyId, float, byte, void>)p28;
            b2Body_GetMass = (delegate* unmanaged[Cdecl]<BodyId, float>)p29;
            b2Body_GetRotationalInertia = (delegate* unmanaged[Cdecl]<BodyId, float>)p30;
            b2Body_GetLocalCenterOfMass = (delegate* unmanaged[Cdecl]<BodyId, Vec2>)p31;
            b2Body_GetWorldCenterOfMass = (delegate* unmanaged[Cdecl]<BodyId, Vec2>)p32;
            b2Body_SetMassData = (delegate* unmanaged[Cdecl]<BodyId, MassData, void>)p33;
            b2Body_GetMassData = (delegate* unmanaged[Cdecl]<BodyId, MassData>)p34;
            b2Body_ApplyMassFromShapes = (delegate* unmanaged[Cdecl]<BodyId, void>)p35;
            b2Body_SetLinearDamping = (delegate* unmanaged[Cdecl]<BodyId, float, void>)p36;
            b2Body_GetLinearDamping = (delegate* unmanaged[Cdecl]<BodyId, float>)p37;
            b2Body_SetAngularDamping = (delegate* unmanaged[Cdecl]<BodyId, float, void>)p38;
            b2Body_GetAngularDamping = (delegate* unmanaged[Cdecl]<BodyId, float>)p39;
            b2Body_SetGravityScale = (delegate* unmanaged[Cdecl]<BodyId, float, void>)p40;
            b2Body_GetGravityScale = (delegate* unmanaged[Cdecl]<BodyId, float>)p41;
            b2Body_IsAwake = (delegate* unmanaged[Cdecl]<BodyId, byte>)p42;
            b2Body_SetAwake = (delegate* unmanaged[Cdecl]<BodyId, byte, void>)p43;
            b2Body_EnableSleep = (delegate* unmanaged[Cdecl]<BodyId, byte, void>)p44;
            b2Body_IsSleepEnabled = (delegate* unmanaged[Cdecl]<BodyId, byte>)p45;
            b2Body_SetSleepThreshold = (delegate* unmanaged[Cdecl]<BodyId, float, void>)p46;
            b2Body_GetSleepThreshold = (delegate* unmanaged[Cdecl]<BodyId, float>)p47;
            b2Body_IsEnabled = (delegate* unmanaged[Cdecl]<BodyId, byte>)p48;
            b2Body_Disable = (delegate* unmanaged[Cdecl]<BodyId, void>)p49;
            b2Body_Enable = (delegate* unmanaged[Cdecl]<BodyId, void>)p50;
            b2Body_SetFixedRotation = (delegate* unmanaged[Cdecl]<BodyId, byte, void>)p51;
            b2Body_IsFixedRotation = (delegate* unmanaged[Cdecl]<BodyId, byte>)p52;
            b2Body_SetBullet = (delegate* unmanaged[Cdecl]<BodyId, byte, void>)p53;
            b2Body_IsBullet = (delegate* unmanaged[Cdecl]<BodyId, byte>)p54;
            b2Body_EnableContactEvents = (delegate* unmanaged[Cdecl]<BodyId, byte, void>)p55;
            b2Body_EnableHitEvents = (delegate* unmanaged[Cdecl]<BodyId, byte, void>)p56;
            b2Body_GetWorld = (delegate* unmanaged[Cdecl]<BodyId, WorldId>)p57;
            b2Body_GetShapeCount = (delegate* unmanaged[Cdecl]<BodyId, int>)p58;
            b2Body_GetShapes = (delegate* unmanaged[Cdecl]<BodyId, Shape*, int, int>)p59;
            b2Body_GetJointCount = (delegate* unmanaged[Cdecl]<BodyId, int>)p60;
            b2Body_GetJoints = (delegate* unmanaged[Cdecl]<BodyId, JointId*, int, int>)p61;
            b2Body_GetContactCapacity = (delegate* unmanaged[Cdecl]<BodyId, int>)p62;
            b2Body_GetContactData = (delegate* unmanaged[Cdecl]<BodyId, ContactData*, int, int>)p63;
            b2Body_ComputeAABB = (delegate* unmanaged[Cdecl]<BodyId, AABB>)p64;
            b2CreateCircleShape = (delegate* unmanaged[Cdecl]<BodyId, in ShapeDefInternal, in Circle, Shape>)p65;
            b2CreateSegmentShape = (delegate* unmanaged[Cdecl]<BodyId, in ShapeDefInternal, in Segment, Shape>)p66;
            b2CreateCapsuleShape = (delegate* unmanaged[Cdecl]<BodyId, in ShapeDefInternal, in Capsule, Shape>)p67;
            b2CreatePolygonShape = (delegate* unmanaged[Cdecl]<BodyId, in ShapeDefInternal, in Polygon, Shape>)p68;
            b2CreateChain = (delegate* unmanaged[Cdecl]<BodyId, in ChainDefInternal, ChainShapeId>)p69;
        }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DestroyBody")]
    private static extern void b2DestroyBody(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsValid")]
    private static extern byte b2Body_IsValid(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetType")]
    private static extern BodyType b2Body_GetType(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetType")]
    private static extern void b2Body_SetType(BodyId bodyId, BodyType type);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetName")]
    private static extern void b2Body_SetName(BodyId bodyId, string? name);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetName")]
    private static extern nint b2Body_GetName(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetUserData")]
    private static extern void b2Body_SetUserData(BodyId bodyId, nint userData);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetUserData")]
    private static extern nint b2Body_GetUserData(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetPosition")]
    private static extern Vec2 b2Body_GetPosition(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetRotation")]
    private static extern Rotation b2Body_GetRotation(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetTransform")]
    private static extern Transform b2Body_GetTransform(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetTransform")]
    private static extern void b2Body_SetTransform(BodyId bodyId, Vec2 position, Rotation rotation);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalPoint")]
    private static extern Vec2 b2Body_GetLocalPoint(BodyId bodyId, Vec2 worldPoint);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldPoint")]
    private static extern Vec2 b2Body_GetWorldPoint(BodyId bodyId, Vec2 localPoint);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalVector")]
    private static extern Vec2 b2Body_GetLocalVector(BodyId bodyId, Vec2 worldVector);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldVector")]
    private static extern Vec2 b2Body_GetWorldVector(BodyId bodyId, Vec2 localVector);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetLinearVelocity")]
    private static extern void b2Body_SetLinearVelocity(BodyId bodyId, Vec2 linearVelocity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLinearVelocity")]
    private static extern Vec2 b2Body_GetLinearVelocity(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetAngularVelocity")]
    private static extern float b2Body_GetAngularVelocity(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetAngularVelocity")]
    private static extern void b2Body_SetAngularVelocity(BodyId bodyId, float angularVelocity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetTargetTransform")]
    private static extern void b2Body_SetTargetTransform(BodyId bodyId, Transform target, float timeStep);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalPointVelocity")]
    private static extern Vec2 b2Body_GetLocalPointVelocity(BodyId bodyId, Vec2 localPoint);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldPointVelocity")]
    private static extern Vec2 b2Body_GetWorldPointVelocity(BodyId bodyId, Vec2 worldPoint);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyForce")]
    private static extern void b2Body_ApplyForce(BodyId bodyId, Vec2 force, Vec2 point, byte wake);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyForceToCenter")]
    private static extern void b2Body_ApplyForceToCenter(BodyId bodyId, Vec2 force, byte wake);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyTorque")]
    private static extern void b2Body_ApplyTorque(BodyId bodyId, float torque, byte wake);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyLinearImpulse")]
    private static extern void b2Body_ApplyLinearImpulse(BodyId bodyId, Vec2 impulse, Vec2 point, byte wake);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyLinearImpulseToCenter")]
    private static extern void b2Body_ApplyLinearImpulseToCenter(BodyId bodyId, Vec2 impulse, byte wake);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyAngularImpulse")]
    private static extern void b2Body_ApplyAngularImpulse(BodyId bodyId, float impulse, byte wake);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetMass")]
    private static extern float b2Body_GetMass(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetRotationalInertia")]
    private static extern float b2Body_GetRotationalInertia(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalCenterOfMass")]
    private static extern Vec2 b2Body_GetLocalCenterOfMass(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldCenterOfMass")]
    private static extern Vec2 b2Body_GetWorldCenterOfMass(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetMassData")]
    private static extern void b2Body_SetMassData(BodyId bodyId, MassData massData);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetMassData")]
    private static extern MassData b2Body_GetMassData(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyMassFromShapes")]
    private static extern void b2Body_ApplyMassFromShapes(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetLinearDamping")]
    private static extern void b2Body_SetLinearDamping(BodyId bodyId, float linearDamping);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLinearDamping")]
    private static extern float b2Body_GetLinearDamping(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetAngularDamping")]
    private static extern void b2Body_SetAngularDamping(BodyId bodyId, float angularDamping);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetAngularDamping")]
    private static extern float b2Body_GetAngularDamping(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetGravityScale")]
    private static extern void b2Body_SetGravityScale(BodyId bodyId, float gravityScale);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetGravityScale")]
    private static extern float b2Body_GetGravityScale(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsAwake")]
    private static extern byte b2Body_IsAwake(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetAwake")]
    private static extern void b2Body_SetAwake(BodyId bodyId, byte awake);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_EnableSleep")]
    private static extern void b2Body_EnableSleep(BodyId bodyId, byte enableSleep);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsSleepEnabled")]
    private static extern byte b2Body_IsSleepEnabled(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetSleepThreshold")]
    private static extern void b2Body_SetSleepThreshold(BodyId bodyId, float sleepThreshold);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetSleepThreshold")]
    private static extern float b2Body_GetSleepThreshold(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsEnabled")]
    private static extern byte b2Body_IsEnabled(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_Disable")]
    private static extern void b2Body_Disable(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_Enable")]
    private static extern void b2Body_Enable(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetFixedRotation")]
    private static extern void b2Body_SetFixedRotation(BodyId bodyId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsFixedRotation")]
    private static extern byte b2Body_IsFixedRotation(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetBullet")]
    private static extern void b2Body_SetBullet(BodyId bodyId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsBullet")]
    private static extern byte b2Body_IsBullet(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_EnableContactEvents")]
    private static extern void b2Body_EnableContactEvents(BodyId bodyId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_EnableHitEvents")]
    private static extern void b2Body_EnableHitEvents(BodyId bodyId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorld")]
    private static extern WorldId b2Body_GetWorld(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetShapeCount")]
    private static extern int b2Body_GetShapeCount(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetShapes")]
    private static extern unsafe int b2Body_GetShapes(BodyId bodyId, Shape* shapeArray, int capacity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetJointCount")]
    private static extern int b2Body_GetJointCount(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetJoints")]
    private static extern unsafe int b2Body_GetJoints(BodyId bodyId, JointId* jointArray, int capacity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetContactCapacity")]
    private static extern int b2Body_GetContactCapacity(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetContactData")]
    private static extern unsafe int b2Body_GetContactData(BodyId bodyId, ContactData* contactData, int capacity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ComputeAABB")]
    private static extern AABB b2Body_ComputeAABB(BodyId bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateCircleShape")]
    private static extern Shape b2CreateCircleShape(BodyId bodyId, in ShapeDefInternal def, in Circle circle);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateSegmentShape")]
    private static extern Shape b2CreateSegmentShape(BodyId bodyId, in ShapeDefInternal def, in Segment segment);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateCapsuleShape")]
    private static extern Shape b2CreateCapsuleShape(BodyId bodyId, in ShapeDefInternal def, in Capsule capsule);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreatePolygonShape")]
    private static extern Shape b2CreatePolygonShape(BodyId bodyId, in ShapeDefInternal def, in Polygon polygon);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateChain")]
    private static extern ChainShapeId b2CreateChain(BodyId bodyId, in ChainDefInternal def);
#endif
    }
}
