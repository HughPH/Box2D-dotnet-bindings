using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Prototype for a pre-solve callback.<br/>
/// This is called after a contact is updated. This allows you to inspect a
/// contact before it goes to the solver. If you are careful, you can modify the
/// contact manifold (e.g. modify the normal).<br/>
/// Notes:
/// <ul>
/// <li>this function must be thread-safe</li>
/// <li>this is only called if the shape has enabled pre-solve events</li>
/// <li>this is called only for awake dynamic bodies</li>
/// <li>this is not called for sensors</li>
/// <li>the supplied manifold has impulse values from the previous step</li>
/// </ul>
/// Return false if you want to disable the contact this step<br/>
/// <b>Warning: Do not attempt to modify the world inside this callback</b>
/// </summary>
/// <param name="shapeA">The first Shape</param>
/// <param name="shapeB">The second Shape</param>
/// <param name="manifold">The manifold</param>
/// <param name="context">The context</param>
/// <returns>true if the contact should be enabled, false otherwise</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool PreSolveCallback<in TContext>(Shape shapeA, Shape shapeB, Manifold manifold, TContext context) where TContext : class;

/// <summary>
/// Prototype for a pre-solve callback.<br/>
/// This is called after a contact is updated. This allows you to inspect a
/// contact before it goes to the solver. If you are careful, you can modify the
/// contact manifold (e.g. modify the normal).<br/>
/// Notes:
/// <ul>
/// <li>this function must be thread-safe</li>
/// <li>this is only called if the shape has enabled pre-solve events</li>
/// <li>this is called only for awake dynamic bodies</li>
/// <li>this is not called for sensors</li>
/// <li>the supplied manifold has impulse values from the previous step</li>
/// </ul>
/// Return false if you want to disable the contact this step<br/>
/// <b>Warning: Do not attempt to modify the world inside this callback</b>
/// </summary>
/// <param name="shapeA">The first Shape</param>
/// <param name="shapeB">The second Shape</param>
/// <param name="manifold">The manifold</param>
/// <param name="context">The context</param>
/// <returns>true if the contact should be enabled, false otherwise</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool PreSolveRefCallback<TContext>(Shape shapeA, Shape shapeB, Manifold manifold, ref TContext context) where TContext : unmanaged;

/// <summary>
/// Prototype for a pre-solve callback.<br/>
/// This is called after a contact is updated. This allows you to inspect a
/// contact before it goes to the solver. If you are careful, you can modify the
/// contact manifold (e.g. modify the normal).<br/>
/// Notes:
/// <ul>
/// <li>this function must be thread-safe</li>
/// <li>this is only called if the shape has enabled pre-solve events</li>
/// <li>this is called only for awake dynamic bodies</li>
/// <li>this is not called for sensors</li>
/// <li>the supplied manifold has impulse values from the previous step</li>
/// </ul>
/// Return false if you want to disable the contact this step<br/>
/// <b>Warning: Do not attempt to modify the world inside this callback</b>
/// </summary>
/// <param name="shapeA">The first Shape</param>
/// <param name="shapeB">The second Shape</param>
/// <param name="manifold">The manifold</param>
/// <param name="context">The context</param>
/// <returns>true if the contact should be enabled, false otherwise</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool PreSolveNintCallback(Shape shapeA, Shape shapeB, nint manifold, nint context);

/// <summary>
/// Prototype for a pre-solve callback.<br/>
/// This is called after a contact is updated. This allows you to inspect a
/// contact before it goes to the solver. If you are careful, you can modify the
/// contact manifold (e.g. modify the normal).<br/>
/// Notes:
/// <ul>
/// <li>this function must be thread-safe</li>
/// <li>this is only called if the shape has enabled pre-solve events</li>
/// <li>this is called only for awake dynamic bodies</li>
/// <li>this is not called for sensors</li>
/// <li>the supplied manifold has impulse values from the previous step</li>
/// </ul>
/// Return false if you want to disable the contact this step<br/>
/// <b>Warning: Do not attempt to modify the world inside this callback</b>
/// </summary>
/// <param name="shapeA">The first Shape</param>
/// <param name="shapeB">The second Shape</param>
/// <param name="manifold">The manifold</param>
/// <returns>true if the contact should be enabled, false otherwise</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool PreSolveCallback(Shape shapeA, Shape shapeB, Manifold manifold);