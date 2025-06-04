using System.Runtime.InteropServices;

namespace Box2D
{
    partial struct Capsule
    {
#if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<in Capsule, float, MassData> ComputeCapsuleMass;
    private static readonly unsafe delegate* unmanaged[Cdecl]<in Capsule, Transform, AABB> ComputeCapsuleAABB;
    private static readonly unsafe delegate* unmanaged[Cdecl]<Vec2, in Capsule, byte> PointInCapsule;
    private static readonly unsafe delegate* unmanaged[Cdecl]<in RayCastInput, in Capsule, CastOutput> RayCastCapsule;
    private static readonly unsafe delegate* unmanaged[Cdecl]<in ShapeCastInput, in Capsule, CastOutput> ShapeCastCapsule;

    static unsafe Capsule()
    {
        nint lib = nativeLibrary;
        NativeLibrary.TryGetExport(lib, "b2ComputeCapsuleMass", out var p0);
        NativeLibrary.TryGetExport(lib, "b2ComputeCapsuleAABB", out var p1);
        NativeLibrary.TryGetExport(lib, "b2PointInCapsule", out var p2);
        NativeLibrary.TryGetExport(lib, "b2RayCastCapsule", out var p3);
        NativeLibrary.TryGetExport(lib, "b2ShapeCastCapsule", out var p4);

        ComputeCapsuleMass = (delegate* unmanaged[Cdecl]<in Capsule, float, MassData>)p0;
        ComputeCapsuleAABB = (delegate* unmanaged[Cdecl]<in Capsule, Transform, AABB>)p1;
        PointInCapsule = (delegate* unmanaged[Cdecl]<Vec2, in Capsule, byte>)p2;
        RayCastCapsule = (delegate* unmanaged[Cdecl]<in RayCastInput, in Capsule, CastOutput>)p3;
        ShapeCastCapsule = (delegate* unmanaged[Cdecl]<in ShapeCastInput, in Capsule, CastOutput>)p4;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ComputeCapsuleMass")]
    private static extern MassData ComputeCapsuleMass(in Capsule shape, float density);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ComputeCapsuleAABB")]
    private static extern AABB ComputeCapsuleAABB(in Capsule shape, Transform transform);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2PointInCapsule")]
    private static extern byte PointInCapsule(Vec2 point, in Capsule shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2RayCastCapsule")]
    private static extern CastOutput RayCastCapsule(in RayCastInput input, in Capsule shape);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ShapeCastCapsule")]
    private static extern CastOutput ShapeCastCapsule(in ShapeCastInput input, in Capsule shape);
#endif
    }
}
