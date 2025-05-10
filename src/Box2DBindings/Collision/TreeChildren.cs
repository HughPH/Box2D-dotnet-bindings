using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// The children of a <see cref="TreeNode"/>.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly struct TreeChildren
{
    /// <summary>
    /// The first child of the node.
    /// </summary>
    public readonly int Child1;
    /// <summary>
    /// The second child of the node.
    /// </summary>
    public readonly int Child2;
}