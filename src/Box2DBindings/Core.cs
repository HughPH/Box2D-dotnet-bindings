global using static Box2D.Core;
using JetBrains.Annotations;
using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

[assembly:InternalsVisibleTo("UnitTests")]

namespace Box2D;

/// <summary>
/// Core Box2D functions that don't fit into other categories.
/// </summary>
[PublicAPI]
public static partial class Core
{
    #if NET5_0_OR_GREATER
    private static string libraryName = Path.Combine(
        Assembly.GetExecutingAssembly().Location,
        "runtimes",
        (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? "win" :
        RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? "linux" :
        RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? "osx" : "") + "-" +
        (RuntimeInformation.ProcessArchitecture switch
            {
                Architecture.X64 => "x64",
                Architecture.Arm64 => "arm64",
                Architecture.X86 => "x86",
                _ => ""
            }),
        "native",
        "libbox2d" + (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? ".dll" :
        RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? ".so" :
        RuntimeInformation.IsOSPlatform(OSPlatform.OSX) ? ".dylib" : ""));
    #else
    internal const string libraryName = "libbox2d";
    #endif

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
    /// Compute the distance between two line segments, clamping at the end points if needed.
    /// </summary>
    public static unsafe SegmentDistanceResult SegmentDistance(in Segment segmentA, in Segment segmentB) =>
        SegmentDistance(segmentA.Point1, segmentA.Point2, segmentB.Point1, segmentB.Point2);
    
    /// <summary>
    /// Compute the closest points between two shapes represented as point clouds.
    /// SimplexCache cache is input/output. On the first call set SimplexCache.Count to zero.
    /// </summary>
    public static unsafe DistanceOutput ShapeDistance(in DistanceInput input, ref SimplexCache cache, ReadOnlySpan<Simplex> simplexes)
    {
        fixed (Simplex* simplexPtr = simplexes)
            return b2ShapeDistance(input, ref cache, simplexPtr, simplexes.Length);
    }
    
    /// <summary>
    /// Make a proxy for use in GJK and related functions.
    /// </summary>
    public static unsafe ShapeProxy MakeProxy(ReadOnlySpan<Vec2> vertices, float radius)
    {
        fixed (Vec2* p = vertices)
            return b2MakeProxy(p, vertices.Length, radius);
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
        return b2MakeProxy(vertices, 1, 0.0f);
    }

    /// <summary>
    /// Make a proxy for use in GJK and related functions.
    /// </summary>
    public static unsafe ShapeProxy MakeProxy(Shape shape, float radius)
    {
        Vec2* vertices = shape.GetVertices(out int count);
        return b2MakeProxy(vertices, count, radius);
    }
    
    /// <summary>
    /// Make a proxy for use in GJK and related functions.
    /// </summary>
    public static unsafe ShapeProxy MakeProxy(Segment segment)
    {
        Vec2* vertices = &segment.Point1;
        return b2MakeProxy(vertices, 2, 0.0f);
    }
    
    /// <summary>
    /// Make a proxy for use in GJK and related functions.
    /// </summary>
    public static unsafe ShapeProxy MakeProxy(Circle circle)
    {
        Vec2* vertices = &circle.Center;
        return b2MakeProxy(vertices, 1, circle.Radius);
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
        return b2MakeProxy(&capsule.Center1,2, radius);
    }
    
    /// <summary>
    /// Make a proxy for use in GJK and related functions.
    /// </summary>
    public static unsafe ShapeProxy MakeProxy(ChainSegment segment, float radius)
    {
        return b2MakeProxy(&segment.Segment.Point1, 2, radius);
    }

    /// <summary>
    /// Length units per meter. By default 1.0 corresponds to 1 meter.
    /// </summary>
    public static unsafe float LengthUnitsPerMeter
    {
        get => b2GetLengthUnitsPerMeter();
        set => b2SetLengthUnitsPerMeter(value);
    }

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

    
#if NET5_0_OR_GREATER
    internal static unsafe object? GetObjectAtPointer<T>(delegate* unmanaged[Cdecl]<T, nint> getFunc, T param)
    {
        return GetObjectAtPointer(getFunc(param));
    }
    
    internal static unsafe void SetObjectAtPointer<T>(delegate* unmanaged[Cdecl]<T, nint> getFunc, delegate* unmanaged[Cdecl]<T, nint, void> setFunc, T param, object? value)
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

#else
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

#endif
    
    private static GCHandle assertFunctionHandle;
        
    /// <summary>
    /// Set assert function
    /// </summary>
    /// <param name="assertFcn">Pointer to the assert function</param>
    public static unsafe void SetAssertFunction(AssertFunction assertFcn)
    {
        if (assertFunctionHandle is { IsAllocated: true }) // free
        {
            assertFunctionHandle.Free();
            assertFunctionHandle = default;
        }
            
        assertFunctionHandle = GCHandle.Alloc(assertFcn);
        var ptr = Marshal.GetFunctionPointerForDelegate(assertFcn);
        b2SetAssertFcn(ptr);
    }
    
    internal static int Assert(string condition, string fileName, int lineNumber)
    {
        Console.Error.WriteLine($"Box2D Assertion failed: {condition} in {fileName} at line {lineNumber}");
        return 0;
    }
}

 