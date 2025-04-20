using Box2D.Delegates.Safe;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// This class holds callbacks you can implement to draw a Box2D world.
/// </summary>
public class DebugDrawSimple:DebugDraw
{
    
    /// <summary>
    /// Callback function to draw a closed polygon provided in CCW order.
    /// </summary>
    public unsafe DrawPolygonDelegateSafe DrawPolygon
    {
        set
        {
            var del = value;
            void Wrapper(Vec2* vertices, int vertexCount, HexColor color, nint _) =>
                del(new ReadOnlySpan<Vec2>(vertices,vertexCount), color);
            _internal.DrawPolygon = Wrapper;
        }
    }

    /// <summary>
    /// Callback function to draw a solid closed polygon provided in CCW order.
    /// </summary>
    public unsafe DrawSolidPolygonDelegateSafe DrawSolidPolygon
    {
        set
        {
            var del = value;
            void Wrapper(Transform transform, Vec2* vertices, int vertexCount, float radius, HexColor color, nint _) =>
                del(transform, new ReadOnlySpan<Vec2>(vertices, vertexCount), radius, color);
            _internal.DrawSolidPolygon = Wrapper;
        }
    }

    /// <summary>
    /// Callback function to draw a circle.
    /// </summary>
    public DrawCircleDelegateSafe DrawCircle
    {
        set
        {
            var del = value;
            void Wrapper(Vec2 center, float radius, HexColor color, nint _) =>
                del(center, radius, color);
            _internal.DrawCircle = Wrapper;
        }
    }
    
    /// <summary>
    /// Callback function to draw a solid circle.
    /// </summary>
    public DrawSolidCircleDelegateSafe DrawSolidCircle
    {
        set
        {
            var del = value;
            void Wrapper(Transform transform, float radius, HexColor color, nint _) =>
                del(transform, radius, color);
            _internal.DrawSolidCircle = Wrapper;
        }
    }
    
    /// <summary>
    /// Callback function to draw a solid capsule.
    /// </summary>
    public DrawSolidCapsuleDelegateSafe DrawSolidCapsule
    {
        set
        {
            var del = value;
            void Wrapper(Vec2 p1, Vec2 p2, float radius, HexColor color, nint _) =>
                del(p1, p2, radius, color);
            _internal.DrawSolidCapsule = Wrapper;
        }
    }
    
    /// <summary>
    /// Callback function to draw a line segment.
    /// </summary>
    public DrawSegmentDelegateSafe DrawSegment
    {
        set
        {
            var del = value;
            void Wrapper(Vec2 p1, Vec2 p2, HexColor color, nint _) =>
                del(p1, p2, color);
            _internal.DrawSegment = Wrapper;
        }
    }
    
    /// <summary>
    /// Callback function to draw a transform. Choose your own length scale.
    /// </summary>
    public DrawTransformDelegateSafe DrawTransform
    {
        set
        {
            var del = value;
            void Wrapper(Transform transform, nint _) =>
                del(transform);
            _internal.DrawTransform = Wrapper;
        }
    }
   
    /// <summary>
    /// Callback function to draw a point.
    /// </summary>
    public DrawPointDelegateSafe DrawPoint
    {
        set
        {
            var del = value;
            void Wrapper(Vec2 p, float size, HexColor color, nint _) =>
                del(p, size, color);
            _internal.DrawPoint = Wrapper;
        }
    }
    
    /// <summary>
    /// Callback function to draw a string in world space
    /// </summary>
    public DrawStringDelegateSafe DrawString
    {
        set
        {
            var del = value;
            void Wrapper(Vec2 p, nint s, HexColor color, nint _)
            {
                string? str = Marshal.PtrToStringUTF8(s);
                del(p, str, color);
            }
            _internal.DrawString = Wrapper;
        }
    }
    
    
}