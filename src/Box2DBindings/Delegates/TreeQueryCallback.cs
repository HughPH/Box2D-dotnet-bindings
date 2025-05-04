using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// This function receives proxies found in the AABB query.
/// </summary>
/// <returns>True if the query should continue</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool TreeQueryCallback<in TContext>(int proxyId, uint64_t userData, TContext context) where TContext : class;

/// <summary>
/// This function receives proxies found in the AABB query.
/// </summary>
/// <returns>True if the query should continue</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool TreeQueryRefCallback<TContext>(int proxyId, uint64_t userData, ref TContext context) where TContext : unmanaged;

/// <summary>
/// This function receives proxies found in the AABB query.
/// </summary>
/// <returns>True if the query should continue</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool TreeQueryCallback(int proxyId, uint64_t userData);

/// <summary>
/// This function receives proxies found in the AABB query.
/// </summary>
/// <returns>True if the query should continue</returns>
[UnmanagedFunctionPointer(CallingConvention.Cdecl)] [return:MarshalAs(UnmanagedType.I1)]
public delegate bool TreeQueryNintCallback(int proxyId, uint64_t userData, nint context);

