using Box2D.Comparers;
using JetBrains.Annotations;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A shape is a geometric object used for collision detection.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public struct Shape : IEquatable<Shape>
{
    private int index1;
    private ushort world0;
    private ushort generation;

    /// <summary>
    /// Create a shape on the specified body with the specified definition.
    /// </summary>
    /// <param name="body">The body on which to create the shape</param>
    /// <param name="def">The shape definition</param>
    /// <param name="circle">The shape to create</param>
    public Shape(Body body, in ShapeDef def, in Circle circle)
    {
        this = body.CreateShape(def, circle);
    }
    
    /// <summary>
    /// Create a shape on the specified body with the specified definition.
    /// </summary>
    /// <param name="body">The body on which to create the shape</param>
    /// <param name="def">The shape definition</param>
    /// <param name="segment">The shape to create</param>
    public Shape(Body body, in ShapeDef def, in Segment segment)
    {
        this = body.CreateShape(def, segment);
    }
    
    /// <summary>
    /// Create a shape on the specified body with the specified definition.
    /// </summary>
    /// <param name="body">The body on which to create the shape</param>
    /// <param name="def">The shape definition</param>
    /// <param name="capsule">The shape to create</param>
    public Shape(Body body, in ShapeDef def, in Capsule capsule)
    {
        this = body.CreateShape(def, capsule);
    }
    
    /// <summary>
    /// Create a shape on the specified body with the specified definition.
    /// </summary>
    /// <param name="body">The body on which to create the shape</param>
    /// <param name="def">The shape definition</param>
    /// <param name="polygon">The shape to create</param>
    public Shape(Body body, in ShapeDef def, in Polygon polygon)
    {
        this = body.CreateShape(def, polygon);
    }
    
    /// <summary>
    /// Default comparer for Shape instances. This is used for sorting and
    /// comparing shapes. Using the default comparer is recommended
    /// to avoid boxing and unboxing overhead.
    /// </summary>
    public static IComparer<Shape> DefaultComparer { get; } = ShapeComparer.Instance;
    
    /// <summary>
    /// Default equality comparer for Shape instances. This is used for
    /// comparing shapes. Using the default equality comparer is recommended
    /// to avoid boxing and unboxing overhead.
    /// </summary>
    public static IEqualityComparer<Shape> DefaultEqualityComparer { get; } = ShapeComparer.Instance;
    
    /// <summary>
    /// Checks if this Shape and another Shape refer to the same shape
    /// </summary>
    public bool Equals(Shape other) => index1 == other.index1 && world0 == other.world0 && generation == other.generation;

    /// <summary>
    /// Checks if this Shape and another Shape refer to the same shape
    /// </summary>
    public override bool Equals(object? obj) => obj is Shape other && Equals(other);

    /// <summary>
    /// Returns a hash code for this shape
    /// </summary>
    public override int GetHashCode() => HashCode.Combine(index1, world0, generation);

    /// <summary>
    /// Checks if this shape and another shape refer to the same shape
    /// </summary>
    /// <returns>true if this shape and the other shape refer to the same shape</returns>
    public static bool operator ==(Shape left, Shape right) => left.Equals(right);
    
    /// <summary>
    /// Checks if this shape and another shape refer to different shapes
    /// </summary>
    /// <returns>true if this shape and the other shape refer to different shapes</returns>
    public static bool operator !=(Shape left, Shape right) => !left.Equals(right);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DestroyShape")]
    private static extern void b2DestroyShape(Shape shape, byte updateBodyMass);

    /// <summary>
    /// Destroys this shape
    /// </summary>
    /// <param name="updateBodyMass">Option to defer the body mass update</param>
    /// <remarks>You may defer the body mass update which can improve performance if several shapes on a body are destroyed at once</remarks>
    public void Destroy(bool updateBodyMass)
    {
        if (!Valid) return;
        // dealloc user data
        nint userDataPtr = b2Shape_GetUserData(this);
        FreeHandle(ref userDataPtr);
        b2Shape_SetUserData(this, 0);

        b2DestroyShape(this, updateBodyMass ? (byte)1 : (byte)0);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_IsValid")]
    private static extern byte b2Shape_IsValid(Shape shape);

    /// <summary>
    /// Checks if this shape is valid
    /// </summary>
    /// <returns>true if this shape is valid</returns>
    /// <remarks>Provides validation for up to 64K allocations</remarks>
    public bool Valid => b2Shape_IsValid(this) != 0;

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetType")]
    private static extern ShapeType b2Shape_GetType(Shape shape);

    /// <summary>
    /// Gets the type of this shape
    /// </summary>
    /// <returns>The type of this shape</returns>
    public ShapeType Type => Valid ? b2Shape_GetType(this) : throw new InvalidOperationException("Shape is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetBody")]
    private static extern Body b2Shape_GetBody(Shape shape);

    /// <summary>
    /// Gets the body that this shape is attached to
    /// </summary>
    /// <returns>The body that this shape is attached to</returns>
    public Body Body => Valid ? b2Shape_GetBody(this) : throw new InvalidOperationException("Shape is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetWorld")]
    private static extern WorldId b2Shape_GetWorld(Shape shape);

    /// <summary>
    /// Gets the world that this shape belongs to
    /// </summary>
    public World World => Valid ? World.GetWorld(b2Shape_GetWorld(this)) : throw new InvalidOperationException("Shape is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_IsSensor")]
    private static extern byte b2Shape_IsSensor(Shape shape);

    /// <summary>
    /// Checks if this shape is a sensor
    /// </summary>
    /// <returns>true if this shape is a sensor</returns>
    /// <remarks>It is not possible to change a shape
    /// from sensor to solid dynamically because this breaks the contract for
    /// sensor events.</remarks>
    public bool Sensor => Valid ? b2Shape_IsSensor(this) != 0 : throw new InvalidOperationException("Shape is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_EnableSensorEvents")]
    private static extern void b2Shape_EnableSensorEvents(Shape shape, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_AreSensorEventsEnabled")]
    private static extern byte b2Shape_AreSensorEventsEnabled(Shape shape);

    /// <summary>
    /// Gets or sets the Sensor Events Enabled state for this shape
    /// </summary>
    private bool SensorEventsEnabled
    {
        get => Valid ? b2Shape_AreSensorEventsEnabled(this) != 0 : throw new InvalidOperationException("Shape is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Shape is not valid");
            b2Shape_EnableSensorEvents(this, value ? (byte)1 : (byte)0);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetUserData")]
    private static extern void b2Shape_SetUserData(Shape shape, nint userData);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetUserData")]
    private static extern nint b2Shape_GetUserData(Shape shape);

    /// <summary>
    /// The user data object for this shape.
    /// </summary>
    public object? UserData
    {
        get => Valid ? GetObjectAtPointer(b2Shape_GetUserData, this) : throw new InvalidOperationException("Shape is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Shape is not valid");
            SetObjectAtPointer(b2Shape_GetUserData, b2Shape_SetUserData, this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetDensity")]
    private static extern void b2Shape_SetDensity(Shape shape, float density, byte updateBodyMass);

    /// <summary>
    /// Sets the mass density of this shape
    /// </summary>
    /// <param name="density">The mass density</param>
    /// <param name="updateBodyMass">Option to update the mass properties on the parent body</param>
    /// <remarks>This will optionally update the mass properties on the parent body</remarks>
    public void SetDensity(float density, bool updateBodyMass) => b2Shape_SetDensity(this, density, updateBodyMass ? (byte)1 : (byte)0);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetDensity")]
    private static extern float b2Shape_GetDensity(Shape shape);

    /// <summary>
    /// The mass density of this shape
    /// </summary>
    /// <remarks>This will update the mass properties on the parent body. To avoid this, use <see cref="SetDensity(float,bool)"/></remarks>
    public float Density
    {
        get => Valid ? b2Shape_GetDensity(this) : throw new InvalidOperationException("Shape is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Shape is not valid");
            b2Shape_SetDensity(this, value, 1);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetFriction")]
    private static extern void b2Shape_SetFriction(Shape shape, float friction);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetFriction")]
    private static extern float b2Shape_GetFriction(Shape shape);

    /// <summary>
    /// The friction on this shape
    /// </summary>
    public float Friction
    {
        get => Valid ? b2Shape_GetFriction(this) : throw new InvalidOperationException("Shape is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Shape is not valid");
            b2Shape_SetFriction(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetRestitution")]
    private static extern void b2Shape_SetRestitution(Shape shape, float restitution);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetRestitution")]
    private static extern float b2Shape_GetRestitution(Shape shape);

    /// <summary>
    /// Restitution of this shape
    /// </summary>
    /// <remarks>This is the coefficient of restitution (bounce) usually in the range [0,1].</remarks>
    public float Restitution
    {
        get => Valid ? b2Shape_GetRestitution(this) : throw new InvalidOperationException("Shape is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Shape is not valid");
            b2Shape_SetRestitution(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetMaterial")]
    private static extern void b2Shape_SetMaterial(Shape shape, int material);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetMaterial")]
    private static extern int b2Shape_GetMaterial(Shape shape);

    /// <summary>
    /// Gets/sets the material ID for this shape - this does not affect the shape's physical properties
    /// </summary>
    public int Material
    {
        get => Valid ? b2Shape_GetMaterial(this) : throw new InvalidOperationException("Shape is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Shape is not valid");
            b2Shape_SetMaterial(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetSurfaceMaterial")]
    private static extern void b2Shape_SetSurfaceMaterial(Shape shape, SurfaceMaterial surfaceMaterial);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetSurfaceMaterial")]
    private static extern SurfaceMaterial b2Shape_GetSurfaceMaterial(Shape shape);
    
    /// <summary>
    /// Gets/sets the surface material for this shape
    /// </summary>
    public SurfaceMaterial SurfaceMaterial
    {
        get => Valid ? b2Shape_GetSurfaceMaterial(this) : throw new InvalidOperationException("Shape is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Shape is not valid");
            b2Shape_SetSurfaceMaterial(this, value);
        }
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetFilter")]
    private static extern Filter b2Shape_GetFilter(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetFilter")]
    private static extern void b2Shape_SetFilter(Shape shape, Filter filter);

    /// <summary>
    /// The filter for this shape
    /// </summary>
    /// <remarks>This may cause
    /// contacts to be immediately destroyed. However contacts are not created until the next world step.
    /// Sensor overlap state is also not updated until the next world step.</remarks>
    public Filter Filter
    {
        get => Valid ? b2Shape_GetFilter(this) : throw new InvalidOperationException("Shape is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Shape is not valid");
            b2Shape_SetFilter(this, value);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_EnableContactEvents")]
    private static extern void b2Shape_EnableContactEvents(Shape shape, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_AreContactEventsEnabled")]
    private static extern byte b2Shape_AreContactEventsEnabled(Shape shape);

    /// <summary>
    /// Contact enabled state for this shape
    /// </summary>
    public bool ContactEventsEnabled
    {
        get => Valid ? b2Shape_AreContactEventsEnabled(this) != 0 : throw new InvalidOperationException("Shape is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Shape is not valid");
            b2Shape_EnableContactEvents(this, value ? (byte)1 : (byte)0);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_EnablePreSolveEvents")]
    private static extern void b2Shape_EnablePreSolveEvents(Shape shape, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_ArePreSolveEventsEnabled")]
    private static extern byte b2Shape_ArePreSolveEventsEnabled(Shape shape);

    /// <summary>
    /// The pre-solve contact enabled state for this shape
    /// </summary>
    /// <remarks>Only applies to dynamic bodies. <br/><br/><b>Warning: These are expensive and must be carefully handled due to multithreading.</b><br/><br/>Ignored for sensors</remarks>
    public bool PreSolveEventsEnabled
    {
        get => Valid ? b2Shape_ArePreSolveEventsEnabled(this) != 0 : throw new InvalidOperationException("Shape is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException( "Shape is not valid");
            b2Shape_EnablePreSolveEvents(this, value ? (byte)1 : (byte)0);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_EnableHitEvents")]
    private static extern void b2Shape_EnableHitEvents(Shape shape, byte flag);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_AreHitEventsEnabled")]
    private static extern byte b2Shape_AreHitEventsEnabled(Shape shape);

    /// <summary>
    /// Hit events enabled state for this shape
    /// </summary>
    public bool HitEventsEnabled
    {
        get => Valid ? b2Shape_AreHitEventsEnabled(this) != 0 : throw new InvalidOperationException("Shape is not valid");
        set
        {
            if (!Valid)
                throw new InvalidOperationException("Shape is not valid");
            b2Shape_EnableHitEvents(this, value ? (byte)1 : (byte)0);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_TestPoint")]
    private static extern byte b2Shape_TestPoint(Shape shape, Vec2 point);

    /// <summary>
    /// Tests a point for overlap with this shape
    /// </summary>
    /// <param name="point">The point</param>
    /// <returns>true if the point overlaps with this shape</returns>
    public bool TestPoint(Vec2 point) => b2Shape_TestPoint(this, point) != 0;

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_RayCast")]
    private static extern CastOutput b2Shape_RayCast(Shape shape, in RayCastInput input);

    /// <summary>
    /// Ray casts this shape directly
    /// </summary>
    /// <param name="input">The ray cast input</param>
    /// <returns>The ray cast output</returns>
    public CastOutput RayCast(in RayCastInput input) => b2Shape_RayCast(this, input);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetCircle")]
    private static extern Circle b2Shape_GetCircle(Shape shape);

    /// <summary>
    /// Gets a copy of the shape's circle
    /// </summary>
    /// <returns>The circle</returns>
    /// <remarks>Asserts the type is correct</remarks>
    public Circle GetCircle() => b2Shape_GetCircle(this);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetSegment")]
    private static extern Segment b2Shape_GetSegment(Shape shape);

    /// <summary>
    /// Gets a copy of the shape's line segment
    /// </summary>
    /// <returns>The segment</returns>
    /// <remarks>Asserts the type is correct</remarks>
    public Segment GetSegment() => b2Shape_GetSegment(this);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetChainSegment")]
    private static extern ChainSegment b2Shape_GetChainSegment(Shape shape);

    /// <summary>
    /// Gets a copy of the shape's chain segment
    /// </summary>
    /// <returns>The chain segment</returns>
    /// <remarks>These come from chain shapes. Asserts the type is correct</remarks>
    public ChainSegment GetChainSegment() => b2Shape_GetChainSegment(this);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetCapsule")]
    private static extern Capsule b2Shape_GetCapsule(Shape shape);

    /// <summary>
    /// Gets a copy of the shape's capsule
    /// </summary>
    /// <returns>The capsule</returns>
    /// <remarks>Asserts the type is correct</remarks>
    public Capsule GetCapsule() => b2Shape_GetCapsule(this);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetPolygon")]
    private static extern Polygon b2Shape_GetPolygon(Shape shape);

    /// <summary>
    /// Gets a copy of the shape's convex polygon
    /// </summary>
    /// <returns>The polygon</returns>
    /// <remarks>Asserts the type is correct</remarks>
    public Polygon GetPolygon() => b2Shape_GetPolygon(this);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetCircle")]
    private static extern void b2Shape_SetCircle(Shape shape, in Circle circle);

    /// <summary>
    /// Allows you to change this shape to be a circle or update the current circle
    /// </summary>
    /// <param name="circle">The circle</param>
    /// <remarks>This does not modify the mass properties</remarks>
    public void SetCircle(in Circle circle) => b2Shape_SetCircle(this, circle);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetCapsule")]
    private static extern void b2Shape_SetCapsule(Shape shape, in Capsule capsule);

    /// <summary>
    /// Allows you to change this shape to be a capsule or update the current capsule
    /// </summary>
    /// <param name="capsule">The capsule</param>
    /// <remarks>This does not modify the mass properties</remarks>
    public void SetCapsule(in Capsule capsule) => b2Shape_SetCapsule(this, capsule);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetSegment")]
    private static extern void b2Shape_SetSegment(Shape shape, in Segment segment);

    /// <summary>
    /// Allows you to change this shape to be a segment or update the current segment
    /// </summary>
    /// <param name="segment">The segment</param>
    public void SetSegment(in Segment segment) => b2Shape_SetSegment(this, segment);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_SetPolygon")]
    private static extern void b2Shape_SetPolygon(Shape shape, in Polygon polygon);

    /// <summary>
    /// Allows you to change this shape to be a polygon or update the current polygon
    /// </summary>
    /// <param name="polygon">The polygon</param>
    /// <remarks>This does not modify the mass properties</remarks>
    public void SetPolygon(in Polygon polygon) => b2Shape_SetPolygon(this, polygon);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetParentChain")]
    private static extern ChainShape b2Shape_GetParentChain(Shape shape);

    /// <summary>
    /// Gets the parent chain id if the shape type is a chain segment
    /// </summary>
    /// <returns>The parent chain id if the shape type is a chain segment, otherwise returns 0</returns>
    public ChainShape GetParentChain() => b2Shape_GetParentChain(this);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetContactCapacity")]
    private static extern int b2Shape_GetContactCapacity(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetContactData")]
    private static extern unsafe int b2Shape_GetContactData(Shape shape, ContactData* contactData, int capacity);

    /// <summary>
    /// Gets the contact data for this shape
    /// </summary>
    public unsafe ReadOnlySpan<ContactData> ContactData
    {
        get
        {
            if (!Valid)
                throw new InvalidOperationException("Shape is not valid");
            int needed = b2Shape_GetContactCapacity(this);
            ContactData[] buffer = 
#if NET5_0_OR_GREATER
                GC.AllocateUninitializedArray<ContactData>(needed);
#else
                new ContactData[needed];
#endif
            int written;
            fixed (ContactData* p = buffer)
            {
                written = b2Shape_GetContactData(this, p, buffer.Length);
            }
            return buffer.AsSpan(0, written);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetSensorCapacity")]
    private static extern int b2Shape_GetSensorCapacity(Shape shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetSensorOverlaps")]
    private static extern unsafe int b2Shape_GetSensorOverlaps(Shape shape, Shape* overlaps, int capacity);

    /// <summary>
    /// Gets the overlapped shapes for this sensor shape
    /// </summary>
    /// <remarks>
    /// Overlaps may contain destroyed shapes so use <see cref="Valid"/> to confirm each overlap.<br/><br/>
    /// <b>Warning: Do not fetch this property during the contact callbacks</b>
    /// </remarks>
    public unsafe ReadOnlySpan<Shape> SensorOverlaps
    {
        get
        {
            if (!Valid)
                throw new InvalidOperationException("Shape is not valid");
            int needed = b2Shape_GetSensorCapacity(this);
            Shape[] buffer = 
#if NET5_0_OR_GREATER
                GC.AllocateUninitializedArray<Shape>(needed);
#else
                new Shape[needed];
#endif
            int written;
            fixed (Shape* p = buffer)
            {
                written = b2Shape_GetSensorOverlaps(this, p, buffer.Length);
            }
            return buffer.AsSpan(0, written);
        }
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetAABB")]
    private static extern AABB b2Shape_GetAABB(Shape shape);

    /// <summary>
    /// Gets the current world AABB
    /// </summary>
    /// <returns>The current world AABB</returns>
    /// <remarks>This is the axis-aligned bounding box in world coordinates</remarks>
    public AABB AABB => Valid ? b2Shape_GetAABB(this) : throw new InvalidOperationException("Shape is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetMassData")]
    private static extern MassData b2Shape_GetMassData(Shape shape);

    /// <summary>
    /// Gets the mass data for this shape
    /// </summary>
    public MassData MassData => Valid ? b2Shape_GetMassData(this) : throw new InvalidOperationException("Shape is not valid");

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Shape_GetClosestPoint")]
    private static extern Vec2 b2Shape_GetClosestPoint(Shape shape, Vec2 target);

    /// <summary>
    /// Gets the closest point on this shape to a target point
    /// </summary>
    /// <param name="target">The target point</param>
    /// <returns>The closest point on this shape to the target point</returns>
    /// <remarks>Target and result are in world space</remarks>
    public Vec2 GetClosestPoint(Vec2 target)
    {
        return b2Shape_GetClosestPoint(this, target);
    }

    /// <summary>
    /// Gets the local vertices of this shape
    /// </summary>
    public ReadOnlySpan<Vec2> LocalVertices
    {
        get
        {
            if (!Valid)
                throw new InvalidOperationException("Shape is not valid");
            switch (Type)
            {
                case ShapeType.Circle:
                    return new([GetCircle().Center]);
                case ShapeType.Segment:
                    var segment = GetSegment();
                    return new([segment.Point1, segment.Point2]);
                case ShapeType.Polygon:
                    return GetPolygon().Vertices;
                case ShapeType.Capsule:
                    var capsule = GetCapsule();
                    return new([capsule.Center1, capsule.Center2]);
                case ShapeType.ChainSegment:
                    var chainSegment = GetChainSegment();
                    return new([chainSegment.Segment.Point1, chainSegment.Segment.Point2]);
                default:
                    return [];
            }
        }
    }

    /// <summary>
    /// Gets the world vertices of this shape
    /// </summary>
    public ReadOnlySpan<Vec2> WorldVertices
    {
        get
        {
            if (!Valid)
                throw new InvalidOperationException("Shape is not valid");

            var localVertices = LocalVertices;

            Vec2[] worldVertices = new Vec2[localVertices.Length];

            var body = Body;

            for (int i = 0; i < localVertices.Length; i++)
                worldVertices[i] = body.GetWorldPoint(localVertices[i]);

            return worldVertices;
        }
    }

    internal unsafe Vec2* GetVertices(out int count)
    {
        switch (Type)
        {
            case ShapeType.Circle:
                var circle = GetCircle();
                count = 1;
                return &circle.Center;
            case ShapeType.Segment:
                var segment = GetSegment();
                count = 2;
                return &segment.Point1;
            case ShapeType.Polygon:
                var readOnlySpan = GetPolygon().Vertices;
                count = readOnlySpan.Length;
                Vec2 reference = readOnlySpan.GetPinnableReference();
                return &reference;
            case ShapeType.Capsule:
                var capsule = GetCapsule();
                count = 2;
                return &capsule.Center1;
            case ShapeType.ChainSegment:
                var chainSegment = GetChainSegment();
                count = 2;
                return &chainSegment.Segment.Point1;
        }
        count = 0;
        return (Vec2*)0;
    }
}
