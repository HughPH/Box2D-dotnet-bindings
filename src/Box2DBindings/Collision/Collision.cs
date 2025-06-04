using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Functions for computing contact manifolds.
/// </summary>
[PublicAPI]
public static class Collision
{
#if NET5_0_OR_GREATER
    /// <summary>
    /// Compute the contact manifold between two circles
    /// </summary>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Circle, Transform, in Circle, Transform, Manifold> CollideCircles;

    /// <summary>
    /// Compute the contact manifold between a capsule and circle
    /// </summary>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Capsule, Transform, in Circle, Transform, Manifold> CollideCapsuleAndCircle;

    /// <summary>
    /// Compute the contact manifold between an segment and a circle
    /// </summary>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Segment, Transform, in Circle, Transform, Manifold> CollideSegmentAndCircle;

    /// <summary>
    /// Compute the contact manifold between a polygon and a circle
    /// </summary>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Polygon, Transform, in Circle, Transform, Manifold> CollidePolygonAndCircle;

    /// <summary>
    /// Compute the contact manifold between a capsule and circle
    /// </summary>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Capsule, Transform, in Capsule, Transform, Manifold> CollideCapsules;

    /// <summary>
    /// Compute the contact manifold between an segment and a capsule
    /// </summary>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Segment, Transform, in Capsule, Transform, Manifold> CollideSegmentAndCapsule;

    /// <summary>
    /// Compute the contact manifold between a polygon and capsule
    /// </summary>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Polygon, Transform, in Capsule, Transform, Manifold> CollidePolygonAndCapsule;

    /// <summary>
    /// Compute the contact manifold between two polygons
    /// </summary>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Polygon, Transform, in Polygon, Transform, Manifold> CollidePolygons;

    /// <summary>
    /// Compute the contact manifold between a segment and a polygon
    /// </summary>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Segment, Transform, in Polygon, Transform, Manifold> CollideSegmentAndPolygon;

    /// <summary>
    /// Compute the contact manifold between a chain segment and a circle
    /// </summary>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in ChainSegment, Transform, in Circle, Transform, Manifold> CollideChainSegmentAndCircle;

    /// <summary>
    /// Compute the contact manifold between a chain segment and a capsule
    /// </summary>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in ChainSegment, Transform, in Capsule, Transform, ref SimplexCache, Manifold> CollideChainSegmentAndCapsule;

    /// <summary>
    /// Compute the contact manifold between a chain segment and a rounded polygon
    /// </summary>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in ChainSegment, in Transform, in Polygon, in Transform, ref SimplexCache, Manifold> CollideChainSegmentAndPolygon;

    static unsafe Collision()
    {
        nint lib = nativeLibrary;

        NativeLibrary.TryGetExport(lib, "b2CollideCircles", out var p0);
        NativeLibrary.TryGetExport(lib, "b2CollideCapsuleAndCircle", out var p1);
        NativeLibrary.TryGetExport(lib, "b2CollideSegmentAndCircle", out var p2);
        NativeLibrary.TryGetExport(lib, "b2CollidePolygonAndCircle", out var p3);
        NativeLibrary.TryGetExport(lib, "b2CollideCapsules", out var p4);
        NativeLibrary.TryGetExport(lib, "b2CollideSegmentAndCapsule", out var p5);
        NativeLibrary.TryGetExport(lib, "b2CollidePolygonAndCapsule", out var p6);
        NativeLibrary.TryGetExport(lib, "b2CollidePolygons", out var p7);
        NativeLibrary.TryGetExport(lib, "b2CollideSegmentAndPolygon", out var p8);
        NativeLibrary.TryGetExport(lib, "b2CollideChainSegmentAndCircle", out var p9);
        NativeLibrary.TryGetExport(lib, "b2CollideChainSegmentAndCapsule", out var p10);
        NativeLibrary.TryGetExport(lib, "b2CollideChainSegmentAndPolygon", out var p11);

        CollideCircles = (delegate* unmanaged[Cdecl]<in Circle, Transform, in Circle, Transform, Manifold>)p0;
        CollideCapsuleAndCircle = (delegate* unmanaged[Cdecl]<in Capsule, Transform, in Circle, Transform, Manifold>)p1;
        CollideSegmentAndCircle = (delegate* unmanaged[Cdecl]<in Segment, Transform, in Circle, Transform, Manifold>)p2;
        CollidePolygonAndCircle = (delegate* unmanaged[Cdecl]<in Polygon, Transform, in Circle, Transform, Manifold>)p3;
        CollideCapsules = (delegate* unmanaged[Cdecl]<in Capsule, Transform, in Capsule, Transform, Manifold>)p4;
        CollideSegmentAndCapsule = (delegate* unmanaged[Cdecl]<in Segment, Transform, in Capsule, Transform, Manifold>)p5;
        CollidePolygonAndCapsule = (delegate* unmanaged[Cdecl]<in Polygon, Transform, in Capsule, Transform, Manifold>)p6;
        CollidePolygons = (delegate* unmanaged[Cdecl]<in Polygon, Transform, in Polygon, Transform, Manifold>)p7;
        CollideSegmentAndPolygon = (delegate* unmanaged[Cdecl]<in Segment, Transform, in Polygon, Transform, Manifold>)p8;
        CollideChainSegmentAndCircle = (delegate* unmanaged[Cdecl]<in ChainSegment, Transform, in Circle, Transform, Manifold>)p9;
        CollideChainSegmentAndCapsule = (delegate* unmanaged[Cdecl]<in ChainSegment, Transform, in Capsule, Transform, ref SimplexCache, Manifold>)p10;
        CollideChainSegmentAndPolygon = (delegate* unmanaged[Cdecl]<in ChainSegment, in Transform, in Polygon, in Transform, ref SimplexCache, Manifold>)p11;
    }
#else
    /// <summary>
    /// Compute the contact manifold between two circles
    /// </summary>
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideCircles")]
    public static extern Manifold Collide(in Circle circleA, Transform xfA, in Circle circleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between a capsule and circle
    /// </summary>
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideCapsuleAndCircle")]
    public static extern Manifold Collide(in Capsule capsuleA, Transform xfA, in Circle circleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between an segment and a circle
    /// </summary>
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideSegmentAndCircle")]
    public static extern Manifold Collide(in Segment segmentA, Transform xfA, in Circle circleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between a polygon and a circle
    /// </summary>
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollidePolygonAndCircle")]
    public static extern Manifold Collide(in Polygon polygonA, Transform xfA, in Circle circleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between a capsule and circle
    /// </summary>
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideCapsules")]
    public static extern Manifold Collide(in Capsule capsuleA, Transform xfA, in Capsule capsuleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between an segment and a capsule
    /// </summary>
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideSegmentAndCapsule")]
    public static extern Manifold Collide(in Segment segmentA, Transform xfA, in Capsule capsuleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between a polygon and capsule
    /// </summary>
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollidePolygonAndCapsule")]
    public static extern Manifold Collide(in Polygon polygonA, Transform xfA, in Capsule capsuleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between two polygons
    /// </summary>
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollidePolygons")]
    public static extern Manifold Collide(in Polygon polygonA, Transform xfA, in Polygon polygonB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between a segment and a polygon
    /// </summary>
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideSegmentAndPolygon")]
    public static extern Manifold Collide(in Segment segmentA, Transform xfA, in Polygon polygonB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between a chain segment and a circle
    /// </summary>
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideChainSegmentAndCircle")]
    public static extern Manifold Collide(in ChainSegment segmentA, Transform xfA, in Circle circleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between a chain segment and a capsule
    /// </summary>
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideChainSegmentAndCapsule")]
    public static extern Manifold Collide(in ChainSegment segmentA, Transform xfA, in Capsule capsuleB, Transform xfB, ref SimplexCache cache);

    /// <summary>
    /// Compute the contact manifold between a chain segment and a rounded polygon
    /// </summary>
        [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideChainSegmentAndPolygon")]
    public static extern Manifold Collide(in ChainSegment segmentA, in Transform xfA, in Polygon polygonB, in Transform xfB, ref SimplexCache cache);
#endif
}
