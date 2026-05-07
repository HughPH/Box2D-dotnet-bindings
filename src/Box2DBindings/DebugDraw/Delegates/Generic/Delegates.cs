using System;

namespace Box2D.Delegates.Generic;

/// <summary>
/// Draw a circle.
/// </summary>
/// <param name="center">The circle center</param>
/// <param name="radius">The radius</param>
/// <param name="color">The color</param>
/// <param name="context">The context</param>
public delegate void DrawCircleDelegate<in TContext>(Vec2 center, float radius, HexColor color, TContext context) where TContext : class;

/// <summary>
/// Draw a point.
/// </summary>
/// <param name="p">The point</param>
/// <param name="size">The size</param>
/// <param name="color">The color</param>
/// <param name="context">The context</param>
public delegate void DrawPointDelegate<in TContext>(Vec2 p, float size, HexColor color, TContext context) where TContext : class;

/// <summary>
/// Draw a closed polygon provided in CCW order.
/// </summary>
/// <param name="vertices">The vertices</param>
/// <param name="color">The color</param>
/// <param name="context">The context</param>
public delegate void DrawPolygonDelegate<in TContext>(ReadOnlySpan<Vec2> vertices, HexColor color, TContext context) where TContext : class;

/// <summary>
/// Draw a line segment.
/// </summary>
/// <param name="p1">The first point</param>
/// <param name="p2">The second point</param>
/// <param name="color">The color</param>
/// <param name="context">The context</param>
public delegate void DrawSegmentDelegate<in TContext>(Vec2 p1, Vec2 p2, HexColor color, TContext context) where TContext : class;

/// <summary>
/// Draw a solid capsule.
/// </summary>
/// <param name="p1">The first point</param>
/// <param name="p2">The second point</param>
/// <param name="radius">The radius</param>
/// <param name="color">The color</param>
/// <param name="context">The context</param>
public delegate void DrawSolidCapsuleDelegate<in TContext>(Vec2 p1, Vec2 p2, float radius, HexColor color, TContext context) where TContext : class;

/// <summary>
/// Draw a solid circle.
/// </summary>
/// <param name="transform">The transform</param>
/// <param name="radius">The radius</param>
/// <param name="color">The color</param>
/// <param name="context">The context</param>
public delegate void DrawSolidCircleDelegate<in TContext>(Transform transform, float radius, HexColor color, TContext context) where TContext : class;

/// <summary>
/// Draw a solid closed polygon provided in CCW order.
/// </summary>
/// <param name="transform">The transform</param>
/// <param name="vertices">The vertices</param>
/// <param name="radius">The radius</param>
/// <param name="color">The color</param>
/// <param name="context">The context</param>
public delegate void DrawSolidPolygonDelegate<in TContext>(Transform transform, ReadOnlySpan<Vec2> vertices, float radius, HexColor color, TContext context) where TContext : class;
 
/// <summary>
/// Draw a string in world space
/// </summary>
/// <param name="point">The point</param>
/// <param name="text">The string</param>
/// <param name="color">The color</param>
/// <param name="context">The context</param>
public delegate void DrawStringDelegate<in TContext>(Vec2 point, string? text, HexColor color, TContext context) where TContext : class;

/// <summary>
/// Draw a transform.
/// </summary>
/// <param name="transform">The transform</param>
/// <param name="context">The context</param>
/// <remarks>Choose your own length scale</remarks>
public delegate void DrawTransformDelegate<in TContext>(Transform transform, TContext context) where TContext : class;