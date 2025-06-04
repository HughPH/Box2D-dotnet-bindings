using System.Runtime.InteropServices;

namespace Box2D;

partial class World
{
    #if NET5_0_OR_GREATER
    private static readonly unsafe delegate* unmanaged[Cdecl]<WorldId, Vec2, Vec2, QueryFilter, RayResult> b2World_CastRayClosest;
    private static readonly unsafe delegate* unmanaged[Cdecl]<WorldId, Vec2, Vec2, QueryFilter, nint, nint, TreeStats> b2World_CastRay_;
    private static readonly unsafe delegate* unmanaged[Cdecl]<WorldId, in ShapeProxy, Vec2, QueryFilter, nint, nint, TreeStats> b2World_CastShape_;
    private static readonly unsafe delegate* unmanaged[Cdecl]<WorldId, in Capsule, Vec2, QueryFilter, float> b2World_CastMover;
    private static readonly unsafe delegate* unmanaged[Cdecl]<WorldId, in Capsule, QueryFilter, nint, nint, void> b2World_CollideMover_;
    private static readonly unsafe delegate* unmanaged[Cdecl]<WorldId, AABB, QueryFilter, nint, nint, TreeStats> b2World_OverlapAABB_;
    private static readonly unsafe delegate* unmanaged[Cdecl]<WorldId, in ShapeProxy, QueryFilter, nint, nint, TreeStats> b2World_OverlapShape_;
    
    private static unsafe TreeStats b2World_CastRay(WorldId worldId, Vec2 origin, Vec2 translation, QueryFilter filter, CastResultNintCallback fcn, nint context)
    {
        GCHandle handle = GCHandle.Alloc(fcn);
        try
        {
            return b2World_CastRay_(worldId, origin, translation, filter, Marshal.GetFunctionPointerForDelegate(fcn), context);
        }
        finally
        {
            handle.Free();
        }
    }

    private static unsafe TreeStats b2World_CastShape(WorldId worldId, in ShapeProxy proxy, Vec2 translation, QueryFilter filter, CastResultNintCallback fcn, nint context)
    {
        GCHandle handle = GCHandle.Alloc(fcn);
        try
        {
            return b2World_CastShape_(worldId, proxy, translation, filter, Marshal.GetFunctionPointerForDelegate(fcn), context);
        }
        finally
        {
            handle.Free();
        }
    }
    
    private static unsafe void b2World_CollideMover(WorldId worldId, in Capsule mover, QueryFilter filter, PlaneResultNintCallback fcn, nint context)
    {
        GCHandle handle = GCHandle.Alloc(fcn);
        try
        {
            b2World_CollideMover_(worldId, mover, filter, Marshal.GetFunctionPointerForDelegate(fcn), context);
        }
        finally
        {
            handle.Free();
        }
    }
    
    private static unsafe TreeStats b2World_OverlapAABB(WorldId worldId, AABB aabb, QueryFilter filter, OverlapResultNintCallback fcn, nint context)
    {
        GCHandle handle = GCHandle.Alloc(fcn);
        try
        {
            return b2World_OverlapAABB_(worldId, aabb, filter, Marshal.GetFunctionPointerForDelegate(fcn), context);
        }
        finally
        {
            handle.Free();
        }
    }

    private static unsafe TreeStats b2World_OverlapShape(WorldId worldId, in ShapeProxy proxy, QueryFilter filter, OverlapResultNintCallback fcn, nint context)
    {
        GCHandle handle = GCHandle.Alloc(fcn);
        try
        {
            return b2World_OverlapShape_(worldId, proxy, filter, Marshal.GetFunctionPointerForDelegate(fcn), context);
        }
        finally
        {
            handle.Free();
        }
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_CastRayClosest")]
    private static extern RayResult b2World_CastRayClosest(WorldId worldId, Vec2 origin, Vec2 translation, QueryFilter filter);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_CastRay")]
    private static extern TreeStats b2World_CastRay(WorldId worldId, Vec2 origin, Vec2 translation, QueryFilter filter, CastResultNintCallback fcn, nint context);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_CastShape")]
    private static extern TreeStats b2World_CastShape(WorldId worldId, in ShapeProxy proxy, Vec2 translation, QueryFilter filter, CastResultNintCallback fcn, nint context);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_CastMover")]
    private static extern float b2World_CastMover(WorldId worldId, in Capsule mover, Vec2 translation, QueryFilter filter);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_CollideMover")]
    private static extern void b2World_CollideMover(WorldId worldId, in Capsule mover, QueryFilter filter, PlaneResultNintCallback fcn, nint context);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_OverlapAABB")]
    private static extern TreeStats b2World_OverlapAABB(WorldId worldId, AABB aabb, QueryFilter filter, OverlapResultNintCallback fcn, nint context);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2World_OverlapShape")]
    private static extern TreeStats b2World_OverlapShape(WorldId worldId, in ShapeProxy proxy, QueryFilter filter, OverlapResultNintCallback fcn, nint context);
#endif
    
}