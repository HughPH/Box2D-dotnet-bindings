using System;
using System.Runtime.InteropServices;

namespace Box2D
{
    partial struct Polygon
    {
#if NET9_0_OR_GREATER
        private static readonly unsafe delegate* unmanaged[Cdecl]<in Hull, float, Polygon> MakePolygon_;
        private static readonly unsafe delegate* unmanaged[Cdecl]<in Hull, Vec2, Rotation, Polygon> MakeOffsetPolygon_;
        private static readonly unsafe delegate* unmanaged[Cdecl]<in Hull, Vec2, Rotation, float, Polygon> MakeOffsetRoundedPolygon_;
        private static readonly unsafe delegate* unmanaged[Cdecl]<float, Polygon> MakeSquare_;
        private static readonly unsafe delegate* unmanaged[Cdecl]<float, float, Polygon> MakeBox_;
        private static readonly unsafe delegate* unmanaged[Cdecl]<float, float, float, Polygon> MakeRoundedBox_;
        private static readonly unsafe delegate* unmanaged[Cdecl]<float, float, Vec2, Rotation, Polygon> MakeOffsetBox_;
        private static readonly unsafe delegate* unmanaged[Cdecl]<float, float, Vec2, Rotation, float, Polygon> MakeOffsetRoundedBox_;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Transform, in Polygon, Polygon> TransformPolygon_;
        private static readonly unsafe delegate* unmanaged[Cdecl]<in Polygon, float, MassData> ComputePolygonMass_;
        private static readonly unsafe delegate* unmanaged[Cdecl]<in Polygon, Transform, AABB> ComputePolygonAABB_;
        private static readonly unsafe delegate* unmanaged[Cdecl]<Vec2, in Polygon, byte> PointInPolygon_;
        private static readonly unsafe delegate* unmanaged[Cdecl]<in RayCastInput, in Polygon, CastOutput> RayCastPolygon_;
        private static readonly unsafe delegate* unmanaged[Cdecl]<in ShapeCastInput, in Polygon, CastOutput> ShapeCastPolygon_;

        static unsafe Polygon()
        {
            nint p;
            NativeLibrary.TryGetExport(nativeLibrary, "b2MakePolygon", out p);
            MakePolygon_ = (delegate* unmanaged[Cdecl]<in Hull, float, Polygon>)p;
            NativeLibrary.TryGetExport(nativeLibrary, "b2MakeOffsetPolygon", out p);
            MakeOffsetPolygon_ = (delegate* unmanaged[Cdecl]<in Hull, Vec2, Rotation, Polygon>)p;
            NativeLibrary.TryGetExport(nativeLibrary, "b2MakeOffsetRoundedPolygon", out p);
            MakeOffsetRoundedPolygon_ = (delegate* unmanaged[Cdecl]<in Hull, Vec2, Rotation, float, Polygon>)p;
            NativeLibrary.TryGetExport(nativeLibrary, "b2MakeSquare", out p);
            MakeSquare_ = (delegate* unmanaged[Cdecl]<float, Polygon>)p;
            NativeLibrary.TryGetExport(nativeLibrary, "b2MakeBox", out p);
            MakeBox_ = (delegate* unmanaged[Cdecl]<float, float, Polygon>)p;
            NativeLibrary.TryGetExport(nativeLibrary, "b2MakeRoundedBox", out p);
            MakeRoundedBox_ = (delegate* unmanaged[Cdecl]<float, float, float, Polygon>)p;
            NativeLibrary.TryGetExport(nativeLibrary, "b2MakeOffsetBox", out p);
            MakeOffsetBox_ = (delegate* unmanaged[Cdecl]<float, float, Vec2, Rotation, Polygon>)p;
            NativeLibrary.TryGetExport(nativeLibrary, "b2MakeOffsetRoundedBox", out p);
            MakeOffsetRoundedBox_ = (delegate* unmanaged[Cdecl]<float, float, Vec2, Rotation, float, Polygon>)p;
            NativeLibrary.TryGetExport(nativeLibrary, "b2TransformPolygon", out p);
            TransformPolygon_ = (delegate* unmanaged[Cdecl]<Transform, in Polygon, Polygon>)p;
            NativeLibrary.TryGetExport(nativeLibrary, "b2ComputePolygonMass", out p);
            ComputePolygonMass_ = (delegate* unmanaged[Cdecl]<in Polygon, float, MassData>)p;
            NativeLibrary.TryGetExport(nativeLibrary, "b2ComputePolygonAABB", out p);
            ComputePolygonAABB_ = (delegate* unmanaged[Cdecl]<in Polygon, Transform, AABB>)p;
            NativeLibrary.TryGetExport(nativeLibrary, "b2PointInPolygon", out p);
            PointInPolygon_ = (delegate* unmanaged[Cdecl]<Vec2, in Polygon, byte>)p;
            NativeLibrary.TryGetExport(nativeLibrary, "b2RayCastPolygon", out p);
            RayCastPolygon_ = (delegate* unmanaged[Cdecl]<in RayCastInput, in Polygon, CastOutput>)p;
            NativeLibrary.TryGetExport(nativeLibrary, "b2ShapeCastPolygon", out p);
            ShapeCastPolygon_ = (delegate* unmanaged[Cdecl]<in ShapeCastInput, in Polygon, CastOutput>)p;
        }

#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MakePolygon")]
    private static extern Polygon MakePolygon_(in Hull hull, float radius);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MakeOffsetPolygon")]
    private static extern Polygon MakeOffsetPolygon_(in Hull hull, Vec2 position, Rotation rotation);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MakeOffsetRoundedPolygon")]
    private static extern Polygon MakeOffsetRoundedPolygon_(in Hull hull, Vec2 position, Rotation rotation, float radius);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MakeSquare")]
    private static extern Polygon MakeSquare_(float halfWidth);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MakeBox")]
    private static extern Polygon MakeBox_(float halfWidth, float halfHeight);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MakeRoundedBox")]
    private static extern Polygon MakeRoundedBox_(float halfWidth, float halfHeight, float radius);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MakeOffsetBox")]
    private static extern Polygon MakeOffsetBox_(float halfWidth, float halfHeight, Vec2 center, Rotation rotation);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2MakeOffsetRoundedBox")]
    private static extern Polygon MakeOffsetRoundedBox_(float halfWidth, float halfHeight, Vec2 center, Rotation rotation, float radius);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2TransformPolygon")]
    private static extern Polygon TransformPolygon_(Transform transform, in Polygon polygon);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ComputePolygonMass")]
    private static extern MassData ComputePolygonMass_(in Polygon shape, float density);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ComputePolygonAABB")]
    private static extern AABB ComputePolygonAABB_(in Polygon shape, Transform transform);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PointInPolygon")]
    private static extern byte PointInPolygon_(Vec2 point, in Polygon shape);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RayCastPolygon")]
    private static extern CastOutput RayCastPolygon_(in RayCastInput input, in Polygon shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ShapeCastPolygon")]
    private static extern CastOutput ShapeCastPolygon_(in ShapeCastInput input, in Polygon shape);
#endif
    }
}
