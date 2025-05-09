global using static Box2D.Core;
using JetBrains.Annotations;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly:InternalsVisibleTo("UnitTests")]

namespace Box2D;

/// <summary>
/// Core Box2D functions that don't fit into other categories.
/// </summary>
[PublicAPI]
public static class Core
{
    internal const string libraryName = "libbox2d";

    /// <summary>
    /// Multiply and subtract two vectors.
    /// </summary>
    public static Vec2 MultiplySubtract(this Vec2 a, float s, Vec2 b)
    {
        return new(a.X - s * b.X, a.Y - s * b.Y);
    }
    
    /// <summary>
    /// Transform a point by a transform. The transform rotates the point about the origin.
    /// </summary>
    public static Vec2 TransformPoint(Transform t, in Vec2 p)
    {
        float x = ( t.Rotation.Cos * p.X - t.Rotation.Sin * p.Y ) + t.Position.X;
        float y = ( t.Rotation.Sin * p.X + t.Rotation.Cos * p.Y ) + t.Position.Y;

        return new(x, y);
    }
    
    /// <summary>
    /// Get the current version of Box2D
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2GetVersion")]
    public static extern Box2DVersion GetVersion();
    
    /// <summary>
    /// Get the absolute number of system ticks. The value is platform specific.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2GetTicks")]
    public static extern ulong GetTicks();
    
    /// <summary>
    /// Get the milliseconds passed from an initial tick value.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2GetMilliseconds")]
    public static extern float GetMilliseconds(ulong ticks);
    
    /// <summary>
    /// Get the milliseconds passed from an initial tick value. Resets the passed in
    /// value to the current tick value.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2GetMillisecondsAndReset")]
    public static extern float GetMillisecondsAndReset(ref ulong ticks);
    
    /// <summary>
    /// Yield to be used in a busy loop.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Yield")]
    public static extern void Yield();

    /// <summary>
    /// Simple djb2 hash function for determinism testing
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2Hash")]
    public static extern uint Hash(uint hash, byte[] data, int count);
    
    /// <summary>
    /// Compute the distance between two line segments, clamping at the end points if needed.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2SegmentDistance")]
    public static extern SegmentDistanceResult SegmentDistance(in Vec2 p1, in Vec2 q1, in Vec2 p2, in Vec2 q2);

    /// <summary>
    /// Compute the distance between two line segments, clamping at the end points if needed.
    /// </summary>
    public static SegmentDistanceResult SegmentDistance(in Segment segmentA, in Segment segmentB) =>
        SegmentDistance(segmentA.Point1, segmentA.Point2, segmentB.Point1, segmentB.Point2);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ShapeDistance")]
    private static extern unsafe DistanceOutput ShapeDistance(in DistanceInput input, ref SimplexCache cache, Simplex* simplexes, int simplexCapacity);
    
    /// <summary>
    /// Compute the closest points between two shapes represented as point clouds.
    /// SimplexCache cache is input/output. On the first call set SimplexCache.Count to zero.
    /// </summary>
    public static unsafe DistanceOutput ShapeDistance(in DistanceInput input, ref SimplexCache cache, ReadOnlySpan<Simplex> simplexes)
    {
        fixed (Simplex* simplexPtr = simplexes)
            return ShapeDistance(input, ref cache, simplexPtr, simplexes.Length);
    }

    /// <summary>
    /// Perform a linear shape cast of shape B moving and shape A fixed. Determines the hit point, normal, and translation fraction.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ShapeCast")]
    public static extern CastOutput ShapeCast(in ShapeCastPairInput input);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MakeProxy")]
    private static extern unsafe ShapeProxy MakeProxy(Vec2* points, int count, float radius);
    
    /// <summary>
    /// Make a proxy for use in GJK and related functions.
    /// </summary>
    public static unsafe ShapeProxy MakeProxy(ReadOnlySpan<Vec2> vertices, float radius)
    {
        fixed (Vec2* p = vertices)
            return MakeProxy(p, vertices.Length, radius);
    }
    
    /// <summary>
    /// Make a proxy for use in GJK and related functions.
    /// </summary>
    public static ShapeProxy MakeProxy(Vec2[] vertices, float radius)
        => MakeProxy(vertices.AsSpan(), radius);
    
    /// <summary>
    /// Make a proxy for use in GJK and related functions.
    /// </summary>
    public static unsafe ShapeProxy MakeProxy(Vec2 vertex)
    {
        Vec2* vertices = &vertex;
        return MakeProxy(vertices, 1, 0.0f);
    }

    /// <summary>
    /// Make a proxy for use in GJK and related functions.
    /// </summary>
    public static unsafe ShapeProxy MakeProxy(Shape shape, float radius)
    {
        Vec2* vertices = shape.GetVertices(out int count);
        return MakeProxy(vertices, count, radius);
    }
    
    /// <summary>
    /// Make a proxy for use in GJK and related functions.
    /// </summary>
    public static unsafe ShapeProxy MakeProxy(Segment segment)
    {
        Vec2* vertices = &segment.Point1;
        return MakeProxy(vertices, 2, 0.0f);
    }
    
    /// <summary>
    /// Make a proxy for use in GJK and related functions.
    /// </summary>
    public static unsafe ShapeProxy MakeProxy(Circle circle)
    {
        Vec2* vertices = &circle.Center;
        return MakeProxy(vertices, 1, circle.Radius);
    }
    
    /// <summary>
    /// Make a proxy for use in GJK and related functions.
    /// </summary>
    public static ShapeProxy MakeProxy(Polygon polygon, float radius)
    {
        var readOnlySpan = polygon.Vertices;
        return MakeProxy(readOnlySpan, radius);
    }
    
    /// <summary>
    /// Make a proxy for use in GJK and related functions.
    /// </summary>
    public static unsafe ShapeProxy MakeProxy(Capsule capsule, float radius)
    {
        return MakeProxy(&capsule.Center1,2, radius);
    }
    
    /// <summary>
    /// Make a proxy for use in GJK and related functions.
    /// </summary>
    public static unsafe ShapeProxy MakeProxy(ChainSegment segment, float radius)
    {
        return MakeProxy(&segment.Segment.Point1, 2, radius);
    }
    
    /// <summary>
    /// Compute the upper bound on time before two shapes penetrate. Time is represented as
    /// a fraction between [0,tMax]. This uses a swept separating axis and may miss some intermediate,
    /// non-tunneling collisions. If you change the time interval, you should call this function
    /// again.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2TimeOfImpact")]
    public static extern TOIOutput TimeOfImpact(in TOIInput input);
    
    /// <summary>
    /// Set LengthUnitsPerMeter. By default, 1.0 corresponds to 1 meter.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2SetLengthUnitsPerMeter")]
    private static extern void SetLengthUnitsPerMeter(float lengthUnitsPerMeter);

    /// <summary>
    /// Get LengthUnitsPerMeter. By default, 1.0 corresponds to 1 meter.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2GetLengthUnitsPerMeter")]
    private static extern float GetLengthUnitsPerMeter();

    /// <summary>
    /// Length units per meter. By default 1.0 corresponds to 1 meter.
    /// </summary>
    public static float LengthUnitsPerMeter
    {
        get => GetLengthUnitsPerMeter();
        set => SetLengthUnitsPerMeter(value);
    }

    /// <summary>
    /// Set assert function
    /// </summary>
    /// <param name="assertFcn">Pointer to the assert function</param>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2SetAssertFcn")]
    public static extern void SetAssertFunction(AssertFunction assertFcn);
    
    /// <summary>
    /// Assert function. This is called when an assertion fails.
    /// </summary>
    public delegate int AssertFunction(string condition, string fileName, int lineNumber);
    
    internal static object? GetObjectAtPointer(nint ptr)
    {
        if (ptr == 0) return null;
        GCHandle handle = GCHandle.FromIntPtr(ptr);
        if (!handle.IsAllocated) return null;
        object? userData = handle.Target;
        return userData;
    }

    internal static void SetObjectAtPointer(ref nint ptr, object? value)
    {
        FreeHandle(ref ptr);
        if (value == null) return;
        GCHandle newHandle = GCHandle.Alloc(value);
        ptr = GCHandle.ToIntPtr(newHandle);
    }

    internal static void FreeHandle(ref nint ptr)
    {
        if (ptr != 0)
        {
            var hnd = GCHandle.FromIntPtr(ptr);
            if (hnd.IsAllocated)
                hnd.Free();
            ptr = 0;
        }
    }

    internal static object? GetObjectAtPointer<T>(Func<T, nint> getFunc, T param)
    {
        return GetObjectAtPointer(getFunc(param));
    }

    internal static void SetObjectAtPointer<T>(Func<T, nint> getFunc, Action<T, nint> setFunc, T param, object? value)
    {
        // dealloc previous user data
        nint userDataPtr = getFunc(param);
        GCHandle handle;
        if (userDataPtr != 0)
        {
            handle = GCHandle.FromIntPtr(userDataPtr);
            if (handle.IsAllocated) handle.Free();
        }
        if (value == null)
        {
            setFunc(param, 0);
            return;
        }
        handle = GCHandle.Alloc(value);
        userDataPtr = GCHandle.ToIntPtr(handle);
        setFunc(param, userDataPtr);
    }
    
    internal static int Assert(string condition, string fileName, int lineNumber)
    {
        Console.Error.WriteLine($"Box2D Assertion failed: {condition} in {fileName} at line {lineNumber}");
        return 0;
    }
}

 