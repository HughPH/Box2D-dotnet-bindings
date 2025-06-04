using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D.Character_Movement;

/// <summary>
/// Functions for solving planes and clipping vectors.
/// </summary>
[PublicAPI]
public partial class Mover
{
    /// <summary>
    /// Solves the position of a mover that satisfies the given collision planes.
    /// </summary>
    /// <param name="targetDelta">The desired movement from the position used to generate the collision planes</param>
    /// <param name="planes">The collision planes</param>
    /// <returns>The result of the plane solver</returns>
    /// <exception cref="ArgumentNullException">The planes array is null or empty</exception>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__character.html#ga7a3867906e407502b61822281afe4d04">b2SolvePlanes</a></remarks>
    public static unsafe PlaneSolverResult SolvePlanes(in Vec2 targetDelta, CollisionPlane[] planes)
    {
        if (planes is not { Length: not 0 })
            throw new ArgumentNullException(nameof(planes));
        return b2SolvePlanes(targetDelta, planes, planes.Length);
    }
    
    /// <summary>
    /// Solves the position of a mover that satisfies the given collision planes.
    /// </summary>
    /// <param name="targetDelta">The desired movement from the position used to generate the collision planes</param>
    /// <param name="planes">The collision planes</param>
    /// <param name="planeCount">The number of planes to use</param>
    /// <returns>The result of the plane solver</returns>
    /// <exception cref="ArgumentNullException">The planes array is null or empty</exception>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__character.html#ga7a3867906e407502b61822281afe4d04">b2SolvePlanes</a></remarks>
    public static unsafe PlaneSolverResult SolvePlanes(in Vec2 targetDelta, CollisionPlane[] planes, int planeCount)
    {
        if (planes is not { Length: not 0 })
            throw new ArgumentNullException(nameof(planes));
        return b2SolvePlanes(targetDelta, planes, planeCount);
    }
    
    /// <summary>
    /// Clips the velocity against the given collision planes. Planes with zero push or clipVelocity
    /// set to false are skipped.
    /// </summary>
    /// <param name="vector">The vector to clip</param>
    /// <param name="planes">The collision planes</param>
    /// <returns>The clipped vector</returns>
    /// <exception cref="ArgumentNullException">The planes array is null or empty</exception>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__character.html#ga64bf3e04de538c317bc377a2d2c22370">b2ClipVector</a></remarks>
    public static unsafe Vec2 ClipVector(in Vec2 vector, CollisionPlane[] planes)
    {
        if (planes is not { Length: not 0 })
            throw new ArgumentNullException(nameof(planes));

        return b2ClipVector(vector, planes, planes.Length);
    }
    
    /// <summary>
    /// Clips the velocity against the given collision planes. Planes with zero push or clipVelocity
    /// set to false are skipped.
    /// </summary>
    /// <param name="vector">The vector to clip</param>
    /// <param name="planes">The collision planes</param>
    /// <param name="planeCount">The number of planes to use</param>
    /// <returns>The clipped vector</returns>
    /// <exception cref="ArgumentNullException">The planes array is null or empty</exception>
    /// <remarks>This wraps <a href="https://box2d.org/documentation/group__character.html#ga64bf3e04de538c317bc377a2d2c22370">b2ClipVector</a></remarks>
    public static unsafe Vec2 ClipVector(in Vec2 vector, CollisionPlane[] planes, int planeCount)
    {
        if (planes is not { Length: not 0 })
            throw new ArgumentNullException(nameof(planes));
        
        return b2ClipVector(vector, planes, planeCount);
    }
}
