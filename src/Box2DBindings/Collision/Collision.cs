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
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga743fbd552b92de3730a41a26009179c2">b2CollideCircles</a></remarks>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Circle, Transform, in Circle, Transform, Manifold> CollideCircles;
    
    /// <summary>
    /// Compute the contact manifold between a capsule and circle
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga873487b2bd8e19b8ea543982853a61d6">b2CollideCapsuleAndCircle</a></remarks>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Capsule, Transform, in Circle, Transform, Manifold> CollideCapsuleAndCircle;
    
    /// <summary>
    /// Compute the contact manifold between an segment and a circle
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga0c9ae397f90adfa30827d0a349356db9">b2CollideSegmentAndCircle</a></remarks>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Segment, Transform, in Circle, Transform, Manifold> CollideSegmentAndCircle;
    
    /// <summary>
    /// Compute the contact manifold between a polygon and a circle
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga38723c2436af515b153da03aee701af6">b2CollidePolygonAndCircle</a></remarks>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Polygon, Transform, in Circle, Transform, Manifold> CollidePolygonAndCircle;
    
    /// <summary>
    /// Compute the contact manifold between a capsule and circle
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga2a30a6693c0a26f88111697385d66031">b2CollideCapsules</a></remarks>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Capsule, Transform, in Capsule, Transform, Manifold> CollideCapsules;
    
    /// <summary>
    /// Compute the contact manifold between an segment and a capsule
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga5b653e7b6cfffce8afe5b7efc596ece5">b2CollideSegmentAndCapsule</a></remarks>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Segment, Transform, in Capsule, Transform, Manifold> CollideSegmentAndCapsule;
    
    /// <summary>
    /// Compute the contact manifold between a polygon and capsule
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga607e05c7029dc317455cc14f9cf8e188">b2CollidePolygonAndCapsule</a></remarks>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Polygon, Transform, in Capsule, Transform, Manifold> CollidePolygonAndCapsule;
    
    /// <summary>
    /// Compute the contact manifold between two polygons
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga8c4680c93621128941b9bcb13eb5bc00">b2CollidePolygons</a></remarks>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Polygon, Transform, in Polygon, Transform, Manifold> CollidePolygons;
    
    /// <summary>
    /// Compute the contact manifold between a segment and a polygon
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga2e09e7422a016285105505c587328380">b2CollideSegmentAndPolygon</a></remarks>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in Segment, Transform, in Polygon, Transform, Manifold> CollideSegmentAndPolygon;
    
    /// <summary>
    /// Compute the contact manifold between a chain segment and a circle
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga2a8f7fd661289943e65586af11f8b30e">b2CollideChainSegmentAndCircle</a></remarks>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in ChainSegment, Transform, in Circle, Transform, Manifold> CollideChainSegmentAndCircle;
    
    /// <summary>
    /// Compute the contact manifold between a chain segment and a capsule
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#gad860aec3081bdf4198c4ad022012c3b6">b2CollideChainSegmentAndCapsule</a></remarks>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in ChainSegment, Transform, in Capsule, Transform, ref SimplexCache, Manifold> CollideChainSegmentAndCapsule;
    
    /// <summary>
    /// Compute the contact manifold between a chain segment and a rounded polygon
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga1e3bb346f59682dbe15a54518e794921">b2CollideChainSegmentAndPolygon</a></remarks>
    public static readonly unsafe delegate* unmanaged[Cdecl]<in ChainSegment, in Transform, in Polygon, in Transform, ref SimplexCache, Manifold> CollideChainSegmentAndPolygon;

    static unsafe Collision()
    {
        nint lib = NativeLibrary.Load(libraryName);

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
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga743fbd552b92de3730a41a26009179c2">b2CollideCircles</a></remarks>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideCircles")]
    public static extern Manifold Collide(in Circle circleA, Transform xfA, in Circle circleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between a capsule and circle
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga873487b2bd8e19b8ea543982853a61d6">b2CollideCapsuleAndCircle</a></remarks>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideCapsuleAndCircle")]
    public static extern Manifold Collide(in Capsule capsuleA, Transform xfA, in Circle circleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between an segment and a circle
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga0c9ae397f90adfa30827d0a349356db9">b2CollideSegmentAndCircle</a></remarks>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideSegmentAndCircle")]
    public static extern Manifold Collide(in Segment segmentA, Transform xfA, in Circle circleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between a polygon and a circle
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga38723c2436af515b153da03aee701af6">b2CollidePolygonAndCircle</a></remarks>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollidePolygonAndCircle")]
    public static extern Manifold Collide(in Polygon polygonA, Transform xfA, in Circle circleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between a capsule and circle
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga2a30a6693c0a26f88111697385d66031">b2CollideCapsules</a></remarks>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideCapsules")]
    public static extern Manifold Collide(in Capsule capsuleA, Transform xfA, in Capsule capsuleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between an segment and a capsule
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga5b653e7b6cfffce8afe5b7efc596ece5">b2CollideSegmentAndCapsule</a></remarks>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideSegmentAndCapsule")]
    public static extern Manifold Collide(in Segment segmentA, Transform xfA, in Capsule capsuleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between a polygon and capsule
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga607e05c7029dc317455cc14f9cf8e188">b2CollidePolygonAndCapsule</a></remarks>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollidePolygonAndCapsule")]
    public static extern Manifold Collide(in Polygon polygonA, Transform xfA, in Capsule capsuleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between two polygons
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga8c4680c93621128941b9bcb13eb5bc00">b2CollidePolygons</a></remarks>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollidePolygons")]
    public static extern Manifold Collide(in Polygon polygonA, Transform xfA, in Polygon polygonB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between a segment and a polygon
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga2e09e7422a016285105505c587328380">b2CollideSegmentAndPolygon</a></remarks>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideSegmentAndPolygon")]
    public static extern Manifold Collide(in Segment segmentA, Transform xfA, in Polygon polygonB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between a chain segment and a circle
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga2a8f7fd661289943e65586af11f8b30e">b2CollideChainSegmentAndCircle</a></remarks>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideChainSegmentAndCircle")]
    public static extern Manifold Collide(in ChainSegment segmentA, Transform xfA, in Circle circleB, Transform xfB);

    /// <summary>
    /// Compute the contact manifold between a chain segment and a capsule
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#gad860aec3081bdf4198c4ad022012c3b6">b2CollideChainSegmentAndCapsule</a></remarks>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideChainSegmentAndCapsule")]
    public static extern Manifold Collide(in ChainSegment segmentA, Transform xfA, in Capsule capsuleB, Transform xfB, ref SimplexCache cache);

    /// <summary>
    /// Compute the contact manifold between a chain segment and a rounded polygon
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#ga1e3bb346f59682dbe15a54518e794921">b2CollideChainSegmentAndPolygon</a></remarks>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2CollideChainSegmentAndPolygon")]
    public static extern Manifold Collide(in ChainSegment segmentA, in Transform xfA, in Polygon polygonB, in Transform xfB, ref SimplexCache cache);
#endif
}
