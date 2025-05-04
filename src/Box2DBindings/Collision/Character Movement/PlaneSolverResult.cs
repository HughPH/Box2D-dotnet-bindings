using JetBrains.Annotations;
using System.Runtime.InteropServices;

namespace Box2D.Character_Movement;

/// <summary>
/// Result returned by b2SolvePlanes.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
[PublicAPI]
public readonly struct PlaneSolverResult
{
    /// <summary>
    /// The final position of the mover.
    /// </summary>
    public readonly Vec2 Position;

    /// <summary>
    /// The number of iterations used by the plane solver. For diagnostics.
    /// </summary>
    public readonly int IterationCount;
}