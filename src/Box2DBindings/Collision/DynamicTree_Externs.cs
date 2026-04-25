using System;
using System.Runtime.InteropServices;

namespace Box2D;

unsafe partial struct DynamicTree
{
#if NET9_0_OR_GREATER
    /// <summary>
    /// Constructing the tree initializes the node pool.
    /// </summary>
    public static readonly delegate* unmanaged[Cdecl]<DynamicTree> Create;
    private static readonly delegate* unmanaged[Cdecl]<ref DynamicTree, void> b2DynamicTree_Destroy;
    private static readonly delegate* unmanaged[Cdecl]<ref DynamicTree, AABB, ulong, ulong, int> b2DynamicTree_CreateProxy;
    private static readonly delegate* unmanaged[Cdecl]<ref DynamicTree, int, void> b2DynamicTree_DestroyProxy;
    private static readonly delegate* unmanaged[Cdecl]<ref DynamicTree, int, AABB, void> b2DynamicTree_MoveProxy;
    private static readonly delegate* unmanaged[Cdecl]<ref DynamicTree, int, AABB, void> b2DynamicTree_EnlargeProxy;
    private static readonly delegate* unmanaged[Cdecl]<ref DynamicTree, int, ulong, void> b2DynamicTree_SetCategoryBits;
    private static readonly delegate* unmanaged[Cdecl]<ref DynamicTree, int, ulong> b2DynamicTree_GetCategoryBits;
    private static readonly delegate* unmanaged[Cdecl]<in DynamicTree, AABB, ulong, nint, nint, TreeStats> b2DynamicTree_Query_;
    private static readonly delegate* unmanaged[Cdecl]<in DynamicTree, ShapeCastInput*, ulong, nint, nint, TreeStats> b2DynamicTree_ShapeCast_;
    private static readonly delegate* unmanaged[Cdecl]<in DynamicTree, RayCastInput*, ulong, nint, nint, TreeStats> b2DynamicTree_RayCast_;
    private static readonly delegate* unmanaged[Cdecl]<in DynamicTree, int> b2DynamicTree_GetHeight;
    private static readonly delegate* unmanaged[Cdecl]<in DynamicTree, float> b2DynamicTree_GetAreaRatio;
    private static readonly delegate* unmanaged[Cdecl]<in DynamicTree, AABB> b2DynamicTree_GetRootBounds;
    private static readonly delegate* unmanaged[Cdecl]<in DynamicTree, int> b2DynamicTree_GetProxyCount;
    private static readonly delegate* unmanaged[Cdecl]<ref DynamicTree, byte, int> b2DynamicTree_Rebuild;
    private static readonly delegate* unmanaged[Cdecl]<in DynamicTree, int> b2DynamicTree_GetByteCount;
    private static readonly delegate* unmanaged[Cdecl]<in DynamicTree, int, ulong> b2DynamicTree_GetUserData;
    private static readonly delegate* unmanaged[Cdecl]<in DynamicTree, int, AABB> b2DynamicTree_GetAABB;
    private static readonly delegate* unmanaged[Cdecl]<in DynamicTree, void> b2DynamicTree_Validate;
    private static readonly delegate* unmanaged[Cdecl]<in DynamicTree, void> b2DynamicTree_ValidateNoEnlarged;

    static DynamicTree()
    {
        nint lib = nativeLibrary;
        nint p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_Create", out p);
        Create = (delegate* unmanaged[Cdecl]<DynamicTree>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_Destroy", out p);
        b2DynamicTree_Destroy = (delegate* unmanaged[Cdecl]<ref DynamicTree, void>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_CreateProxy", out p);
        b2DynamicTree_CreateProxy = (delegate* unmanaged[Cdecl]<ref DynamicTree, AABB, ulong, ulong, int>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_DestroyProxy", out p);
        b2DynamicTree_DestroyProxy = (delegate* unmanaged[Cdecl]<ref DynamicTree, int, void>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_MoveProxy", out p);
        b2DynamicTree_MoveProxy = (delegate* unmanaged[Cdecl]<ref DynamicTree, int, AABB, void>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_EnlargeProxy", out p);
        b2DynamicTree_EnlargeProxy = (delegate* unmanaged[Cdecl]<ref DynamicTree, int, AABB, void>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_SetCategoryBits", out p);
        b2DynamicTree_SetCategoryBits = (delegate* unmanaged[Cdecl]<ref DynamicTree, int, ulong, void>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetCategoryBits", out p);
        b2DynamicTree_GetCategoryBits = (delegate* unmanaged[Cdecl]<ref DynamicTree, int, ulong>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_Query", out p);
        b2DynamicTree_Query_ = (delegate* unmanaged[Cdecl]<in DynamicTree, AABB, ulong, IntPtr, IntPtr, TreeStats>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_ShapeCast", out p);
        b2DynamicTree_ShapeCast_ = (delegate* unmanaged[Cdecl]<in DynamicTree, ShapeCastInput*, ulong, IntPtr, IntPtr, TreeStats>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_RayCast", out p);
        b2DynamicTree_RayCast_ = (delegate* unmanaged[Cdecl]<in DynamicTree, RayCastInput*, ulong, IntPtr, IntPtr, TreeStats>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetHeight", out p);
        b2DynamicTree_GetHeight = (delegate* unmanaged[Cdecl]<in DynamicTree, int>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetAreaRatio", out p);
        b2DynamicTree_GetAreaRatio = (delegate* unmanaged[Cdecl]<in DynamicTree, float>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetRootBounds", out p);
        b2DynamicTree_GetRootBounds = (delegate* unmanaged[Cdecl]<in DynamicTree, AABB>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetProxyCount", out p);
        b2DynamicTree_GetProxyCount = (delegate* unmanaged[Cdecl]<in DynamicTree, int>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_Rebuild", out p);
        b2DynamicTree_Rebuild = (delegate* unmanaged[Cdecl]<ref DynamicTree, byte, int>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetByteCount", out p);
        b2DynamicTree_GetByteCount = (delegate* unmanaged[Cdecl]<in DynamicTree, int>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetUserData", out p);
        b2DynamicTree_GetUserData = (delegate* unmanaged[Cdecl]<in DynamicTree, int, ulong>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetAABB", out p);
        b2DynamicTree_GetAABB = (delegate* unmanaged[Cdecl]<in DynamicTree, int, AABB>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_Validate", out p);
        b2DynamicTree_Validate = (delegate* unmanaged[Cdecl]<in DynamicTree, void>)p;
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_ValidateNoEnlarged", out p);
        b2DynamicTree_ValidateNoEnlarged = (delegate* unmanaged[Cdecl]<in DynamicTree, void>)p;
    }

    private static TreeStats b2DynamicTree_Query(in DynamicTree dynamictree, AABB aabb, ulong maskBits, TreeQueryNintCallback callback, nint context)
    {
        var handle = GCHandle.Alloc(callback);
        try
        {
            var fnPtr = Marshal.GetFunctionPointerForDelegate(callback);
            return b2DynamicTree_Query_(in dynamictree, aabb, maskBits, fnPtr, context);
        }
        finally
        {
            handle.Free();
        }
    }

    private static TreeStats b2DynamicTree_ShapeCast(in DynamicTree dynamictree, in ShapeCastInput input, ulong maskBits, TreeShapeCastNintCallback callback, IntPtr context)
    {
        var handle = GCHandle.Alloc(callback);
        try
        {
            var fnPtr = Marshal.GetFunctionPointerForDelegate(callback);
            fixed (ShapeCastInput* inputPtr = &input)
                return b2DynamicTree_ShapeCast_(in dynamictree, inputPtr, maskBits, fnPtr, context);
        }
        finally
        {
            handle.Free();
        }
    }

    private static TreeStats b2DynamicTree_RayCast(in DynamicTree dynamictree, in RayCastInput input, ulong maskBits, TreeRayCastNintCallback callback, IntPtr context)
    {
        var handle = GCHandle.Alloc(callback);
        try
        {
            var fnPtr = Marshal.GetFunctionPointerForDelegate(callback);
            fixed (RayCastInput* inputPtr = &input)
                return b2DynamicTree_RayCast_(in dynamictree, inputPtr, maskBits, fnPtr, context);
        }
        finally
        {
            handle.Free();
        }
    }
#else
    /// <summary>
    /// Constructing the tree initializes the node pool.
    /// </summary>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_Create")]
    public static extern DynamicTree Create();

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_Destroy")]
    private static extern void b2DynamicTree_Destroy(ref DynamicTree tree);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_CreateProxy")]
    private static extern int b2DynamicTree_CreateProxy(ref DynamicTree tree, AABB aabb, uint64_t categoryBits, uint64_t userData);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_DestroyProxy")]
    private static extern void b2DynamicTree_DestroyProxy(ref DynamicTree tree, int proxyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_MoveProxy")]
    private static extern void b2DynamicTree_MoveProxy(ref DynamicTree tree, int proxyId, AABB aabb);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_EnlargeProxy")]
    private static extern void b2DynamicTree_EnlargeProxy(ref DynamicTree tree, int proxyId, AABB aabb);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_SetCategoryBits")]
    private static extern void b2DynamicTree_SetCategoryBits(ref DynamicTree tree, int proxyId, uint64_t categoryBits);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetCategoryBits")]
    private static extern uint64_t b2DynamicTree_GetCategoryBits(ref DynamicTree tree, int proxyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_ShapeCast")]
    private static extern TreeStats b2DynamicTree_ShapeCast(in DynamicTree tree, in ShapeCastInput input, uint64_t maskBits, TreeShapeCastNintCallback callback, nint context);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_Query")]
    private static extern TreeStats b2DynamicTree_Query(in DynamicTree tree, AABB aabb, uint64_t maskBits, TreeQueryNintCallback callback, nint context);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_RayCast")]
    private static extern TreeStats b2DynamicTree_RayCast(in DynamicTree tree, in RayCastInput input, uint64_t maskBits, TreeRayCastNintCallback callback, nint context);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetHeight")]
    private static extern int b2DynamicTree_GetHeight(in DynamicTree tree);
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetAreaRatio")]
    private static extern float b2DynamicTree_GetAreaRatio(in DynamicTree tree);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetRootBounds")]
    private static extern AABB b2DynamicTree_GetRootBounds(in DynamicTree tree);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetProxyCount")]
    private static extern int b2DynamicTree_GetProxyCount(in DynamicTree tree);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_Rebuild")]
    private static extern int b2DynamicTree_Rebuild(ref DynamicTree tree, byte fullBuild);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetByteCount")]
    private static extern int b2DynamicTree_GetByteCount(in DynamicTree tree);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetUserData")]
    private static extern uint64_t b2DynamicTree_GetUserData(in DynamicTree tree, int proxyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetAABB")]
    private static extern AABB b2DynamicTree_GetAABB(in DynamicTree tree, int proxyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_Validate")]
    private static extern void b2DynamicTree_Validate(in DynamicTree tree);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_ValidateNoEnlarged")]
    private static extern void b2DynamicTree_ValidateNoEnlarged(in DynamicTree tree);

#endif
}
