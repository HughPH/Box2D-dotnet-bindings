using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Base class for DebugDrawGeneric implementations - i.e. those that receive a managed type context.
/// </summary>
[PublicAPI]
public abstract class DebugDrawGenericBase<TContext> : DebugDraw where TContext : class
{
    private TContext context;

    private bool initialized;
    
    /// <summary>
    /// Constructor for DebugDrawGenericBase.
    /// </summary>
    /// <param name="context">The context object to be passed to the callback functions.</param>
    protected DebugDrawGenericBase(TContext context)
    {
        this.context = context;
        DrawBodyNames = true;
        DrawBounds = true;
        DrawShapes = true;
        DrawJoints = true;
        DrawContactFeatures = true;
        DrawContactImpulses = true;
        DrawContactNormals = true;
        DrawContacts = true;
        DrawGraphColors = true;
        DrawFrictionImpulses = true;
        DrawIslands = true;
        DrawJointExtras = true;
        DrawMass = true;
    }
    
    //! \internal
    internal override unsafe ref DebugDrawInternal Internal
    {
        get
        {
            if (!initialized)
            {
                initialized = true;
                @internal.DrawPolygon = (vertexPtr, vertexCount, color, _) =>
                {
                    var vertices = new ReadOnlySpan<Vec2>(vertexPtr, vertexCount);
                    DrawPolygon(vertices, color, context);
                };
                @internal.DrawSolidPolygon = (transform, vertexPtr, vertexCount, radius, color, _) =>
                {
                    var vertices = new ReadOnlySpan<Vec2>(vertexPtr, vertexCount);
                    DrawSolidPolygon(transform, vertices, radius, color, context);
                };
                @internal.DrawCircle = (center, radius, color, _) => DrawCircle(center, radius, color, context);
                @internal.DrawSolidCircle = (transform, radius, color, _) => DrawSolidCircle(transform, radius, color, context);
                @internal.DrawSolidCapsule = (center1, center2, radius, color, _) => DrawSolidCapsule(center1, center2, radius, color, context);
                @internal.DrawSegment = (p1, p2, color, _) => DrawSegment(p1, p2, color, context);
                @internal.DrawTransform = (transform, _) => DrawTransform(transform, context);
                @internal.DrawPoint = (p, size, color, _) => DrawPoint(p, size, color, context);
                @internal.DrawString = (p, s, color, _) =>
                {
                    string? str = Marshal.PtrToStringUTF8(s);
                    DrawString(p, str, color, context);
                };
            }
            
            return ref @internal;
        }
    }

    /// <summary>
    /// Callback function to draw a closed polygon provided in CCW order.
    /// </summary>
    protected abstract void DrawPolygon(ReadOnlySpan<Vec2> vertices, HexColor color, TContext context);
    
    /// <summary>
    /// Callback function to draw a solid closed polygon provided in CCW order.
    /// </summary>
    protected abstract void DrawSolidPolygon(Transform transform, ReadOnlySpan<Vec2> vertices, float radius, HexColor color, TContext context);
    
    /// <summary>
    /// Callback function to draw a circle.
    /// </summary>
    protected abstract void DrawCircle(Vec2 center, float radius, HexColor color, TContext context);
    
    /// <summary>
    /// Callback function to draw a solid circle.
    /// </summary>
    protected abstract void DrawSolidCircle(Transform transform, float radius, HexColor color, TContext context);
    
    /// <summary>
    /// Callback function to draw a solid capsule.
    /// </summary>
    protected abstract void DrawSolidCapsule(Vec2 center1, Vec2 center2, float radius, HexColor color, TContext context);
    
    /// <summary>
    /// Callback function to draw a line segment.
    /// </summary>
    protected abstract void DrawSegment(Vec2 p1, Vec2 p2, HexColor color, TContext context);
    
    /// <summary>
    /// Callback function to draw a transform. Choose your own length scale.
    /// </summary>
    protected abstract void DrawTransform(Transform transform, TContext context);
    
    /// <summary>
    /// Callback function to draw a point.
    /// </summary>
    protected abstract void DrawPoint(Vec2 p, float size, HexColor color, TContext context);
    
    /// <summary>
    /// Callback function to draw a string in world space
    /// </summary>
    protected abstract void DrawString(Vec2 p, string? s, HexColor color, TContext context);
}