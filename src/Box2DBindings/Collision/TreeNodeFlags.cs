using JetBrains.Annotations;
using System;

namespace Box2D;

/// <summary>
/// Flags for a <see cref="TreeNode"/>.
/// </summary>
[Flags]
[PublicAPI]
public enum TreeNodeFlags : ushort
{
    /// <summary>
    /// The node is allocated.
    /// </summary>
    AllocatedNode = 0x0001,
    /// <summary>
    /// The node is enlarged.
    /// </summary>
    EnlargedNode = 0x0002,
    /// <summary>
    /// The node is a leaf node.
    /// </summary>
    LeafNode = 0x0004,
}