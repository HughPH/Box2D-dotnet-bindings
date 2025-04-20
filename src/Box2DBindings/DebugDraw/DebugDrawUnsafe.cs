using Box2D.Delegates.Unsafe;

namespace Box2D;

/// <summary>
/// This class holds callbacks you can implement to draw a Box2D world.
/// </summary>
public class DebugDrawUnsafe : DebugDraw
{
    /// <summary>
    /// Callback function to draw a closed polygon provided in CCW order.
    /// </summary>
    public unsafe DrawPolygonDelegate DrawPolygon
    {
        set => _internal.DrawPolygon = value;
    }

    /// <summary>
    /// Callback function to draw a solid closed polygon provided in CCW order.
    /// </summary>
    public unsafe DrawSolidPolygonDelegate DrawSolidPolygon
    {
        set => _internal.DrawSolidPolygon = value;
    }

    /// <summary>
    /// Callback function to draw a circle.
    /// </summary>
    public DrawCircleDelegate DrawCircle
    {
        set => _internal.DrawCircle = value;
    }

    /// <summary>
    /// Callback function to draw a solid circle.
    /// </summary>
    public DrawSolidCircleDelegate DrawSolidCircle
    {
        set => _internal.DrawSolidCircle = value;
    }
        
    /// <summary>
    /// Callback function to draw a solid capsule.
    /// </summary>
    public DrawSolidCapsuleDelegate DrawSolidCapsule
    {
        set => _internal.DrawSolidCapsule = value;
    }

    /// <summary>
    /// Callback function to draw a line segment.
    /// </summary>
    public DrawSegmentDelegate DrawSegment
    {
        set => _internal.DrawSegment = value;
    }

    /// <summary>
    /// Callback function to draw a transform. Choose your own length scale.
    /// </summary>
    public DrawTransformDelegate DrawTransform
    {
        set => _internal.DrawTransform = value;
    }

    /// <summary>
    /// Callback function to draw a point.
    /// </summary>
    public DrawPointDelegate DrawPoint
    {
        set => _internal.DrawPoint = value;
    }

    /// <summary>
    /// Callback function to draw a string in world space
    /// </summary>
    public DrawStringDelegate DrawString
    {
        set => _internal.DrawString = value;
    }
}