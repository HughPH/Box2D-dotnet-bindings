using System.Runtime.InteropServices;
namespace Box2D;

/// <summary>
/// Box2D version information.
/// </summary>
[StructLayout(LayoutKind.Sequential)]
public readonly ref struct Box2DVersion
{
    /// <summary>
    /// Significant changes
    /// </summary>
    public readonly int Major;

    /// <summary>
    /// Incremental changes
    /// </summary>
    public readonly int Minor;

    /// <summary>
    /// Bug fixes
    /// </summary>
    public readonly int Revision;
}