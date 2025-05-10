namespace Box2D;

/// <summary>
/// Debug Draw base class. You cannot set DebugDraw delegates with this class. Use an instance of one of these implementations:
/// <ul>
/// <li><see cref="DebugDrawSimple" /></li>
/// <li><see cref="DebugDrawGeneric{TContext}" /></li>
/// <li><see cref="DebugDrawUnsafe" /></li>
/// </ul>
/// <i>or</i> implement one of these base classes:
/// <ul>
/// <li><see cref="DebugDrawSimpleBase" /></li>
/// <li><see cref="DebugDrawGenericBase{TContext}" /></li>
/// <li><see cref="DebugDrawUnsafeBase" /></li>
/// </ul>
/// </summary>
/// <remarks>
/// Pass your DebugDraw to <see cref="World"/>.<see cref="World.Draw" /> to draw the world.
/// Abstract base classes default to all drawing options enabled.
/// </remarks>
public abstract class DebugDraw
{
    //! \internal
    internal DebugDrawInternal @internal = new();

    //! \internal
    internal abstract ref DebugDrawInternal Internal { get; }

    /// <summary>
    /// Drawing bounds for the debug draw.
    /// </summary>
    public ref AABB DrawingBounds => ref @internal.DrawingBounds;

    /// <summary>
    /// Option to restrict drawing to a rectangular region. May suffer from unstable depth sorting.
    /// </summary>
    public bool UseDrawingBounds
    {
        get => @internal.UseDrawingBounds != 0;
        set => @internal.UseDrawingBounds = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// Option to draw shapes
    /// </summary>
    public bool DrawShapes
    {
        get => @internal.DrawShapes != 0;
        set => @internal.DrawShapes = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// Option to draw joints
    /// </summary>
    public bool DrawJoints
    {
        get => @internal.DrawJoints != 0;
        set => @internal.DrawJoints = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// Option to draw additional information for joints
    /// </summary>
    public bool DrawJointExtras
    {
        get => @internal.DrawJointExtras != 0;
        set => @internal.DrawJointExtras = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// Option to draw the bounding boxes for shapes
    /// </summary>
    public bool DrawBounds
    {
        get => @internal.DrawBounds != 0;
        set => @internal.DrawBounds = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// Option to draw the mass and center of mass of dynamic bodies
    /// </summary>
    public bool DrawMass
    {
        get => @internal.DrawMass != 0;
        set => @internal.DrawMass = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// Option to draw body names
    /// </summary>
    public bool DrawBodyNames
    {
        get => @internal.DrawBodyNames != 0;
        set => @internal.DrawBodyNames = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// Option to draw contact points
    /// </summary>
    public bool DrawContacts
    {
        get => @internal.DrawContacts != 0;
        set => @internal.DrawContacts = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// Option to visualize the graph coloring used for contacts and joints
    /// </summary>
    public bool DrawGraphColors
    {
        get => @internal.DrawGraphColors != 0;
        set => @internal.DrawGraphColors = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// Option to draw contact normals
    /// </summary>
    public bool DrawContactNormals
    {
        get => @internal.DrawContactNormals != 0;
        set => @internal.DrawContactNormals = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// Option to draw contact normal impulses
    /// </summary>
    public bool DrawContactImpulses
    {
        get => @internal.DrawContactImpulses != 0;
        set => @internal.DrawContactImpulses = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// Option to draw contact friction impulses
    /// </summary>
    public bool DrawFrictionImpulses
    {
        get => @internal.DrawFrictionImpulses != 0;
        set => @internal.DrawFrictionImpulses = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// Option to draw contact feature ids
    /// </summary>
    public bool DrawContactFeatures
    {
        get => @internal.DrawContactFeatures != 0;
        set => @internal.DrawContactFeatures = (byte)(value ? 1 : 0);
    }

    /// <summary>
    /// Option to draw contact friction impulses
    /// </summary>
    public bool DrawIslands
    {
        get => @internal.DrawIslands != 0;
        set => @internal.DrawIslands = (byte)(value ? 1 : 0);
    }
}