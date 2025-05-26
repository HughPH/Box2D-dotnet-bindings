namespace Box2D;

/// <summary>
/// Prototype callback for ray and shape casts.<br/>
/// Called for each shape found in the query. You control how the ray cast
/// proceeds by returning a float:
/// <ul>
/// <li>return -1: ignore this shape and continue</li>
/// <li>return 0: terminate the ray cast</li>
/// <li>return fraction: clip the ray to this point</li>
/// <li>return 1: don't clip the ray and continue</li>
/// </ul>
/// </summary>
/// <param name="shape">The Shape</param>
/// <param name="point">The point of initial intersection</param>
/// <param name="normal">The normal vector at the point of intersection, zero for a shape cast with initial overlap</param>
/// <param name="fraction">The fraction along the ray at the point of intersection, zero for a shape cast with initial overlap</param>
/// <param name="context">The user context</param>
/// <returns>-1 to filter, 0 to terminate, fraction to clip the ray for closest hit, 1 to continue<br/>
/// A cast with initial overlap will return a zero fraction and a zero normal.</returns>
public delegate float CastResultCallback<in TContext>(Shape shape, Vec2 point, Vec2 normal, float fraction, TContext context) where TContext : class;

/// <summary>
/// Prototype callback for ray and shape casts.<br/>
/// Called for each shape found in the query. You control how the ray cast
/// proceeds by returning a float:
/// <ul>
/// <li>return -1: ignore this shape and continue</li>
/// <li>return 0: terminate the ray cast</li>
/// <li>return fraction: clip the ray to this point</li>
/// <li>return 1: don't clip the ray and continue</li>
/// </ul>
/// </summary>
/// <param name="shape">The Shape</param>
/// <param name="point">The point of initial intersection</param>
/// <param name="normal">The normal vector at the point of intersection, zero for a shape cast with initial overlap</param>
/// <param name="fraction">The fraction along the ray at the point of intersection, zero for a shape cast with initial overlap</param>
/// <param name="context">The user context</param>
/// <returns>-1 to filter, 0 to terminate, fraction to clip the ray for closest hit, 1 to continue<br/>
/// A cast with initial overlap will return a zero fraction and a zero normal.</returns>
public delegate float CastResultRefCallback<TContext>(Shape shape, Vec2 point, Vec2 normal, float fraction, ref TContext context) where TContext : unmanaged;

/// <summary>
/// Prototype callback for ray and shape casts.<br/>
/// Called for each shape found in the query. You control how the ray cast
/// proceeds by returning a float:
/// <ul>
/// <li>return -1: ignore this shape and continue</li>
/// <li>return 0: terminate the ray cast</li>
/// <li>return fraction: clip the ray to this point</li>
/// <li>return 1: don't clip the ray and continue</li>
/// </ul>
/// </summary>
/// <param name="shape">The Shape</param>
/// <param name="point">The point of initial intersection</param>
/// <param name="normal">The normal vector at the point of intersection, zero for a shape cast with initial overlap</param>
/// <param name="fraction">The fraction along the ray at the point of intersection, zero for a shape cast with initial overlap</param>
/// <returns>-1 to filter, 0 to terminate, fraction to clip the ray for closest hit, 1 to continue<br/>
/// A cast with initial overlap will return a zero fraction and a zero normal.</returns>
public delegate float CastResultCallback(Shape shape, Vec2 point, Vec2 normal, float fraction);

/// <summary>
/// Prototype callback for ray and shape casts.<br/>
/// Called for each shape found in the query. You control how the ray cast
/// proceeds by returning a float:
/// <ul>
/// <li>return -1: ignore this shape and continue</li>
/// <li>return 0: terminate the ray cast</li>
/// <li>return fraction: clip the ray to this point</li>
/// <li>return 1: don't clip the ray and continue</li>
/// </ul>
/// </summary>
/// <param name="shape">The Shape</param>
/// <param name="point">The point of initial intersection</param>
/// <param name="normal">The normal vector at the point of intersection, zero for a shape cast with initial overlap</param>
/// <param name="fraction">The fraction along the ray at the point of intersection, zero for a shape cast with initial overlap</param>
/// <param name="context">The user context</param>
/// <returns>-1 to filter, 0 to terminate, fraction to clip the ray for closest hit, 1 to continue<br/>
/// A cast with initial overlap will return a zero fraction and a zero normal.</returns>
public delegate float CastResultNintCallback(Shape shape, Vec2 point, Vec2 normal, float fraction, nint context);
