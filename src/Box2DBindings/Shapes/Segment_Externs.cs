using System.Runtime.InteropServices;

namespace Box2D
{
    partial struct Segment
    {
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<in Segment, Transform, AABB> b2ComputeSegmentAABB;
    private static readonly unsafe delegate* unmanaged[Cdecl]<in RayCastInput, in Segment, byte, CastOutput> b2RayCastSegment;
    private static readonly unsafe delegate* unmanaged[Cdecl]<in ShapeCastInput, in Segment, CastOutput> b2ShapeCastSegment;
    private static readonly unsafe delegate* unmanaged[Cdecl]<Vec2, Vec2, Vec2, Vec2, SegmentDistanceResult> b2SegmentDistance;

    static unsafe Segment()
    {
        nint lib = nativeLibrary;
        NativeLibrary.TryGetExport(lib, "b2ComputeSegmentAABB", out var p0);
        NativeLibrary.TryGetExport(lib, "b2RayCastSegment", out var p1);
        NativeLibrary.TryGetExport(lib, "b2ShapeCastSegment", out var p2);
        NativeLibrary.TryGetExport(lib, "b2SegmentDistance", out var p3);

        b2ComputeSegmentAABB = (delegate* unmanaged[Cdecl]<in Segment, Transform, AABB>)p0;
        b2RayCastSegment = (delegate* unmanaged[Cdecl]<in RayCastInput, in Segment, byte, CastOutput>)p1;
        b2ShapeCastSegment = (delegate* unmanaged[Cdecl]<in ShapeCastInput, in Segment, CastOutput>)p2;
        b2SegmentDistance = (delegate* unmanaged[Cdecl]<Vec2, Vec2, Vec2, Vec2, SegmentDistanceResult>)p3;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ComputeSegmentAABB")]
    private static extern AABB b2ComputeSegmentAABB(in Segment shape, Transform transform);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RayCastSegment")]
    private static extern CastOutput b2RayCastSegment(in RayCastInput input, in Segment shape, byte oneSided);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ShapeCastSegment")]
    private static extern CastOutput b2ShapeCastSegment(in ShapeCastInput input, in Segment shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2SegmentDistance")]
    private static extern SegmentDistanceResult b2SegmentDistance(Vec2 p1, Vec2 q1, Vec2 p2, Vec2 q2);
#endif 
    }
}
