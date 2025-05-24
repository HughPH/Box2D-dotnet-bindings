using System.Runtime.InteropServices;

namespace Box2D
{
    partial struct Body
    {
#if NET5_0_OR_GREATER
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, void> b2DestroyBody;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, byte> b2Body_IsValid;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, BodyType> b2Body_GetType;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, BodyType, void> b2Body_SetType;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, string?, void> b2Body_SetName;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, nint> b2Body_GetName;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, nint, void> b2Body_SetUserData;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, nint> b2Body_GetUserData;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2> b2Body_GetPosition;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Rotation> b2Body_GetRotation;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Transform> b2Body_GetTransform;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2, Rotation, void> b2Body_SetTransform;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2, Vec2> b2Body_GetLocalPoint;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2, Vec2> b2Body_GetWorldPoint;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2, Vec2> b2Body_GetLocalVector;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2, Vec2> b2Body_GetWorldVector;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2, void> b2Body_SetLinearVelocity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2> b2Body_GetLinearVelocity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, float> b2Body_GetAngularVelocity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, float, void> b2Body_SetAngularVelocity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Transform, float, void> b2Body_SetTargetTransform;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2, Vec2> b2Body_GetLocalPointVelocity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2, Vec2> b2Body_GetWorldPointVelocity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2, Vec2, byte, void> b2Body_ApplyForce;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2, byte, void> b2Body_ApplyForceToCenter;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, float, byte, void> b2Body_ApplyTorque;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2, Vec2, byte, void> b2Body_ApplyLinearImpulse;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2, byte, void> b2Body_ApplyLinearImpulseToCenter;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, float, byte, void> b2Body_ApplyAngularImpulse;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, float> b2Body_GetMass;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, float> b2Body_GetRotationalInertia;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2> b2Body_GetLocalCenterOfMass;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Vec2> b2Body_GetWorldCenterOfMass;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, MassData, void> b2Body_SetMassData;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, MassData> b2Body_GetMassData;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, void> b2Body_ApplyMassFromShapes;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, float, void> b2Body_SetLinearDamping;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, float> b2Body_GetLinearDamping;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, float, void> b2Body_SetAngularDamping;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, float> b2Body_GetAngularDamping;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, float, void> b2Body_SetGravityScale;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, float> b2Body_GetGravityScale;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, byte> b2Body_IsAwake;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, byte, void> b2Body_SetAwake;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, byte, void> b2Body_EnableSleep;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, byte> b2Body_IsSleepEnabled;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, float, void> b2Body_SetSleepThreshold;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, float> b2Body_GetSleepThreshold;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, byte> b2Body_IsEnabled;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, void> b2Body_Disable;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, void> b2Body_Enable;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, byte, void> b2Body_SetFixedRotation;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, byte> b2Body_IsFixedRotation;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, byte, void> b2Body_SetBullet;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, byte> b2Body_IsBullet;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, byte, void> b2Body_EnableContactEvents;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, byte, void> b2Body_EnableHitEvents;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, WorldId> b2Body_GetWorld;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, int> b2Body_GetShapeCount;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, Shape*, int, int> b2Body_GetShapes;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, int> b2Body_GetJointCount;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, JointId*, int, int> b2Body_GetJoints;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, int> b2Body_GetContactCapacity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, ContactData*, int, int> b2Body_GetContactData;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, AABB> b2Body_ComputeAABB;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, in ShapeDefInternal, in Circle, Shape> b2CreateCircleShape;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, in ShapeDefInternal, in Segment, Shape> b2CreateSegmentShape;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, in ShapeDefInternal, in Capsule, Shape> b2CreateCapsuleShape;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, in ShapeDefInternal, in Polygon, Shape> b2CreatePolygonShape;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Body, in ChainDefInternal, ChainShape> b2CreateChain;

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

            b2DestroyBody = (delegate* unmanaged[Cdecl]<Body, void>)p0;
            b2Body_IsValid = (delegate* unmanaged[Cdecl]<Body, byte>)p1;
            b2Body_GetType = (delegate* unmanaged[Cdecl]<Body, BodyType>)p2;
            b2Body_SetType = (delegate* unmanaged[Cdecl]<Body, BodyType, void>)p3;
            b2Body_SetName = (delegate* unmanaged[Cdecl]<Body, string?, void>)p4;
            b2Body_GetName = (delegate* unmanaged[Cdecl]<Body, nint>)p5;
            b2Body_SetUserData = (delegate* unmanaged[Cdecl]<Body, nint, void>)p6;
            b2Body_GetUserData = (delegate* unmanaged[Cdecl]<Body, nint>)p7;
            b2Body_GetPosition = (delegate* unmanaged[Cdecl]<Body, Vec2>)p8;
            b2Body_GetRotation = (delegate* unmanaged[Cdecl]<Body, Rotation>)p9;
            b2Body_GetTransform = (delegate* unmanaged[Cdecl]<Body, Transform>)p10;
            b2Body_SetTransform = (delegate* unmanaged[Cdecl]<Body, Vec2, Rotation, void>)p11;
            b2Body_GetLocalPoint = (delegate* unmanaged[Cdecl]<Body, Vec2, Vec2>)p12;
            b2Body_GetWorldPoint = (delegate* unmanaged[Cdecl]<Body, Vec2, Vec2>)p13;
            b2Body_GetLocalVector = (delegate* unmanaged[Cdecl]<Body, Vec2, Vec2>)p14;
            b2Body_GetWorldVector = (delegate* unmanaged[Cdecl]<Body, Vec2, Vec2>)p15;
            b2Body_SetLinearVelocity = (delegate* unmanaged[Cdecl]<Body, Vec2, void>)p16;
            b2Body_GetLinearVelocity = (delegate* unmanaged[Cdecl]<Body, Vec2>)p17;
            b2Body_GetAngularVelocity = (delegate* unmanaged[Cdecl]<Body, float>)p18;
            b2Body_SetAngularVelocity = (delegate* unmanaged[Cdecl]<Body, float, void>)p19;
            b2Body_SetTargetTransform = (delegate* unmanaged[Cdecl]<Body, Transform, float, void>)p20;
            b2Body_GetLocalPointVelocity = (delegate* unmanaged[Cdecl]<Body, Vec2, Vec2>)p21;
            b2Body_GetWorldPointVelocity = (delegate* unmanaged[Cdecl]<Body, Vec2, Vec2>)p22;
            b2Body_ApplyForce = (delegate* unmanaged[Cdecl]<Body, Vec2, Vec2, byte, void>)p23;
            b2Body_ApplyForceToCenter = (delegate* unmanaged[Cdecl]<Body, Vec2, byte, void>)p24;
            b2Body_ApplyTorque = (delegate* unmanaged[Cdecl]<Body, float, byte, void>)p25;
            b2Body_ApplyLinearImpulse = (delegate* unmanaged[Cdecl]<Body, Vec2, Vec2, byte, void>)p26;
            b2Body_ApplyLinearImpulseToCenter = (delegate* unmanaged[Cdecl]<Body, Vec2, byte, void>)p27;
            b2Body_ApplyAngularImpulse = (delegate* unmanaged[Cdecl]<Body, float, byte, void>)p28;
            b2Body_GetMass = (delegate* unmanaged[Cdecl]<Body, float>)p29;
            b2Body_GetRotationalInertia = (delegate* unmanaged[Cdecl]<Body, float>)p30;
            b2Body_GetLocalCenterOfMass = (delegate* unmanaged[Cdecl]<Body, Vec2>)p31;
            b2Body_GetWorldCenterOfMass = (delegate* unmanaged[Cdecl]<Body, Vec2>)p32;
            b2Body_SetMassData = (delegate* unmanaged[Cdecl]<Body, MassData, void>)p33;
            b2Body_GetMassData = (delegate* unmanaged[Cdecl]<Body, MassData>)p34;
            b2Body_ApplyMassFromShapes = (delegate* unmanaged[Cdecl]<Body, void>)p35;
            b2Body_SetLinearDamping = (delegate* unmanaged[Cdecl]<Body, float, void>)p36;
            b2Body_GetLinearDamping = (delegate* unmanaged[Cdecl]<Body, float>)p37;
            b2Body_SetAngularDamping = (delegate* unmanaged[Cdecl]<Body, float, void>)p38;
            b2Body_GetAngularDamping = (delegate* unmanaged[Cdecl]<Body, float>)p39;
            b2Body_SetGravityScale = (delegate* unmanaged[Cdecl]<Body, float, void>)p40;
            b2Body_GetGravityScale = (delegate* unmanaged[Cdecl]<Body, float>)p41;
            b2Body_IsAwake = (delegate* unmanaged[Cdecl]<Body, byte>)p42;
            b2Body_SetAwake = (delegate* unmanaged[Cdecl]<Body, byte, void>)p43;
            b2Body_EnableSleep = (delegate* unmanaged[Cdecl]<Body, byte, void>)p44;
            b2Body_IsSleepEnabled = (delegate* unmanaged[Cdecl]<Body, byte>)p45;
            b2Body_SetSleepThreshold = (delegate* unmanaged[Cdecl]<Body, float, void>)p46;
            b2Body_GetSleepThreshold = (delegate* unmanaged[Cdecl]<Body, float>)p47;
            b2Body_IsEnabled = (delegate* unmanaged[Cdecl]<Body, byte>)p48;
            b2Body_Disable = (delegate* unmanaged[Cdecl]<Body, void>)p49;
            b2Body_Enable = (delegate* unmanaged[Cdecl]<Body, void>)p50;
            b2Body_SetFixedRotation = (delegate* unmanaged[Cdecl]<Body, byte, void>)p51;
            b2Body_IsFixedRotation = (delegate* unmanaged[Cdecl]<Body, byte>)p52;
            b2Body_SetBullet = (delegate* unmanaged[Cdecl]<Body, byte, void>)p53;
            b2Body_IsBullet = (delegate* unmanaged[Cdecl]<Body, byte>)p54;
            b2Body_EnableContactEvents = (delegate* unmanaged[Cdecl]<Body, byte, void>)p55;
            b2Body_EnableHitEvents = (delegate* unmanaged[Cdecl]<Body, byte, void>)p56;
            b2Body_GetWorld = (delegate* unmanaged[Cdecl]<Body, WorldId>)p57;
            b2Body_GetShapeCount = (delegate* unmanaged[Cdecl]<Body, int>)p58;
            b2Body_GetShapes = (delegate* unmanaged[Cdecl]<Body, Shape*, int, int>)p59;
            b2Body_GetJointCount = (delegate* unmanaged[Cdecl]<Body, int>)p60;
            b2Body_GetJoints = (delegate* unmanaged[Cdecl]<Body, JointId*, int, int>)p61;
            b2Body_GetContactCapacity = (delegate* unmanaged[Cdecl]<Body, int>)p62;
            b2Body_GetContactData = (delegate* unmanaged[Cdecl]<Body, ContactData*, int, int>)p63;
            b2Body_ComputeAABB = (delegate* unmanaged[Cdecl]<Body, AABB>)p64;
            b2CreateCircleShape = (delegate* unmanaged[Cdecl]<Body, in ShapeDefInternal, in Circle, Shape>)p65;
            b2CreateSegmentShape = (delegate* unmanaged[Cdecl]<Body, in ShapeDefInternal, in Segment, Shape>)p66;
            b2CreateCapsuleShape = (delegate* unmanaged[Cdecl]<Body, in ShapeDefInternal, in Capsule, Shape>)p67;
            b2CreatePolygonShape = (delegate* unmanaged[Cdecl]<Body, in ShapeDefInternal, in Polygon, Shape>)p68;
            b2CreateChain = (delegate* unmanaged[Cdecl]<Body, in ChainDefInternal, ChainShape>)p69;
        }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DestroyBody")]
    private static extern void b2DestroyBody(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsValid")]
    private static extern byte b2Body_IsValid(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetType")]
    private static extern BodyType b2Body_GetType(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetType")]
    private static extern void b2Body_SetType(Body bodyId, BodyType type);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetName")]
    private static extern void b2Body_SetName(Body bodyId, string? name);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetName")]
    private static extern nint b2Body_GetName(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetUserData")]
    private static extern void b2Body_SetUserData(Body bodyId, nint userData);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetUserData")]
    private static extern nint b2Body_GetUserData(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetPosition")]
    private static extern Vec2 b2Body_GetPosition(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetRotation")]
    private static extern Rotation b2Body_GetRotation(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetTransform")]
    private static extern Transform b2Body_GetTransform(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetTransform")]
    private static extern void b2Body_SetTransform(Body bodyId, Vec2 position, Rotation rotation);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalPoint")]
    private static extern Vec2 b2Body_GetLocalPoint(Body bodyId, Vec2 worldPoint);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldPoint")]
    private static extern Vec2 b2Body_GetWorldPoint(Body bodyId, Vec2 localPoint);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalVector")]
    private static extern Vec2 b2Body_GetLocalVector(Body bodyId, Vec2 worldVector);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldVector")]
    private static extern Vec2 b2Body_GetWorldVector(Body bodyId, Vec2 localVector);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetLinearVelocity")]
    private static extern void b2Body_SetLinearVelocity(Body bodyId, Vec2 linearVelocity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLinearVelocity")]
    private static extern Vec2 b2Body_GetLinearVelocity(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetAngularVelocity")]
    private static extern float b2Body_GetAngularVelocity(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetAngularVelocity")]
    private static extern void b2Body_SetAngularVelocity(Body bodyId, float angularVelocity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetTargetTransform")]
    private static extern void b2Body_SetTargetTransform(Body bodyId, Transform target, float timeStep);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalPointVelocity")]
    private static extern Vec2 b2Body_GetLocalPointVelocity(Body bodyId, Vec2 localPoint);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldPointVelocity")]
    private static extern Vec2 b2Body_GetWorldPointVelocity(Body bodyId, Vec2 worldPoint);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyForce")]
    private static extern void b2Body_ApplyForce(Body bodyId, Vec2 force, Vec2 point, byte wake);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyForceToCenter")]
    private static extern void b2Body_ApplyForceToCenter(Body bodyId, Vec2 force, byte wake);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyTorque")]
    private static extern void b2Body_ApplyTorque(Body bodyId, float torque, byte wake);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyLinearImpulse")]
    private static extern void b2Body_ApplyLinearImpulse(Body bodyId, Vec2 impulse, Vec2 point, byte wake);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyLinearImpulseToCenter")]
    private static extern void b2Body_ApplyLinearImpulseToCenter(Body bodyId, Vec2 impulse, byte wake);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyAngularImpulse")]
    private static extern void b2Body_ApplyAngularImpulse(Body bodyId, float impulse, byte wake);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetMass")]
    private static extern float b2Body_GetMass(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetRotationalInertia")]
    private static extern float b2Body_GetRotationalInertia(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLocalCenterOfMass")]
    private static extern Vec2 b2Body_GetLocalCenterOfMass(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorldCenterOfMass")]
    private static extern Vec2 b2Body_GetWorldCenterOfMass(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetMassData")]
    private static extern void b2Body_SetMassData(Body bodyId, MassData massData);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetMassData")]
    private static extern MassData b2Body_GetMassData(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ApplyMassFromShapes")]
    private static extern void b2Body_ApplyMassFromShapes(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetLinearDamping")]
    private static extern void b2Body_SetLinearDamping(Body bodyId, float linearDamping);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetLinearDamping")]
    private static extern float b2Body_GetLinearDamping(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetAngularDamping")]
    private static extern void b2Body_SetAngularDamping(Body bodyId, float angularDamping);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetAngularDamping")]
    private static extern float b2Body_GetAngularDamping(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetGravityScale")]
    private static extern void b2Body_SetGravityScale(Body bodyId, float gravityScale);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetGravityScale")]
    private static extern float b2Body_GetGravityScale(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsAwake")]
    private static extern byte b2Body_IsAwake(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetAwake")]
    private static extern void b2Body_SetAwake(Body bodyId, byte awake);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_EnableSleep")]
    private static extern void b2Body_EnableSleep(Body bodyId, byte enableSleep);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsSleepEnabled")]
    private static extern byte b2Body_IsSleepEnabled(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetSleepThreshold")]
    private static extern void b2Body_SetSleepThreshold(Body bodyId, float sleepThreshold);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetSleepThreshold")]
    private static extern float b2Body_GetSleepThreshold(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsEnabled")]
    private static extern byte b2Body_IsEnabled(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_Disable")]
    private static extern void b2Body_Disable(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_Enable")]
    private static extern void b2Body_Enable(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetFixedRotation")]
    private static extern void b2Body_SetFixedRotation(Body bodyId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsFixedRotation")]
    private static extern byte b2Body_IsFixedRotation(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_SetBullet")]
    private static extern void b2Body_SetBullet(Body bodyId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_IsBullet")]
    private static extern byte b2Body_IsBullet(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_EnableContactEvents")]
    private static extern void b2Body_EnableContactEvents(Body bodyId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_EnableHitEvents")]
    private static extern void b2Body_EnableHitEvents(Body bodyId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetWorld")]
    private static extern WorldId b2Body_GetWorld(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetShapeCount")]
    private static extern int b2Body_GetShapeCount(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetShapes")]
    private static extern unsafe int b2Body_GetShapes(Body bodyId, Shape* shapeArray, int capacity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetJointCount")]
    private static extern int b2Body_GetJointCount(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetJoints")]
    private static extern unsafe int b2Body_GetJoints(Body bodyId, JointId* jointArray, int capacity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetContactCapacity")]
    private static extern int b2Body_GetContactCapacity(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_GetContactData")]
    private static extern unsafe int b2Body_GetContactData(Body bodyId, ContactData* contactData, int capacity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Body_ComputeAABB")]
    private static extern AABB b2Body_ComputeAABB(Body bodyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateCircleShape")]
    private static extern Shape b2CreateCircleShape(Body bodyId, in ShapeDefInternal def, in Circle circle);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateSegmentShape")]
    private static extern Shape b2CreateSegmentShape(Body bodyId, in ShapeDefInternal def, in Segment segment);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateCapsuleShape")]
    private static extern Shape b2CreateCapsuleShape(Body bodyId, in ShapeDefInternal def, in Capsule capsule);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreatePolygonShape")]
    private static extern Shape b2CreatePolygonShape(Body bodyId, in ShapeDefInternal def, in Polygon polygon);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateChain")]
    private static extern ChainShape b2CreateChain(Body bodyId, in ChainDefInternal def);
#endif
    }
}
