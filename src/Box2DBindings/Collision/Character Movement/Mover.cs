using JetBrains.Annotations;
using System;
using System.Runtime.InteropServices;

namespace Box2D.Character_Movement;

/// <summary>
/// Functions for solving planes and clipping vectors.
/// </summary>
public static class Mover
{
    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2SolvePlanes")]
    private static extern PlaneSolverResult b2SolvePlanes(Vec2 position, [In] CollisionPlane[] planes, int count);

    /// <summary>
    /// Solves the position of a mover that satisfies the given collision planes.
    /// </summary>
    /// <param name="position">This must be the position used to generate the collision planes</param>
    /// <param name="planes">The collision planes</param>
    /// <returns>The result of the plane solver</returns>
    /// <exception cref="ArgumentNullException">The planes array is null or empty</exception>
    [PublicAPI]
    public static PlaneSolverResult SolvePlanes(in Vec2 position, CollisionPlane[] planes)
    {
        if (planes is not { Length: not 0 })
            throw new ArgumentNullException(nameof(planes));
        return b2SolvePlanes(position, planes, planes.Length);
    }
    
    /// <summary>
    /// Solves the position of a mover that satisfies the given collision planes.
    /// </summary>
    /// <param name="position">This must be the position used to generate the collision planes</param>
    /// <param name="planes">The collision planes</param>
    /// <param name="planeCount">The number of planes to use</param>
    /// <returns>The result of the plane solver</returns>
    /// <exception cref="ArgumentNullException">The planes array is null or empty</exception>
    [PublicAPI]
    public static PlaneSolverResult SolvePlanes(in Vec2 position, CollisionPlane[] planes, int planeCount)
    {
        if (planes is not { Length: not 0 })
            throw new ArgumentNullException(nameof(planes));

        return b2SolvePlanes(position, planes, planeCount);
    }

    [DllImport(libraryName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "b2ClipVector")]
    private static extern Vec2 b2ClipVector(Vec2 vector, [In] CollisionPlane[] planes, int count);

    /// <summary>
    /// Clips the velocity against the given collision planes. Planes with zero push or clipVelocity
    /// set to false are skipped.
    /// </summary>
    /// <param name="vector">The vector to clip</param>
    /// <param name="planes">The collision planes</param>
    /// <returns>The clipped vector</returns>
    /// <exception cref="ArgumentNullException">The planes array is null or empty</exception>
    [PublicAPI]
    public static Vec2 ClipVector(in Vec2 vector, CollisionPlane[] planes)
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
    /// <exception cref="ArgumentNullException">The planes array is null or empty</exception>S
    [PublicAPI]
    public static Vec2 ClipVector(in Vec2 vector, CollisionPlane[] planes, int planeCount)
    {
        if (planes is not { Length: not 0 })
            throw new ArgumentNullException(nameof(planes));
        
        return b2ClipVector(vector, planes, planeCount);
    }
}
