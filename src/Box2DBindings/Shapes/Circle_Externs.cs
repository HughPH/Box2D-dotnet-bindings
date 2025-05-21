using System.Runtime.InteropServices;

namespace Box2D
{
    public partial struct Circle
    {
        #if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<in Circle, float, MassData> b2ComputeCircleMass;
    private static readonly unsafe delegate* unmanaged[Cdecl]<in Circle, in Transform, AABB> b2ComputeCircleAABB;
    private static readonly unsafe delegate* unmanaged[Cdecl]<in Vec2, in Circle, byte> b2PointInCircle;
    private static readonly unsafe delegate* unmanaged[Cdecl]<in RayCastInput, in Circle, CastOutput> b2RayCastCircle;
    private static readonly unsafe delegate* unmanaged[Cdecl]<in ShapeCastInput, in Circle, CastOutput> b2ShapeCastCircle;

    static unsafe Circle()
    {
        nint lib = Core.NativeLibHandle;
        NativeLibrary.TryGetExport(lib, "b2ComputeCircleMass", out var p0);
        NativeLibrary.TryGetExport(lib, "b2ComputeCircleAABB", out var p1);
        NativeLibrary.TryGetExport(lib, "b2PointInCircle", out var p2);
        NativeLibrary.TryGetExport(lib, "b2RayCastCircle", out var p3);
        NativeLibrary.TryGetExport(lib, "b2ShapeCastCircle", out var p4);

        b2ComputeCircleMass = (delegate* unmanaged[Cdecl]<in Circle, float, MassData>)p0;
        b2ComputeCircleAABB = (delegate* unmanaged[Cdecl]<in Circle, in Transform, AABB>)p1;
        b2PointInCircle = (delegate* unmanaged[Cdecl]<in Vec2, in Circle, byte>)p2;
        b2RayCastCircle = (delegate* unmanaged[Cdecl]<in RayCastInput, in Circle, CastOutput>)p3;
        b2ShapeCastCircle = (delegate* unmanaged[Cdecl]<in ShapeCastInput, in Circle, CastOutput>)p4;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ComputeCircleMass")]
    private static extern MassData b2ComputeCircleMass(in Circle shape, float density);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ComputeCircleAABB")]
    private static extern AABB b2ComputeCircleAABB(in Circle shape, in Transform transform);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PointInCircle")]
    private static extern byte b2PointInCircle(in Vec2 point, in Circle shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RayCastCircle")]
    private static extern CastOutput b2RayCastCircle(in RayCastInput input, in Circle shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ShapeCastCircle")]
    private static extern CastOutput b2ShapeCastCircle(in ShapeCastInput input, in Circle shape);
#endif
    }
}
