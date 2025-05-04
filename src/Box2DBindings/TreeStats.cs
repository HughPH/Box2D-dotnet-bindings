using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// These are performance results returned by dynamic tree queries.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct TreeStats
{
    /// <summary>
    /// Number of internal nodes visited during the query
    /// </summary>
    public readonly int NodeVisits;

    /// <summary>
    /// Number of leaf nodes visited during the query
    /// </summary>
    public readonly int LeafVisits;
}