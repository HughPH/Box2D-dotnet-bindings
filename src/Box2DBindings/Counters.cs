using System.Runtime.InteropServices;

namespace Box2D;

/// <summary>
/// Counters that give details of the simulation size.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public unsafe struct Counters
{
    /// <summary>
    /// The number of bodies in the world.
    /// </summary>
    public readonly int BodyCount;
    /// <summary>
    /// The number of shapes in the world.
    /// </summary>
    public readonly int ShapeCount;
    // ContactCount:
    /// <summary>
    /// The number of contacts in the world.
    /// </summary>
    public readonly int ContactCount;
    /// <summary>
    /// The number of joints in the world.
    /// </summary>
    public readonly int JointCount;
    /// <summary>
    /// The number of broad-phase islands.
    /// </summary>
    public readonly int IslandCount;
    /// <summary>
    /// The number of bytes allocated in the stack allocator.
    /// </summary>
    public readonly int StackUsed;
    /// <summary>
    /// The height of the static tree.
    /// </summary>
    public readonly int StaticTreeHeight;
    /// <summary>
    /// The height of the dynamic tree.
    /// </summary>
    public readonly int TreeHeight;
    /// <summary>
    /// The number of bytes allocated in the heap allocator.
    /// </summary>
    public readonly int ByteCount;
    /// <summary>
    /// The number of tasks in the world.
    /// </summary>
    public readonly int TaskCount;
    /// <summary>
    /// Nothing to do with visible colors.
    /// </summary>
    /// <remarks>See the <a href="https://en.wikipedia.org/wiki/Graph_coloring">Wikipedia article on Graph colouring</a> to learn more about this.</remarks>
    public fixed int ColorCounts[12];
}