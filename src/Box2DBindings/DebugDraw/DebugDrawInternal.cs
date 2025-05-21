using Box2D.Delegates.Unsafe;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// This struct holds callbacks you can implement to draw a Box2D world.
/// This structure should be zero initialized.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
struct DebugDrawInternal
{
#if NET5_0_OR_GREATER
    public static unsafe delegate* unmanaged[Cdecl]<DebugDrawInternal> b2DefaultDebugDraw;

    static unsafe DebugDrawInternal()
    {
        nint lib = Core.NativeLibHandle;
        NativeLibrary.TryGetExport(lib, "b2DefaultDebugDraw", out var ptr);
        b2DefaultDebugDraw = (delegate* unmanaged[Cdecl]<DebugDrawInternal>)ptr;
    }
#else
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2DefaultDebugDraw")]
    public static extern DebugDrawInternal b2DefaultDebugDraw();
#endif
    
    /// <summary>
    /// Callback function to draw a closed polygon provided in CCW order.
    /// </summary>
    [FieldOffset(0)]
    internal DrawPolygonDelegate DrawPolygon;

    /// <summary>
    /// Callback function to draw a solid closed polygon provided in CCW order.
    /// </summary>
    [FieldOffset(8)]
    internal DrawSolidPolygonDelegate DrawSolidPolygon;
    
    /// <summary>
    /// Callback function to draw a circle.
    /// </summary>
    [FieldOffset(16)]
    internal DrawCircleDelegate DrawCircle;
    
    /// <summary>
    /// Callback function to draw a solid circle.
    /// </summary>
    [FieldOffset(24)]
    internal DrawSolidCircleDelegate DrawSolidCircle;
    
    /// <summary>
    /// Callback function to draw a solid capsule.
    /// </summary>
    [FieldOffset(32)]
    internal DrawSolidCapsuleDelegate DrawSolidCapsule;
    
    /// <summary>
    /// Callback function to draw a line segment.
    /// </summary>
    [FieldOffset(40)]
    internal DrawSegmentDelegate DrawSegment;

    /// <summary>
    /// Callback function to draw a transform. Choose your own length scale.
    /// </summary>
    [FieldOffset(48)]
    internal DrawTransformDelegate DrawTransform;

    /// <summary>
    /// Callback function to draw a point.
    /// </summary>
    [FieldOffset(56)]
    internal DrawPointDelegate DrawPoint;

    /// <summary>
    /// Callback function to draw a string in world space
    /// </summary>
    [FieldOffset(64)]
    internal DrawStringDelegate DrawString;

    /// <summary>
    /// Drawing bounds
    /// </summary>
    [FieldOffset(72)]
    internal AABB DrawingBounds;

    /// <summary>
    /// Option to restrict drawing to a rectangular region. May suffer from unstable depth sorting.
    /// </summary>
    [FieldOffset(88)]
    internal byte UseDrawingBounds;

    /// <summary>
    /// Option to draw shapes
    /// </summary>
    [FieldOffset(89)]
    internal byte DrawShapes;

    /// <summary>
    /// Option to draw joints
    /// </summary>
    [FieldOffset(90)]
    internal byte DrawJoints;

    /// <summary>
    /// Option to draw additional information for joints
    /// </summary>
    [FieldOffset(91)]
    internal byte DrawJointExtras;

    /// <summary>
    /// Option to draw the bounding boxes for shapes
    /// </summary>
    [FieldOffset(92)]
    internal byte DrawBounds;

    /// <summary>
    /// Option to draw the mass and center of mass of dynamic bodies
    /// </summary>
    [FieldOffset(93)]
    internal byte DrawMass;

    /// <summary>
    /// Option to draw body names
    /// </summary>
    [FieldOffset(94)]
    internal byte DrawBodyNames;

    /// <summary>
    /// Option to draw contact points
    /// </summary>
    [FieldOffset(95)]
    internal byte DrawContacts;

    /// <summary>
    /// Option to visualize the graph coloring used for contacts and joints
    /// </summary>
    [FieldOffset(96)]
    internal byte DrawGraphColors;

    /// <summary>
    /// Option to draw contact normals
    /// </summary>
    [FieldOffset(97)]
    internal byte DrawContactNormals;

    /// <summary>
    /// Option to draw contact normal impulses
    /// </summary>
    [FieldOffset(98)]
    internal byte DrawContactImpulses;
    
    /// <summary>
    /// Option to draw contact friction impulses
    /// </summary>
    [FieldOffset(99)]
    internal byte DrawFrictionImpulses;

    /// <summary>
    /// Option to draw contact feature ids
    /// </summary>
    [FieldOffset(100)]
    internal byte DrawContactFeatures;
    
    /// <summary>
    /// Option to draw contact friction impulses
    /// </summary>
    [FieldOffset(101)]
    internal byte DrawIslands;
    
    /// <summary>
    /// User context that is passed as an argument to drawing callback functions
    /// </summary>
    [FieldOffset(104)] // align to next 4 byte boundary
    internal nint context;

    public unsafe DebugDrawInternal()
    {
        this = b2DefaultDebugDraw();
    }
}