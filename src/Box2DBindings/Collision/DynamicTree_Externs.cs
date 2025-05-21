using System;
using System.Runtime.InteropServices;

namespace Box2D;

unsafe partial struct DynamicTree
{
#if NET5_0_OR_GREATER
    /// <summary>
    /// Constructing the tree initializes the node pool.
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gab54e4f2cbe26d0f306b9b8b5223c1838">b2DynamicTree_Create</a></remarks>
    public static  readonly delegate* unmanaged[Cdecl]<DynamicTree> Create;
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
        nint lib = NativeLibrary.Load(libraryName);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_Create", out var p0);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_Destroy", out var p1);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_CreateProxy", out var p2);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_DestroyProxy", out var p3);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_MoveProxy", out var p4);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_EnlargeProxy", out var p5);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_SetCategoryBits", out var p6);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetCategoryBits", out var p7);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_Query", out var p8);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_ShapeCast", out var p9);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_RayCast", out var p20);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetHeight", out var p10);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetAreaRatio", out var p11);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetRootBounds", out var p12);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetProxyCount", out var p13);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_Rebuild", out var p14);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetByteCount", out var p15);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetUserData", out var p16);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_GetAABB", out var p17);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_Validate", out var p18);
        NativeLibrary.TryGetExport(lib, "b2DynamicTree_ValidateNoEnlarged", out var p19);

        Create = (delegate* unmanaged[Cdecl]<DynamicTree>)p0;
        b2DynamicTree_Destroy = (delegate* unmanaged[Cdecl]<ref DynamicTree, void>)p1;
        b2DynamicTree_CreateProxy = (delegate* unmanaged[Cdecl]<ref DynamicTree, AABB, ulong, ulong, int>)p2;
        b2DynamicTree_DestroyProxy = (delegate* unmanaged[Cdecl]<ref DynamicTree, int, void>)p3;
        b2DynamicTree_MoveProxy = (delegate* unmanaged[Cdecl]<ref DynamicTree, int, AABB, void>)p4;
        b2DynamicTree_EnlargeProxy = (delegate* unmanaged[Cdecl]<ref DynamicTree, int, AABB, void>)p5;
        b2DynamicTree_SetCategoryBits = (delegate* unmanaged[Cdecl]<ref DynamicTree, int, ulong, void>)p6;
        b2DynamicTree_GetCategoryBits = (delegate* unmanaged[Cdecl]<ref DynamicTree, int, ulong>)p7;
        b2DynamicTree_Query_ = (delegate* unmanaged[Cdecl]<in DynamicTree, AABB, ulong, IntPtr, IntPtr, TreeStats>)p8;
        b2DynamicTree_ShapeCast_ = (delegate* unmanaged[Cdecl]<in DynamicTree, ShapeCastInput*, ulong, IntPtr, IntPtr, TreeStats>)p9;
        b2DynamicTree_RayCast_ = (delegate* unmanaged[Cdecl]<in DynamicTree, RayCastInput*, ulong, IntPtr, IntPtr, TreeStats>)p20;
        b2DynamicTree_GetHeight = (delegate* unmanaged[Cdecl]<in DynamicTree, int>)p10;
        b2DynamicTree_GetAreaRatio = (delegate* unmanaged[Cdecl]<in DynamicTree, float>)p11;
        b2DynamicTree_GetRootBounds = (delegate* unmanaged[Cdecl]<in DynamicTree, AABB>)p12;
        b2DynamicTree_GetProxyCount = (delegate* unmanaged[Cdecl]<in DynamicTree, int>)p13;
        b2DynamicTree_Rebuild = (delegate* unmanaged[Cdecl]<ref DynamicTree, byte, int>)p14;
        b2DynamicTree_GetByteCount = (delegate* unmanaged[Cdecl]<in DynamicTree, int>)p15;
        b2DynamicTree_GetUserData = (delegate* unmanaged[Cdecl]<in DynamicTree, int, ulong>)p16;
        b2DynamicTree_GetAABB = (delegate* unmanaged[Cdecl]<in DynamicTree, int, AABB>)p17;
        b2DynamicTree_Validate = (delegate* unmanaged[Cdecl]<in DynamicTree, void>)p18;
        b2DynamicTree_ValidateNoEnlarged = (delegate* unmanaged[Cdecl]<in DynamicTree, void>)p19;
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
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gab54e4f2cbe26d0f306b9b8b5223c1838">b2DynamicTree_Create</a></remarks>
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