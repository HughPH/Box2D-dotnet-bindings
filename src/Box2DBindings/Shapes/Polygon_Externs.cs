using System.Runtime.InteropServices;

namespace Box2D
{
    partial struct Polygon
    {
#if NET5_0_OR_GREATER
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
            nint lib = Core.NativeLibHandle;
            NativeLibrary.TryGetExport(lib, "b2MakePolygon", out var p0);
            NativeLibrary.TryGetExport(lib, "b2MakeOffsetPolygon", out var p1);
            NativeLibrary.TryGetExport(lib, "b2MakeOffsetRoundedPolygon", out var p2);
            NativeLibrary.TryGetExport(lib, "b2MakeSquare", out var p3);
            NativeLibrary.TryGetExport(lib, "b2MakeBox", out var p4);
            NativeLibrary.TryGetExport(lib, "b2MakeRoundedBox", out var p5);
            NativeLibrary.TryGetExport(lib, "b2MakeOffsetBox", out var p6);
            NativeLibrary.TryGetExport(lib, "b2MakeOffsetRoundedBox", out var p7);
            NativeLibrary.TryGetExport(lib, "b2TransformPolygon", out var p8);
            NativeLibrary.TryGetExport(lib, "b2ComputePolygonMass", out var p9);
            NativeLibrary.TryGetExport(lib, "b2ComputePolygonAABB", out var p10);
            NativeLibrary.TryGetExport(lib, "b2PointInPolygon", out var p11);
            NativeLibrary.TryGetExport(lib, "b2RayCastPolygon", out var p12);
            NativeLibrary.TryGetExport(lib, "b2ShapeCastPolygon", out var p13);

            MakePolygon_ = (delegate* unmanaged[Cdecl]<in Hull, float, Polygon>)p0;
            MakeOffsetPolygon_ = (delegate* unmanaged[Cdecl]<in Hull, Vec2, Rotation, Polygon>)p1;
            MakeOffsetRoundedPolygon_ = (delegate* unmanaged[Cdecl]<in Hull, Vec2, Rotation, float, Polygon>)p2;
            MakeSquare_ = (delegate* unmanaged[Cdecl]<float, Polygon>)p3;
            MakeBox_ = (delegate* unmanaged[Cdecl]<float, float, Polygon>)p4;
            MakeRoundedBox_ = (delegate* unmanaged[Cdecl]<float, float, float, Polygon>)p5;
            MakeOffsetBox_ = (delegate* unmanaged[Cdecl]<float, float, Vec2, Rotation, Polygon>)p6;
            MakeOffsetRoundedBox_ = (delegate* unmanaged[Cdecl]<float, float, Vec2, Rotation, float, Polygon>)p7;
            TransformPolygon_ = (delegate* unmanaged[Cdecl]<Transform, in Polygon, Polygon>)p8;
            ComputePolygonMass_ = (delegate* unmanaged[Cdecl]<in Polygon, float, MassData>)p9;
            ComputePolygonAABB_ = (delegate* unmanaged[Cdecl]<in Polygon, Transform, AABB>)p10;
            PointInPolygon_ = (delegate* unmanaged[Cdecl]<Vec2, in Polygon, byte>)p11;
            RayCastPolygon_ = (delegate* unmanaged[Cdecl]<in RayCastInput, in Polygon, CastOutput>)p12;
            ShapeCastPolygon_ = (delegate* unmanaged[Cdecl]<in ShapeCastInput, in Polygon, CastOutput>)p13;
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
