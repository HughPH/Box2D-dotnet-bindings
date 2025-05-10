using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Dynamic tree for broad-phase collision detection.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public unsafe struct DynamicTree
{
    private TreeNode* nodes;

    private int root;
    private int nodeCount;
    private int nodeCapacity;
    private int freeList;
    private int proxyCount;
    private int* leafIndices;
    private AABB* leafBoxes;
    private Vec2* leafCenters;
    private int* binIndices;
    private int rebuildCapacity;

    /// <summary>
    /// The nodes in the tree. This is a read-only span of the nodes.
    /// </summary>
    public ReadOnlySpan<TreeNode> Nodes => new(nodes, nodeCapacity);
    
    /// <summary>
    /// The indices of the leaves in the tree. This is a read-only span of the leaf indices.
    /// </summary>
    public ReadOnlySpan<int> LeafIndices => new(leafIndices, rebuildCapacity);
    
    /// <summary>
    /// The bounding boxes of the leaves in the tree. This is a read-only span of the leaf boxes.
    /// </summary>
    public ReadOnlySpan<AABB> LeafBoxes => new(leafBoxes, rebuildCapacity);
    
    /// <summary>
    /// The centers of the leaves in the tree. This is a read-only span of the leaf centers.
    /// </summary>
    public ReadOnlySpan<Vec2> LeafCenters => new(leafCenters, rebuildCapacity);
    
    /// <summary>
    /// The bin indices of the leaves in the tree. This is a read-only span of the bin indices.
    /// </summary>
    public ReadOnlySpan<int> BinIndices => new(binIndices, rebuildCapacity);
    
    /// <summary>
    /// Constructing the tree initializes the node pool.
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gab54e4f2cbe26d0f306b9b8b5223c1838">b2DynamicTree_Create</a></remarks>
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_Create")]
    public static extern DynamicTree Create();

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_Destroy")]
    private static extern void Destroy(ref DynamicTree tree);

    /// <summary>
    /// Destroy the tree, freeing the node pool.
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#ga3ffc351d31681acfb3b342eaf1ad1841">b2DynamicTree_Destroy</a></remarks>
    public void Destroy() =>
        Destroy(ref this);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_CreateProxy")]
    private static extern int CreateProxy(ref DynamicTree tree, AABB aabb, uint64_t categoryBits, uint64_t userData);

    /// <summary>
    /// Create a proxy. Provide an AABB and a userData value.
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gaa28141343bcbb19c4fe8a29a33e7ed9d">b2DynamicTree_CreateProxy</a></remarks>
    public int CreateProxy(AABB aabb, uint64_t categoryBits, uint64_t userData) =>
        CreateProxy(ref this, aabb, categoryBits, userData);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_DestroyProxy")]
    private static extern void DestroyProxy(ref DynamicTree tree, int proxyId);

    /// <summary>
    /// Destroy a proxy. This asserts if the id is invalid.
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#ga57ffc60b96419607d1b9e1316ea449eb">b2DynamicTree_DestroyProxy</a></remarks>
    public void DestroyProxy(int proxyId)
    {
        DestroyProxy(ref this, proxyId);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_MoveProxy")]
    private static extern void MoveProxy(ref DynamicTree tree, int proxyId, AABB aabb);

    /// <summary>
    /// Move a proxy to a new AABB by removing and reinserting into the tree.
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#ga949cfa92f1879f1b84ce68997f536c5e">b2DynamicTree_MoveProxy</a></remarks>
    public void MoveProxy(int proxyId, AABB aabb) =>
        MoveProxy(ref this, proxyId, aabb);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_EnlargeProxy")]
    private static extern void EnlargeProxy(ref DynamicTree tree, int proxyId, AABB aabb);

    /// <summary>
    /// Enlarge a proxy and enlarge ancestors as necessary.
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gad2526a64dac608a03c9b2d3909113c0e">b2DynamicTree_EnlargeProxy</a></remarks>
    public void EnlargeProxy(int proxyId, AABB aabb) =>
        EnlargeProxy(ref this, proxyId, aabb);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_SetCategoryBits")]
    private static extern void SetCategoryBits(ref DynamicTree tree, int proxyId, uint64_t categoryBits);

    /// <summary>
    /// Modify the category bits on a proxy. This is an expensive operation.
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gaec7e26ac9ff60b88f1ed63bcf4e0a43b">b2DynamicTree_SetCategoryBits</a></remarks>
    public void SetCategoryBits(int proxyId, uint64_t categoryBits) =>
        SetCategoryBits(ref this, proxyId, categoryBits);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetCategoryBits")]
    private static extern uint64_t GetCategoryBits(ref DynamicTree tree, int proxyId);

    /// <summary>
    /// Get the category bits on a proxy.
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#ga957d56235f304fcd016c0f5cccc37a32">b2DynamicTree_GetCategoryBits</a></remarks>
    public uint64_t GetCategoryBits(int proxyId) =>
        GetCategoryBits(ref this, proxyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_Query")]
    private static extern TreeStats b2DynamicTree_Query(in DynamicTree tree, AABB aabb, uint64_t maskBits,
        TreeQueryNintCallback callback, nint context);

    private static bool TreeQueryCallbackThunk<TContext>(int proxyId, uint64_t userData, nint context) where TContext : class
    {
        var contextBuffer = (nint*)context;
        TContext contextObj = (TContext)GCHandle.FromIntPtr(contextBuffer[0]).Target!;
        var callback = (TreeQueryCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(proxyId, userData, contextObj);
    }
    
    private static bool TreeQueryCallbackRefThunk<TContext>(int proxyId, uint64_t userData, nint context) where TContext : unmanaged
    {
        var contextBuffer = (nint*)context;
        ref TContext contextObj = ref *(TContext*)contextBuffer[0];
        var callback = (TreeQueryRefCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(proxyId, userData, ref contextObj);
    }
    
    /// <summary>
    /// Query an AABB for overlapping proxies. The callback class is called for each proxy that overlaps the supplied AABB.
    /// </summary>
    /// <returns>Performance data</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gaf1b7b5ee38bdef334840ba9dbef10177">b2DynamicTree_Query</a></remarks>
    public TreeStats Query<TContext>(AABB aabb, uint64_t maskBits, TreeQueryCallback<TContext> callback, TContext context) where TContext : class
    {
        var contextBuffer = stackalloc nint[2];
        contextBuffer[0] = GCHandle.ToIntPtr(GCHandle.Alloc(context));
        contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            return b2DynamicTree_Query(this, aabb, maskBits, TreeQueryCallbackThunk<TContext>, (nint)contextBuffer);            
        }
        finally
        {
            GCHandle.FromIntPtr(contextBuffer[0]).Free();
            GCHandle.FromIntPtr(contextBuffer[1]).Free();
        }
    }
    
    /// <summary>
    /// Query an AABB for overlapping proxies. The callback class is called for each proxy that overlaps the supplied AABB.
    /// </summary>
    /// <returns>Performance data</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gaf1b7b5ee38bdef334840ba9dbef10177">b2DynamicTree_Query</a></remarks>
    public TreeStats Query<TContext>(AABB aabb, uint64_t maskBits, TreeQueryRefCallback<TContext> callback, ref TContext context) where TContext : unmanaged
    {
        fixed (TContext* contextPtr = &context)
        {
            var contextBuffer = stackalloc nint[2];
            contextBuffer[0] = (nint)contextPtr;
            contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
            try
            {
                return b2DynamicTree_Query(this, aabb, maskBits, TreeQueryCallbackRefThunk<TContext>, (nint)contextBuffer);
            }
            finally
            {
                GCHandle.FromIntPtr(contextBuffer[1]).Free();
            }
        }
    }
    
    private static bool TreeQueryCallbackThunk(int proxyId, uint64_t userData, nint context)
    {
        var callback = (TreeQueryCallback)GCHandle.FromIntPtr(context).Target!;
        return callback(proxyId, userData);
    }
    
    /// <summary>
    /// Query an AABB for overlapping proxies. The callback class is called for each proxy that overlaps the supplied AABB.
    /// </summary>
    /// <returns>Performance data</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gaf1b7b5ee38bdef334840ba9dbef10177">b2DynamicTree_Query</a></remarks>
    public TreeStats Query(AABB aabb, uint64_t maskBits, TreeQueryCallback callback)
    {
        nint context = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            return b2DynamicTree_Query(this, aabb, maskBits, TreeQueryCallbackThunk, context);
        }
        finally
        {
            GCHandle.FromIntPtr(context).Free();
        }
    }
    
    /// <summary>
    /// Query an AABB for overlapping proxies. The callback class is called for each proxy that overlaps the supplied AABB.
    /// </summary>
    /// <returns>Performance data</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gaf1b7b5ee38bdef334840ba9dbef10177">b2DynamicTree_Query</a></remarks>
    public TreeStats Query(AABB aabb, uint64_t maskBits, TreeQueryNintCallback callback, nint context) =>
        b2DynamicTree_Query(this, aabb, maskBits, callback, context);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_RayCast")]
    private static extern TreeStats b2DynamicTree_RayCast(in DynamicTree tree, in RayCastInput input, uint64_t maskBits,
        TreeRayCastNintCallback callback, nint context);
    
    private static float TreeRayCastCallbackThunk<TContext>(in RayCastInput input, int proxyId, uint64_t userData, nint context) where TContext : class
    {
        var contextBuffer = (nint*)context;
        TContext contextObj = (TContext)GCHandle.FromIntPtr(contextBuffer[0]).Target!;
        var callback = (TreeRayCastCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(input, proxyId, userData, contextObj);
    }
    
    private static float TreeRayCastCallbackRefThunk<TContext>(in RayCastInput input, int proxyId, uint64_t userData, nint context) where TContext : unmanaged
    {
        var contextBuffer = (nint*)context;
        ref TContext contextObj = ref *(TContext*)contextBuffer[0];
        var callback = (TreeRayCastRefCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(input, proxyId, userData, ref contextObj);
    }
    
    /// <summary>
    /// Ray cast against the proxies in the tree. This relies on the callback
    /// to perform a exact ray cast in the case were the proxy contains a shape.
    /// The callback also performs the any collision filtering. This has performance
    /// roughly equal to k * log(n), where k is the number of collisions and n is the
    /// number of proxies in the tree.
    /// </summary>
    /// <param name="input">The ray cast input data. The ray extends from p1 to p1 + maxFraction * (p2 - p1)</param>
    /// <param name="maskBits">Mask bit hint: `bool accept = (maskBits &amp; node-&gt;categoryBits) != 0;`</param>
    /// <param name="callback">A callback class that is called for each proxy that is hit by the ray</param>
    /// <param name="context">User context that is passed to the callback</param>
    /// <returns>Performance data</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gad36e038846acb7576a095176da0a98e4">b2DynamicTree_RayCast</a></remarks>
    public TreeStats RayCast<TContext>(in RayCastInput input, uint64_t maskBits, TreeRayCastCallback<TContext> callback, TContext context) where TContext : class
    {
        nint* contextBuffer = stackalloc nint[2];
        contextBuffer[0] = GCHandle.ToIntPtr(GCHandle.Alloc(context));
        contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            return b2DynamicTree_RayCast(this, input, maskBits, TreeRayCastCallbackThunk<TContext>, (nint)contextBuffer);
        }
        finally
        {
            GCHandle.FromIntPtr(contextBuffer[0]).Free();
            GCHandle.FromIntPtr(contextBuffer[1]).Free();
        }
    }
    
    /// <summary>
    /// Ray cast against the proxies in the tree. This relies on the callback
    /// to perform a exact ray cast in the case were the proxy contains a shape.
    /// The callback also performs the any collision filtering. This has performance
    /// roughly equal to k * log(n), where k is the number of collisions and n is the
    /// number of proxies in the tree.
    /// </summary>
    /// <param name="input">The ray cast input data. The ray extends from p1 to p1 + maxFraction * (p2 - p1)</param>
    /// <param name="maskBits">Mask bit hint: `bool accept = (maskBits &amp; node-&gt;categoryBits) != 0;`</param>
    /// <param name="callback">A callback class that is called for each proxy that is hit by the ray</param>
    /// <param name="context">User context that is passed to the callback</param>
    /// <returns>Performance data</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gad36e038846acb7576a095176da0a98e4">b2DynamicTree_RayCast</a></remarks>
    public TreeStats RayCast<TContext>(in RayCastInput input, uint64_t maskBits, TreeRayCastRefCallback<TContext> callback, ref TContext context) where TContext : unmanaged
    {
        fixed(TContext* contextPtr = &context){
            nint* contextBuffer = stackalloc nint[2];
            contextBuffer[0] = (nint)contextPtr;
            contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
            try
            {
                return b2DynamicTree_RayCast(this, input, maskBits, TreeRayCastCallbackRefThunk<TContext>, (nint)contextBuffer);
            }
            finally
            {
                GCHandle.FromIntPtr(contextBuffer[1]).Free();
            }
        }
    }
    
    private static float TreeRayCastCallbackThunk(in RayCastInput input, int proxyId, uint64_t userData, nint context)
    {
        var callback = (TreeRayCastCallback)GCHandle.FromIntPtr(context).Target!;
        return callback(input, proxyId, userData);
    }
    
    /// <summary>
    /// Ray cast against the proxies in the tree. This relies on the callback
    /// to perform a exact ray cast in the case were the proxy contains a shape.
    /// The callback also performs the any collision filtering. This has performance
    /// roughly equal to k * log(n), where k is the number of collisions and n is the
    /// number of proxies in the tree.
    /// </summary>
    /// <param name="input">The ray cast input data. The ray extends from p1 to p1 + maxFraction * (p2 - p1)</param>
    /// <param name="maskBits">Mask bit hint: `bool accept = (maskBits &amp; node-&gt;categoryBits) != 0;`</param>
    /// <param name="callback">A callback class that is called for each proxy that is hit by the ray</param>
    /// <returns>Performance data</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gad36e038846acb7576a095176da0a98e4">b2DynamicTree_RayCast</a></remarks>
    public TreeStats RayCast(in RayCastInput input, uint64_t maskBits, TreeRayCastCallback callback)
    {
        nint context = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            return b2DynamicTree_RayCast(this, input, maskBits, TreeRayCastCallbackThunk, context);
        }
        finally
        {
            GCHandle.FromIntPtr(context).Free();
        }
    }
    
    /// <summary>
    /// Ray cast against the proxies in the tree. This relies on the callback
    /// to perform a exact ray cast in the case were the proxy contains a shape.
    /// The callback also performs the any collision filtering. This has performance
    /// roughly equal to k * log(n), where k is the number of collisions and n is the
    /// number of proxies in the tree.
    /// </summary>
    /// <param name="input">The ray cast input data. The ray extends from p1 to p1 + maxFraction * (p2 - p1)</param>
    /// <param name="maskBits">Mask bit hint: `bool accept = (maskBits &amp; node-&gt;categoryBits) != 0;`</param>
    /// <param name="callback">A callback class that is called for each proxy that is hit by the ray</param>
    /// <param name="context">User context that is passed to the callback</param>
    /// <returns>Performance data</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gad36e038846acb7576a095176da0a98e4">b2DynamicTree_RayCast</a></remarks>
    public TreeStats RayCast(in RayCastInput input, uint64_t maskBits, TreeRayCastNintCallback callback, nint context)
    {
        return b2DynamicTree_RayCast(this, input, maskBits, callback, context);
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_ShapeCast")]
    private static extern TreeStats b2DynamicTree_ShapeCast(in DynamicTree tree, in ShapeCastInput input, uint64_t maskBits,
        TreeShapeCastNintCallback callback, nint context);

    private static float TreeShapeCastCallbackThunk<TContext>(in ShapeCastInput input, int proxyId, uint64_t userData, nint context) where TContext : class
    {
        var contextBuffer = (nint*)context;
        TContext contextObj = (TContext)GCHandle.FromIntPtr(contextBuffer[0]).Target!;
        var callback = (TreeShapeCastCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(input, proxyId, userData, contextObj);
    }
    
    private static float TreeShapeCastCallbackRefThunk<TContext>(in ShapeCastInput input, int proxyId, uint64_t userData, nint context) where TContext : unmanaged
    {
        var contextBuffer = (nint*)context;
        ref TContext contextObj = ref *(TContext*)contextBuffer[0];
        var callback = (TreeShapeCastRefCallback<TContext>)GCHandle.FromIntPtr(contextBuffer[1]).Target!;
        return callback(input, proxyId, userData, ref contextObj);
    }
    
    /// <summary>
    /// Ray cast against the proxies in the tree. This relies on the callback
    /// to perform a exact ray cast in the case were the proxy contains a shape.
    /// The callback also performs the any collision filtering. This has performance
    /// roughly equal to k * log(n), where k is the number of collisions and n is the
    /// number of proxies in the tree.
    /// </summary>
    /// <param name="input">The ray cast input data. The ray extends from p1 to p1 + maxFraction * (p2 - p1).</param>
    /// <param name="maskBits">Filter bits: `bool accept = (maskBits &amp; node-&gt;categoryBits) != 0;`</param>
    /// <param name="callback">A callback class that is called for each proxy that is hit by the shape</param>
    /// <param name="context">User context that is passed to the callback</param>
    /// <returns>Performance data</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#ga6b7031cb2b916c74968ed7e53e69abe3">b2DynamicTree_ShapeCast</a></remarks>
    public TreeStats ShapeCast<TContext>(in ShapeCastInput input, uint64_t maskBits, TreeShapeCastCallback<TContext> callback, TContext context) where TContext : class
    {
        nint* contextBuffer = stackalloc nint[2];
        contextBuffer[0] = GCHandle.ToIntPtr(GCHandle.Alloc(context));
        contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            return b2DynamicTree_ShapeCast(this, input, maskBits, TreeShapeCastCallbackThunk<TContext>, (nint)contextBuffer);
        }
        finally
        {
            GCHandle.FromIntPtr(contextBuffer[0]).Free();
            GCHandle.FromIntPtr(contextBuffer[1]).Free();
        }
    }
    
    /// <summary>
    /// Ray cast against the proxies in the tree. This relies on the callback
    /// to perform a exact ray cast in the case were the proxy contains a shape.
    /// The callback also performs the any collision filtering. This has performance
    /// roughly equal to k * log(n), where k is the number of collisions and n is the
    /// number of proxies in the tree.
    /// </summary>
    /// <param name="input">The ray cast input data. The ray extends from p1 to p1 + maxFraction * (p2 - p1).</param>
    /// <param name="maskBits">Filter bits: `bool accept = (maskBits &amp; node-&gt;categoryBits) != 0;`</param>
    /// <param name="callback">A callback class that is called for each proxy that is hit by the shape</param>
    /// <param name="context">User context that is passed to the callback</param>
    /// <returns>Performance data</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#ga6b7031cb2b916c74968ed7e53e69abe3">b2DynamicTree_ShapeCast</a></remarks>
    public TreeStats ShapeCast<TContext>(in ShapeCastInput input, uint64_t maskBits, TreeShapeCastRefCallback<TContext> callback, ref TContext context) where TContext : unmanaged
    {
        fixed (TContext* contextPtr = &context)
        {
            nint* contextBuffer = stackalloc nint[2];
            contextBuffer[0] = (nint)contextPtr;
            contextBuffer[1] = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
            try
            {
                return b2DynamicTree_ShapeCast(this, input, maskBits, TreeShapeCastCallbackRefThunk<TContext>, (nint)contextBuffer);
            }
            finally
            {
                GCHandle.FromIntPtr(contextBuffer[1]).Free();
            }
        }
    }
    
    private static float TreeShapeCastCallbackThunk(in ShapeCastInput input, int proxyId, uint64_t userData, nint context)
    {
        var callback = (TreeShapeCastCallback)GCHandle.FromIntPtr(context).Target!;
        return callback(input, proxyId, userData);
    }
    
    /// <summary>
    /// Ray cast against the proxies in the tree. This relies on the callback
    /// to perform a exact ray cast in the case were the proxy contains a shape.
    /// The callback also performs the any collision filtering. This has performance
    /// roughly equal to k * log(n), where k is the number of collisions and n is the
    /// number of proxies in the tree.
    /// </summary>
    /// <param name="input">The ray cast input data. The ray extends from p1 to p1 + maxFraction * (p2 - p1).</param>
    /// <param name="maskBits">Filter bits: `bool accept = (maskBits &amp; node-&gt;categoryBits) != 0;`</param>
    /// <param name="callback">A callback class that is called for each proxy that is hit by the shape</param>
    /// <returns>Performance data</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#ga6b7031cb2b916c74968ed7e53e69abe3">b2DynamicTree_ShapeCast</a></remarks>
    public TreeStats ShapeCast(in ShapeCastInput input, uint64_t maskBits, TreeShapeCastCallback callback)
    {
        nint context = GCHandle.ToIntPtr(GCHandle.Alloc(callback));
        try
        {
            return b2DynamicTree_ShapeCast(this, input, maskBits, TreeShapeCastCallbackThunk, context);
        }
        finally
        {
            GCHandle.FromIntPtr(context).Free();
        }
    }
    
    /// <summary>
    /// Ray cast against the proxies in the tree. This relies on the callback
    /// to perform a exact ray cast in the case were the proxy contains a shape.
    /// The callback also performs the any collision filtering. This has performance
    /// roughly equal to k * log(n), where k is the number of collisions and n is the
    /// number of proxies in the tree.
    /// </summary>
    /// <param name="input">The ray cast input data. The ray extends from p1 to p1 + maxFraction * (p2 - p1).</param>
    /// <param name="maskBits">Filter bits: `bool accept = (maskBits &amp; node-&gt;categoryBits) != 0;`</param>
    /// <param name="callback">A callback class that is called for each proxy that is hit by the shape</param>
    /// <param name="context">User context that is passed to the callback</param>
    /// <returns>Performance data</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#ga6b7031cb2b916c74968ed7e53e69abe3">b2DynamicTree_ShapeCast</a></remarks>
    public TreeStats ShapeCast(in ShapeCastInput input, uint64_t maskBits, TreeShapeCastNintCallback callback, nint context)
    {
        return b2DynamicTree_ShapeCast(this, input, maskBits, callback, context);
    }
    
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetHeight")]
    private static extern int GetHeight(in DynamicTree tree);
        
    /// <summary>
    /// Get the height of the binary tree.
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gabb18581e8abd1a7916f013d2274c803c">b2DynamicTree_GetHeight</a></remarks>
    public int Height => GetHeight(in this);
        
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetAreaRatio")]
    private static extern float GetAreaRatio(in DynamicTree tree);
        
    /// <summary>
    /// Get the ratio of the sum of the node areas to the root area.
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#ga4ba23de8db5ba7ee19b84fe1430b20f0">b2DynamicTree_GetAreaRatio</a></remarks>
    public float AreaRatio => GetAreaRatio(in this);
        
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetRootBounds")]
    private static extern AABB GetRootBounds(in DynamicTree tree);
        
    /// <summary>
    /// Get the bounding box that contains the entire tree
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#ga6c880e1bc20548d3d7722e7d31427559">b2DynamicTree_GetRootBounds</a></remarks>
    public AABB RootBounds => GetRootBounds(in this);
        
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetProxyCount")]
    private static extern int GetProxyCount(in DynamicTree tree);
        
    /// <summary>
    /// Get the number of proxies created
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#ga84ec4a5ce6a153e67e8a48877a3b809e">b2DynamicTree_GetProxyCount</a></remarks>
    public int ProxyCount => GetProxyCount(in this);
        
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_Rebuild")]
    private static extern int Rebuild(ref DynamicTree tree, byte fullBuild);
        
    /// <summary>
    /// Rebuild the tree while retaining subtrees that haven't changed. Returns the number of boxes sorted.
    /// </summary>
    /// <param name="fullBuild">If true, the tree is fully rebuilt. If false, only the boxes that have changed are rebuilt.</param>
    /// <returns>The number of boxes sorted.</returns>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#ga369a432bada6d1c97fd07978d4e93755">b2DynamicTree_Rebuild</a></remarks>
    public int Rebuild(bool fullBuild) =>
        Rebuild(ref this, fullBuild ? (byte)1 : (byte)0);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetByteCount")]
    private static extern int GetByteCount(in DynamicTree tree);
        
    /// <summary>
    /// Get the number of bytes used by this tree
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gadb7ec47050e828b46b86daf0bf70d8d8">b2DynamicTree_GetByteCount</a></remarks>
    public int ByteCount => GetByteCount(in this);
        
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetUserData")]
    private static extern uint64_t GetUserData(in DynamicTree tree, int proxyId);
        
    /// <summary>
    /// Get proxy user data
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#ga2542608df6c4b508353118c1f17c9b51">b2DynamicTree_GetUserData</a></remarks>
    public uint64_t GetUserData(int proxyId) =>
        GetUserData(in this, proxyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_GetAABB")]
    private static extern AABB GetAABB(in DynamicTree tree, int proxyId);
        
    /// <summary>
    /// Get the AABB of a proxy
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gad17ae0125f95bfdf6a8c296e8a41f6cf">b2DynamicTree_GetAABB</a></remarks>
    public AABB GetAABB(int proxyId) =>
        GetAABB(in this, proxyId);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_Validate")]
    private static extern void Validate(in DynamicTree tree);
        
    /// <summary>
    /// Validate this tree. For testing.
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#ga44e19f08118eb947ac4ded090a791e27">b2DynamicTree_Validate</a></remarks>
    public void Validate() =>
        Validate(in this);

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DynamicTree_ValidateNoEnlarged")]
    private static extern void ValidateNoEnlarged(in DynamicTree tree);
        
    /// <summary>
    /// Validate this tree. For testing.
    /// </summary>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__collision.html#https://box2d.org/documentation/group__tree.html#gacc47ffa9443eb0b8dae89eeda55f7ad1">b2DynamicTree_ValidateNoEnlarged</a></remarks>
    public void ValidateNoEnlarged() =>
        ValidateNoEnlarged(in this);
}