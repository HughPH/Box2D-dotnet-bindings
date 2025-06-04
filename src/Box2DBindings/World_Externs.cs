using System.Runtime.InteropServices;

namespace Box2D;

partial class World
{
#if NET5_0_OR_GREATER
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, byte> b2World_IsValid;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, float, int, void> b2World_Step;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, in DebugDrawInternal, void> b2World_Draw_;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, BodyEvents> b2World_GetBodyEvents;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, SensorEvents> b2World_GetSensorEvents;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, ContactEvents> b2World_GetContactEvents;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, byte, void> b2World_EnableSleeping;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, byte> b2World_IsSleepingEnabled;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, byte, void> b2World_EnableContinuous;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, byte> b2World_IsContinuousEnabled;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, float, void> b2World_SetRestitutionThreshold;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, float> b2World_GetRestitutionThreshold;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, float, void> b2World_SetHitEventThreshold;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, float> b2World_GetHitEventThreshold;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, nint, nint, void> b2World_SetCustomFilterCallback_;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, nint, nint, void> b2World_SetPreSolveCallback_;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, Vec2, void> b2World_SetGravity;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, Vec2> b2World_GetGravity;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, in ExplosionDef, void> b2World_Explode;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, float, float, float, void> b2World_SetContactTuning;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, float, float, void> b2World_SetJointTuning;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, float, void> b2World_SetMaximumLinearSpeed;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, float> b2World_GetMaximumLinearSpeed;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, byte, void> b2World_EnableWarmStarting;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, byte> b2World_IsWarmStartingEnabled;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, int> b2World_GetAwakeBodyCount;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, Profile> b2World_GetProfile;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, Counters> b2World_GetCounters;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, nint, void> b2World_SetUserData;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, nint> b2World_GetUserData;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, nint, void> b2World_SetFrictionCallback_;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, nint, void> b2World_SetRestitutionCallback_;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, void> b2World_DumpMemoryStats;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, in BodyDefInternal, Body> b2CreateBody;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, in DistanceJointDefInternal, JointId> b2CreateDistanceJoint;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, in MotorJointDefInternal, JointId> b2CreateMotorJoint;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, in MouseJointDefInternal, JointId> b2CreateMouseJoint;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, in FilterJointDefInternal, JointId> b2CreateFilterJoint;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, in PrismaticJointDefInternal, JointId> b2CreatePrismaticJoint;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, in RevoluteJointDefInternal, JointId> b2CreateRevoluteJoint;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, in WeldJointDefInternal, JointId> b2CreateWeldJoint;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, in WheelJointDefInternal, JointId> b2CreateWheelJoint;
    private static unsafe delegate* unmanaged[Cdecl]<in WorldDefInternal, WorldId> b2CreateWorld;
    private static unsafe delegate* unmanaged[Cdecl]<WorldId, void> b2DestroyWorld;

    private static GCHandle filterHandle;
    private static GCHandle preSolveHandle;
    private static GCHandle frictionHandle;
    private static GCHandle restitutionHandle;

    private static unsafe void b2World_SetCustomFilterCallback(WorldId worldId, CustomFilterNintCallback cb, nint context)
    {
        if (filterHandle.IsAllocated) filterHandle.Free();
        filterHandle = GCHandle.Alloc(cb);
        var ptr = Marshal.GetFunctionPointerForDelegate(cb);
        b2World_SetCustomFilterCallback_(worldId, ptr, context);
    }

    private static unsafe void b2World_SetPreSolveCallback(WorldId worldId, PreSolveNintCallback cb, nint context)
    {
        if (preSolveHandle.IsAllocated) preSolveHandle.Free();
        preSolveHandle = GCHandle.Alloc(cb);
        var ptr = Marshal.GetFunctionPointerForDelegate(cb);
        b2World_SetPreSolveCallback_(worldId, ptr, context);
    }

    private static unsafe void b2World_SetFrictionCallback(WorldId worldId, FrictionCallback cb)
    {
        if (frictionHandle.IsAllocated) frictionHandle.Free();
        frictionHandle = GCHandle.Alloc(cb);
        var ptr = Marshal.GetFunctionPointerForDelegate(cb);
        b2World_SetFrictionCallback_(worldId, ptr);
    }
    
    private static unsafe void b2World_SetRestitutionCallback(WorldId worldId, RestitutionCallback cb)
    {
        if (restitutionHandle.IsAllocated) restitutionHandle.Free();
        restitutionHandle = GCHandle.Alloc(cb);
        var ptr = Marshal.GetFunctionPointerForDelegate(cb);
        b2World_SetRestitutionCallback_(worldId, ptr);
    }
    
    private static unsafe void b2World_Draw(WorldId worldId, ref DebugDrawInternal draw)
    {
        b2World_Draw_(worldId, in draw);
    }
    
    static unsafe World()
    {
        var lib = nativeLibrary;

        NativeLibrary.TryGetExport(lib, "b2World_IsValid", out var p0);
        NativeLibrary.TryGetExport(lib, "b2World_Step", out var p1);
        NativeLibrary.TryGetExport(lib, "b2World_Draw", out var p2);
        NativeLibrary.TryGetExport(lib, "b2World_GetBodyEvents", out var p3);
        NativeLibrary.TryGetExport(lib, "b2World_GetSensorEvents", out var p4);
        NativeLibrary.TryGetExport(lib, "b2World_GetContactEvents", out var p5);
        NativeLibrary.TryGetExport(lib, "b2World_EnableSleeping", out var p6);
        NativeLibrary.TryGetExport(lib, "b2World_IsSleepingEnabled", out var p7);
        NativeLibrary.TryGetExport(lib, "b2World_EnableContinuous", out var p8);
        NativeLibrary.TryGetExport(lib, "b2World_IsContinuousEnabled", out var p9);
        NativeLibrary.TryGetExport(lib, "b2World_SetRestitutionThreshold", out var p10);
        NativeLibrary.TryGetExport(lib, "b2World_GetRestitutionThreshold", out var p11);
        NativeLibrary.TryGetExport(lib, "b2World_SetHitEventThreshold", out var p12);
        NativeLibrary.TryGetExport(lib, "b2World_GetHitEventThreshold", out var p13);
        NativeLibrary.TryGetExport(lib, "b2World_SetCustomFilterCallback", out var p14);
        NativeLibrary.TryGetExport(lib, "b2World_SetPreSolveCallback", out var p15);
        NativeLibrary.TryGetExport(lib, "b2World_SetGravity", out var p16);
        NativeLibrary.TryGetExport(lib, "b2World_GetGravity", out var p17);
        NativeLibrary.TryGetExport(lib, "b2World_Explode", out var p18);
        NativeLibrary.TryGetExport(lib, "b2World_SetContactTuning", out var p19);
        NativeLibrary.TryGetExport(lib, "b2World_SetJointTuning", out var p20);
        NativeLibrary.TryGetExport(lib, "b2World_SetMaximumLinearSpeed", out var p21);
        NativeLibrary.TryGetExport(lib, "b2World_GetMaximumLinearSpeed", out var p22);
        NativeLibrary.TryGetExport(lib, "b2World_EnableWarmStarting", out var p23);
        NativeLibrary.TryGetExport(lib, "b2World_IsWarmStartingEnabled", out var p24);
        NativeLibrary.TryGetExport(lib, "b2World_GetAwakeBodyCount", out var p25);
        NativeLibrary.TryGetExport(lib, "b2World_GetProfile", out var p26);
        NativeLibrary.TryGetExport(lib, "b2World_GetCounters", out var p27);
        NativeLibrary.TryGetExport(lib, "b2World_SetUserData", out var p28);
        NativeLibrary.TryGetExport(lib, "b2World_GetUserData", out var p29);
        NativeLibrary.TryGetExport(lib, "b2World_SetFrictionCallback", out var p30);
        NativeLibrary.TryGetExport(lib, "b2World_SetRestitutionCallback", out var p31);
        NativeLibrary.TryGetExport(lib, "b2World_DumpMemoryStats", out var p32);
        NativeLibrary.TryGetExport(lib, "b2CreateBody", out var p33);
        NativeLibrary.TryGetExport(lib, "b2CreateDistanceJoint", out var p34);
        NativeLibrary.TryGetExport(lib, "b2CreateMotorJoint", out var p35);
        NativeLibrary.TryGetExport(lib, "b2CreateMouseJoint", out var p36);
        NativeLibrary.TryGetExport(lib, "b2CreateFilterJoint", out var p37);
        NativeLibrary.TryGetExport(lib, "b2CreatePrismaticJoint", out var p38);
        NativeLibrary.TryGetExport(lib, "b2CreateRevoluteJoint", out var p39);
        NativeLibrary.TryGetExport(lib, "b2CreateWeldJoint", out var p40);
        NativeLibrary.TryGetExport(lib, "b2CreateWheelJoint", out var p41);
        NativeLibrary.TryGetExport(lib, "b2CreateWorld", out var p42);
        NativeLibrary.TryGetExport(lib, "b2DestroyWorld", out var p43);
        
        b2World_IsValid = (delegate* unmanaged[Cdecl]<WorldId, byte>)p0;
        b2World_Step = (delegate* unmanaged[Cdecl]<WorldId, float, int, void>)p1;
        b2World_Draw_ = (delegate* unmanaged[Cdecl]<WorldId, in DebugDrawInternal, void>)p2;
        b2World_GetBodyEvents = (delegate* unmanaged[Cdecl]<WorldId, BodyEvents>)p3;
        b2World_GetSensorEvents = (delegate* unmanaged[Cdecl]<WorldId, SensorEvents>)p4;
        b2World_GetContactEvents = (delegate* unmanaged[Cdecl]<WorldId, ContactEvents>)p5;
        b2World_EnableSleeping = (delegate* unmanaged[Cdecl]<WorldId, byte, void>)p6;
        b2World_IsSleepingEnabled = (delegate* unmanaged[Cdecl]<WorldId, byte>)p7;
        b2World_EnableContinuous = (delegate* unmanaged[Cdecl]<WorldId, byte, void>)p8;
        b2World_IsContinuousEnabled = (delegate* unmanaged[Cdecl]<WorldId, byte>)p9;
        b2World_SetRestitutionThreshold = (delegate* unmanaged[Cdecl]<WorldId, float, void>)p10;
        b2World_GetRestitutionThreshold = (delegate* unmanaged[Cdecl]<WorldId, float>)p11;
        b2World_SetHitEventThreshold = (delegate* unmanaged[Cdecl]<WorldId, float, void>)p12;
        b2World_GetHitEventThreshold = (delegate* unmanaged[Cdecl]<WorldId, float>)p13;
        b2World_SetCustomFilterCallback_ = (delegate* unmanaged[Cdecl]<WorldId, nint, nint, void>)p14;
        b2World_SetPreSolveCallback_ = (delegate* unmanaged[Cdecl]<WorldId, nint, nint, void>)p15;
        b2World_SetGravity = (delegate* unmanaged[Cdecl]<WorldId, Vec2, void>)p16;
        b2World_GetGravity = (delegate* unmanaged[Cdecl]<WorldId, Vec2>)p17;
        b2World_Explode = (delegate* unmanaged[Cdecl]<WorldId, in ExplosionDef, void>)p18;
        b2World_SetContactTuning = (delegate* unmanaged[Cdecl]<WorldId, float, float, float, void>)p19;
        b2World_SetJointTuning = (delegate* unmanaged[Cdecl]<WorldId, float, float, void>)p20;
        b2World_SetMaximumLinearSpeed = (delegate* unmanaged[Cdecl]<WorldId, float, void>)p21;
        b2World_GetMaximumLinearSpeed = (delegate* unmanaged[Cdecl]<WorldId, float>)p22;
        b2World_EnableWarmStarting = (delegate* unmanaged[Cdecl]<WorldId, byte, void>)p23;
        b2World_IsWarmStartingEnabled = (delegate* unmanaged[Cdecl]<WorldId, byte>)p24;
        b2World_GetAwakeBodyCount = (delegate* unmanaged[Cdecl]<WorldId, int>)p25;
        b2World_GetProfile = (delegate* unmanaged[Cdecl]<WorldId, Profile>)p26;
        b2World_GetCounters = (delegate* unmanaged[Cdecl]<WorldId, Counters>)p27;
        b2World_SetUserData = (delegate* unmanaged[Cdecl]<WorldId, nint, void>)p28;
        b2World_GetUserData = (delegate* unmanaged[Cdecl]<WorldId, nint>)p29;
        b2World_SetFrictionCallback_ = (delegate* unmanaged[Cdecl]<WorldId, nint, void>)p30;
        b2World_SetRestitutionCallback_ = (delegate* unmanaged[Cdecl]<WorldId, nint, void>)p31;
        b2World_DumpMemoryStats = (delegate* unmanaged[Cdecl]<WorldId, void>)p32;
        b2CreateBody = (delegate* unmanaged[Cdecl]<WorldId, in BodyDefInternal, Body>)p33;
        b2CreateDistanceJoint = (delegate* unmanaged[Cdecl]<WorldId, in DistanceJointDefInternal, JointId>)p34;
        b2CreateMotorJoint = (delegate* unmanaged[Cdecl]<WorldId, in MotorJointDefInternal, JointId>)p35;
        b2CreateMouseJoint = (delegate* unmanaged[Cdecl]<WorldId, in MouseJointDefInternal, JointId>)p36;
        b2CreateFilterJoint = (delegate* unmanaged[Cdecl]<WorldId, in FilterJointDefInternal, JointId>)p37;
        b2CreatePrismaticJoint = (delegate* unmanaged[Cdecl]<WorldId, in PrismaticJointDefInternal, JointId>)p38;
        b2CreateRevoluteJoint = (delegate* unmanaged[Cdecl]<WorldId, in RevoluteJointDefInternal, JointId>)p39;
        b2CreateWeldJoint = (delegate* unmanaged[Cdecl]<WorldId, in WeldJointDefInternal, JointId>)p40;
        b2CreateWheelJoint = (delegate* unmanaged[Cdecl]<WorldId, in WheelJointDefInternal, JointId>)p41;
        b2CreateWorld = (delegate* unmanaged[Cdecl]<in WorldDefInternal, WorldId>)p42;
        b2DestroyWorld = (delegate* unmanaged[Cdecl]<WorldId, void>)p43;
        
        NativeLibrary.TryGetExport(lib, "b2World_CastRayClosest", out var p44);
        NativeLibrary.TryGetExport(lib, "b2World_CastRay", out var p45);
        NativeLibrary.TryGetExport(lib, "b2World_CastShape", out var p46);
        NativeLibrary.TryGetExport(lib, "b2World_CastMover", out var p47);
        NativeLibrary.TryGetExport(lib, "b2World_CollideMover", out var p48);
        NativeLibrary.TryGetExport(lib, "b2World_OverlapAABB", out var p49);
        NativeLibrary.TryGetExport(lib, "b2World_OverlapShape", out var p50);

        b2World_CastRayClosest = (delegate* unmanaged[Cdecl]<WorldId, Vec2, Vec2, QueryFilter, RayResult>)p44;
        b2World_CastRay_ = (delegate* unmanaged[Cdecl]<WorldId, Vec2, Vec2, QueryFilter, nint, nint, TreeStats>)p45;
        b2World_CastShape_ = (delegate* unmanaged[Cdecl]<WorldId, in ShapeProxy, Vec2, QueryFilter, nint, nint, TreeStats>)p46;
        b2World_CastMover = (delegate* unmanaged[Cdecl]<WorldId, in Capsule, Vec2, QueryFilter, float>)p47;
        b2World_CollideMover_ = (delegate* unmanaged[Cdecl]<WorldId, in Capsule, QueryFilter, nint, nint, void>)p48;
        b2World_OverlapAABB_ = (delegate* unmanaged[Cdecl]<WorldId, AABB, QueryFilter, nint, nint, TreeStats>)p49;
        b2World_OverlapShape_ = (delegate* unmanaged[Cdecl]<WorldId, in ShapeProxy, QueryFilter, nint, nint, TreeStats>)p50;
    }
    
#else

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateWorld")]
    private static extern WorldId b2CreateWorld(in WorldDefInternal def);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_IsValid")]
    private static extern byte b2World_IsValid(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_Step")]
    private static extern void b2World_Step(WorldId worldId, float timeStep, int subStepCount);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_Draw")]
    private static extern void b2World_Draw(WorldId worldId, ref DebugDrawInternal draw);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetBodyEvents")]
    private static extern BodyEvents b2World_GetBodyEvents(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetSensorEvents")]
    private static extern SensorEvents b2World_GetSensorEvents(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetContactEvents")]
    private static extern ContactEvents b2World_GetContactEvents(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_EnableSleeping")]
    private static extern void b2World_EnableSleeping(WorldId worldId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_IsSleepingEnabled")]
    private static extern byte b2World_IsSleepingEnabled(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_EnableContinuous")]
    private static extern void b2World_EnableContinuous(WorldId worldId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_IsContinuousEnabled")]
    private static extern byte b2World_IsContinuousEnabled(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetRestitutionThreshold")]
    private static extern void b2World_SetRestitutionThreshold(WorldId worldId, float value);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetRestitutionThreshold")]
    private static extern float b2World_GetRestitutionThreshold(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetHitEventThreshold")]
    private static extern void b2World_SetHitEventThreshold(WorldId worldId, float value);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetHitEventThreshold")]
    private static extern float b2World_GetHitEventThreshold(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetCustomFilterCallback")]
    private static extern void b2World_SetCustomFilterCallback(WorldId worldId, CustomFilterNintCallback fcn, nint context);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetPreSolveCallback")]
    private static extern void b2World_SetPreSolveCallback(WorldId worldId, PreSolveNintCallback fcn, nint context);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetGravity")]
    private static extern void b2World_SetGravity(WorldId worldId, Vec2 gravity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetGravity")]
    private static extern Vec2 b2World_GetGravity(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_Explode")]
    private static extern void b2World_Explode(WorldId worldId, in ExplosionDef explosionDef);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetContactTuning")]
    private static extern void b2World_SetContactTuning(WorldId worldId, float hertz, float dampingRatio, float pushSpeed);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetJointTuning")]
    private static extern void b2World_SetJointTuning(WorldId worldId, float hertz, float dampingRatio);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetMaximumLinearSpeed")]
    private static extern void b2World_SetMaximumLinearSpeed(WorldId worldId, float maximumLinearSpeed);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetMaximumLinearSpeed")]
    private static extern float b2World_GetMaximumLinearSpeed(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_EnableWarmStarting")]
    private static extern void b2World_EnableWarmStarting(WorldId worldId, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_IsWarmStartingEnabled")]
    private static extern byte b2World_IsWarmStartingEnabled(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetAwakeBodyCount")]
    private static extern int b2World_GetAwakeBodyCount(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetProfile")]
    private static extern Profile b2World_GetProfile(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetCounters")]
    private static extern Counters b2World_GetCounters(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetUserData")]
    private static extern void b2World_SetUserData(WorldId worldId, nint userData);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_GetUserData")]
    private static extern nint b2World_GetUserData(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetFrictionCallback")]
    private static extern void b2World_SetFrictionCallback(WorldId worldId, FrictionCallback callback);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_SetRestitutionCallback")]
    private static extern void b2World_SetRestitutionCallback(WorldId worldId, RestitutionCallback callback);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_DumpMemoryStats")]
    private static extern void b2World_DumpMemoryStats(WorldId worldId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateBody")]
    private static extern Body b2CreateBody(WorldId worldId, in BodyDefInternal def);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateDistanceJoint")]
    private static extern JointId b2CreateDistanceJoint(WorldId worldId, in DistanceJointDefInternal def);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateMotorJoint")]
    private static extern JointId b2CreateMotorJoint(WorldId worldId, in MotorJointDefInternal def);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateMouseJoint")]
    private static extern JointId b2CreateMouseJoint(WorldId worldId, in MouseJointDefInternal def);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateFilterJoint")]
    private static extern JointId b2CreateFilterJoint(WorldId worldId, in FilterJointDefInternal def);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreatePrismaticJoint")]
    private static extern JointId b2CreatePrismaticJoint(WorldId worldId, in PrismaticJointDefInternal def);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateRevoluteJoint")]
    private static extern JointId b2CreateRevoluteJoint(WorldId worldId, in RevoluteJointDefInternal def);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateWeldJoint")]
    private static extern JointId b2CreateWeldJoint(WorldId worldId, in WeldJointDefInternal def);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CreateWheelJoint")]
    private static extern JointId b2CreateWheelJoint(WorldId worldId, in WheelJointDefInternal def);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DestroyWorld")]
    private static extern void b2DestroyWorld(WorldId worldId);

#endif
}
