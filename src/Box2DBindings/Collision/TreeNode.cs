using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// A node in a <see cref="DynamicTree"/>.
/// </summary>
[StructLayout(LayoutKind.Explicit)]
public readonly struct TreeNode
{
    /// <summary>
    /// The bounding box of the node.
    /// </summary>
    [FieldOffset(0)]
    public readonly AABB AABB; // 16 bytes

    /// <summary>
    /// The category bits for the node.
    /// </summary>
    [FieldOffset(16)]
    public readonly ulong CategoryBits; // 8 bytes

    // Union: userData OR children
    /// <summary>
    /// The user data for the node.
    /// </summary>
    [FieldOffset(24)]
    public readonly ulong UserData;

    /// <summary>
    /// The children of the node.
    /// </summary>
    [FieldOffset(24)]
    public readonly TreeChildren Children;

    // Union: parent OR next
    /// <summary>
    /// The parent of the node.
    /// </summary>
    [FieldOffset(32)]
    public readonly int Parent;

    /// <summary>
    /// The next node in the linked list.
    /// </summary>
    [FieldOffset(32)]
    public readonly int Next;

    /// <summary>
    /// The height of the node in the tree.
    /// </summary>
    [FieldOffset(36)]
    public readonly ushort Height;

    /// <summary>
    /// The flags for the node.
    /// </summary>
    [FieldOffset(38)]
    public readonly TreeNodeFlags Flags;
}