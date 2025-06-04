using Box2D.Character_Movement;
using System.Runtime.InteropServices;

namespace Box2D;

public partial class World
{
#region CallbackThunks
    private static float CastResultThunk(Shape shape, Vec2 point, Vec2 normal, float fraction, nint ctx)
    {
        var callback = (CastResultCallback)GCHandle.FromIntPtr(ctx).Target!;
        return callback(shape, point, normal, fraction);
    }
    
    private static unsafe float CastResultThunk<TContext>(Shape shape, Vec2 point, Vec2 normal, float fraction, nint ctx) where TContext : class
    {
        var contextBuffer = (nint*)ctx;
        TContext contextObj = (TContext)GCHandle.FromIntPtr(contextBuffer[0]).Target!;
        var callback = (CastResultCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(shape, point, normal, fraction, contextObj);
    }

    private static unsafe float CastResultRefThunk<TContext>(Shape shape, Vec2 point, Vec2 normal, float fraction, nint ctx) where TContext : unmanaged
    {
        var contextBuffer = (nint*)ctx;
        ref TContext contextObj = ref *(TContext*)contextBuffer[0];
        var callback = (CastResultRefCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(shape, point, normal, fraction, ref contextObj);
    }

    private static bool PlaneResultThunk(Shape shape, in PlaneResult plane, nint context)
    {
        var callback = (PlaneResultCallback)GCHandle.FromIntPtr(context).Target!;
        return callback(shape, plane);
    }
    
    private static unsafe bool PlaneResultThunk<TContext>(Shape shape, in PlaneResult plane, nint context) where TContext : class
    {
        var contextBuffer = (nint*)context;
        TContext contextObj = (TContext)GCHandle.FromIntPtr(contextBuffer[0]).Target!;
        var callback = (PlaneResultCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(shape, plane, contextObj);
    }
    
    private static unsafe bool PlaneResultRefThunk<TContext>(Shape shape, in PlaneResult plane, nint context) where TContext : unmanaged
    {
        var contextBuffer = (nint*)context;
        ref TContext contextObj = ref *(TContext*)contextBuffer[0];
        var callback = (PlaneResultRefCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(shape, plane, ref contextObj);
    }

    private static unsafe bool OverlapResultThunk<TContext>(Shape shape, nint ptr) where TContext : class
    {
        var contextBuffer = (nint*)ptr;
        TContext context = (TContext)GCHandle.FromIntPtr(contextBuffer[0]).Target!;
        var callback = (OverlapResultCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(shape, context);
    }
    
    private static unsafe bool OverlapResultRefThunk<TContext>(Shape shape, nint ptr) where TContext : unmanaged
    {
        var contextBuffer = (nint*)ptr;
        ref TContext context = ref *(TContext*)contextBuffer[0];
        var callback = (OverlapResultRefCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(shape, ref context);
    }

    private static bool OverlapResultThunk(Shape shape, nint ptr)
    {
        var callback = (OverlapResultCallback)GCHandle.FromIntPtr(ptr).Target!;
        return callback(shape);
    }

#endregion

#region RayCastClosest

    /// <summary>
    /// Cast a ray into the world to collect the closest hit. This is a convenience function. Ignores initial overlap.
    /// </summary>
    /// <param name="origin">The start point of the ray</param>
    /// <param name="translation">The translation of the ray from the start point to the end point</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <returns>The ray result</returns>
    /// <remarks>This is less general than b2World_CastRay() and does not allow for custom filtering</remarks>
    public unsafe RayResult CastRayClosest(Vec2 origin, Vec2 translation, QueryFilter filter) => b2World_CastRayClosest(id, origin, translation, filter);

#endregion

#region CastRay

    /// <summary>
    /// Cast a ray into the world to collect shapes in the path of the ray.
    /// </summary>
    /// <param name="origin">The start point of the ray</param>
    /// <param name="translation">The translation of the ray from the start point to the end point</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// <param name="context">A user context that is passed along to the callback function</param>
    /// <returns>Traversal performance counters</returns>
    /// <remarks>Your callback function controls whether you get the closest point, any point, or n-points. The ray-cast ignores shapes that contain the starting point. The callback function may receive shapes in any order</remarks>
    public unsafe TreeStats CastRay<TContext>(Vec2 origin, Vec2 translation, QueryFilter filter, CastResultCallback<TContext> callback, TContext context) where TContext : class
    {
        nint* contextBuffer = stackalloc nint[2];
        contextBuffer[0] = GCHandle.ToIntPtr(GCHandle.Alloc(context));
        contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            return b2World_CastRay(id, origin, translation, filter, CastResultThunk<TContext>, (nint)contextBuffer);
        }
        finally
        {
            GCHandle.FromIntPtr(contextBuffer[0]).Free();
            GCHandle.FromIntPtr(contextBuffer[1]).Free();
        }
    }

    /// <summary>
    /// Cast a ray into the world to collect shapes in the path of the ray.
    /// </summary>
    /// <param name="origin">The start point of the ray</param>
    /// <param name="translation">The translation of the ray from the start point to the end point</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// <param name="context">A user context that is passed along to the callback function</param>
    /// <returns>Traversal performance counters</returns>
    /// <remarks>Your callback function controls whether you get the closest point, any point, or n-points. The ray-cast ignores shapes that contain the starting point. The callback function may receive shapes in any order</remarks>
    public unsafe TreeStats CastRay<TContext>(Vec2 origin, Vec2 translation, QueryFilter filter, CastResultRefCallback<TContext> callback, ref TContext context) where TContext : unmanaged
    {
        fixed (TContext* contextPtr = &context)
        {
            nint* contextBuffer = stackalloc nint[2];
            contextBuffer[0] = (nint)contextPtr;
            contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
            try
            {
                return b2World_CastRay(id, origin, translation, filter, CastResultRefThunk<TContext>, (nint)contextBuffer);
            }
            finally
            {
                GCHandle.FromIntPtr(contextBuffer[1]).Free();
            }
        }
    }
    
    /// <summary>
    /// Cast a ray into the world to collect shapes in the path of the ray.
    /// </summary>
    /// <param name="origin">The start point of the ray</param>
    /// <param name="translation">The translation of the ray from the start point to the end point</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// <returns>Traversal performance counters</returns>
    /// <remarks>Your callback function controls whether you get the closest point, any point, or n-points.<br/>
    /// <i>Note: The callback function may receive shapes in any order</i></remarks>
    public TreeStats CastRay(Vec2 origin, Vec2 translation, QueryFilter filter, CastResultCallback callback)
    {
        nint contextBuffer = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            return b2World_CastRay(id, origin, translation, filter, CastResultThunk, contextBuffer);
        }
        finally
        {
            GCHandle.FromIntPtr(contextBuffer).Free();
        }
    }

    /// <summary>
    /// Cast a ray into the world to collect shapes in the path of the ray.
    /// </summary>
    /// <param name="origin">The start point of the ray</param>
    /// <param name="translation">The translation of the ray from the start point to the end point</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// /// <param name="context">A user context that is passed along to the callback function</param>
    /// <returns>Traversal performance counters</returns>
    /// <remarks>Your callback function controls whether you get the closest point, any point, or n-points.<br/>
    /// <i>Note: The callback function may receive shapes in any order</i></remarks>
    public unsafe TreeStats CastRay(Vec2 origin, Vec2 translation, QueryFilter filter, CastResultNintCallback callback, nint context)
    {
        return b2World_CastRay(id, origin, translation, filter, callback, context);
    }

#endregion

#region CastShape

    /// <summary>
    /// Cast a shape through the world. Similar to a cast ray except that a shape is cast instead of a point.
    /// </summary>
    /// <param name="proxy">The shape proxy</param>
    /// <param name="translation">The translation of the shape from the start point to the end point</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// <param name="context">A user context that is passed along to the callback function</param>
    public unsafe TreeStats CastShape<TContext>(in ShapeProxy proxy, Vec2 translation, QueryFilter filter, CastResultCallback<TContext> callback, TContext context) where TContext : class
    {
        nint* contextBuffer = stackalloc nint[2];
        contextBuffer[0] = GCHandle.ToIntPtr(GCHandle.Alloc(context));
        contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            return b2World_CastShape(id, in proxy, translation, filter, CastResultThunk<TContext>, (nint)contextBuffer);
        }
        finally
        {
            GCHandle.FromIntPtr(contextBuffer[0]).Free();
            GCHandle.FromIntPtr(contextBuffer[1]).Free();
        }
    }

    /// <summary>
    /// Cast a shape through the world. Similar to a cast ray except that a shape is cast instead of a point.
    /// </summary>
    /// <param name="proxy">The shape proxy</param>
    /// <param name="translation">The translation of the shape from the start point to the end point</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// <param name="context">A user context that is passed along to the callback function</param>
    public unsafe TreeStats CastShape<TContext>(in ShapeProxy proxy, Vec2 translation, QueryFilter filter, CastResultRefCallback<TContext> callback, ref TContext context) where TContext : unmanaged
    {
        fixed (TContext* contextPtr = &context)
        {
            nint* contextBuffer = stackalloc nint[2];
            contextBuffer[0] = (nint)contextPtr;
            contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
            try
            {
                return b2World_CastShape(id, in proxy, translation, filter, CastResultRefThunk<TContext>, (nint)contextBuffer);
            }
            finally
            {
                GCHandle.FromIntPtr(contextBuffer[1]).Free();
            }
        }
    }
    
    /// <summary>
    /// Cast a shape through the world. Similar to a cast ray except that a shape is cast instead of a point.
    /// </summary>
    /// <param name="proxy">The shape proxy</param>
    /// <param name="translation">The translation of the shape from the start point to the end point</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    public TreeStats CastShape(in ShapeProxy proxy, Vec2 translation, QueryFilter filter, CastResultCallback callback)
    {
        nint contextBuffer = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            return b2World_CastShape(id, in proxy, translation, filter, CastResultThunk, contextBuffer);
        }
        finally
        {
            GCHandle.FromIntPtr(contextBuffer).Free();
        }
    }

    /// <summary>
    /// Cast a shape through the world. Similar to a cast ray except that a shape is cast instead of a point.
    /// </summary>
    /// <param name="proxy">The shape proxy</param>
    /// <param name="translation">The translation of the shape from the start point to the end point</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// <param name="context">A user context that is passed along to the callback function</param>
    public TreeStats CastShape(in ShapeProxy proxy, Vec2 translation, QueryFilter filter, CastResultNintCallback callback, nint context)
    {
        return b2World_CastShape(id, in proxy, translation, filter, callback, context);
    }

#endregion

#region CastMover

    /// <summary>
    /// Cast a capsule mover through the world. This is a special shape cast that handles sliding along other shapes while reducing clipping.
    /// </summary>
    /// <param name="mover">The capsule mover</param>
    /// <param name="translation">The translation of the capsule from the start point to the end point</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <returns>The fraction of the translation that was completed before a collision occurred</returns>
    public unsafe float CastMover(in Capsule mover, Vec2 translation, QueryFilter filter) =>
        b2World_CastMover(id, in mover, translation, filter);

#endregion

#region CollideMover

    /// <summary>
    /// Collide a capsule mover with the world, gathering collision planes that can be fed to b2SolvePlanes. Useful for kinematic character movement.
    /// </summary>
    /// <param name="mover">The capsule mover</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// <param name="context">A user context that is passed along to the callback function</param>
    public unsafe void CollideMover<TContext>(in Capsule mover, QueryFilter filter, PlaneResultCallback<TContext> callback, TContext context) where TContext : class
    {
        nint* contextBuffer = stackalloc nint[2];
        contextBuffer[0] = GCHandle.ToIntPtr(GCHandle.Alloc(context));
        contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        
        try
        {
            b2World_CollideMover(id, in mover, filter, PlaneResultThunk<TContext>, (nint)contextBuffer);
        }
        finally
        {
            GCHandle.FromIntPtr(contextBuffer[0]).Free();
            GCHandle.FromIntPtr(contextBuffer[1]).Free();
        }
    }
    
    /// <summary>
    /// Collide a capsule mover with the world, gathering collision planes that can be fed to b2SolvePlanes. Useful for kinematic character movement.
    /// </summary>
    /// <param name="mover">The capsule mover</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// <param name="context">A user context that is passed along to the callback function</param>
    public unsafe void CollideMover<TContext>(in Capsule mover, QueryFilter filter, PlaneResultRefCallback<TContext> callback, ref TContext context) where TContext : unmanaged
    {
        fixed (TContext* contextPtr = &context)
        {
            nint* contextBuffer = stackalloc nint[2];
            contextBuffer[0] = (nint)contextPtr;
            contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
            try
            {
                b2World_CollideMover(id, in mover, filter, PlaneResultRefThunk<TContext>, (nint)contextBuffer);
            }
            finally
            {
                GCHandle.FromIntPtr(contextBuffer[1]).Free();
            }
        }
    }

    /// <summary>
    /// Collide a capsule mover with the world, gathering collision planes that can be fed to b2SolvePlanes. Useful for kinematic character movement.
    /// </summary>
    /// <param name="mover">The capsule mover</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    public void CollideMover(in Capsule mover, QueryFilter filter, PlaneResultCallback callback)
    {
        nint contextBuffer = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            b2World_CollideMover(id, in mover, filter, PlaneResultThunk, contextBuffer);
        }
        finally
        {
            GCHandle.FromIntPtr(contextBuffer).Free();
        }
    }

    /// <summary>
    /// Collide a capsule mover with the world, gathering collision planes that can be fed to b2SolvePlanes. Useful for kinematic character movement.
    /// </summary>
    /// <param name="mover">The capsule mover</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// <param name="context">A user context that is passed along to the callback function</param>
    public void CollideMover(in Capsule mover, QueryFilter filter, PlaneResultNintCallback callback, nint context) =>
        b2World_CollideMover(id, in mover, filter, callback, context);

#endregion

#region OverlapAABB

    /// <summary>
    /// Overlap test for all shapes that *potentially* overlap the provided AABB
    /// </summary>
    /// <param name="aabb">The AABB</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// <param name="context">The context</param>
    public unsafe TreeStats OverlapAABB<TContext>(AABB aabb, QueryFilter filter, OverlapResultCallback<TContext> callback, TContext context) where TContext : class
    {
        nint* contextBuffer = stackalloc nint[2];
        contextBuffer[0] = GCHandle.ToIntPtr(GCHandle.Alloc(context));
        contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        
        try
        {
            return b2World_OverlapAABB(id, aabb, filter, OverlapResultThunk<TContext>, (nint)contextBuffer);
        }
        finally
        {
            GCHandle.FromIntPtr(contextBuffer[0]).Free();
            GCHandle.FromIntPtr(contextBuffer[1]).Free();
        }
    }

    /// <summary>
    /// Overlap test for all shapes that *potentially* overlap the provided AABB
    /// </summary>
    /// <param name="aabb">The AABB</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// <param name="context">The context</param>
    public unsafe TreeStats OverlapAABB<TContext>(AABB aabb, QueryFilter filter, OverlapResultRefCallback<TContext> callback, ref TContext context) where TContext : unmanaged
    {
        fixed (TContext* contextPtr = &context)
        {
            nint* contextBuffer = stackalloc nint[2];
            contextBuffer[0] = (nint)contextPtr;
            contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
            try
            {
                return b2World_OverlapAABB(id, aabb, filter, OverlapResultRefThunk<TContext>, (nint)contextBuffer);
            }
            finally
            {
                GCHandle.FromIntPtr(contextBuffer[1]).Free();
            }
        }
    }
    
    /// <summary>
    /// Overlap test for all shapes that *potentially* overlap the provided AABB
    /// </summary>
    /// <param name="aabb">The AABB</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    public TreeStats OverlapAABB(AABB aabb, QueryFilter filter, ref OverlapResultCallback callback)
    {
        nint contextBuffer = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        
        try
        {
            return b2World_OverlapAABB(id, aabb, filter, OverlapResultThunk, contextBuffer);
        }
        finally
        {
            GCHandle.FromIntPtr(contextBuffer).Free();
        }
    }

    /// <summary>
    /// Overlap test for all shapes that *potentially* overlap the provided AABB
    /// </summary>
    /// <param name="aabb">The AABB</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// <param name="context">The context</param>
    public TreeStats OverlapAABB(AABB aabb, QueryFilter filter, OverlapResultNintCallback callback, nint context)
    {
        return b2World_OverlapAABB(id, aabb, filter, callback, context);
    }

#endregion

#region OverlapShape

    /// <summary>
    /// Overlap test for all shapes that overlap the provided shape proxy
    /// </summary>
    /// <param name="proxy">The shape proxy</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// <param name="context">The context</param>
    public unsafe TreeStats OverlapShape<TContext>(in ShapeProxy proxy, QueryFilter filter, OverlapResultCallback<TContext> callback, TContext context) where TContext : class
    {
        nint* contextBuffer = stackalloc nint[2];
        contextBuffer[0] = GCHandle.ToIntPtr(GCHandle.Alloc(context));
        contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            return b2World_OverlapShape(id, in proxy, filter, OverlapResultThunk<TContext>, (nint)contextBuffer);
        }
        finally
        {
            GCHandle.FromIntPtr(contextBuffer[0]).Free();
            GCHandle.FromIntPtr(contextBuffer[1]).Free();
        }
    }
    
    /// <summary>
    /// Overlap test for all shapes that overlap the provided shape proxy
    /// </summary>
    /// <param name="proxy">The shape proxy</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// <param name="context">The context</param>
    public unsafe TreeStats OverlapShape<TContext>(in ShapeProxy proxy, QueryFilter filter, OverlapResultRefCallback<TContext> callback, ref TContext context) where TContext : unmanaged
    {
        fixed (TContext* contextPtr = &context)
        {
            nint* contextBuffer = stackalloc nint[2];
            contextBuffer[0] = (nint)contextPtr;
            contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
            try
            {
                return b2World_OverlapShape(id, in proxy, filter, OverlapResultRefThunk<TContext>, (nint)contextBuffer);
            }
            finally
            {
                GCHandle.FromIntPtr(contextBuffer[1]).Free();
            }
        }
    }

    /// <summary>
    /// Overlap test for all shapes that overlap the provided shape proxy
    /// </summary>
    /// <param name="proxy">The shape proxy</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    /// <param name="context">The context</param>
    public TreeStats OverlapShape<TContext>(in ShapeProxy proxy, QueryFilter filter, OverlapResultNintCallback callback, nint context)
    {
        return b2World_OverlapShape(id, in proxy, filter, callback, context);
    }

    /// <summary>
    /// Overlap test for all shapes that overlap the provided shape proxy
    /// </summary>
    /// <param name="proxy">The shape proxy</param>
    /// <param name="filter">Contains bit flags to filter unwanted shapes from the results</param>
    /// <param name="callback">A user implemented callback function</param>
    public TreeStats OverlapShape(in ShapeProxy proxy, QueryFilter filter, OverlapResultCallback callback)
    {
        nint contextBuffer = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            return b2World_OverlapShape(id, in proxy, filter, OverlapResultThunk, contextBuffer);
        }
        finally
        {
            GCHandle.FromIntPtr(contextBuffer).Free();
        }
    }

#endregion
}
