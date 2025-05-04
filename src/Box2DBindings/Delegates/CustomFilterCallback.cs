using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Prototype for a contact filter callback.<br/>
/// This is called when a contact pair is considered for collision. This allows you to
/// perform custom logic to prevent collision between shapes. This is only called if
/// one of the two shapes has custom filtering enabled.<br/>
/// Notes:
/// <ul>
/// <li>this function must be thread-safe</li>
/// <li>this is only called if one of the two shapes has enabled custom filtering</li>
/// <li>this is called only for awake dynamic bodies</li>
/// </ul>
/// Return false if you want to disable the collision<br/>
/// <b>Warning: Do not attempt to modify the world inside this callback</b>
/// </summary>
/// <param name="shapeA">The first shape</param>
/// <param name="shapeB">The second shape</param>
/// <param name="context">The user context</param>
/// <returns>true if the collision should be enabled, false otherwise</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool CustomFilterCallback<in TContext>(Shape shapeA, Shape shapeB, TContext context) where TContext : class;

/// <summary>
/// Prototype for a contact filter callback.<br/>
/// This is called when a contact pair is considered for collision. This allows you to
/// perform custom logic to prevent collision between shapes. This is only called if
/// one of the two shapes has custom filtering enabled.<br/>
/// Notes:
/// <ul>
/// <li>this function must be thread-safe</li>
/// <li>this is only called if one of the two shapes has enabled custom filtering</li>
/// <li>this is called only for awake dynamic bodies</li>
/// </ul>
/// Return false if you want to disable the collision<br/>
/// <b>Warning: Do not attempt to modify the world inside this callback</b>
/// </summary>
/// <param name="shapeA">The first shape</param>
/// <param name="shapeB">The second shape</param>
/// <param name="context">The user context</param>
/// <returns>true if the collision should be enabled, false otherwise</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool CustomFilterRefCallback<TContext>(Shape shapeA, Shape shapeB, ref TContext context) where TContext : unmanaged;

/// <summary>
/// Prototype for a contact filter callback.<br/>
/// This is called when a contact pair is considered for collision. This allows you to
/// perform custom logic to prevent collision between shapes. This is only called if
/// one of the two shapes has custom filtering enabled.<br/>
/// Notes:
/// <ul>
/// <li>this function must be thread-safe</li>
/// <li>this is only called if one of the two shapes has enabled custom filtering</li>
/// <li>this is called only for awake dynamic bodies</li>
/// </ul>
/// Return false if you want to disable the collision<br/>
/// <b>Warning: Do not attempt to modify the world inside this callback</b>
/// </summary>
/// <param name="shapeA">The first shape</param>
/// <param name="shapeB">The second shape</param>
/// <returns>true if the collision should be enabled, false otherwise</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool CustomFilterCallback(Shape shapeA, Shape shapeB);

/// <summary>
/// Prototype for a contact filter callback.<br/>
/// This is called when a contact pair is considered for collision. This allows you to
/// perform custom logic to prevent collision between shapes. This is only called if
/// one of the two shapes has custom filtering enabled.<br/>
/// Notes:
/// <ul>
/// <li>this function must be thread-safe</li>
/// <li>this is only called if one of the two shapes has enabled custom filtering</li>
/// <li>this is called only for awake dynamic bodies</li>
/// </ul>
/// Return false if you want to disable the collision<br/>
/// <b>Warning: Do not attempt to modify the world inside this callback</b>
/// </summary>
/// <param name="shapeA">The first shape</param>
/// <param name="shapeB">The second shape</param>
/// <param name="context">The user context</param>
/// <returns>true if the collision should be enabled, false otherwise</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool CustomFilterNintCallback(Shape shapeA, Shape shapeB, nint context);
