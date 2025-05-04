using Box2D.Character_Movement;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Used to collect collision planes for character movers.
/// </summary>
/// <param name="shapeId">The shape ID</param>
/// <param name="plane">The plane</param>
/// <param name="context">The user context</param>
/// <returns>True to continue gathering planes</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool PlaneResultCallback<in TContext>(Shape shapeId, in PlaneResult plane, TContext context) where TContext : class;

/// <summary>
/// Used to collect collision planes for character movers.
/// </summary>
/// <param name="shapeId">The shape ID</param>
/// <param name="plane">The plane</param>
/// <param name="context">The user context</param>
/// <returns>True to continue gathering planes</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool PlaneResultRefCallback<TContext>(Shape shapeId, in PlaneResult plane, ref TContext context) where TContext : unmanaged;

/// <summary>
/// Used to collect collision planes for character movers.
/// </summary>
/// <param name="shapeId">The shape ID</param>
/// <param name="plane">The plane</param>
/// <returns>True to continue gathering planes</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool PlaneResultCallback(Shape shapeId, in PlaneResult plane);

/// <summary>
/// Used to collect collision planes for character movers.
/// </summary>
/// <param name="shapeId">The shape ID</param>
/// <param name="plane">The plane</param>
/// <param name="context">The user context</param>
/// <returns>True to continue gathering planes</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool PlaneResultNintCallback(Shape shapeId, in PlaneResult plane, nint context);

