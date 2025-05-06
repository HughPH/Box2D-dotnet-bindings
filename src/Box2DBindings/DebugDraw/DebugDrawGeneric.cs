using Box2D.Delegates.Generic;
using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// This class holds callbacks you can implement to draw a Box2D world.
/// </summary>
[PublicAPI]
public sealed class DebugDrawGeneric<TContext> : DebugDraw where TContext:class
{
    private TContext context;

    internal override ref DebugDrawInternal Internal => ref @internal;

    /// <summary>
    /// Constructor for DebugDrawGeneric.
    /// </summary>
    /// <param name="context">The context object to be passed to the callback functions.</param>
    public DebugDrawGeneric(TContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Callback function to draw a closed polygon provided in CCW order.
    /// </summary>
    public unsafe DrawPolygonDelegate<TContext> DrawPolygon
    {
        set
        {
            var del = value;
            var ctx = context;
            void Wrapper(Vec2* vertices, int vertexCount, HexColor color, nint _) =>
                del(new(vertices, vertexCount), color, ctx);
            @internal.DrawPolygon = Wrapper;
        }
    }

    /// <summary>
    /// Callback function to draw a solid closed polygon provided in CCW order.
    /// </summary>
    public unsafe DrawSolidPolygonDelegate<TContext> DrawSolidPolygon
    {
        set
        {
            var del = value;
            var ctx = context;
            void Wrapper(Transform transform, Vec2* vertices, int vertexCount, float radius, HexColor color, nint _) =>
                del(transform, new(vertices, vertexCount), radius, color, ctx);
            @internal.DrawSolidPolygon = Wrapper;
        }
    }

    /// <summary>
    /// Callback function to draw a circle.
    /// </summary>
    public DrawCircleDelegate<TContext> DrawCircle
    {
        set
        {
            var del = value;
            var ctx = context;
            void Wrapper(Vec2 center, float radius, HexColor color, nint _) =>
                del(center, radius, color, ctx);
            @internal.DrawCircle = Wrapper;
        }
    }

    /// <summary>
    /// Callback function to draw a solid circle.
    /// </summary>
    public DrawSolidCircleDelegate<TContext> DrawSolidCircle
    {
        set
        {
            var del = value;
            var ctx = context;
            void Wrapper(Transform transform, float radius, HexColor color, nint _) =>
                del(transform, radius, color, ctx);
            @internal.DrawSolidCircle = Wrapper;
        }
    }


    /// <summary>
    /// Callback function to draw a solid capsule.
    /// </summary>
    public DrawSolidCapsuleDelegate<TContext> DrawSolidCapsule
    {
        set
        {
            var del = value;
            var ctx = context;
            void Wrapper(Vec2 p1, Vec2 p2, float radius, HexColor color, nint _) =>
                del(p1, p2, radius, color, ctx);
            @internal.DrawSolidCapsule = Wrapper;
        }
    }

    /// <summary>
    /// Callback function to draw a line segment.
    /// </summary>
    public DrawSegmentDelegate<TContext> DrawSegment
    {
        set
        {
            var del = value;
            var ctx = context;
            void Wrapper(Vec2 p1, Vec2 p2, HexColor color, nint _) =>
                del(p1, p2, color, ctx);
            @internal.DrawSegment = Wrapper;
        }
    }

    /// <summary>
    /// Callback function to draw a transform. Choose your own length scale.
    /// </summary>
    public DrawTransformDelegate<TContext> DrawTransform
    {
        set
        {
            var del = value;
            var ctx = context;
            void Wrapper(Transform transform, nint _) =>
                del(transform, ctx);
            @internal.DrawTransform = Wrapper;
        }
    }

    /// <summary>
    /// Callback function to draw a point.
    /// </summary>
    public DrawPointDelegate<TContext> DrawPoint
    {
        set
        {
            var del = value;
            var ctx = context;
            void Wrapper(Vec2 p, float size, HexColor color, nint _) =>
                del(p, size, color, ctx);
            @internal.DrawPoint = Wrapper;
        }
    }

    /// <summary>
    /// Callback function to draw a string in world space
    /// </summary>
    public DrawStringDelegate<TContext> DrawString
    {
        set
        {
            var del = value;
            var ctx = context;
            void Wrapper(Vec2 p, nint s, HexColor color, nint _)
            {
                string? str = Marshal.PtrToStringUTF8(s);
                del(p, str, color, ctx);
            }
            @internal.DrawString = Wrapper;
        }
    }
}