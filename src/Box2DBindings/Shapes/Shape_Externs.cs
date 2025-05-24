using System;
using System.Runtime.InteropServices;

namespace Box2D
{
    partial struct Shape
    {
#if NET5_0_OR_GREATER
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, byte, void> b2DestroyShape;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, byte> b2Shape_IsValid;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, ShapeType> b2Shape_GetType;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, Body> b2Shape_GetBody;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, WorldId> b2Shape_GetWorld;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, byte> b2Shape_IsSensor;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, byte, void> b2Shape_EnableSensorEvents;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, byte> b2Shape_AreSensorEventsEnabled;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, nint, void> b2Shape_SetUserData;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, nint> b2Shape_GetUserData;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, float, byte, void> b2Shape_SetDensity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, float> b2Shape_GetDensity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, float, void> b2Shape_SetFriction;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, float> b2Shape_GetFriction;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, float, void> b2Shape_SetRestitution;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, float> b2Shape_GetRestitution;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, int, void> b2Shape_SetMaterial;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, int> b2Shape_GetMaterial;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, SurfaceMaterial, void> b2Shape_SetSurfaceMaterial;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, SurfaceMaterial> b2Shape_GetSurfaceMaterial;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, Filter> b2Shape_GetFilter;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, Filter, void> b2Shape_SetFilter;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, byte, void> b2Shape_EnableContactEvents;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, byte> b2Shape_AreContactEventsEnabled;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, byte, void> b2Shape_EnablePreSolveEvents;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, byte> b2Shape_ArePreSolveEventsEnabled;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, byte, void> b2Shape_EnableHitEvents;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, byte> b2Shape_AreHitEventsEnabled;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, Vec2, byte> b2Shape_TestPoint;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, in RayCastInput, CastOutput> b2Shape_RayCast;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, Circle> b2Shape_GetCircle;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, Segment> b2Shape_GetSegment;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, ChainSegment> b2Shape_GetChainSegment;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, Capsule> b2Shape_GetCapsule;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, Polygon> b2Shape_GetPolygon;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, in Circle, void> b2Shape_SetCircle;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, in Capsule, void> b2Shape_SetCapsule;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, in Segment, void> b2Shape_SetSegment;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, in Polygon, void> b2Shape_SetPolygon;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, ChainShapeId> b2Shape_GetParentChain;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, int> b2Shape_GetContactCapacity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, ContactData*, int, int> b2Shape_GetContactData;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, int> b2Shape_GetSensorCapacity;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, Shape*, int, int> b2Shape_GetSensorOverlaps;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, AABB> b2Shape_GetAABB;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, MassData> b2Shape_GetMassData;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Shape, Vec2, Vec2> b2Shape_GetClosestPoint;

        static unsafe Shape()
        {
            nint lib = nativeLibrary;
            NativeLibrary.TryGetExport(lib, "b2DestroyShape", out var p0);
            NativeLibrary.TryGetExport(lib, "b2Shape_IsValid", out var p1);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetType", out var p2);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetBody", out var p3);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetWorld", out var p4);
            NativeLibrary.TryGetExport(lib, "b2Shape_IsSensor", out var p5);
            NativeLibrary.TryGetExport(lib, "b2Shape_EnableSensorEvents", out var p6);
            NativeLibrary.TryGetExport(lib, "b2Shape_AreSensorEventsEnabled", out var p7);
            NativeLibrary.TryGetExport(lib, "b2Shape_SetUserData", out var p8);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetUserData", out var p9);
            NativeLibrary.TryGetExport(lib, "b2Shape_SetDensity", out var p10);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetDensity", out var p11);
            NativeLibrary.TryGetExport(lib, "b2Shape_SetFriction", out var p12);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetFriction", out var p13);
            NativeLibrary.TryGetExport(lib, "b2Shape_SetRestitution", out var p14);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetRestitution", out var p15);
            NativeLibrary.TryGetExport(lib, "b2Shape_SetMaterial", out var p16);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetMaterial", out var p17);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetFilter", out var p18);
            NativeLibrary.TryGetExport(lib, "b2Shape_SetFilter", out var p19);
            NativeLibrary.TryGetExport(lib, "b2Shape_EnableContactEvents", out var p20);
            NativeLibrary.TryGetExport(lib, "b2Shape_AreContactEventsEnabled", out var p21);
            NativeLibrary.TryGetExport(lib, "b2Shape_EnablePreSolveEvents", out var p22);
            NativeLibrary.TryGetExport(lib, "b2Shape_ArePreSolveEventsEnabled", out var p23);
            NativeLibrary.TryGetExport(lib, "b2Shape_EnableHitEvents", out var p24);
            NativeLibrary.TryGetExport(lib, "b2Shape_AreHitEventsEnabled", out var p25);
            NativeLibrary.TryGetExport(lib, "b2Shape_TestPoint", out var p26);
            NativeLibrary.TryGetExport(lib, "b2Shape_RayCast", out var p27);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetCircle", out var p28);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetSegment", out var p29);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetChainSegment", out var p30);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetCapsule", out var p31);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetPolygon", out var p32);
            NativeLibrary.TryGetExport(lib, "b2Shape_SetCircle", out var p33);
            NativeLibrary.TryGetExport(lib, "b2Shape_SetCapsule", out var p34);
            NativeLibrary.TryGetExport(lib, "b2Shape_SetSegment", out var p35);
            NativeLibrary.TryGetExport(lib, "b2Shape_SetPolygon", out var p36);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetParentChain", out var p37);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetContactCapacity", out var p38);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetContactData", out var p39);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetSensorCapacity", out var p40);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetSensorOverlaps", out var p41);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetAABB", out var p42);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetMassData", out var p43);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetClosestPoint", out var p44);
            NativeLibrary.TryGetExport(lib, "b2Shape_SetSurfaceMaterial", out var p45);
            NativeLibrary.TryGetExport(lib, "b2Shape_GetSurfaceMaterial", out var p46);

            b2DestroyShape = (delegate* unmanaged[Cdecl]<Shape, byte, void>)p0;
            b2Shape_IsValid = (delegate* unmanaged[Cdecl]<Shape, byte>)p1;
            b2Shape_GetType = (delegate* unmanaged[Cdecl]<Shape, ShapeType>)p2;
            b2Shape_GetBody = (delegate* unmanaged[Cdecl]<Shape, Body>)p3;
            b2Shape_GetWorld = (delegate* unmanaged[Cdecl]<Shape, WorldId>)p4;
            b2Shape_IsSensor = (delegate* unmanaged[Cdecl]<Shape, byte>)p5;
            b2Shape_EnableSensorEvents = (delegate* unmanaged[Cdecl]<Shape, byte, void>)p6;
            b2Shape_AreSensorEventsEnabled = (delegate* unmanaged[Cdecl]<Shape, byte>)p7;
            b2Shape_SetUserData = (delegate* unmanaged[Cdecl]<Shape, nint, void>)p8;
            b2Shape_GetUserData = (delegate* unmanaged[Cdecl]<Shape, nint>)p9;
            b2Shape_SetDensity = (delegate* unmanaged[Cdecl]<Shape, float, byte, void>)p10;
            b2Shape_GetDensity = (delegate* unmanaged[Cdecl]<Shape, float>)p11;
            b2Shape_SetFriction = (delegate* unmanaged[Cdecl]<Shape, float, void>)p12;
            b2Shape_GetFriction = (delegate* unmanaged[Cdecl]<Shape, float>)p13;
            b2Shape_SetRestitution = (delegate* unmanaged[Cdecl]<Shape, float, void>)p14;
            b2Shape_GetRestitution = (delegate* unmanaged[Cdecl]<Shape, float>)p15;
            b2Shape_SetMaterial = (delegate* unmanaged[Cdecl]<Shape, int, void>)p16;
            b2Shape_GetMaterial = (delegate* unmanaged[Cdecl]<Shape, int>)p17;
            b2Shape_GetFilter = (delegate* unmanaged[Cdecl]<Shape, Filter>)p18;
            b2Shape_SetFilter = (delegate* unmanaged[Cdecl]<Shape, Filter, void>)p19;
            b2Shape_EnableContactEvents = (delegate* unmanaged[Cdecl]<Shape, byte, void>)p20;
            b2Shape_AreContactEventsEnabled = (delegate* unmanaged[Cdecl]<Shape, byte>)p21;
            b2Shape_EnablePreSolveEvents = (delegate* unmanaged[Cdecl]<Shape, byte, void>)p22;
            b2Shape_ArePreSolveEventsEnabled = (delegate* unmanaged[Cdecl]<Shape, byte>)p23;
            b2Shape_EnableHitEvents = (delegate* unmanaged[Cdecl]<Shape, byte, void>)p24;
            b2Shape_AreHitEventsEnabled = (delegate* unmanaged[Cdecl]<Shape, byte>)p25;
            b2Shape_TestPoint = (delegate* unmanaged[Cdecl]<Shape, Vec2, byte>)p26;
            b2Shape_RayCast = (delegate* unmanaged[Cdecl]<Shape, in RayCastInput, CastOutput>)p27;
            b2Shape_GetCircle = (delegate* unmanaged[Cdecl]<Shape, Circle>)p28;
            b2Shape_GetSegment = (delegate* unmanaged[Cdecl]<Shape, Segment>)p29;
            b2Shape_GetChainSegment = (delegate* unmanaged[Cdecl]<Shape, ChainSegment>)p30;
            b2Shape_GetCapsule = (delegate* unmanaged[Cdecl]<Shape, Capsule>)p31;
            b2Shape_GetPolygon = (delegate* unmanaged[Cdecl]<Shape, Polygon>)p32;
            b2Shape_SetCircle = (delegate* unmanaged[Cdecl]<Shape, in Circle, void>)p33;
            b2Shape_SetCapsule = (delegate* unmanaged[Cdecl]<Shape, in Capsule, void>)p34;
            b2Shape_SetSegment = (delegate* unmanaged[Cdecl]<Shape, in Segment, void>)p35;
            b2Shape_SetPolygon = (delegate* unmanaged[Cdecl]<Shape, in Polygon, void>)p36;
            b2Shape_GetParentChain = (delegate* unmanaged[Cdecl]<Shape, ChainShapeId>)p37;
            b2Shape_GetContactCapacity = (delegate* unmanaged[Cdecl]<Shape, int>)p38;
            b2Shape_GetContactData = (delegate* unmanaged[Cdecl]<Shape, ContactData*, int, int>)p39;
            b2Shape_GetSensorCapacity = (delegate* unmanaged[Cdecl]<Shape, int>)p40;
            b2Shape_GetSensorOverlaps = (delegate* unmanaged[Cdecl]<Shape, Shape*, int, int>)p41;
            b2Shape_GetAABB = (delegate* unmanaged[Cdecl]<Shape, AABB>)p42;
            b2Shape_GetMassData = (delegate* unmanaged[Cdecl]<Shape, MassData>)p43;
            b2Shape_GetClosestPoint = (delegate* unmanaged[Cdecl]<Shape, Vec2, Vec2>)p44;
            b2Shape_SetSurfaceMaterial = (delegate* unmanaged[Cdecl]<Shape, SurfaceMaterial, void>)p45;
            b2Shape_GetSurfaceMaterial = (delegate* unmanaged[Cdecl]<Shape, SurfaceMaterial>)p46;
        }
        #else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DestroyShape")]
    private static extern void b2DestroyShape(Shape shape, byte updateBodyMass);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_IsValid")]
    private static extern byte b2Shape_IsValid(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetType")]
    private static extern ShapeType b2Shape_GetType(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetBody")]
    private static extern Body b2Shape_GetBody(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetWorld")]
    private static extern WorldId b2Shape_GetWorld(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_IsSensor")]
    private static extern byte b2Shape_IsSensor(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_EnableSensorEvents")]
    private static extern void b2Shape_EnableSensorEvents(Shape shape, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_AreSensorEventsEnabled")]
    private static extern byte b2Shape_AreSensorEventsEnabled(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetUserData")]
    private static extern void b2Shape_SetUserData(Shape shape, nint userData);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetUserData")]
    private static extern nint b2Shape_GetUserData(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetDensity")]
    private static extern void b2Shape_SetDensity(Shape shape, float density, byte updateBodyMass);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetDensity")]
    private static extern float b2Shape_GetDensity(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetFriction")]
    private static extern void b2Shape_SetFriction(Shape shape, float friction);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetFriction")]
    private static extern float b2Shape_GetFriction(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetRestitution")]
    private static extern void b2Shape_SetRestitution(Shape shape, float restitution);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetRestitution")]
    private static extern float b2Shape_GetRestitution(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetMaterial")]
    private static extern void b2Shape_SetMaterial(Shape shape, int material);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetMaterial")]
    private static extern int b2Shape_GetMaterial(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetFilter")]
    private static extern Filter b2Shape_GetFilter(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetFilter")]
    private static extern void b2Shape_SetFilter(Shape shape, Filter filter);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_EnableContactEvents")]
    private static extern void b2Shape_EnableContactEvents(Shape shape, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_AreContactEventsEnabled")]
    private static extern byte b2Shape_AreContactEventsEnabled(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_EnablePreSolveEvents")]
    private static extern void b2Shape_EnablePreSolveEvents(Shape shape, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_ArePreSolveEventsEnabled")]
    private static extern byte b2Shape_ArePreSolveEventsEnabled(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_EnableHitEvents")]
    private static extern void b2Shape_EnableHitEvents(Shape shape, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_AreHitEventsEnabled")]
    private static extern byte b2Shape_AreHitEventsEnabled(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_TestPoint")]
    private static extern byte b2Shape_TestPoint(Shape shape, Vec2 point);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_RayCast")]
    private static extern CastOutput b2Shape_RayCast(Shape shape, in RayCastInput input);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetCircle")]
    private static extern Circle b2Shape_GetCircle(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetSegment")]
    private static extern Segment b2Shape_GetSegment(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetChainSegment")]
    private static extern ChainSegment b2Shape_GetChainSegment(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetCapsule")]
    private static extern Capsule b2Shape_GetCapsule(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetPolygon")]
    private static extern Polygon b2Shape_GetPolygon(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetCircle")]
    private static extern void b2Shape_SetCircle(Shape shape, in Circle circle);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetCapsule")]
    private static extern void b2Shape_SetCapsule(Shape shape, in Capsule capsule);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetSegment")]
    private static extern void b2Shape_SetSegment(Shape shape, in Segment segment);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetPolygon")]
    private static extern void b2Shape_SetPolygon(Shape shape, in Polygon polygon);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetParentChain")]
    private static extern ChainShapeId b2Shape_GetParentChain(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetContactCapacity")]
    private static extern int b2Shape_GetContactCapacity(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetContactData")]
    private static extern unsafe int b2Shape_GetContactData(Shape shape, ContactData* contactData, int capacity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetSensorCapacity")]
    private static extern int b2Shape_GetSensorCapacity(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetSensorOverlaps")]
    private static extern unsafe int b2Shape_GetSensorOverlaps(Shape shape, Shape* overlaps, int capacity);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetAABB")]
    private static extern AABB b2Shape_GetAABB(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetMassData")]
    private static extern MassData b2Shape_GetMassData(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetClosestPoint")]
    private static extern Vec2 b2Shape_GetClosestPoint(Shape shape, Vec2 target);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetSurfaceMaterial")]
    private static extern void b2Shape_SetSurfaceMaterial(Shape shape, SurfaceMaterial surfaceMaterial);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetSurfaceMaterial")]
    private static extern SurfaceMaterial b2Shape_GetSurfaceMaterial(Shape shape);
#endif
    }
}
